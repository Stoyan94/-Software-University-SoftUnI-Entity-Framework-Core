using System.Xml.Serialization;

namespace ProductShop
{
    public static class XmlHelper
    {

        // Generic XML deserializer
        public static T Deserialize<T>(string inputXml, string rootName)
        {
            // Create an XmlRootAttribute with the specified root name
            XmlRootAttribute root = new XmlRootAttribute(rootName);

            // Initialize the XmlSerializer with the type and root
            XmlSerializer serializer = new XmlSerializer(typeof(T), root);

            // Use StringReader to treat the inputXml string as a readable stream
            using StringReader reader = new StringReader(inputXml);

            // Deserialize the XML into an object of type T
            T dtos = (T)serializer.Deserialize(reader);

            // Return the deserialized object
            return dtos;
        }
    }
}
