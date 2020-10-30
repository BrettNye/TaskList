using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using Task_List;

namespace Nye_TaskList.Pages
{
    public class RegisterModel : PageModel
    {
        clsSQLManager manager = new clsSQLManager();
        clsPasswordManager passManager = new clsPasswordManager();
        public void OnGet()
        {
        }

        /// <summary>
        /// Get user input, generate salt, hash salt and password,
        /// Verify Username is not already taken
        /// </summary>
        /// <returns>on success /IndexPG, on fail /AlreadyInUsePG</returns>
        public IActionResult OnPostRegister()
        {
            string username = Request.Form["username"].ToString();
            string salt = BitConverter.ToString(passManager.GenerateSalt()).Replace("-", "");
            string HashedPassword = passManager.ProtectPass(Request.Form["password"].ToString());
            string Email = Request.Form["email"].ToString();

            string dbUsername = manager.ValidateUsername(username);
            if (username != dbUsername)
            {
                int count = manager.InsertUser(username, HashedPassword, salt, Email);
                if (count == 1)
                {
                    clsUserStatus.registered = true;
                }
                return Redirect("/Index");
            }
            else
            {
                
                return Redirect("/AlreadyInUse");
            }
        }
    }
}
