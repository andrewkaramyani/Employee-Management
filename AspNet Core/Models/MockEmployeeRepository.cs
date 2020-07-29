using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Models
{
    public class MockEmployeeRepository : IEmployeeReository
    {
        private List<Employee> _employees;

        public MockEmployeeRepository()
        {
            _employees = new List<Employee>
            {
                new Employee{Id=1 , Name="Andrew" , Department=Dept.IT, Email="andrew@gmail.com" },
                new Employee{Id=2 , Name="Ali" , Department=Dept.Manger, Email="Ali@gmail.com" },
                new Employee{Id=3 , Name="Tony" , Department=Dept.None, Email="Tony@gmail.com" }

            };
        }

        public Employee Add(Employee employee)
        {
           employee.Id= _employees.Max(e => e.Id) + 1;
            _employees.Add(employee);
           return employee;
        }

        public Employee Delete(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee != null)
            {
                _employees.Remove(GetEmployeeById(id));
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
           return  _employees;
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var Oldemployee = GetEmployeeById(employeeChanges.Id);
            if (Oldemployee != null)
            {
                Oldemployee.Name = employeeChanges.Name;
                Oldemployee.Email = employeeChanges.Email;
                Oldemployee.Department = employeeChanges.Department;
            }
            return employeeChanges;
        }
    }
}
