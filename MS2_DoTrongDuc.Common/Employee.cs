using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.Common
{
    public class Employee
    {/// <summary>
     /// Mã Guid nhân viên
     /// </summary>
        public Guid EmployeeId
        {
            get;
            set;
        }
        /// <summary>
        /// Mã Guid nhân viên dưới dạng string
        /// </summary>
        public string Id
        {
            get
            {
                return EmployeeId.ToString();
            }
        }

        /// <summary>
        /// Mã nhân viên - NV + Thứ tự của nhân viên trong công ty
        /// </summary>
        [Required("Mã nhân viên")]
        [Duplicate("Mã nhân viên")]
        [MaxLength("Mã nhân viên", 20)]
        [MinLength("Mã nhân viên", 3)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required("Tên nhân viên")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính nhân viên
        /// Chưa biết = 0
        /// Nam = 1
        /// Nữ = 2
        /// Khác = 3
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Số căn cước công dân hoặc chứng minh thư
        /// </summary>
        [Required("Căn cước công dân - chứng minh thư nhân dân")]
        [Duplicate("Căn cước công dân - chứng minh thư nhân dân")]
        [Length("Căn cước công dân - chứng minh thư nhân dân", new int[] {11, 13})]
        public string IdentificationNumber { get; set; }
        
        /// <summary>
        /// Ngày cấp chứng minh thư nhân dân - căn cước công dân
        /// </summary>
        public DateTime? IdentificationDate { get; set; }
        
        /// <summary>
        /// Nơi cấp chứng mình thư - căn cước công dân
        /// </summary>
        public string IdentificationPlace { get; set; }

        /// <summary>
        /// Email của nhân viên
        /// </summary>
        [Required("Email nhân viên")]
        [Duplicate("Email nhân viên")]
        [TypeOf("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required("Số điện thoại nhân viên")]
        [Duplicate("Số điện thoại nhân viên")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Mã vị trí công việc
        /// </summary>
        public Guid? JobId
        {
            get; set;
        }
        
        /// <summary>
        /// Mã vị trí công việc kiểu String
        /// </summary>
        public string JobIdContains
        {
            get
            {
                return JobId.ToString();
            }

        }
        
        /// <summary>
        /// Mã phòng ban thuộc về
        /// </summary>
        public Guid? DepartmentId { get; set; }
        
        /// <summary>
        /// Mã phòng ban kiểu String
        /// </summary>
        public string DepartmentIdContains
        {
            get
            {
                return DepartmentId.ToString();
            }
        }
        
        /// <summary>
        /// Mã thuế cá nhân
        /// </summary>
        public string PersonalTaxCode { get; set; }
        
        /// <summary>
        /// Lương (VNĐ)
        /// </summary>
        public int Salary { get; set; }
        
        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        public DateTime? StartedWorkDate { get; set; }
        
        /// <summary>
        /// Tình trạng công việc
        /// </summary>
        public string JobState { get; set; }
    }
}
