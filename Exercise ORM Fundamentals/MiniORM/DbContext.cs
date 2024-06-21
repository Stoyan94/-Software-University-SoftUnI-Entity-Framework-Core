namespace MiniORM
{
    public class DbContext
    {
        internal static HashSet<Type> AllowedSqlTypes { get; set; } = new HashSet<Type>
        {
            typeof(string),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime),
        };
    }
}
