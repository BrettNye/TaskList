using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nye_TaskList
{
    /// <summary>
    /// A model for dbTasks
    /// </summary>
    public class clsTask
    {
        private int iTaskID;
        private string sTaskName;
        private DateTime dTaskDue;
        private char cTaskStatus;
        private string sTaskDesc;

        public int TaskID
        {
            get { return iTaskID; }
            set { iTaskID = value; }
        }

        public string TaskName
        {
            get { return sTaskName; }
            set { sTaskName = value; }
        }

        public DateTime TaskDue
        {
            get { return dTaskDue; }
            set { dTaskDue = value; }
        }

        public char TaskStatus
        {
            get { return cTaskStatus; }
            set { cTaskStatus = value; }
        }

        public string TaskDesc
        {
            get { return sTaskDesc; }
            set { sTaskDesc = value; }
        }
    }
}
