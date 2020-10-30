using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Nye_TaskList.Pages
{
    public class EditTaskModel : PageModel
    {
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
            clsStaticInfo.TaskID = clsStaticInfo.ListTasks[clsStaticInfo.ListCounter].TaskID;
            clsStaticInfo.TaskName = clsStaticInfo.ListTasks[clsStaticInfo.ListCounter].TaskName;
            clsStaticInfo.TaskDesc = clsStaticInfo.ListTasks[clsStaticInfo.ListCounter].TaskDesc;
            clsStaticInfo.TaskDue = clsStaticInfo.ListTasks[clsStaticInfo.ListCounter].TaskDue;
        }

        public IActionResult OnPostEdit()
        {
            int id;
            Int32.TryParse(Request.Form["id"], out id);
            string name = Request.Form["taskname"].ToString();
            string desc = Request.Form["taskDesc"].ToString();
            DateTime due = DateTime.Now;
            if (name == "")
            {
                name = clsStaticInfo.TaskName;
            }
            if(desc == "")
            {
                desc = clsStaticInfo.TaskDesc;
            }
            if(Request.Form["taskDue"] == "")
            {
                due = clsStaticInfo.TaskDue;
            } else
            {
                Convert.ToDateTime(Request.Form["taskDue"]);
            }
            for (int i = 0; i < clsStaticInfo.ListTasks.Count; i++)
            {
                if (clsStaticInfo.ListTasks[i].TaskName == name)
                {
                    return Redirect("/TaskFailed");
                }
            }
            manager.UpdateTask(id, name, desc, due);
            return Redirect("/ListDetails");
        }


    }
}
