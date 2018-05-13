using System;
using System.Collections.ObjectModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Trafikverket.Request
{
    [Serializable()]
    [XmlType("Query", Namespace = "")]
    public class Query
    {
        [XmlElement("FILTER", Namespace = "")]
        public BaseGroup Filter
        { get; set; }

        [XmlElement("INCLUDE", Namespace = "", DataType = "string")]
        public Collection<string> Include
        { get; set; }

        [XmlAttribute("objecttype", Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string ObjectType
        { get; set; }

        [XmlAttribute("orderby", Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string OrderBy
        { get; set; }

        public Query()
        {
            Include = new Collection<string>();
        }
    }
}
