using HSPService.DBContext;
using HSPService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HSPServices_API_test
{
    public class UnitTest1 : IDisposable
    {
        protected TestServer _testServer;

        public UnitTest1() {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();

            _testServer = new TestServer(webBuilder);

        }

        [Fact]
        public async Task TestReadMethodsFail()
        {
            var response = await _testServer.CreateRequest("/api/Services/6").SendAsync("GET");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task TestReadMethodsPass()
        {
            var response = await _testServer.CreateRequest("/api/Services/3").SendAsync("GET");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task TestDeleteMethodsFail()
        {
            var response = await _testServer.CreateRequest("/api/Services/6").SendAsync("DELETE");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task TestDeleteMethodsPass()
        {
            var response = await _testServer.CreateRequest("/api/Services/5").SendAsync("DELETE");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        public void Dispose()
        {
            _testServer.Dispose();
        }
    }

}
