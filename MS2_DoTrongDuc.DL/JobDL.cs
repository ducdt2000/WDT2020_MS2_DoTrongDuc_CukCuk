using MS2_DoTrongDuc.Common;
using MS2_DoTrongDuc.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.DL
{
    public class JobDL : IJobDL
    {

        DbConnector _dbConnector;

        public JobDL()
        {
            _dbConnector = new DbConnector();
        }

        public IEnumerable<Job> GetJobs()
        {
            return _dbConnector.GettAllData<Job>();
        }
    }
}
