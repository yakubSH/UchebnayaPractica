using System;
using System.Linq;
using RefactoringLabWinForms.Models;

namespace RefactoringLabWinForms.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public bool AddEmployee(string name, int age, decimal salary, out string errorMessage)
        {
            var employee = new Employee(name, age, salary);

            if (!employee.IsValid(out errorMessage))
            {
                return false;
            }

            _repository.Add(employee);
            errorMessage = string.Empty;
            return true;
        }
        public void RemoveEmployee(Employee employee)
        {
            _repository.Remove(employee);
        }
        public System.Collections.Generic.List<Employee> GetAllEmployees()
        {
            return _repository.GetAll();
        }
        public decimal CalculateAverageSalary()
        {
            var employees = _repository.GetAll();

            if (employees.Count == 0)
            {
                return 0;
            }

            return employees.Average(e => e.Salary);
        }
        public Employee FindYoungestEmployee()
        {
            var employees = _repository.GetAll();
            return employees.OrderBy(e => e.Age).FirstOrDefault();
        }
        public int GetEmployeeCount()
        {
            return _repository.GetAll().Count;
        }
    }
}
