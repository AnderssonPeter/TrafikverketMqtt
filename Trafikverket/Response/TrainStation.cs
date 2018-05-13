using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    [Serializable()]
    [XmlType("TrainStation", Namespace = "")]
    public class TrainStation
    {

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool Advertised
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string AdvertisedLocationName
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string AdvertisedShortLocationName
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string CountryCode
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<County> CountyNo
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool Deleted
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string LocationInformationText
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string LocationSignature
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "dateTime")]
        public DateTime ModifiedTime
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Collection<string> PlatformLine
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool Prognosticated
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Geometry Geometry
        { get; set; }

        public TrainStation()
        {
            CountyNo = new Collection<County>();
            PlatformLine = new Collection<string>();
        }
    }
}
