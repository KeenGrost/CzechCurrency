using System;
using CzechCurrency.Services.Contract;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using CzechCurrency.API.Controllers;
using CzechCurrency.API.Requests;
using CzechCurrency.Api.Tests.Fixtures;
using CzechCurrency.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CzechCurrency.Api.Tests
{
    public class ExchangeRateControllerTest
    {

        private ILogger<ExchangeRateController> MakeFakeLogger()
        {
            var service = Substitute.For<ILogger<ExchangeRateController>>();
            return service;
        }

        private IExchangeRateService MakeFakeExchangeRateService()
        {
            var service = Substitute.For<IExchangeRateService>();
            service.Get(Arg.Any<string>(), Arg.Any<DateTime>()).Returns(new ExchangeRateFixture().Create());
            return service;
        }

        private IBusControl MakeFakeBusControl()
        {
            var service = Substitute.For<IBusControl>();
            return service;
        }

        [Test]
        public async Task Get_ByDefault_ReturnsSuccess()
        {
            // Arrange
            var controller = new ExchangeRateController(
                MakeFakeLogger(),
                MakeFakeExchangeRateService(),
                MakeFakeBusControl()
                );

            ExchangeRateRequest activateActionRequest = new ExchangeRateRequestFixture().Create();

            // Act
            var result = (await controller.Get(activateActionRequest)).Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(123.45M, ((ExchangeRate)result.Value).Value);
            Assert.AreEqual("RUB", ((ExchangeRate)result.Value).CurrencyCode);
        }
    }
}
