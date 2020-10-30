using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nye_TaskList
{
    /// <summary>
    /// Stores all SQL Statements used in application
    /// </summary>
    public class clsSQLManager
    {
        private string sSQL;
        clsDataAccess data = new clsDataAccess();

        //SQL for LOGIN FUNCTIONALITY    
        /// <summary>
        /// Insert a user into dbUser table of database
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="phash"></param>
        /// <param name="psalt"></param>
        /// <param name="uemail"></param>
        /// <returns></returns>
        public int InsertUser(string uname, string phash, string psalt, string uemail)
        {
            sSQL = "INSERT INTO dbUser(dbUsername, dbUserEmail, dbUserPasswordHash, dbUserPasswordSalt, lastUpdatedBy, lastUpdatedDate) " +
                "VALUES ('" + uname + "', '" + uemail +"', '" + phash + "', '" + psalt + "', 'System', '" + DateTime.Now + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Validates username
        /// </summary>
        /// <param name="uname"></param>
        /// <returns>Matching usernames</returns>
        public string ValidateUsername(string uname)
        {
            sSQL = "SELECT dbUsername FROM dbUser WHERE dbUsername = '" + uname + "'";
            return data.ExecuteScalarSQL(sSQL);
        }

        /// <summary>
        /// Gets User Information
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns>List<clsAccount></returns>
        public List<clsAccount> GetUser(string username, string passwordHash)
        {
            DataSet AllUsers;
            List<clsAccount> UserList = new List<clsAccount>();
            int iRetVal = 0;
            string sSQL = "SELECT * FROM dbUser WHERE dbUsername = '" + username + "' AND dbUserPasswordHash = '" + passwordHash + "'";
            AllUsers = data.ExecuteSQLStatment(sSQL, ref iRetVal);
            for(int i = 0; i < iRetVal; i++)
            { 
                clsAccount user = new clsAccount();
                user.User_id = (int)AllUsers.Tables[0].Rows[i][0];
                user.Username = AllUsers.Tables[0].Rows[i][1].ToString();
                user.Email = AllUsers.Tables[0].Rows[i][2].ToString();
                user.PasswordHash = AllUsers.Tables[0].Rows[i][3].ToString();
                user.PasswordSalt = AllUsers.Tables[0].Rows[i][4].ToString();
                user.LastUpdateBy = AllUsers.Tables[0].Rows[i][5].ToString();
                user.LastUpdatedDate = AllUsers.Tables[0].Rows[i][6].ToString();
                UserList.Add(user);
            }
            return UserList;
        }

        //SQL for LIST FUNCTIONALITY
        /// <summary>
        /// Insert Into dbLists
        /// </summary>
        /// <param name="ListName"></param>
        /// <returns></returns>
        public int InsertUserList(string ListName)
        {
            sSQL = "INSERT INTO dbLists (dbTaskListName) VALUES ('" + ListName + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Insert into dbUserTaskLists
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="listID"></param>
        /// <returns></returns>
        public int InsertUserTaskList(int userID, int listID)
        {
            sSQL = "INSERT INTO dbUserTaskLists(dbUser_id, dbTaskList_id)" +
                "VALUES ('" + userID + "', '" + listID + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Validates ListName from dbLists
        /// </summary>
        /// <param name="ListName"></param>
        /// <returns>ListName</returns>
        public int ValidateListName(string ListName)
        {
            sSQL = "SELECT * FROM dbLists WHERE dbTaskListName = '" + ListName + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Gets ListID from dbLists
        /// </summary>
        /// <param name="ListName"></param>
        /// <returns>ListID</returns>
        public string GetListID(string ListName)
        {
            sSQL = "SELECT dbTaskList_id FROM dbLists WHERE dbTaskListName = '" + ListName + "'";
            return data.ExecuteScalarSQL(sSQL);
        }

        /// <summary>
        /// Gets List of Users Lists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<clsList></returns>
        public List<clsList> GetUserLists(string username)
        {
            DataSet AllLists;
            List<clsList> UserList = new List<clsList>();
            int iRetVal = 0;
            sSQL = "SELECT dbLists.dbTaskList_id, dbTaskListName FROM dbLists INNER JOIN dbUserTaskLists ON dbLists.dbTaskList_id = dbUserTaskLists.dbTaskList_id INNER JOIN dbUser ON dbUserTaskLists.dbUser_id = dbUser.dbUser_id WHERE dbUsername = '" + username + "'";
            AllLists = data.ExecuteSQLStatment(sSQL, ref iRetVal);
            for (int i = 0; i < iRetVal; i++)
            {
                clsList List = new clsList();
                List.ListID = (int)AllLists.Tables[0].Rows[i][0];
                List.ListName = AllLists.Tables[0].Rows[i][1].ToString();
                UserList.Add(List);
            }
            return UserList;
        }

        /// <summary>
        /// Deletes UserTaskList Item
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="listID"></param>
        /// <returns></returns>
        public int DeleteUserTaskList(int userID, int listID)
        {
            sSQL = "DELETE FROM dbUserTaskLists WHERE dbTaskList_id = '" + listID + "' AND dbUser_id = '" + userID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Deletes List item from dbLists
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public int DeleteUserList(int listID)
        {
            sSQL = "DELETE FROM dbLists WHERE dbTaskList_id = '" + listID + "'";
            return data.ExecuteNonQuery(sSQL);
        }
        
        //SQL for TASK FUNCTIONALITY
        /// <summary>
        /// Get list of Tasks in a List
        /// </summary>
        /// <param name="listID"></param>
        /// <returns>List<clsTask></returns>
        public List<clsTask> GetListTasks(int listID)
        {
            DataSet AllTasks;
            List<clsTask> ListTasks = new List<clsTask>();
            int iRetVal = 0;
            int temp;
            sSQL = "SELECT t.dbTask_id, dbTaskName, dbTaskDue, dbTaskStatus, dbDescription FROM dbTasks t INNER JOIN " +
                "dbListTask lt ON t.dbTask_id = lt.dbTask_id INNER JOIN dbLists l ON " +
                "lt.dbTaskList_id = l.dbTaskList_id WHERE l.dbTaskList_id = '" + listID + "'";
            AllTasks = data.ExecuteSQLStatment(sSQL, ref iRetVal);
            for (int i = 0; i < iRetVal; i++)
            {
                clsTask task = new clsTask();
                Int32.TryParse(AllTasks.Tables[0].Rows[i][0].ToString(), out temp); 
                task.TaskID = temp;
                task.TaskName = AllTasks.Tables[0].Rows[i][1].ToString();
                task.TaskDue = Convert.ToDateTime(AllTasks.Tables[0].Rows[i][2].ToString());
                task.TaskStatus = Convert.ToChar(AllTasks.Tables[0].Rows[i][3]);
                task.TaskDesc = AllTasks.Tables[0].Rows[i][4].ToString();
                ListTasks.Add(task);
            }
            return ListTasks;
        }

        /// <summary>
        /// Get list of Tasks in a List that have status Pending
        /// </summary>
        /// <param name="listID"></param>
        /// <returns>List<clsTask></returns>
        public List<clsTask> GetListTaskPending(int listID)
        {
            DataSet AllTasks;
            List<clsTask> ListTasks = new List<clsTask>();
            int iRetVal = 0;
            int temp;
            sSQL = "SELECT t.dbTask_id, dbTaskName, dbTaskDue, dbTaskStatus, dbDescription FROM dbTasks t INNER JOIN " +
                "dbListTask lt ON t.dbTask_id = lt.dbTask_id INNER JOIN dbLists l ON " +
                "lt.dbTaskList_id = l.dbTaskList_id WHERE l.dbTaskList_id = '" + listID + "' AND dbTaskStatus = 'P'";
            AllTasks = data.ExecuteSQLStatment(sSQL, ref iRetVal);
            for (int i = 0; i < iRetVal; i++)
            {
                clsTask task = new clsTask();
                Int32.TryParse(AllTasks.Tables[0].Rows[i][0].ToString(), out temp);
                task.TaskID = temp;
                task.TaskName = AllTasks.Tables[0].Rows[i][1].ToString();
                task.TaskDue = Convert.ToDateTime(AllTasks.Tables[0].Rows[i][2].ToString());
                task.TaskStatus = Convert.ToChar(AllTasks.Tables[0].Rows[i][3]);
                task.TaskDesc = AllTasks.Tables[0].Rows[i][4].ToString();
                ListTasks.Add(task);
            }
            return ListTasks;
        }

        /// <summary>
        /// Get list of Tasks in a List that have status Complete
        /// </summary>
        /// <param name="listID"></param>
        /// <returns>List<clsTask></returns>
        public List<clsTask> GetListTaskCompleted(int listID)
        {
            DataSet AllTasks;
            List<clsTask> ListTasks = new List<clsTask>();
            int iRetVal = 0;
            int temp;
            sSQL = "SELECT t.dbTask_id, dbTaskName, dbTaskDue, dbTaskStatus, dbDescription FROM dbTasks t INNER JOIN " +
                "dbListTask lt ON t.dbTask_id = lt.dbTask_id INNER JOIN dbLists l ON " +
                "lt.dbTaskList_id = l.dbTaskList_id WHERE l.dbTaskList_id = '" + listID + "' AND dbTaskStatus = 'C'";
            AllTasks = data.ExecuteSQLStatment(sSQL, ref iRetVal);
            for (int i = 0; i < iRetVal; i++)
            {
                clsTask task = new clsTask();
                Int32.TryParse(AllTasks.Tables[0].Rows[i][0].ToString(), out temp);
                task.TaskID = temp;
                task.TaskName = AllTasks.Tables[0].Rows[i][1].ToString();
                task.TaskDue = Convert.ToDateTime(AllTasks.Tables[0].Rows[i][2].ToString());
                task.TaskStatus = Convert.ToChar(AllTasks.Tables[0].Rows[i][3]);
                task.TaskDesc = AllTasks.Tables[0].Rows[i][4].ToString();
                ListTasks.Add(task);
            }
            return ListTasks;
        }

        /// <summary>
        /// Insert a Task into dbTasks
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Due"></param>
        /// <param name="Desc"></param>
        /// <returns></returns>
        public int InsertTask(string Name, DateTime Due, string Desc)
        {
            sSQL = "INSERT INTO dbTasks " +
                    "(dbTaskName, dbTaskDue, dbTaskStatus, dbDescription) " +
                    "VALUES " +
                    "('" + Name + "', '" + Due + "', 'P', '" + Desc + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Insert List Task into dbListTask
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="ListID"></param>
        /// <returns></returns>
        public int InsertListTask(int TaskID, int ListID)
        {
            sSQL = "INSERT INTO dbListTask" +
                "(dbTask_id, dbTaskList_id)" +
                "VALUES" +
                "('" + TaskID + "', '" + ListID + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Get TaskID from dbTasks
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>TaskID</returns>
        public string GetTaskID(string Name)
        {
            sSQL = "SELECT dbTask_id FROM dbTasks WHERE dbTaskName = '" + Name + "'";
            return data.ExecuteScalarSQL(sSQL);
        }

        /// <summary>
        /// Delete List Task Item from dbListTask
        /// </summary>
        /// <param name="listID"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int DeleteListTask(int listID, int taskID)
        {
            sSQL = "DELETE FROM dbListTask WHERE dbTaskList_id = '" + listID + "' AND dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Delete task item from dbTasks
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int DeleteTask(int taskID)
        {
            sSQL = "DELETE FROM dbTasks WHERE dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Update status of task item in dbTasks
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int UpdateStatus(int taskID)
        {
            sSQL = "UPDATE dbTasks SET dbTaskStatus = 'C' WHERE dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Update any task value in dbTasks
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskName"></param>
        /// <param name="taskDesc"></param>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        public int UpdateTask(int taskID, string taskName, string taskDesc, DateTime dueDate)
        {
            sSQL = "UPDATE dbTasks " +
                "SET " +
                "dbTaskName = '" + taskName + "', " +
                "dbDescription = '" + taskDesc + "', " +
                "dbTaskDue = '" + dueDate + "' " +
                "WHERE dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// Update Tasks where taskname remains the same
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskDesc"></param>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        public int UpdateTask(int taskID, string taskDesc, DateTime dueDate)
        {
            sSQL = "UPDATE dbTasks " +
                "SET " +
                "dbDescription = '" + taskDesc + "', " +
                "dbTaskDue = '" + dueDate + "' " +
                "WHERE dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }
    }
}
