using Microsoft.AspNetCore.Mvc;
using MS2_DoTrongDuc.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.BL.Interfaces
{
    public interface IEmployeeBL
    {
        ServiceResult GetEmployeeById(string id);
        ServiceResult GetEmployeeCodeMax();
        ServiceResult InsertEmployee(Employee employee);
        ServiceResult Get20Employees();
        ServiceResult GetEmployeesByFilter1(string CodePhoneName, string JobIdContains, string DepartmentIdContains);
        ServiceResult UpdateEmployee(Employee employee);
        ServiceResult DeleteEmployee(string employeeId);
    }

}
