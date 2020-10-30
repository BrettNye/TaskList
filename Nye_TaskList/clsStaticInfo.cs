using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Nye_TaskList
{
    public class clsStaticInfo
    {
        /// <summary>
        /// Holds logged in user email
        /// </summary>
        public static string Email { get; set; }

        public static int UserID { get; set; }

        /// <summary>
        /// Used to display User Lists
        /// </summary>
        public static List<clsList> UserLists = new List<clsList>();
        public static string ListID { get; set; }
        public static string ListName { get; set; }

        public static bool ListDetails { get; set; }

        public static int ListCounter { get; set; }

        public static List<clsTask> ListTasks = new List<clsTask>();

        public static int TaskID;
        public static string TaskName;
        public static DateTime TaskDue;
        public static string sTaskDue;
        public static char TaskStatus;
        public static string TaskDesc;

        public static int Filter = 1;

        public static bool AddFail = false;
    }
}
