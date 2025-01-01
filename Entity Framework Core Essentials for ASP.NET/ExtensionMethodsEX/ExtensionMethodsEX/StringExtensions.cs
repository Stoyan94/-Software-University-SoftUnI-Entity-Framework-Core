namespace ExtensionMethodsEX
{
    public static class StringExtensions
    {
        public static string ToWeirdCase(string str) // without the key word => this 
        {
            string result = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2==0)
                {
                    result += str[i].ToString().ToUpper();
                }
                else
                {
                    result += str[i].ToString().ToLower();
                } 
            }

            return result;
        }

        public static string ToWeirdCase2(this string str) // with the key word => this the method turns in to extension method
        {
            string result = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                {
                    result += str[i].ToString().ToUpper();
                }
                else
                {
                    result += str[i].ToString().ToLower();
                }
            }

            return str;
        }
    }
}
