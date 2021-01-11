using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.DL
{
    public class DepartmentDL : IDepartmentDL
    {
        DbConnector _dbConnector;

        public DepartmentDL()
        {
            _dbConnector = new DbConnector();
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _dbConnector.GettAllData<Department>();
        }
    }
}
