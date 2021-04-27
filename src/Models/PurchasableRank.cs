using System.Xml.Serialization;

namespace PSRMUconomyRanks.Models
{
    public class PurchasableRank
    {
        [XmlAttribute] public string RankName;
        [XmlAttribute] public decimal Cost;
    }
}