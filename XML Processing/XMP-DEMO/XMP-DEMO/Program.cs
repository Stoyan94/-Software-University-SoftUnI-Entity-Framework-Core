using System.Xml.Linq;

string xml = @"<?xml version=""1.0""?>
<library name=""Developer's Library"">
 <book>
    <title>Professional C# and .NET</title>
    <author>Christian Nagel</author>
    <isbn>978-0-470-50225-9</isbn>
 </book>
 <book>
    <title>Teach Yourself XML in 10
    Minutes</title>
    <author>Andrew H. Watt</author>
    <isbn>978-0-672-32471-0</isbn>
 </book>
</library>";

// Create XML-File
XDocument doc2 = new XDocument();

doc2.Add(
     new XElement("class",
                new XElement("student",
                    new XAttribute("course", "C#"),
                    new XElement("name", "Stoyan")),
                new XElement("student",
                    new XAttribute("course", "C++"),
                    new XElement("name", "Gosho"))
    )
);

doc2.Save("First-Xml"); // => <?xml version="1.0" encoding="utf-8"?>
                            // <class>
                            //   <student course ="C#>
                            //     <name> Stoyan </name>
                            //   </student>
                            //   <student course ="C++">
                            //     <name > Gosho </name>
                            //   </student>
                            // </class>


void LINQ_TO_XML()
{
    XDocument doc = XDocument.Parse(xml);

    // LINQ TO XML

    //Console.WriteLine(doc.Root.Value); // All elements in all roots

    //Console.WriteLine(doc.Root.Descendants()
    //    .First()
    //    .Elements()
    //    .First().Value); return => // Professional C# and .NET - First element from the first Child with LINQ

    //string authorName = doc.Root.Descendants()
    //                            .First(e => e.Name == "author").Value;

    //Console.WriteLine(authorName); // return => Christian Nagel

    //doc.Root.Descendants()
    //        .First()
    //        .SetElementValue("issueDate", "2024-07-25");

    //doc.Save("test.xml"); // add Element in the first child element => <book>
                                                                        // <title>Professional C# and .NET</title>
                                                                        // <author>Christian Nagel</author>
                                                                        // <isbn>978-0-470-50225-9</isbn>
                                                                        // <issueDate>2024-07-25</issueDate>

    //doc.Root.Descendants()
    //        .First()
    //        .SetAttributeValue("issueDate", "2024-07-25");

    //doc.Save("test.xml"); // add Attribute in the first child element => <book issueDate="2024-07-25">
                                                                         // <title>Professional C# and .NET</title>
                                                                         // <author>Christian Nagel</author>
                                                                         // <isbn>978-0-470-50225-9</isbn>
}
