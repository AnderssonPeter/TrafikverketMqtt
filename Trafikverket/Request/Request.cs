using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trafikverket.Request
{
    [Serializable()]
    [XmlType("REQUEST", Namespace = "", AnonymousType = true)]
    [XmlRoot(ElementName = "REQUEST")]
    public class Request
    {
        [XmlElement("LOGIN", Namespace = "")]
        public Login Login { get; set; }

        [XmlElement("QUERY", Namespace = "")]
        public Query Query { get; set; }
    }
}
