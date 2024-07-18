using Newtonsoft.Json;

namespace JSON_Demo;

public class Newthon_JSON
{    
    static void Main(string[] args)
    {
        var person = new Person()
        {
            FullName = "Petar Petrov",
            Age = 25,
            Height = 185,
            Weight = 83.7
        };

        var settings = new JsonSerializerSettings()
        {

        };

        string data = JsonConvert.SerializeObject(person);

        Console.WriteLine(data);

        Person? person1 = JsonConvert.DeserializeObject<Person>(data);

        Console.WriteLine($"{person1.FullName} is {person1.Age} years old and is {person1.Height}cm high!");
    }
}
