using EmployeeManagerConsoleApp;

namespace EmployeeManagerTests
{
    [TestFixture]
    public class Tests
    {
        [Test, Order(1)]
        public void TestSaveEmployee()
        {
            EmployeeRepository repository = new EmployeeRepository();
            Employee employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Department = "IT"
            };
            repository.SaveEmployee(employee);

            employee = repository.SearchEmployee("John", "Doe");

            Assert.NotNull(employee);

        }

        [Test, Order(2)]
        public void TestSearchEmployee()
        {
            EmployeeRepository repository = new EmployeeRepository();
            Employee employee = repository.SearchEmployee("John", "Doe");

            Assert.NotNull(employee);
            Assert.AreEqual("John", employee.FirstName);
            Assert.AreEqual("Doe", employee.LastName);
        }

        [Test, Order(3)]
        public void TestDeleteEmployee()
        {
            EmployeeRepository repository = new EmployeeRepository();

            repository.DeleteEmployee("John", "Doe");

            Employee employee = repository.SearchEmployee("John", "Doe");

            Assert.IsNull(employee);

        }
    }
}
