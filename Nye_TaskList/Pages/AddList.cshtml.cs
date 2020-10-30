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
        //Initialize SQL manager class
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
        }

        public IActionResult OnPostAdd()
        {
            string listName = Request.Form["listname"]; //Request listName from form
            //Loop through the users list and verify new listname is unique
            for (int i = 0; i < clsStaticInfo.UserLists.Count; i++)
            {
                if (clsStaticInfo.UserLists[i].ListName == listName)
                {
                    return Redirect("/LoginFailed"); //Redirect to Login Failed page and tell user List name already taken
                }
            }
            manager.InsertUserList(listName);//Use InsertUserList method to add a list to dbLists
            string slistID = manager.GetListID(listName);//Use GetListID to retrieve listID using new listname as string
            int ilistID;
            Int32.TryParse(slistID, out ilistID); //Convert listID to int
            manager.InsertUserTaskList(clsStaticInfo.UserID, ilistID); //Insert new UserTaskList item using current userID and iListID
            return Redirect("/LoginHome");
        }
    }
}
