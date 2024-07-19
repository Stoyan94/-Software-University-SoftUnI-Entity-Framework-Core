namespace JSON_Demo;

internal class Person
{
    public string FullName { get; set; } = null!;
    public int Age { get; set; }
    public int Height { get; set; }
    public double Weight { get; set; }

    public Address Address { get; set; }
    
}

internal class Address
{
    public string City { get; set; }

    public string Street { get; set; }
}