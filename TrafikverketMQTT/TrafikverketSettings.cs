using System;
using System.Collections.Generic;
using System.Text;

namespace TrafikverketMQTT
{
    public class TrafikverketSettings
    {
        public string ApiKey
        { get; set; }

        public ICollection<TrainSettings> Trains
        { get; set; }
    }

    public class TrainSettings
    {
        public string Name
        { get; set; }

        public string DepartureLocationName
        { get; set; }

        public string DestinationLocationName
        { get; set; }

        public TimeSpan? DepatureTime
        { get; set; }
    }
}
