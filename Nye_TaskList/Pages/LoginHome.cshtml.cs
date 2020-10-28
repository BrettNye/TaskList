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
            clsStaticInfo.UserLists = manager.GetUserLists(clsUserStatus.currentUser);
        }

        public IActionResult OnPostDelete()
        {
            int iListid=0;
            Int32.TryParse(Request.Form["listID"].ToString(), out iListid);
            manager.DeleteUserTaskList(clsStaticInfo.UserID, iListid);
            clsStaticInfo.UserLists = manager.GetUserLists(clsUserStatus.currentUser);
            return Redirect("/LoginHome");
        }
    }
}
