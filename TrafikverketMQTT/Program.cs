using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TrafikverketMQTT
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IServiceProvider serviceProvider;
        static void Configure(string configFile)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(configFile))
                .AddJsonFile(Path.GetFileName(configFile), optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder => loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"))
                                                            .AddConsole()
                                                            .AddDebug())
                .AddOptions()
                .Configure<TrafikverketSettings>(Configuration.GetSection("TrafikverketSettings"))
                .Configure<MqttSettings>(Configuration.GetSection("Mqtt"))
                .AddSingleton<IEngine, Engine>()
                .AddTransient<IMqttSender, MqttSender>()
                .BuildServiceProvider();

        }

        public static async Task<int> Main(string[] args)
        {
            var configFile = args != null && args.Length > 0 ? args[0] : Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            Configure(configFile);
            var engine = serviceProvider.GetService<IEngine>();
            await engine.RunAsync();
            return -1;
            /*var client = await MqttClient.CreateAsync("192.168.0.7");
            await client.ConnectAsync(new MqttClientCredentials("TrafikverketMQTT"));

            while(true)
            {
                Console.Write("Topic: ");
                var topic = Console.ReadLine();
                Console.Write("Payload: ");
                var payload = Encoding.UTF8.GetBytes(Console.ReadLine());
                await client.PublishAsync(new MqttApplicationMessage("Peter/Trafikverket/TillJobb1", payload), MqttQualityOfService.ExactlyOnce, true);
            }*/

        }
    }
}
