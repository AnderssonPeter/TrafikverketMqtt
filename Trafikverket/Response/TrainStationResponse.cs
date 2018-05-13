using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    [Serializable()]
    [XmlRoot("RESPONSE", Namespace = "")]
    public class TrainStationResponse
    {
        [XmlArray("RESULT", Namespace = "")]
        [XmlArrayItem("TrainStation")]
        public Collection<TrainStation> Result
        { get; set; }
    }
}
