using MS2_DoTrongDuc.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.DL.Interfaces
{
    public interface IEmployeeDL
    {
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesWithPageOrderAndNumberEmployee(int pageNumber, int numberEmployee);
        Employee GetEmployeeById(string employeeId);
        IEnumerable<Employee> GetEmployeeByFilter1(string CodePhoneName, string jobIdContains, string departmentIdContains);
        int InsertEmployee(Employee employee);
        public bool CheckDuplicate<T>(string nameProperty, T valueProperty);
        public string NextEmployeeCode();
        int DeleteEmployee(Employee employee);
        int UpdateEmployee(Employee employee);
    }
}
