using CurrencyBot.BL;
using CurrencyBot.BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Polling;

namespace CurrencyBot
{
    public static class DependancyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IBank, Bank>();
            services.AddTransient<IUpdateHandler, UpdateHandler>();
            services.AddTransient<IUserResponseParser, UserResponseParser>();
            services.AddTransient<IBankErrorHandler, BankErrorHandler>();
            services.AddTransient<IBankValidator, BankValidator>();

            return services;
        }
    }
}
