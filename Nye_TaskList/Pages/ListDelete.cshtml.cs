using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Nye_TaskList.Pages
{
    public class ListDeleteModel : PageModel
    {
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
        }

        public IActionResult OnPostDelete()
        {
            return Redirect("/ListDelete");
        }

        public IActionResult OnPostKeep()
        {
            return Redirect("/LoginHome");
        }
    }
}
