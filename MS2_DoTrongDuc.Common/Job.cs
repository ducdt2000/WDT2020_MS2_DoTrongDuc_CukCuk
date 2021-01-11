using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2_DoTrongDuc.Common
{
    public class Job
    {
        /// <summary>
        /// Mã công việc kiểu Guid
        /// </summary>
        public Guid JobId { get; set; }
        /// <summary>
        /// Mã Guid vị trí công việc dạng String
        /// </summary>
        public string JobIdConstraint
        {
            get
            {
                return JobId.ToString();
            }
        }
        /// <summary>
        /// Tên vị trí công việc
        /// </summary>
        public string JobName { get; set; }
    }
}
