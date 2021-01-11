using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.Common.Enum
{
    public enum DUCServiceCode
    {
        /// <summary>
        /// Lỗi dữ liệu
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,
        /// <summary>
        /// Gặp lỗi Logic
        /// </summary>
        Excaption = 500
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3,
        NoContent = 0
    }
}
