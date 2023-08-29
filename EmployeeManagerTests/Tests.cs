using EmployeeManagerConsoleApp;

namespace EmployeeManagerTests
{
    [TestFixture]
    public class Tests
    {
        [Test, Order(1)]
        public void TestSaveEmployee()
        {
            // Create an instance of the EmployeeRepository class to interact with employee data.
            EmployeeRepository repository = new EmployeeRepository();
            // Create a new Employee object with the specified details.
            Employee employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Department = "IT"
            };
            
            // Save the newly created employee using the repository.
            repository.SaveEmployee(employee);

            // Search for an employee with the given first name "John" and last name "Doe".
            employee = repository.SearchEmployee("John", "Doe");

            // Use the Assert.NotNull method to verify that the employee object is not null.
            Assert.NotNull(employee);

        }

        [Test, Order(2)]
        public void TestSearchEmployee()
        {
            // Create an instance of the EmployeeRepository class to interact with employee data.
            EmployeeRepository repository = new EmployeeRepository();

            // Search for an employee with the given first name "John" and last name "Doe".
            Employee employee = repository.SearchEmployee("John", "Doe");

            // Use the Assert.NotNull method to verify that the employee object is not null.
            Assert.NotNull(employee);

            // Use the Assert.AreEqual method to verify that the employee's first name is "John".
            Assert.AreEqual("John", employee.FirstName);

            // Use the Assert.AreEqual method to verify that the employee's last name is "Doe".
            Assert.AreEqual("Doe", employee.LastName);
        }

        [Test, Order(3)]
        public void TestDeleteEmployee()
        {
            // Create an instance of the EmployeeRepository class to interact with employee data.
            EmployeeRepository repository = new EmployeeRepository();

            // Delete employee with the given first name "John" and last name "Doe".
            repository.DeleteEmployee("John", "Doe");

            // Search for an employee with the given first name "John" and last name "Doe".
            Employee employee = repository.SearchEmployee("John", "Doe");

            // Use the Assert.NotNull method to verify that the employee object is null.
            Assert.IsNull(employee);

        }
    }
}
