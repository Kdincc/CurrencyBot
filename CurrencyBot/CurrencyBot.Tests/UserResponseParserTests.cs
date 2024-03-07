using CurrencyBot.Interfaces;

namespace CurrencyBot.Tests
{
    [TestClass]
    public class UserResponseParserTests
    {
        private readonly IUserResponseParser _parser = new UserResponseParser();

        [TestMethod]
        public void ParseExchangeRate()
        {
            string response = "Code: USD, Date: 12.03.2023";

            var results = _parser.ParseExchangeInfo(response);

            Assert.IsTrue(results.IsValid);
        }
    }
}