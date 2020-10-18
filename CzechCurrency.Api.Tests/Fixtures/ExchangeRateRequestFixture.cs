using System;
using Common.Test.Base;
using CzechCurrency.API.Requests;

namespace CzechCurrency.Api.Tests.Fixtures
{
    internal class ExchangeRateRequestFixture : IFixture<ExchangeRateRequest>
    {
        private string _currencyCode = "RUB";
        private readonly DateTime _date = new DateTime(2020,8,1);

        public ExchangeRateRequest Create()
        {
            return new ExchangeRateRequest
            {
               CurrencyCode = _currencyCode,
               Date = _date
            };
        }
    }
}
