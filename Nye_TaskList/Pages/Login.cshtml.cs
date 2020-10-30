using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task_List;

namespace Nye_TaskList.Pages
{
    public class LoginModel : PageModel
    {
        clsPasswordManager passManager = new clsPasswordManager();
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
        }

        /// <summary>
        /// Request form input, match against database values, 
        /// if match, log user in
        /// if fail, notify user and allow another try
        /// </summary>
        /// <returns>/IndexPG on success, /LoginFailedPG on fail</returns>
        public IActionResult OnPostLogin()
        {
            string username = Request.Form["username"].ToString();
            string password = passManager.ProtectPass(Request.Form["password"].ToString());
            List<clsAccount> users = manager.GetUser(username, password);
            for(int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == username)
                {
                    if(users[i].PasswordHash == password)
                    {
                        clsUserStatus.loggedIn = true;
                        clsUserStatus.currentUser = username;
                        clsStaticInfo.Email = users[i].Email;
                        clsStaticInfo.UserID = users[i].User_id;
                        return Redirect("/Index");
                    }
                }
            }
            return Redirect("LoginFailed");
        }
    }
}
