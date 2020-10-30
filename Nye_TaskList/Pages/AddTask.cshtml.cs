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
            clsStaticInfo.AddFail = false;                          //Reset AddFail variable
            string name = Request.Form["taskName"].ToString();      //Request taskname from form
            DateTime due = DateTime.Now;                            //Set due variable to default;
            string desc = Request.Form["taskDesc"].ToString();      //Request taskdescription from form
            manager.InsertTask(name, due, desc);                    //Insert a task into dbTasks
            string taskID = manager.GetTaskID(name);                //Retrieve taskID using taskName
            int temp;   //Stores taskID temporarily
            int temp2;  //Stores ListID temporarily
            Int32.TryParse(taskID, out temp);
            Int32.TryParse(clsStaticInfo.ListID, out temp2);
            //Loop list to ensure task name is unique
            for (int i = 0; i < clsStaticInfo.ListTasks.Count; i++)
            {
                if (clsStaticInfo.ListTasks[i].TaskName == name)
                {
                    clsStaticInfo.AddFail = true;
                    return Redirect("/TaskFailed");
                }
            }
            //If form not included set to clsStaticInfo.TaskDue
            if (Request.Form["taskDue"] == "")
            {
                due = clsStaticInfo.TaskDue;
            }
            //Else request DateTime from form
            else
            { 
                due = Convert.ToDateTime(Request.Form["taskDue"]);
            }
            clsStaticInfo.AddFail = false; //Set if Add did not fail
            manager.InsertListTask(temp, temp2); //Insert a ListTask
            return Redirect("/ListDetails");
        }
    }
}
