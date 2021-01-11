using MS2_DoTrongDuc.BL.Interfaces;
using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.Common.Enum;
using MS2_DoTrongDuc.Common.Properties;
using MS2_DoTrongDuc.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MS2_DoTrongDuc.BL
{
    public class EmployeeBL : IEmployeeBL
    {
        IEmployeeDL _employeeDL;
        ServiceResult _serviceResult;

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
            _serviceResult = new ServiceResult();
            _serviceResult.DUCCode = DUCServiceCode.Success;
        }

        /// <summary>
        /// Lấy dữ liệu của nhân viên theo id
        /// </summary>
        /// <param name="id">id của nhân viên</param>
        /// <returns>Trả về nhân viên có mã id</returns>
        public ServiceResult GetEmployeeById(string id)
        {
            var employee = _employeeDL.GetEmployeeById(id);
            if(employee == null)
            {
                _serviceResult.Messenger = new List<string> { Resources.Msg_No_Exist_Employee };
                _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                return _serviceResult;
            }

            _serviceResult.Data = employee;
            _serviceResult.Messenger = new List<string> { Resources.Msg_Get_Employee_Success };
            return _serviceResult;
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employee">Nhân viên cần xóa</param>
        /// <returns>Kết quả trả về của việc xóa nhân viên</returns>
        public ServiceResult DeleteEmployee(string employeeId)
        {
            //Kiểm tra sự tồn tại của nhân viên trong csdl
            var checkExistEmployee = _employeeDL.CheckDuplicate("EmployeeId", employeeId);
            
            //Nếu không tồn tại nhân viên trong csdl
            if (!checkExistEmployee)
            {
                _serviceResult.Data = employeeId;
                _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                _serviceResult.Messenger = new List<string>() { Resources.Msg_No_Exist_Employee };
                return _serviceResult;
            }

            var employee = _employeeDL.GetEmployeeById(employeeId);
            var affectRows = _employeeDL.DeleteEmployee(employee);
            
            _serviceResult.Data = employee;
            //Nếu việc xóa nhân viên thất bại
            if (affectRows == 0)
            {
                _serviceResult.Messenger = new List<string>() { Resources.Msg_Delete_Employee_Fali };
                _serviceResult.DUCCode = DUCServiceCode.Excaption;
                return _serviceResult;
            }

            //Nếu việc xóa nhân viên thành công
            _serviceResult.Messenger = new List<string>() { Resources.Msg_Delete_Employee_Success };
            return _serviceResult;
        }

        /// <summary>
        /// Lấy ra 20 nhân viên
        /// </summary>
        /// <returns>Kết quả của việc lấy ra 20 nhân viên</returns>
        public ServiceResult Get20Employees()
        {
            _serviceResult.Data = _employeeDL.GetEmployeesWithPageOrderAndNumberEmployee(0, 20);
            _serviceResult.Messenger = new List<string>() { Resources.Msg_Get_Employee_Success };
            return _serviceResult;
        }

        /// <summary>
        /// Lấy ra nhân viên theo Filter (Code || Phone || Name) && jobId && departmentId
        /// </summary>
        /// <param name="codePhoneName">dữ liệu cần tìm kiếm thuộc 1 trong 3 trường mã nhân viên - số điện thoại - tên</param>
        /// <param name="jobIdContains">mã vị trí công việc</param>
        /// <param name="departmentIdContains">mã phòng ban</param>
        /// <returns>Kết quả của việc lấy ra dữ liệu</returns>
        public ServiceResult GetEmployeesByFilter1(string codePhoneName, string jobIdContains, string departmentIdContains)
        {
            var result = _employeeDL.GetEmployeeByFilter1(codePhoneName, jobIdContains, departmentIdContains);
            if (result.Count() > 0)
            {
                _serviceResult.Messenger = new List<string>() { Resources.Msg_Get_Employee_Success };
            }
            else
            {
                _serviceResult.Messenger = new List<string>() { Resources.Msg_No_Exist_Employee };
            }
            _serviceResult.Data = result;
            return _serviceResult;
        }

        /// <summary>
        /// Thêm nhân viên mới
        /// </summary>
        /// <param name="employee">Nhân viên mới cần thêm</param>
        /// <returns>Kết quả của việc thêm mới, Data thể hiện số lượng nhân viên thêm thành công</returns>
        public ServiceResult InsertEmployee(Employee employee)
        {
            //Kiểm tra dữ liệu nhân viên đầu vào
            ValidateObject(employee);

            //Trường hợp dữ liệu đầu vào xấu
            if (_serviceResult.DUCCode == DUCServiceCode.BadRequest)
            {
                return _serviceResult;
            }

            //Trường hợp dữ liệu đầu vào là thuận lợi
            _serviceResult.Messenger = new List<string>() { Resources.Msg_Add_Employee_Success };
            _serviceResult.Data = _employeeDL.InsertEmployee(employee);
            return _serviceResult;
        }

        /// <summary>
        /// Cập nhật nhân viên
        /// </summary>
        /// <param name="employee">nhân viên cần cập nhật</param>
        /// <returns>Kết quả của việc cập nhật nhân viên.</returns>
        public ServiceResult UpdateEmployee(Employee employee)
        {
            //Kiểm tra đầu vào của nhân viên
            ValidateObject(employee);
            //Trường hợp đầu vào lỗi
            if (_serviceResult.DUCCode == DUCServiceCode.BadRequest)
            {
                return _serviceResult;
            }

            //Trường hợp đầu vào đúng chuẩn
            _serviceResult.Messenger = new List<string>() { Resources.Msg_Update_Employee_Success };
            _serviceResult.Data = _employeeDL.UpdateEmployee(employee);
            return _serviceResult;
        }

        /// <summary>
        /// Trả về mã nhân viên tiếp theo nên đặt
        /// </summary>
        /// <returns>mã số lớn nhất</returns>
        public ServiceResult GetEmployeeCodeMax()
        {
            _serviceResult.Data = _employeeDL.NextEmployeeCode();
            _serviceResult.Messenger = new List<string>{ Resources.Msg_Get_Employee_Success};
            return _serviceResult;
        }

        //private method
        //Kiểm tra về việc dữ liệu đầu vào
        private void ValidateObject(Employee employee)
        {
            var properties = typeof(Employee).GetProperties();
            //Kiểm tra các điều kiện đúng đắn về mặt dữ liệu
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(employee);
                var propertyName = property.Name;

                //trim dữ liệu
                if (propertyName == "string")
                {
                    propertyValue = propertyValue.ToString().Trim();
                }

                //Kiểm tra bắt buộc nhập
                if (property.IsDefined(typeof(Required), true) && (propertyValue == null || propertyValue.ToString() == string.Empty))
                {
                    var requiredAttribute = property.GetCustomAttributes(typeof(Required), true).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        var errorMessenger = (requiredAttribute as Required).Messenger;
                        _serviceResult.Messenger.Add(errorMessenger);
                    }
                    _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                }

                //Kiểm tra trùng lặp
                if (property.IsDefined(typeof(Duplicate), true))
                {
                    var duplicateAttribute = property.GetCustomAttributes(typeof(Duplicate), true).FirstOrDefault();
                    if (duplicateAttribute != null)
                    {
                        var errorMessenger = (duplicateAttribute as Duplicate).Messenger;
                        var checkDuplicate = _employeeDL.CheckDuplicate(propertyName, propertyValue);
                        if (checkDuplicate)
                        {
                            _serviceResult.Messenger.Add(errorMessenger);
                            _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                        }
                    }
                }

                //Kiểm tra độ dài
                //1.Kiểm tra độ dài lớn nhất
                if (property.IsDefined(typeof(MaxLength), true))
                {
                    var maxLengthAttribute = property.GetCustomAttributes(typeof(MaxLength), true).FirstOrDefault();
                    if (maxLengthAttribute != null)
                    {
                        var errorMessenger = (maxLengthAttribute as MaxLength).Messenger;
                        var lengthOfPropertyValue = propertyValue.ToString().Length;
                        if (lengthOfPropertyValue > (maxLengthAttribute as MaxLength).LengthMax)
                        {
                            _serviceResult.Messenger.Add(errorMessenger);
                            _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                        }
                    }
                }

                //2.Kiểm tra độ dài nhỏ nhất
                if (property.IsDefined(typeof(MinLength), true))
                {
                    var minLengthAttribute = property.GetCustomAttributes(typeof(MinLength), true).FirstOrDefault();
                    if (minLengthAttribute != null)
                    {
                        var errorMessenger = (minLengthAttribute as MinLength).Messenger;
                        var lengthOfPropertyValue = propertyValue.ToString().Length;
                        if (lengthOfPropertyValue < (minLengthAttribute as MinLength).LengthMin)
                        {
                            _serviceResult.Messenger.Add(errorMessenger);
                            _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                        }
                    }
                }

                //3.Kiểm tra độ dài cho phép
                if (property.IsDefined(typeof(Length), true))
                {
                    var lengthAttribute = property.GetCustomAttributes(typeof(Length), true).FirstOrDefault();
                    if (lengthAttribute != null)
                    {
                        var errorMessenger = (lengthAttribute as Length).Messenger;
                        var lengthOfProperty = property.ToString().Length;
                        if ((lengthAttribute as Length).LengthList.Contains(lengthOfProperty))
                        {
                            _serviceResult.Messenger.Add(errorMessenger);
                            _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                        }
                    }
                }

                //Kiểm tra sự đúng đắn của kiểu dữ liệu string
                if (property.IsDefined(typeof(TypeOf), true))
                {
                    var typeOfAttribute = property.GetCustomAttributes(typeof(TypeOf), true).FirstOrDefault();
                    if (typeOfAttribute != null)
                    {
                        var errorMessenger = (typeOfAttribute as TypeOf).Messenger;
                        Regex re = new Regex((typeOfAttribute as TypeOf).strRegex);
                        if (!re.IsMatch(propertyValue.ToString()))
                        {
                            _serviceResult.Messenger.Add(errorMessenger);
                            _serviceResult.DUCCode = DUCServiceCode.BadRequest;
                        }
                    }
                }
            }
        }

    }
}
