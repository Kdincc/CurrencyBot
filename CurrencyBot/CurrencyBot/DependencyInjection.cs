using CurrencyBot.BL;
using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Polling;

namespace CurrencyBot
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IApiHelper, ApiHelper>();
            services.AddTransient<ITelegramBotFactory, TelegramBotFactory>();
            services.AddTransient<IBank, Bank>();
            services.AddTransient<IUpdateHandler, UpdateHandler>();
            services.AddTransient<IUserResponseParser, UserResponseParser>();
            services.AddTransient<IBankErrorHandler, BankErrorHandler>();
            services.AddTransient<IBankValidator, BankValidator>();
            services.AddSingleton(TimeProvider.System);

            return services;
        }

        public static IServiceCollection RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BotSettings>(configuration.GetSection(nameof(BotSettings)));

            return services;
        }

        public static IServiceCollection RegisterClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("BankApi", c =>
            {
                c.BaseAddress = new(configuration.GetValue<string>("BankApi"));
            });

            return services;
        }
    }
}
