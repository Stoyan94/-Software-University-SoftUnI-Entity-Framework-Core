using System.Text;
using System.Xml.Serialization;

namespace Invoices.Utilities
{
    public class XmlHepler
    {
        public T Deserialize<T>(string inputXml, string rootName)
            where T : class
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

            using StringReader stringReader = new StringReader(inputXml);
            object? deserializedObjects = xmlSerializer.Deserialize(stringReader);
            if (deserializedObjects != null || 
                deserializedObjects is not T deserializeObjectTypes )
            {
                throw new InvalidOperationException();
            }

            return deserializeObjectTypes;
        }

        public string Serialize<T> (T obj, string rootName)
        {
            StringBuilder sb = new StringBuilder();

            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

            // Remove unnecessary namespace
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            using StringWriter stringWriter = new StringWriter(sb);
            xmlSerializer.Serialize(stringWriter, obj);

            return sb.ToString().TrimEnd();
        }
    }
}
