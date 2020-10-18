using System.Threading.Tasks;
using CzechCurrency.API.Controllers;
using CzechCurrency.Api.Tests.Fixtures;
using CzechCurrency.Entities;
using CzechCurrency.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace CzechCurrency.Api.Tests
{
    public class CurrencyControllerTest
    {
        private ILogger<CurrencyController> MakeFakeLogger()
        {
            var service = Substitute.For<ILogger<CurrencyController>>();
            return service;
        }

        private ICurrencyService MakeFakeCurrencyService()
        {
            var service = Substitute.For<ICurrencyService>();
            Currency[] currencies = new[]
            {
                new CurrencyFixture().WithCode("USD").WithNumber("840").WithName("Доллар США").WithAmount(1).Create(),
                new CurrencyFixture().WithCode("EUR").WithNumber("978").WithName("Евро").WithAmount(1).Create(),
                new CurrencyFixture().WithCode("THB").WithNumber("764").WithName("Тайский Бат").WithAmount(100).Create(),
                new CurrencyFixture().Create()
            };
            service.GetAll().Returns(currencies);
            return service;
        }


        [Test]
        public async Task GetAll_ByDefault_ReturnsSuccess()
        {
            // Arrange
            var controller = new CurrencyController(
                MakeFakeLogger(),
                MakeFakeCurrencyService());

            // Act
            var result = (await controller.GetAll()).Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(true, ((Currency[])result.Value).Any());
        }
    }
}
