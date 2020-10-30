using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nye_TaskList
{
    /// <summary>
    /// Holds currentUser name and status
    /// </summary>
    public class clsUserStatus
    {
        public static bool loggedIn { get; set; }
        public static bool registered { get; set; }
        public static string currentUser { get; set; }
    }
}
