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

        /// <summary>
        /// Holds Current UserID
        /// </summary>
        public static int UserID { get; set; }

        /// <summary>
        /// Holds list of User Lists to pass and update
        /// values between pages
        /// </summary>
        public static List<clsList> UserLists = new List<clsList>();

        /// <summary>
        /// Used to pass ListID between pages and display in html
        /// </summary>
        public static string ListID { get; set; }
        /// <summary>
        /// Used to pass ListName between pages and display in html
        /// </summary>
        public static string ListName { get; set; }

        /// <summary>
        /// Used to hold Count of selected item from a list of lists or tasks
        /// </summary>
        public static int ListCounter { get; set; }

        /// <summary>
        /// Holds list of tasks to pass and update
        /// values between pages
        /// </summary>
        public static List<clsTask> ListTasks = new List<clsTask>();

        /// <summary>
        /// Used to pass TaskID between pages and display in html
        /// </summary>
        public static int TaskID;

        /// <summary>
        /// Used to pass TaskName between pages and display in html
        /// </summary>
        public static string TaskName;

        /// <summary>
        /// Used to pass TaskDue between pages and display in html
        /// </summary>
        public static DateTime TaskDue;

        /// <summary>
        /// Used to pass sTaskDue between pages and display in html
        /// </summary>
        public static string sTaskDue;

        /// <summary>
        /// Used to pass TaskStatus between pages and display in html
        /// </summary>
        public static char TaskStatus;

        /// <summary>
        /// Used to pass TaskDescription between pages and display in html
        /// </summary>
        public static string TaskDesc;


        /// <summary>
        /// Used to track what filter a user has selected
        /// </summary>
        public static int Filter = 1;

        /// <summary>
        /// Determines whether Adding a task failed or succeeded
        /// </summary>
        public static bool AddFail = false;
    }
}
