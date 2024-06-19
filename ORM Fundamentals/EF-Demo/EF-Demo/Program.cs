using System.Data.SqlClient;
using System.Data.SqlTypes;

string connectionString = "Server=STOYAN;Database=SoftUni;Trusted_Connection=True;";
string query = "SELECT EmployeeId, FirstName, LastName, JobTitle FROM Employees WHERE DepartmentID = @departmentId";
int departmentId = 7;

using SqlConnection connection = new SqlConnection(connectionString);

SqlCommand cmd = connection.CreateCommand();