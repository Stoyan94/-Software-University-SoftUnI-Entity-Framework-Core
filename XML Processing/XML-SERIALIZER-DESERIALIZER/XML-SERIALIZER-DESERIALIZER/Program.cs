using System.Xml.Serialization;
using XML_SERIALIZER_DESERIALIZER;

var family = new Family()
{
    FamilyName = "Gorovi",
    Members = new Person[]
    {
        new Person()
        {
            Name = "Pesho",
            Age = 23
        },
          new Person()
        {
            Name = "Gosho",
            Age = 24
        }
    }
};

XmlSerializer serializer = new XmlSerializer(typeof(Family));
using (StreamWriter writer = new StreamWriter("family.xml"))
{
    serializer.Serialize(writer, family);
};