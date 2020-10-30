using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Nye_TaskList.Pages
{
    public class AddTaskModel : PageModel
    {
        clsSQLManager manager = new clsSQLManager();
        public void OnGet()
        {
        }

        public IActionResult OnPostAdd()
        {
            clsStaticInfo.AddFail = false;
            string name = Request.Form["taskName"].ToString();
            DateTime due = DateTime.Now;
            string desc = Request.Form["taskDesc"].ToString();
            manager.InsertTask(name, due, desc);
            string taskID = manager.GetTaskID(name);
            int temp;
            int temp2;
            Int32.TryParse(taskID, out temp);
            Int32.TryParse(clsStaticInfo.ListID, out temp2);
            for (int i = 0; i < clsStaticInfo.ListTasks.Count; i++)
            {
                if (clsStaticInfo.ListTasks[i].TaskName == name)
                {
                    clsStaticInfo.AddFail = true;
                    return Redirect("/TaskFailed");
                }
            }
            if (Request.Form["taskDue"] == "")
            {
                due = clsStaticInfo.TaskDue;
            }
            else
            {
                Convert.ToDateTime(Request.Form["taskDue"]);
            }
            clsStaticInfo.AddFail = false;
            manager.InsertListTask(temp, temp2);
            return Redirect("/ListDetails");
        }
    }
}
