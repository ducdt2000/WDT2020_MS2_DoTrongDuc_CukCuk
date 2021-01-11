using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.DL.Interfaces;
using System.Collections.Generic;

namespace MS2_DoTrongDuc.DL
{
    public class EmployeeDL : DbConnector, IEmployeeDL
    {
        DbConnector _dbConnector;

        public EmployeeDL()
        {
            _dbConnector = new DbConnector();
        }

        /// <summary>
        /// Lấy ra danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Trả về danh sách nhân viên</returns>
        public IEnumerable<Employee> GetEmployees()
        {
            return _dbConnector.GettAllData<Employee>();
        }

        /// <summary>
        /// Thêm 1 nhân viên mới vào trong db
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int InsertEmployee(Employee employee)
        {
            return _dbConnector.Insert<Employee>(employee);
        }

        /// <summary>
        /// Lấy ra  bản ghi ở trang thứ pageNumber
        /// </summary>
        /// <param name="pageNumber">thứ tự trang cần phải lấy</param>
        /// <param name="numberEmployee">Số lượng nhân viên cần cho 1 bảng</param>
        /// <returns>tối đa numberEmployee bản ghi lấy ra ở vị trí numberEmployee*pageNumber -> numberEmployee*(pageNumber+1) - 1</returns>
        public IEnumerable<Employee> GetEmployeesWithPageOrderAndNumberEmployee(int pageNumber, int numberEmployee)
        {
            var sql = $"Select * From Employee Order By EmployeeId Limit {pageNumber * numberEmployee}, {numberEmployee}";
            return _dbConnector.GetDataByCommand<Employee>(sql);
        }

        /// <summary>
        /// Lấy ra 1 nhân viên với Id
        /// </summary>
        /// <param name="employeeId">Id nhân viên cần lấy</param>
        /// <returns>Nhân viên có Id = employeeId</returns>
        public Employee GetEmployeeById(string employeeId)
        {
            return _dbConnector.GetById<Employee>(employeeId);
        }

        /// <summary>
        /// Lấy ra danh sách nhân viên bằng bộ lọc 1
        /// Bộ lọc 1 bao gồm
        /// 1 tham số đầu vào có thể là mã nhân viên - số điện thoại hoặc tên
        /// 1 tham số chứa mã vị trí công việc
        /// 1 tham số chứa mã phòng ban
        /// </summary>
        /// <param name="CodePhoneName1">Giá trị có thể là mã - số điện thoại - tên</param>
        /// <param name="JobIdContains1">Mã vị trí công việc</param>
        /// <param name="DepartmentIdContains1">Mã phòng ban</param>
        /// <returns>Danh sách các nhân viên thỏa mãn filter (Code || Phone || Name) && JobId && DepartmentId</returns>
        public IEnumerable<Employee> GetEmployeeByFilter1(string CodePhoneName1, string JobIdContains1, string DepartmentIdContains1)
        {
            var parametters = new
            {
                CodePhoneName = CodePhoneName1,
                JobIdContains = JobIdContains1,
                DepartmentIdContains = DepartmentIdContains1
            };
            var procName = "Proc_GetEmployeeByFilter1";
            return _dbConnector.GetDataByProcStore<Employee>(procName, parametters);
        }

        /// <summary>
        /// Trả về mã nhân viên tiếp theo
        /// </summary>
        /// <returns>Mã nhân viên tiếp theo theo quy tắc của công ty</returns>
        public string NextEmployeeCode()
        {
            string sql = "SELECT MAX(CAST(SUBSTRING(e.EmployeeCode, 3, LENGTH(e.EmployeeCode) - 2)AS INT)) FROM Employee e";
            int maxNumberCode = (int)_dbConnector.GetDataByCommand1<int>(sql);
            return "NV" + (maxNumberCode + 1);
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employee">Nhân viên cần xóa</param>
        /// <returns>Số lượng nhân viên xóa được</returns>
        public int DeleteEmployee(Employee employee)
        {
            var result = _dbConnector.DeleteById<Employee>(employee.Id);
            return result;
        }


        /// <summary>
        /// Cập nhật 1 nhân viên
        /// </summary>
        /// <param name="employee">nhân viên cần cập nhật</param>
        /// <returns>Trả về số lượng nhân viên cập nhật thành công</returns>
        public int UpdateEmployee(Employee employee)
        {
            return _dbConnector.Update<Employee>(employee);
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của nhân viên với thuộc tính nameProperty mang giá trị valueProperty và kiểu dữ liệu T
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của valueProperty</typeparam>
        /// <param name="nameProperty">Tên của thuộc tính</param>
        /// <param name="valueProperty">giá trị thuộc tính</param>
        /// <returns>trả về có tồn lại (true) hoặc không tồn tại (false)</returns>
        public bool CheckDuplicate<T>(string nameProperty, T valueProperty)
        {
            return _dbConnector.CheckExist<Employee, T>(nameProperty, valueProperty);
        }
    }
}
