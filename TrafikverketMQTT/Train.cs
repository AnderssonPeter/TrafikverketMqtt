using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using Trafikverket.Response;

namespace TrafikverketMQTT
{
    public class Train
    {
        public string Id
        { get; set; }

        public bool Canceled
        { get; set; }

        public DateTime AdvertisedTimeAtLocation
        { get; set; }

        public DateTime? EstimatedTimeAtLocation
        { get; set; }

        public DateTime? TimeAtLocation
        { get; set; }

        public string OtherInformation
        { get; set; }

        public string Deviations
        { get; set; }

        public DateTime ModifiedTime
        { get; set; }

        public TrainState State
        { get; set; }

        public int? NumberOfMinutesDelayed
        { get; set; }

        public static readonly Train DepartureStationNotFound = new Train(TrainState.DepartureStationNotFound);
        public static readonly Train DestinationStationNotFound = new Train(TrainState.DestinationStationNotFound);
        public static readonly Train TrainRideNotFound = new Train(TrainState.TrainRideNotFound);

        private Train(TrainState state)
        {
            this.State = state;
        }

        public Train(TrainAnnouncement trainAnnouncement)
        {
            Id = trainAnnouncement.ActivityId;
            Canceled = trainAnnouncement.Canceled;
            AdvertisedTimeAtLocation = trainAnnouncement.AdvertisedTimeAtLocation;
            EstimatedTimeAtLocation = trainAnnouncement.EstimatedTimeAtLocation;
            TimeAtLocation = trainAnnouncement.TimeAtLocation;
            OtherInformation = string.Join(", ", trainAnnouncement.OtherInformation);
            Deviations = string.Join(", ", trainAnnouncement.Deviation);
            ModifiedTime = trainAnnouncement.ModifiedTime;
            if (Canceled)
            {
                State = TrainState.Canceled;
            }
            else if (EstimatedTimeAtLocation.HasValue &&
                     AdvertisedTimeAtLocation < EstimatedTimeAtLocation.Value)
            {
                State = TrainState.Delayed;
                NumberOfMinutesDelayed = (int)Math.Round((EstimatedTimeAtLocation.Value - trainAnnouncement.AdvertisedTimeAtLocation).TotalMinutes);
            }
            else
            {
                State = TrainState.OnTime;
            }
        }
    }

    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum TrainState
    {
        OnTime,
        TrainRideNotFound,
        DepartureStationNotFound,
        DestinationStationNotFound,
        Delayed,
        Canceled
    }
}
