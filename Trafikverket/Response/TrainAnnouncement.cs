using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    [Serializable()]
    [XmlType("TrainAnnouncement", Namespace = "")]
    public class TrainAnnouncement
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string ActivityId
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public ActivityType ActivityType
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool Advertised
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "dateTime")]
        public DateTime AdvertisedTimeAtLocation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string AdvertisedTrainIdent
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<string> Booking
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool Canceled
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool Deleted
        { get; set; }

        [XmlElement("Deviation")]
        public Collection<string> Deviation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "dateTime")]
        public DateTime? EstimatedTimeAtLocation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool EstimatedTimeIsPreliminary
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string InformationOwner
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string LocationSignature
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string MobileWebLink
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "dateTime")]
        public DateTime ModifiedTime
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "int")]
        public int NewEquipment
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<string> OtherInformation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "dateTime")]
        public DateTime PlannedEstimatedTimeAtLocation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool PlannedEstimatedTimeAtLocationIsValid
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<string> ProductInformation
        { get; set;  }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "dateTime")]
        public DateTime ScheduledDepartureDateTime
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<string> Service
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string TechnicalTrainIdent
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "dateTime")]
        public DateTime TimeAtLocation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string TrackAtLocation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<string> TrainComposition
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string TypeOfTraffic
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string WebLink
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string WebLinkName
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Location FromLocation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Location ToLocation
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<Location> ViaToLocation
        { get; set; }
    }
}
