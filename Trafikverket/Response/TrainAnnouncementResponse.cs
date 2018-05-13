using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    [Serializable()]
    [XmlRoot("RESPONSE", Namespace = "")]
    public class TrainAnnouncementResponse
    {
        [XmlArray("RESULT", Namespace = "")]
        [XmlArrayItem("TrainAnnouncement")]
        public Collection<TrainAnnouncement> Result
        { get; set; }
    }
}
