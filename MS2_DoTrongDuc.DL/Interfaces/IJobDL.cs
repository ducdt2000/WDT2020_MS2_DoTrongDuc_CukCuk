using MS2_DoTrongDuc.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.DL.Interfaces
{
    public interface IJobDL
    {
        IEnumerable<Job> GetJobs();
    }
}
