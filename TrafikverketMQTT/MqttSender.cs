using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nito.AsyncEx;
using System;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace TrafikverketMQTT
{
    public interface IMqttSender
    {
        Task SendAsync<T>(string topic, T data);
    }

    public class MqttSender : IMqttSender, IDisposable
    {
        Encoding encoding = new UTF8Encoding(false);
        private AsyncLock locker = new AsyncLock();
        private readonly ILogger<MqttSender> logger;
        MqttSettings settings;
        public MqttSender(IOptions<MqttSettings> settingsOption, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<MqttSender>();
            settings = settingsOption.Value;
        }

        MqttClient client = null;

        private async Task<MqttClient> GetClient()
        {
            if (client != null && !client.IsConnected)
            {
                client = null;
            }
            if (client == null)
            {
                logger.LogTrace("Connecting to MQTT server");
                client = await Task.Run(() =>
                {
                    var client = settings.Port.HasValue ? new MqttClient(settings.HostAddress, settings.Port.Value, false, MqttSslProtocols.None, null, null) : new MqttClient(settings.HostAddress);
                    logger.LogDebug("Connected to MQTT server, authenticating");
                    if (settings.UserName != null) {
                        client.Connect(settings.ClientId, settings.UserName, settings.Password);
                    }
                    else
                    {
                        client.Connect(settings.ClientId);
                    }
                    return client;
                });

                logger.LogDebug("Authenticated with MQTT server");
            }
            return client;
        }

        JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.None
        };

        public async Task SendAsync<T>(string topic, T data)
        {
            logger.LogTrace("Sending payload to MQTT server");
            var payload = encoding.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(data, jsonSettings));

            using (await locker.LockAsync())
            {
                var client = await GetClient();
                client.Publish(settings.BaseTopic + "/" + topic, payload, (byte)settings.QualityOfService.GetValueOrDefault(MqttQualityOfService.AtLeastOnce), settings.RetainLastMessageOnServer.GetValueOrDefault(false));
                logger.LogDebug("MQTT message sent");
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (client != null && client.IsConnected)
                    {
                        client.Disconnect();
                    }
                    // TODO: dispose managed state (managed objects).
                }
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MqttSender() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public enum NamingConvention
    {
        CamelCase,
        PascalCase,
        SnakeCase
    }
}
