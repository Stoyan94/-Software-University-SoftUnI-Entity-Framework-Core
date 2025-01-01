namespace ExtensionMethodsEX
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string test = "this is a test";

            string result = StringExtensions.ToWeirdCase(test);

            Console.WriteLine(result);


            Console.WriteLine(test.ToWeirdCase2());
        }
    }
}
