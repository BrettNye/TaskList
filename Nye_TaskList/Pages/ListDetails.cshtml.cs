using System;
using System.Collections.Generic;
using System.Globalization;
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
            //If filter value is 1 it will return all tasks in list
            if (clsStaticInfo.Filter == 1)
            {
                int ListID;
                Int32.TryParse(clsStaticInfo.ListID, out ListID);
                clsStaticInfo.ListTasks = manager.GetListTasks(ListID);
            }
        }

        /// <summary>
        /// OnPost will request task id, retrieve list id and delete task and listtask item
        /// And update list of listtasks
        /// </summary>
        /// <returns>/ListDetailsPG</returns>
        public IActionResult OnPostDelete()
        {
            int iTaskID = 0;
            int iListID = 0;
            Int32.TryParse(Request.Form["ID"].ToString(), out iTaskID);
            Int32.TryParse(clsStaticInfo.ListID.ToString(), out iListID);
            manager.DeleteListTask(iListID, iTaskID);
            manager.DeleteTask(iTaskID);
            clsStaticInfo.ListTasks = manager.GetListTasks(iListID);
            return Redirect("/ListDetails");
        }

        /// <summary>
        /// Updates status to complete
        /// </summary>
        /// <returns>/ListDetailsPG</returns>
        public IActionResult OnPostComplete()
        {
            manager.UpdateStatus(clsStaticInfo.TaskID);
            return Redirect("/ListDetails");
        }

        /// <summary>
        /// Get input from form dropdown to determine value to filter for
        /// All 1,
        /// Pending 2,
        /// Complete 3
        /// </summary>
        /// <returns>/ListDetailsPG</returns>
        public IActionResult OnPostFilter()
        {
            string Filter = Request.Form["Options"].ToString();
            int iListID = 0;
            Int32.TryParse(clsStaticInfo.ListID.ToString(), out iListID);
            if (Filter == "All Tasks")
            {
                clsStaticInfo.Filter = 1;
                clsStaticInfo.ListTasks = manager.GetListTasks(iListID);
                return Redirect("/ListDetails");
            }
            else if (Filter == "Pending Tasks")
            {
                clsStaticInfo.Filter = 2;
                clsStaticInfo.ListTasks = manager.GetListTaskPending(iListID);
                return Redirect("/ListDetails");
            } else if(Filter == "Completed Tasks")
            {
                clsStaticInfo.Filter = 3;
                clsStaticInfo.ListTasks = manager.GetListTaskCompleted(iListID);
                return Redirect("/ListDetails");
            }
            return Redirect("/ListDetails");
        }

        /// <summary>
        /// Grab list counter and redirect
        /// </summary>
        /// <returns>/EditTaskPG</returns>
        public IActionResult OnPostEdit()
        {
            int temp;
            Int32.TryParse(Request.Form["counter"], out temp);
            clsStaticInfo.ListCounter = temp-1;
            return Redirect("/EditTask");
        }
    }
}
