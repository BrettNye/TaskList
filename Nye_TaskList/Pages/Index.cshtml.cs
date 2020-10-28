using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Nye_TaskList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (clsUserStatus.loggedIn == true)
            {
                clsUserStatus.loggedIn = true;
            }
            else
            {
                clsUserStatus.loggedIn = false;
                clsUserStatus.registered = false;
                clsUserStatus.currentUser = null;
            }
        }
    }
}
