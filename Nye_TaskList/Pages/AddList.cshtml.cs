using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Nye_TaskList.Pages
{
    public class AddListModel : PageModel
    {
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
        }

        public IActionResult OnPostAdd()
        {
            string listName = Request.Form["listname"];
            for (int i = 0; i < clsStaticInfo.UserLists.Count; i++)
            {
                if (clsStaticInfo.UserLists[i].ListName == listName)
                {
                    return Redirect("/LoginFailed");
                }
            }
            manager.InsertUserList(listName);
            string slistID = manager.GetListID(listName);
            int ilistID;
            Int32.TryParse(slistID, out ilistID);
            manager.InsertUserTaskList(clsStaticInfo.UserID, ilistID);
            return Redirect("/LoginHome");
        }
    }
}
