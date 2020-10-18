using System;
using Common.Test.Base;
using CzechCurrency.API.Requests;
using CzechCurrency.Entities;

namespace CzechCurrency.Api.Tests.Fixtures
{
    internal class ExchangeRateFixture : IFixture<ExchangeRate>
    {
        private string _currencyCode = "RUB";
        private readonly DateTime _date = new DateTime(2020, 8, 1);
        private readonly decimal _value = 123.45M;

        public ExchangeRate Create()
        {
            return new ExchangeRate
            {
                CurrencyCode = _currencyCode,
                Date = _date,
                Value = _value
            };
        }
    }
}
