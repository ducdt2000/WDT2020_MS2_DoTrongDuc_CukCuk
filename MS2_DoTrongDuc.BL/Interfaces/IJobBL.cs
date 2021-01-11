using Microsoft.AspNetCore.Mvc;
using MS2_DoTrongDuc.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.BL.Interfaces
{
    public interface IJobBL
    {
        ServiceResult GetJobs();
    }
}
