using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Core.Common.Http;
using AppointmentBuddy.Service.Identity.API.Core.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net.Http;
using M = AppointmentBuddy.Core.Model;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentBuddy.Service.Identity.API.Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly ServiceUrls _serviceUrls;
        private readonly IHttpClient _apiClient;
        private readonly AppSettings _appSettings;

        private readonly IIdentityRepositoryService _repository;
        private readonly ILogger<IdentityService> _logger;
        private readonly IDatabase _cache;

        public IdentityService(HttpClient serviceClient, IOptions<ServiceUrls> config, IOptions<AppSettings> appSettings, IIdentityRepositoryService repository, ILogger<IdentityService> logger, IConnectionMultiplexer conn)
        {
            _repository = repository;
            _logger = logger;
            _appSettings = appSettings.Value;
            _cache = conn.GetDatabase();

            _serviceUrls = config.Value;
            //_apiClient = serviceClient;
            _apiClient = new CustomHttpClient(serviceClient, logger);

            var s3KeyStoreBucketName = Environment.GetEnvironmentVariable("APPTBDDY_S3_KeyStoreBucketName");
            if (!String.IsNullOrEmpty(s3KeyStoreBucketName))
            {
                _appSettings.S3KeyStoreBucketName = s3KeyStoreBucketName;
            }

            var s3KeyStoreFilePath = Environment.GetEnvironmentVariable("APPTBDDY_S3_KeyStoreFilePath");
            if (!String.IsNullOrEmpty(s3KeyStoreFilePath))
            {
                _appSettings.S3KeyStoreFilePath = s3KeyStoreFilePath;
            }

            var kmsKeyId = Environment.GetEnvironmentVariable("APPTBDDY_KMS_KeyId");
            if (!String.IsNullOrEmpty(kmsKeyId))
            {
                _appSettings.KmsKeyId = kmsKeyId;
            }

            var publicKeyName = Environment.GetEnvironmentVariable("APPTBDDY_S3_PublicKeyName");
            if (!String.IsNullOrEmpty(publicKeyName))
            {
                _appSettings.PublicKeyName = publicKeyName;
            }

            var privateKeyName = Environment.GetEnvironmentVariable("APPTBDDY_S3_PrivateKeyName");
            if (!String.IsNullOrEmpty(privateKeyName))
            {
                _appSettings.PrivateKeyName = privateKeyName;
            }
        }

        public async Task<M.IdentityDto> Authenticate(string username, string password, string userTypeId = "")
        {
            M.IdentityDto data;

            //TODO:Authenticate user
            var IsAuthenticated = true;

            if (IsAuthenticated)
            {
                var user = await _repository.GetUserByUserLogin(username, userTypeId);

                // return null if user not found
                if (user == null)
                {
                    return null;
                }

                var userInfo = new M.UserInfo();
                userInfo.User = user;

                if (user.UserTypeId == Constants.UserType.Admin
                    || user.UserTypeId == Constants.UserType.Staff)
                {
                    userInfo.UserRole = await _repository.GetUserRoleByUserId(user.UserId);
                    userInfo.Role = await _repository.GetRoleByUserId(user.UserId);
                }

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.User.Name));
                identity.AddClaim(new Claim(Constants.AppClaimTypes.Id, userInfo.User.UserId));
                identity.AddClaim(new Claim(Constants.AppClaimTypes.UserType, userInfo.User.UserTypeId));
                identity.AddClaim(new Claim(Constants.AppClaimTypes.Sub, userInfo.User.UserId));

                switch (userInfo.User.UserTypeId)
                {
                    case Constants.UserType.Patient:
                        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleType.Patient));
                        break;
                    case Constants.UserType.Admin:
                        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleType.Admin));
                        break;
                    case Constants.UserType.Staff:
                        identity.AddClaim(new Claim(ClaimTypes.Role, userInfo.Role.RoleTypeId));
                        break;
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var fileUtility = new FileUtility();

                var streamResult = await fileUtility.DownloadFileFromS3AsStream("", "", _appSettings.S3KeyStoreBucketName, _appSettings.S3KeyStoreFilePath, _appSettings.PrivateKeyName);
                var decryptDto = new M.FileDecryptDto
                {
                    KeyId = _appSettings.KmsKeyId,
                    FileStream = streamResult
                };
                var streamResult2 = await fileUtility.Decrypt(decryptDto);

                using (RSA privateRsa = RsaHelper.PrivateKeyFromPemFile(streamResult2))
                {
                    RsaSecurityKey signingKey = new RsaSecurityKey(privateRsa);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = identity,
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    data = new M.IdentityDto();

                    data.UserInfo = userInfo;
                    data.Token = tokenHandler.WriteToken(token);
                }

                return data;
            }


            return null;
        }

    }
}
