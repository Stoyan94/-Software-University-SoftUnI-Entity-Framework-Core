namespace JSON_Demo
{
    public class Rootobject_JSON
    {
        public string fullName { get; set; }
        public int age { get; set; }
        public int height { get; set; }
        public float weight { get; set; }
        public Address_JSON address { get; set; }
    }

    public class Address_JSON
    {
        public string city { get; set; }
        public string street { get; set; }
    }


}
//{ "FullName":"Petar Petrov","Age":25,"Height":185,"Weight":83.7}