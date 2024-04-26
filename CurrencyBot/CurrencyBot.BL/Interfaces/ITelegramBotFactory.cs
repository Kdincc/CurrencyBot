using Telegram.Bot;

namespace CurrencyBot.BL.Interfaces
{
    public interface ITelegramBotFactory
    {
        public ITelegramBotClient GetBot();
    }
}
