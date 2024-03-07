using CurrencyBot.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;

namespace CurrencyBot
{
    public static class DependancyInjection
    {
        public static IServiceCollection RegisterService(this IServiceCollection services) 
        {
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<IUpdateHandler, UpdateHandler>();
            services.AddTransient<IUserResponseParser, UserResponseParser>();

            return services;
        }
    }
}
