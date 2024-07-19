using System.Text.Json;

namespace JSON_Demo
{
    public class JSON_System_Tex
    {
        public static void JSON_System_Tex_()
        {
            var person = new Person()
            {
                FullName = "Petar Petrov",
                Age = 25,
                Height = 185,
                Weight = 83.7
            };

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string data = JsonSerializer.Serialize(person, options);

            Console.WriteLine(data);

            Person? person1 = JsonSerializer.Deserialize<Person>(data, options);

            Console.WriteLine($"{person1.FullName} is {person1.Age} years old and is {person1.Height} cm high!");
        }
    }
}
