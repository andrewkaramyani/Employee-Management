using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Models
{
    public class SqlEmployeeRepository : IEmployeeReository
    {
        private readonly AppDbContext Context;

        public SqlEmployeeRepository(AppDbContext Context)
        {
            this.Context = Context;
        }
        public Employee Add(Employee employee)
        {
            Context.employees.Add(employee);
            Context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = Context.employees.Find(id);
            if (employee != null)
            {
                Context.Remove(employee);
                Context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return Context.employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = Context.employees.Find(id);
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = Context.employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return employeeChanges;
        }
    }
}
