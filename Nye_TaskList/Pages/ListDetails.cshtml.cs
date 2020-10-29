using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Nye_TaskList.Pages
{
    public class ListDetailsModel : PageModel
    {
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
            List<clsList> userLists = new List<clsList>();
            userLists = manager.GetUserLists(clsUserStatus.currentUser);
            int ListID = userLists[clsStaticInfo.ListCounter].ListID;
            clsStaticInfo.ListName = userLists[clsStaticInfo.ListCounter].ListName;
            clsStaticInfo.ListTasks = manager.GetListTasks(ListID);
        }
    }
}
