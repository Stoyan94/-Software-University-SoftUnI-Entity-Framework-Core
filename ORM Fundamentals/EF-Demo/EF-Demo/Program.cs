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

    SqlDataReader reader = cmd.ExecuteReader();

    while (reader.Read()) 
    {
        Console.WriteLine($"{reader[1]} {reader[2]}:  {reader[3]}");
    }

    Console.WriteLine("yes");
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}


