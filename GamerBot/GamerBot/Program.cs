using AutoMapper;
using GamerBot.BusinessLogic.Interfaces;
using GamerBot.BusinessLogic.Services;
using GamerBot.Common;
using GamerBot.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace GamerBot
{
    class Program
    {
        private static UserService _userService;

        public static async Task Main(string[] args)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            string connection = "Server=localhost;Database=PolessuBotApp;Trusted_Connection=True;";
            
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IUserService, UserService>();
                    services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
                    services.AddSingleton(mapper);
                })
                .Build();
            
            ActivatorUtilities.CreateInstance<UserService>(host.Services);

            _userService = ActivatorUtilities.CreateInstance<UserService>(host.Services);
            
            var botClient = new TelegramBotClient("5328932416:AAENjwE5ivIJGxR3J-VsrUDqbjf0_2FkvCk");

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            var messageController = new MessageController(_userService);
            
            botClient.StartReceiving(
                messageController.HandleUpdatesAsync,
                messageController.HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Бот запущен @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }
    }
}   