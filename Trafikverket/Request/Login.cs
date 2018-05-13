using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Trafikverket.Request
{
    [Serializable()]
    [XmlType("Login", Namespace = "")]
    public class Login
    {
        [XmlAttribute("authenticationkey", Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string Authenticationkey { get; set; }
    }
}
