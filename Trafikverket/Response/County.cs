using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trafikverket.Response
{
    public enum County : int
    {
        [XmlEnum(Name = "1")]
        Stockholms = 1,
        [XmlEnum(Name = "3")]
        Uppsala = 3,
        [XmlEnum(Name = "4")]
        Södermanlands = 4,
        [XmlEnum(Name = "5")]
        Östergötlands = 5,
        [XmlEnum(Name = "6")]
        Jönköpings = 6,
        [XmlEnum(Name = "7")]
        Kronobergs = 7,
        [XmlEnum(Name = "7")]
        Kalmar = 8,
        [XmlEnum(Name = "9")]
        Gotlands = 9,
        [XmlEnum(Name = "10")]
        Blekinge = 10,
        [XmlEnum(Name = "12")]
        Skåne = 12,
        [XmlEnum(Name = "13")]
        Hallands = 13,
        [XmlEnum(Name = "14")]
        Västra_Götalands = 14,
        [XmlEnum(Name = "17")]
        Värmlands = 17,
        [XmlEnum(Name = "18")]
        Örebro = 18,
        [XmlEnum(Name = "19")]
        Västmanlands = 19,
        [XmlEnum(Name = "20")]
        Dalarnas = 20,
        [XmlEnum(Name = "21")]
        Gävleborgs = 21,
        [XmlEnum(Name = "22")]
        Västernorrlands = 22,
        [XmlEnum(Name = "23")]
        Jämtlands = 23,
        [XmlEnum(Name = "24")]
        Västerbottens = 24,
        [XmlEnum(Name = "25")]
        Norrbottens = 25
    }
}
