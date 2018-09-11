using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Trafikverket;
using Trafikverket.Response;
using uPLibrary.Networking.M2Mqtt.Exceptions;

namespace TrafikverketMQTT
{
    public class Engine : IEngine
    {
        private readonly ILogger<Engine> logger;
        private readonly IOptions<TrafikverketSettings> settings;
        private readonly TrafikverketTrainStationClient trainStationClient;
        private readonly TrafikverketTrainAnnouncemenClient trainAnnouncementClient;
        private readonly IMqttSender sender;

        public Engine(ILoggerFactory loggerFactory, IOptions<TrafikverketSettings> settings, IMqttSender sender)
        {
            logger = loggerFactory.CreateLogger<Engine>();
            trainStationClient = new TrafikverketTrainStationClient(settings.Value.ApiKey);
            trainAnnouncementClient = new TrafikverketTrainAnnouncemenClient(settings.Value.ApiKey);
            this.sender = sender;
            this.settings = settings;
        }

        public async Task RunAsync()
        {
            logger.LogTrace("Starting engine!");
            var source = new CancellationTokenSource();
            var instance = settings.Value;
            var tasks = new List<Task>();
            var token = source.Token;
            foreach (var train in settings.Value.Trains)
            {
                tasks.Add(RunTrainAsync(train, token));
            }
            while(!token.IsCancellationRequested)
            {
                var completedTask = await Task.WhenAny(tasks);
                logger.LogWarning("RunTrain ended!");
                tasks.Remove(completedTask);
                if (completedTask.Exception != null)
                {
                    logger.LogError(completedTask.Exception, "Exception occurred while querying train");
                    source.Cancel();
                }
            }
        }

        public async Task RunTrainAsync(TrainSettings trainSettings, CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                try
                {
                    var train = await GetDataAsync(trainSettings, token);

                    await sender.SendAsync(trainSettings.Name, train);
                    int delay;
                    if (train.State == TrainState.Delayed && !train.TimeAtLocation.HasValue)
                    {
                        delay = 30 * 1000;
                    }
                    else if (train.AdvertisedTimeAtLocation.Subtract(DateTime.Now).TotalHours < 1 && !train.TimeAtLocation.HasValue)
                    {
                        delay = 60 * 1000;
                    }
                    else
                    {
                        delay = 60 * 1000 * 5;
                    }
                    logger.LogTrace("Sleeping for {0] seconds", delay);
                    await Task.Delay(delay, token);
                }
                catch (HttpRequestException ex)
                {
                    logger.LogWarning(ex, "Failed to communicate with Trafikverket!");
                    await Task.Delay(60 * 1000 * 5, token);
                }
                catch (MqttCommunicationException ex)
                {
                    logger.LogWarning(ex, "Failed to communicate with Trafikverket!");
                    await Task.Delay(60 * 1000 * 5, token);
                }
                catch (MqttTimeoutException ex)
                {
                    logger.LogWarning(ex, "Failed to communicate with Trafikverket!");
                    await Task.Delay(60 * 1000 * 5, token);
                }
                catch (MqttClientException ex)
                {
                    logger.LogWarning(ex, "Failed to communicate with MQTT server, error code: ", ex.ErrorCode);
                    await Task.Delay(60 * 1000 * 5, token);
                }
            }
        }

        public async Task<Train> GetDataAsync(TrainSettings trainSettings, CancellationToken token)
        {
            logger.LogDebug("Getting departure station");
            var departureTrainStation = await trainStationClient.GetAsync(trainSettings.DepartureLocationName, token);
            logger.LogDebug("Getting destination station");
            var destinationTrainStation = await trainStationClient.GetAsync(trainSettings.DestinationLocationName, token);
            if (departureTrainStation == null)
                return Train.DepartureStationNotFound;

            if (destinationTrainStation == null)
                return Train.DestinationStationNotFound;
            TrainAnnouncement trainAnnouncement;
            logger.LogDebug("Getting train announcement");
            if (trainSettings.DepatureTime.HasValue)
            {
                var departureTime = DateTime.Today.Add(trainSettings.DepatureTime.Value);
                trainAnnouncement = await trainAnnouncementClient.GetTrainStopAsync(departureTrainStation.LocationSignature, destinationTrainStation.LocationSignature, departureTime, token);
            }
            else
            {
                trainAnnouncement = await trainAnnouncementClient.GetTrainStopAsync(departureTrainStation.LocationSignature, destinationTrainStation.LocationSignature, DateTime.Now, token);
            }
            if (trainAnnouncement == null)
                return Train.TrainRideNotFound;
            else
                return new Train(trainAnnouncement);
        }
    }
}
