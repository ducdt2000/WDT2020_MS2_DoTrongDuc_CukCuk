using MS2_DoTrongDuc.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.Common
{
    public class ServiceResult
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Thông báo
        /// </summary>
        public List<string> Messenger { get; set; } = new List<string>();
        /// <summary>
        /// Mã kết quả kiểu Duc
        /// </summary>
        public DUCServiceCode DUCCode { get; set; }
    }
}
