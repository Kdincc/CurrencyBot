using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace CurrencyBot.BL
{
    public class UpdateHandler(IUserResponseParser parser, IBank currencyService, IBankErrorHandler errorHandler) : IUpdateHandler
    {
        private readonly IUserResponseParser _parser = parser;
        private readonly IBank _currencyService = currencyService;
        private readonly IBankErrorHandler _errorHandler = errorHandler;

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);

            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is null || string.IsNullOrEmpty(update.Message.Text))
            {
                return;
            }

            ChatId chatId = update.Message.Chat.Id;

            var parsingResults = _parser.ParseExchangeInfo(update.Message.Text);

            if (!parsingResults.IsValid)
            {
                await botClient.SendTextMessageAsync(chatId, "Message not in correct format! Correct format lool like this: USD 01.01.2023", cancellationToken: cancellationToken);

                return;
            }

            _currencyService.Subscribe(_errorHandler);

            var exchangeRate = await _currencyService.GetExchangeRateAsync(parsingResults.Result);

            if (_errorHandler.IsErrorHandled)
            {
                await botClient.SendTextMessageAsync(chatId, _errorHandler.ErrorMessage, cancellationToken: cancellationToken);

                return;
            }

            await ShowExchangeRateAsync(botClient, chatId, RoundExchangeRate(exchangeRate), cancellationToken);
        }

        private async Task ShowExchangeRateAsync(ITelegramBotClient botClient, ChatId chatId, ExchangeRate exchangeRate, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId,
                $"{exchangeRate.Currency} to {exchangeRate.BaseCurrency}", cancellationToken: cancellationToken);
            await botClient.SendTextMessageAsync
                (
                chatId,
                "Privat Bank rate:" +
                $"\nBase currency: {exchangeRate.BaseCurrency}" +
                $"\nPurchase: {exchangeRate.PurchaseRate}" +
                $"\nSale: {exchangeRate.SaleRate}" +
                $"\n\nNational Bank rate:" +
                $"\nPurchase/Sale: {exchangeRate.PurchaseRateNB}",
                cancellationToken: cancellationToken
                );
        }

        private ExchangeRate RoundExchangeRate(ExchangeRate exchangeRate)
        {
            Math.Round(exchangeRate.SaleRate, 3);
            Math.Round(exchangeRate.SaleRateNB, 3);
            Math.Round(exchangeRate.PurchaseRate, 3);
            Math.Round(exchangeRate.PurchaseRateNB, 3);

            return exchangeRate;
        }
    }
}
