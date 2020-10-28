using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
