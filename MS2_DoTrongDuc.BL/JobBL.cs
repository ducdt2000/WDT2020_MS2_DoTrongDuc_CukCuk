using MS2_DoTrongDuc.BL.Interfaces;
using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.Common.Enum;
using MS2_DoTrongDuc.Common.Properties;
using MS2_DoTrongDuc.DL.Interfaces;
using System.Collections.Generic;

namespace MS2_DoTrongDuc.BL
{
    public class JobBL : IJobBL
    {
        IJobDL _jobDL;
        ServiceResult _serviceResult;
        public JobBL(IJobDL jobDL)
        {
            _jobDL = jobDL;
            _serviceResult = new ServiceResult();
            _serviceResult.DUCCode = DUCServiceCode.Success;
        }

        ServiceResult IJobBL.GetJobs()
        {
            _serviceResult.Data = _jobDL.GetJobs();
            _serviceResult.Messenger = new List<string> { Resources.Msg_Success };
            return _serviceResult;
        }
    }
}
