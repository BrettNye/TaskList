using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nye_TaskList
{
    public class clsSQLManager
    {
        private string sSQL;
        clsDataAccess data = new clsDataAccess();

        ///SQL for LOGIN FUNCTIONALITY
        public int InsertUser(string uname, string phash, string psalt, string uemail)
        {
            sSQL = "INSERT INTO dbUser(dbUsername, dbUserEmail, dbUserPasswordHash, dbUserPasswordSalt, lastUpdatedBy, lastUpdatedDate) " +
                "VALUES ('" + uname + "', '" + uemail +"', '" + phash + "', '" + psalt + "', 'System', '" + DateTime.Now + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        public string ValidateUsername(string uname)
        {
            sSQL = "SELECT dbUsername FROM dbUser WHERE dbUsername = '" + uname + "'";
            return data.ExecuteScalarSQL(sSQL);
        }

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
        public int InsertUserList(string ListName)
        {
            sSQL = "INSERT INTO dbLists (dbTaskListName) VALUES ('" + ListName + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        public int InsertUserTaskList(int userID, int listID)
        {
            sSQL = "INSERT INTO dbUserTaskLists(dbUser_id, dbTaskList_id)" +
                "VALUES ('" + userID + "', '" + listID + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        public int ValidateListName(string ListName)
        {
            sSQL = "SELECT * FROM dbLists WHERE dbTaskListName = '" + ListName + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        public string GetListID(string ListName)
        {
            sSQL = "SELECT dbTaskList_id FROM dbLists WHERE dbTaskListName = '" + ListName + "'";
            return data.ExecuteScalarSQL(sSQL);
        }

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

        public int DeleteUserTaskList(int userID, int listID)
        {
            sSQL = "DELETE FROM dbUserTaskLists WHERE dbUser_id = '" + userID + "' AND dbTaskList_id = '" + listID + "'";
            return data.ExecuteNonQuery(sSQL);
        }
        
        //SQL for TASK FUNCTIONALITY
        public List<clsTask> GetListTasks(int listID)
        {
            DataSet AllTasks;
            List<clsTask> ListTasks = new List<clsTask>();
            int iRetVal = 0;
            sSQL = "SELECT dbTaskName, dbTaskDue, dbTaskStatus FROM dbTasks t INNER JOIN " +
                "dbListTask lt ON t.dbTask_id = lt.dbTask_id INNER JOIN dbLists l ON " +
                "lt.dbTaskList_id = l.dbTaskList_id WHERE l.dbTaskList_id = '" + listID + "'";
            AllTasks = data.ExecuteSQLStatment(sSQL, ref iRetVal);
            for (int i = 0; i < iRetVal; i++)
            {
                clsTask task = new clsTask();
                task.TaskID = (int)AllTasks.Tables[0].Rows[i][0];
                task.TaskName = AllTasks.Tables[0].Rows[i][1].ToString();
                task.TaskDue = Convert.ToDateTime(AllTasks.Tables[0].Rows[i][2].ToString());
                task.TaskStatus = Convert.ToChar(AllTasks.Tables[0].Rows[i][3]);
            }
            return ListTasks;
        }
    }
}
