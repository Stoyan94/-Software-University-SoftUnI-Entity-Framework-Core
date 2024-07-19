using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JSON_Demo
{
    public class XML_TO_JSON
    {
        public static void Xml_To_JSON()
        {
            string xml = @"<?xml version=""1.0"" standalone=""no""?>
           <root>
               <person id=""1"">
                   <name>Alan</name>
                   <url>www.google.com</url>
             </person>
             <person id=""2"">
                   <name>Louis</name>
                   <url>www.yahoo.com</url>
             </person>
           </root>";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            string data = JsonConvert.SerializeXmlNode(xmlDocument, Newtonsoft.Json.Formatting.Indented);

            Console.WriteLine(data);
        }       
    }
}
