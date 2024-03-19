using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace CurrencyBot.BL.Interfaces
{
    public interface ITelegramBotFactory
    {
        public ITelegramBotClient GetBot();
    }
}
