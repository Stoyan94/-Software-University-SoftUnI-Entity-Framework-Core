using System.Globalization;
using System.Text;
using System.Xml;
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


        public static string SerializeToXml<T>(T dto, string xmlRootAttribute, bool omitDeclaration = false)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttribute));
            StringBuilder stringBuilder = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitDeclaration,
                Encoding = new UTF8Encoding(false),
                Indent = true
            };

            using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                try
                {
                    xmlSerializer.Serialize(xmlWriter, dto, xmlSerializerNamespaces);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
