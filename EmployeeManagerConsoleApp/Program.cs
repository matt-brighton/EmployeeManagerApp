using System.Data.SqlClient;

namespace EmployeeManagerConsoleApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }

    public class EmployeeRepository
    {
        private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManager;Trusted_Connection=True;MultipleActiveResultSets=true";

        public void SaveEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO dbo.Employees (FirstName, LastName, Department) VALUES (@FirstName, @LastName, @Department)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //Command parameters prevent SQL injection
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Employee SearchEmployee(string firstName, string lastName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.Employees WHERE FirstName = @FirstName AND LastName = @LastName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                Id = (int)reader["Id"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Department = (string)reader["Department"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void DeleteEmployee(string firstName, string lastName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM dbo.Employees  WHERE FirstName = @FirstName AND LastName = @LastName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository repository = new EmployeeRepository();

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Save Employee");
                Console.WriteLine("2. Search Employee");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Enter Department: ");
                        string department = Console.ReadLine();

                        Employee employeeToSave = new Employee
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Department = department
                        };

                        repository.SaveEmployee(employeeToSave);
                        Console.WriteLine("Employee saved successfully!");
                        break;

                    case "2":
                        Console.Write("Enter First Name to search: ");
                        string searchFirstName = Console.ReadLine();
                        Console.Write("Enter Last Name to search: ");
                        string searchLastName = Console.ReadLine();

                        Employee foundEmployee = repository.SearchEmployee(searchFirstName, searchLastName);

                        if (foundEmployee != null)
                        {
                            Console.WriteLine($"Employee Id: {foundEmployee.Id}");
                            Console.WriteLine($"First Name: {foundEmployee.FirstName}");
                            Console.WriteLine($"Last Name: {foundEmployee.LastName}");
                            Console.WriteLine($"Department: {foundEmployee.Department}");
                        }
                        else
                        {
                            Console.WriteLine("Employee not found.");
                        }
                        break;

                    case "3":
                        Console.Write("Enter First Name to delete: ");
                        string deleteFirstName = Console.ReadLine();
                        Console.Write("Enter Last Name to delete: ");
                        string deleteLastName = Console.ReadLine();

                        repository.DeleteEmployee(deleteFirstName, deleteLastName);
                        Console.WriteLine("Employee successfully deleted.");
                        break;

                    case "4":
                        Console.WriteLine("Exiting the application.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please select a valid option.");
                        break;
                }
            }
        }
    }
}
