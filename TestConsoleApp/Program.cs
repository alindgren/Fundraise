using Fundraise;
using Fundraise.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using Fundraise.Core.Services;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //System.IServiceProvider
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            //serviceCollection.G
            //.GetService<ILoggerFactory>()
            //.AddConsole(LogLevel.Debug);

            Application application = new Application(serviceCollection);
            application.Services.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            application.MakeDonation();
            Console.ReadKey();
        }
        static private void ConfigureServices(IServiceCollection serviceCollection)
        {
            //ILoggerFactory loggerFactory = new LoggerFactory();
            serviceCollection.AddLogging();//.AddInstance<ILoggerFactory>(loggerFactory);
            //serviceCollection.AddSingleton<>

            var connectionString = "";
            serviceCollection.AddDbContext<FundraiseContext>(o => o.UseSqlServer(connectionString));

            serviceCollection.AddScoped<ICampaignRepository, CampaignRepository>();
        }
    }
}