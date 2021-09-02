using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Solrevdev.DpdLocalDotnet.Core;
using Xunit;

namespace Core.Tests
{
    public class DpdLocalApiClientTests
    {
        [Fact]
        public void Can_Create_InstanceOf_DpdLocalApiClient()
        {
            // Arrange
            var credentials = MockDpdCredentials();
            var options = Options.Create(credentials);
            var loggerForHttpClient = Mock.Of<ILogger<DpdHttpClient>>();
            var loggerForApi = Mock.Of<ILogger<DpdLocalApiClient>>();
            var mockFactory = MockHttpClientFactory();
            var dpdHttpClient = new DpdHttpClient(options, mockFactory.Object, loggerForHttpClient);

            // Act
            var api = new DpdLocalApiClient(options, loggerForApi, dpdHttpClient);

            // Assert
            Assert.NotNull(api);
        }

        [Fact]
        public void Can_Actually_Login_To_DpdLocalApi()
        {
            // Arrange
            var credentials = new DpdCredentials
            {
                Name = "Actual DPD credentials",
                ApiUrl = "api.dpdlocal.co.uk",
                Username = "magnets1",
                Password = "parcels",
                AccountNumber = "2256001"
            };
            var options = Options.Create(credentials);
            var loggerForHttpClient = Mock.Of<ILogger<DpdHttpClient>>();
            var loggerForApi = Mock.Of<ILogger<DpdLocalApiClient>>();
            var httpClientFactory = new ServiceCollection().AddHttpClient().BuildServiceProvider().GetService<IHttpClientFactory>();
            var dpdHttpClient = new DpdHttpClient(options, httpClientFactory, loggerForHttpClient);

            // Act
            var api = new DpdLocalApiClient(options, loggerForApi, dpdHttpClient);
            api.Login();

            // Assert
            Assert.NotNull(credentials);
            Assert.NotNull(httpClientFactory);
            Assert.NotNull(api);
        }

        private static DpdCredentials MockDpdCredentials() => new DpdCredentials
        {
            Name = "Unit Testing API",
            ApiUrl = "https://api.dpdlocal.com/",
            Username = "UnitTesting",
            Password = "UnitTesting",
            AccountNumber = "123456789",
        };

        private static Mock<IHttpClientFactory> MockHttpClientFactory()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"access_token\": \"123\", \"user_id\": 123}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            return mockFactory;
        }
    }
}
