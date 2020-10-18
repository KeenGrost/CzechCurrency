using System;
using Common.Test.Base;
using CzechCurrency.API.Requests;
using CzechCurrency.Entities;

namespace CzechCurrency.Api.Tests.Fixtures
{
    internal class CurrencyFixture : IFixture<Currency>
    {
        private string _code = "RUB";
        private string _number = "643";
        private string _name = "Российский рубль";
        private int _amount = 100;

        public Currency Create()
        {
            return new Currency
            {
                Code = "RUB",
                Number = "643",
                Name = "Российский рубль",
                Amount = 100
            };
        }

        public CurrencyFixture WithCode(string code)
        {
            _code = code;
            return this;
        }

        public CurrencyFixture WithNumber(string number)
        {
            _number = number;
            return this;
        }

        public CurrencyFixture WithName(string name)
        {
            _name = name;
            return this;
        }

        public CurrencyFixture WithAmount(int amount)
        {
            _amount = amount;
            return this;
        }
    }
}
