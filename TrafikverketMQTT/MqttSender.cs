using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace TrafikverketMQTT
{
    public interface IMqttSender
    {
        Task SendAsync<T>(string topic, T data);
    }

    public class MqttSender : IMqttSender, IDisposable
    {
        private AsyncLock locker = new AsyncLock();
        private readonly ILogger<MqttSender> logger;
        MqttSettings settings;
        public MqttSender(IOptions<MqttSettings> settingsOption, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<MqttSender>();
            settings = settingsOption.Value;
        }

        IMqttClient client = null;

        private async Task<IMqttClient> GetClient()
        {
            using (await locker.LockAsync())
            {
                if (client != null && !client.IsConnected)
                {
                    client.Dispose();
                    client = null;
                }
                if (client == null)
                {
                    logger.LogTrace("Connecting to MQTT server");
                    client = await (settings.Port.HasValue ? MqttClient.CreateAsync(settings.HostAddress, settings.Port.Value) : MqttClient.CreateAsync(settings.HostAddress));
                    logger.LogDebug("Connected to MQTT server, authenticating");
                    await client.ConnectAsync(settings.UserName == null ? new MqttClientCredentials(settings.ClientId) : new MqttClientCredentials(settings.ClientId, settings.UserName, settings.Password));
                    logger.LogDebug("Authenticated with MQTT server");
                }
                return client;
            }
        }

        public async Task SendAsync<T>(string topic, T data)
        {
            logger.LogTrace("Sending payload to MQTT server");
            var payload = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            var client = await GetClient();
            await client.PublishAsync(new MqttApplicationMessage(settings.BaseTopic + "/" + topic, payload), settings.QualityOfService.GetValueOrDefault(MqttQualityOfService.AtLeastOnce), settings.RetainLastMessageOnServer.GetValueOrDefault(true));
            logger.LogDebug("MQTT message sent");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                if (client != null)
                {
                    client.Dispose();
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
}
