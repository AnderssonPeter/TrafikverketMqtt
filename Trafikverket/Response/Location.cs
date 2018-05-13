using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    [Serializable()]
    [XmlType("Location", Namespace = "", AnonymousType = true)]
    public class Location
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string LocationName
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "int")]
        public int Order
        { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "int")]
        public int Priority
        { get; set; }
    }
}
