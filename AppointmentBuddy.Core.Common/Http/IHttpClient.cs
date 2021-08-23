using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppointmentBuddy.Core.Common.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<string> GetStringAsync(string uri);
        Task<HttpResponseMessage> PostAsync(string uri, StringContent content, string connectionId = null);

        HttpClient Client { get; }
        HttpRequestHeaders DefaultRequestHeaders { get; }
    }
}
