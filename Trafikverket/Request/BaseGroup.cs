using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Trafikverket.Request
{
    [Serializable()]
    [XmlType("BaseGroup", Namespace = "")]
    public class BaseGroup
    {
        [XmlElement("EXISTS", Namespace = "")]
        public Collection<BaseCondition> Exists
        { get; set; }

        [XmlElement("LT", Namespace = "")]
        public Collection<BaseCondition> LesserThan
        { get; set; }

        [XmlElement("LTE", Namespace = "")]
        public Collection<BaseCondition> LesserThanEqual
        { get; set; }

        [XmlElement("GT", Namespace = "")]
        public Collection<BaseCondition> GreaterThan
        { get; set; }

        [XmlElement("GTE", Namespace = "")]
        public Collection<BaseCondition> GreaterThanEqual
        { get; set; }

        [XmlElement("EQ", Namespace = "")]
        public new Collection<BaseCondition> Equals
        { get; set; }

        [XmlElement("NE", Namespace = "")]
        public Collection<BaseCondition> NotEquals
        { get; set; }

        [XmlElement("LIKE", Namespace = "")]
        public Collection<BaseCondition> Like
        { get; set; }

        [XmlElement("NOTLIKE", Namespace = "")]
        public Collection<BaseCondition> NotLike
        { get; set; }

        [XmlElement("IN", Namespace = "")]
        public Collection<BaseCondition> In
        { get; set; }

        [XmlElement("NOTIN", Namespace = "")]
        public Collection<BaseCondition> NotIn
        { get; set; }

        [XmlElement("WITHIN", Namespace = "")]
        public Collection<BaseCondition> WithIn
        { get; set; }

        [XmlElement("AND", Namespace = "")]
        public Collection<BaseGroup> And
        { get; set; }

        [XmlElement("OR", Namespace = "")]
        public Collection<BaseGroup> Or
        { get; set; }

        public BaseGroup()
        {
            Exists = new Collection<BaseCondition>();
            LesserThan = new Collection<BaseCondition>();
            LesserThanEqual = new Collection<BaseCondition>();
            GreaterThan = new Collection<BaseCondition>();
            GreaterThanEqual = new Collection<BaseCondition>();
            Equals = new Collection<BaseCondition>();
            NotEquals = new Collection<BaseCondition>();
            Like = new Collection<BaseCondition>();
            NotLike = new Collection<BaseCondition>();
            In = new Collection<BaseCondition>();
            NotIn = new Collection<BaseCondition>();
            WithIn = new Collection<BaseCondition>();
            And = new Collection<BaseGroup>();
            Or = new Collection<BaseGroup>();
        }
    }
}
