using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Nye_TaskList.Pages
{
    public class LoginHomeModel : PageModel
    {
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
            //Get list of UserLists
            clsStaticInfo.UserLists = manager.GetUserLists(clsUserStatus.currentUser);
        }

        /// <summary>
        /// Delete List Item and UserTaskList Item from database
        /// Update UserLists
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostDelete()
        {
            int iListid=0;
            Int32.TryParse(Request.Form["listID"].ToString(), out iListid);
            manager.DeleteUserTaskList(clsStaticInfo.UserID, iListid);
            manager.DeleteUserList(iListid);
            clsStaticInfo.UserLists = manager.GetUserLists(clsUserStatus.currentUser);
            return Redirect("/LoginHome");
        }

        /// <summary>
        /// Get counter for selected item
        /// </summary>
        /// <returns>/ListDetailsPG</returns>
        public IActionResult OnPostDetails()
        {
            int temp;
            Int32.TryParse(Request.Form["ListnumCount"], out temp);
            clsStaticInfo.ListCounter = temp-1;
            return Redirect("/ListDetails");
        }
    }
}
