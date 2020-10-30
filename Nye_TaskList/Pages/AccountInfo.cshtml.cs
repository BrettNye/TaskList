using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Nye_TaskList.Pages
{
    public class AccountInfoModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostDelete()
        {
            //Reset Static Variables
            clsUserStatus.currentUser = "";
            clsStaticInfo.Email = "";
            clsUserStatus.loggedIn = false; //Log User Out
            return Redirect("/Index");      //Redirect to homepage to login/register

        }
    }
}
