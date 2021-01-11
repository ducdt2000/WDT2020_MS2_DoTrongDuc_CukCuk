using Microsoft.AspNetCore.Mvc;
using MS2_DoTrongDuc.BL.Interfaces;
using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.Common.Enum;
using MS2_DoTrongDuc.Common.Properties;
using MS2_DoTrongDuc.DL;
using MS2_DoTrongDuc.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.BL
{
    public class DepartmentBL : IDepartmentBL
    {
        IDepartmentDL _departmentDL;
        ServiceResult _serviceResult;
        public DepartmentBL(IDepartmentDL departmentDL)
        {
            _departmentDL = departmentDL;
            _serviceResult = new ServiceResult();
            _serviceResult.DUCCode = DUCServiceCode.Success;
        }

        ServiceResult IDepartmentBL.GetDepartments()
        {
            _serviceResult.Data = _departmentDL.GetDepartments();
            _serviceResult.Messenger = new List<string> { Resources.Msg_Success };
            return _serviceResult;
        }
    }
}
