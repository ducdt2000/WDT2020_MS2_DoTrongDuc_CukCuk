using Microsoft.AspNetCore.Mvc;
using MS2_DoTrongDuc.BL.Interfaces;
using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.Common.Enum;
using MS2_DoTrongDuc.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WDT2020_MS2_DoTrongDuc_CukCuk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IDepartmentBL _departmentBL;

        public DepartmentsController(IDepartmentBL departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #region Get

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _departmentBL.GetDepartments();
            return switchAction(result);

            #endregion
        }

        private IActionResult switchAction(ServiceResult serviceResult)
        {
            switch (serviceResult.DUCCode)
            {
                case DUCServiceCode.BadRequest:
                    return BadRequest(serviceResult);
                case DUCServiceCode.Success:
                    return Ok(serviceResult);
                case DUCServiceCode.Excaption:
                    return StatusCode(500);
                default:
                    return Ok();
            }
        }
    }
}