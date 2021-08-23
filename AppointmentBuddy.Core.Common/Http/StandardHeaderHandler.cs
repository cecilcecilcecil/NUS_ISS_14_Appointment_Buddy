using AppointmentBuddy.Core.Common.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppointmentBuddy.Core.Common.Http
{
    public class StandardHeaderHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = Constants.Header.ServiceCorrelationHeaderName;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StandardHeaderHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation)
        {
            string correlationId = null;

            #region //--Set CorrelationID HTTP Header--//
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var output))
            {
                correlationId = output.FirstOrDefault();
            }
            //--validation at the HTTPContext level
            if (string.IsNullOrEmpty(correlationId))
            {
                //--validation at the HTTP Request level
                if (!request.Headers.TryGetValues(CorrelationIdHeaderName, out IEnumerable<string> outrequest))
                {
                    request.Headers.Add(CorrelationIdHeaderName, Guid.NewGuid().ToString());
                }
            }
            else
            {
                request.Headers.Add(CorrelationIdHeaderName, correlationId);
            }
            #endregion
            #region //--TODO: Set Authorization HTTP Header--//
            //--impact: this will make the StoreToken() methods obsolete
            #endregion

            return base.SendAsync(request, cancellation);
        }
    }
}
