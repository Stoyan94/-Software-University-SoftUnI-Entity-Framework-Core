using System.Xml.Serialization;

namespace XML_SERIALIZER_DESERIALIZER
{
    public class Family
    {
        [XmlAttribute("name")]
        public string FamilyName { get; set; }

        [XmlArray("FamilyMembers")]
        public Person[] Members { get; set; }
    }
}
