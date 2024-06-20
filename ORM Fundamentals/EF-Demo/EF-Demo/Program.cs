using System.Data.SqlClient;


string connectionString = "Server=STOYAN;Database=SoftUni;User Id=sa;Password=558955;Trusted_Connection=True;";
string query = "SELECT EmployeeID, FirstName, LastName, JobTitle FROM Employees WHERE DepartmentID = @departmentId";
int departmentId = 3;

using SqlConnection connection = new SqlConnection(connectionString);

SqlCommand cmd = new SqlCommand(query, connection);
cmd.Parameters.AddWithValue("@departmentId", departmentId);

try
{
    connection.Open();
        
    using (SqlDataReader reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            string firstName = $"{reader[1]}";
            Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]}: {reader[3]}");

            if (firstName == "Lynn" || firstName.Contains("Lynn"))
            {
                Console.WriteLine(true);
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine("Seckond query");

    using (SqlDataReader reader2 = cmd.ExecuteReader())
    {
        while (reader2.Read())
        {
            //The GetOrdinal method in the SqlDataReader class is used to get the zero-based column ordinal (index) of a column given its name.
            //This is useful when you want to access the columns of a SqlDataReader by their names but still use the more efficient index-based access methods.
            int employeeId = reader2.GetInt32(reader2.GetOrdinal("EmployeeID"));
            string firstName = reader2.GetString(reader2.GetOrdinal("FirstName"));
            string lastName = reader2.GetString(reader2.GetOrdinal("LastName"));
            string jobTitle = reader2.GetString(reader2.GetOrdinal("JobTitle"));

            if (firstName == "Lynn" || firstName.Contains("Lynn"))
            {
                Console.WriteLine(true);
            }

            Console.WriteLine($"{employeeId}: {firstName} {lastName} - {jobTitle}");

            //How GetOrdinal Works
            //Case - Insensitive Search: GetOrdinal performs a case -insensitive search for the column name.
            //Cached Result: It caches the results for better performance when the same column is accessed multiple times.

            //Why Use GetOrdinal
            //Using GetOrdinal is beneficial for several reasons:
            //
            //Readability: Accessing columns by name improves code readability and maintainability.
            //Efficiency: Once you have the column index, you can use it to quickly access the column values in a loop or multiple times without repeatedly performing a lookup.

        }
    }
 }
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}


//Методът GetOrdinal в класа SqlDataReader се използва за получаване на базирания на нула номер на колона (индекс) на колона с нейното име.
//Това е полезно, когато искате да получите достъп до колоните на SqlDataReader по имената им, но все още използвате по-ефективните методи за достъп, базирани на индекс.

//Как работи GetOrdinal
//Търсене без регистър: GetOrdinal извършва търсене без регистър на името на колоната.
//Кеширан резултат: Кешира резултатите за по-добра производителност, когато една и съща колона е достъпна многократно.

//Защо да използвате GetOrdinal
//Използването на GetOrdinal е полезно поради няколко причини:
//Четивност: Достъпът до колони по име подобрява четимостта и поддръжката на кода.
//Ефективност: След като имате индекса на колоната, можете да го използвате за бърз достъп до стойностите на колоната в цикъл или няколко пъти, без многократно извършване на търсене.