using Microsoft.AspNetCore.Mvc;
using MS2_DoTrongDuc.BL.Interfaces;
using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.Common.Enum;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WDT2020_MS2_DoTrongDuc_CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeBL _employeeBL;

        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #region Post

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            var result = _employeeBL.InsertEmployee(employee);
            return switchAction(result);
        }

        #endregion

        #region Get

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeBL.Get20Employees();
            return switchAction(result);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult GetByFilter1([FromQuery] string codePhoneName, string jobId, string departmentId)
        {
            jobId = jobId == null ? "" : jobId;
            departmentId = departmentId == null ? "" : departmentId;

            var result = _employeeBL.GetEmployeesByFilter1(codePhoneName, jobId, departmentId);
            return switchAction(result);
        }

        [HttpGet]
        [Route("id={id}")]
        public IActionResult Get(string id)
        {
            var result = _employeeBL.GetEmployeeById(id);
            return switchAction(result);
        }

        [HttpGet]
        [Route("max-code")]
        public IActionResult GetMaxCode()
        {
            var result = _employeeBL.GetEmployeeCodeMax();
            return switchAction(result);
        }
        #endregion

        #region Put

        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
            var result = _employeeBL.UpdateEmployee(employee);
            return switchAction(result);
        }

        #endregion

        #region Delete
        [HttpDelete]
        [Route("id={employeeId}")]
        public IActionResult Delete(string employeeId)
        {
            var result = _employeeBL.DeleteEmployee(employeeId);
            return switchAction(result);
        }


        #endregion

        //private method
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