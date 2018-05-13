using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Trafikverket.Request
{

    [Serializable()]
    [XmlType("BaseCondition", Namespace = "")]
    public class BaseCondition
    {
        [XmlAttribute("name", Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string Name { get; set; }

        [XmlAttribute("value", Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public string Value { get; set; }

        public BaseCondition()
        {
        }

        public BaseCondition(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
