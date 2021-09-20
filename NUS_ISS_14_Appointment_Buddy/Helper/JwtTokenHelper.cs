using AppointmentBuddy.Core.Common.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using NUS_ISS_14_Appointment_Buddy.WEB.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Helper
{
    public class JwtTokenHelper
    {
        public static async Task<string> GenerateJwtTokenAsync(AppSettings appSettings)
        {
            string jwtToken = "";

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, "Anonymous"));
            identity.AddClaim(new Claim(Constants.AppClaimTypes.Sub, "Anonymous"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var fileUtility = new FileUtility();

            var streamResult = await fileUtility.DownloadFileFromS3AsStream("", "", appSettings.S3KeyStoreBucketName, appSettings.S3KeyStoreFilePath, appSettings.PrivateKeyName);
            var decryptDto = new FileDecryptDto
            {
                KeyId = appSettings.KmsKeyId,
                FileStream = streamResult
            };
            //var streamResult3 = fileUtility.ConvertPrivateKeyToPem();
            //var streamResult3 = await fileUtility.Encrypt(decryptDto);
            var streamResult2 = await fileUtility.Decrypt(decryptDto);

            using (var privateRsa = RsaHelper.PrivateKeyFromPemFile(streamResult2))
            {
                RsaSecurityKey signingKey = new RsaSecurityKey(privateRsa);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                jwtToken = tokenHandler.WriteToken(token);
            }

            return jwtToken;
        }
    }
}
