using CurrencyBot.BL;
using CurrencyBot.BL.Interfaces;

namespace CurrencyBot.Tests
{
    [TestClass]
    public class UserResponseParserTests
    {
        private readonly IUserResponseParser _parser = new UserResponseParser();

        [TestMethod]
        [DataRow("", false)]
        [DataRow("usd 1.2.2233", false)]
        [DataRow("EUH 01.12.2022", true)]
        public void ParseExchangeRate_IsCorrectValidation(string str, bool expected)
        {
            //arrange
            bool actual;

            //act
            actual = _parser.ParseExchangeInfo(str).IsValid;

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}