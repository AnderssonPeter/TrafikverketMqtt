using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    [Serializable()]
    [XmlRoot("RESPONSE", Namespace = "")]
    public class Response<T>
    {
        [XmlElement("RESULT", Namespace = "")]
        public T Result
        { get; set; }
    }
}
