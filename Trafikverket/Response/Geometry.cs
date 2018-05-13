using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    [Serializable()]
    [XmlType("Location", Namespace = "", AnonymousType = true)]
    public class Geometry
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string SWEREF99TM
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string WGS84
        { get; set; }
    }
}
