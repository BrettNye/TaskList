﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            sSQL = "DELETE FROM dbUserTaskLists WHERE dbTaskList_id = '" + listID + "' AND dbUser_id = '" + userID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        public int DeleteUserList(int listID)
        {
            sSQL = "DELETE FROM dbLists WHERE dbTaskList_id = '" + listID + "'";
            return data.ExecuteNonQuery(sSQL);
        }
        
        //SQL for TASK FUNCTIONALITY
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

        public int InsertTask(string Name, DateTime Due, string Desc)
        {
            sSQL = "INSERT INTO dbTasks " +
                    "(dbTaskName, dbTaskDue, dbTaskStatus, dbDescription) " +
                    "VALUES " +
                    "('" + Name + "', '" + Due + "', 'P', '" + Desc + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        public int InsertListTask(int TaskID, int ListID)
        {
            sSQL = "INSERT INTO dbListTask" +
                "(dbTask_id, dbTaskList_id)" +
                "VALUES" +
                "('" + TaskID + "', '" + ListID + "')";
            return data.ExecuteNonQuery(sSQL);
        }

        public string GetTaskID(string Name)
        {
            sSQL = "SELECT dbTask_id FROM dbTasks WHERE dbTaskName = '" + Name + "'";
            return data.ExecuteScalarSQL(sSQL);
        }

        public int DeleteListTask(int listID, int taskID)
        {
            sSQL = "DELETE FROM dbListTask WHERE dbTaskList_id = '" + listID + "' AND dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        public int DeleteTask(int taskID)
        {
            sSQL = "DELETE FROM dbTasks WHERE dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        public int UpdateStatus(int taskID)
        {
            sSQL = "UPDATE dbTasks SET dbTaskStatus = 'C' WHERE dbTask_id = '" + taskID + "'";
            return data.ExecuteNonQuery(sSQL);
        }

        public int UpdateTask(int taskID, string taskName, string taskDesc, DateTime dueDate)
        {
            sSQL = "UPDATE dbTasks " +
                "SET " +
                "dbTaskName = '" + taskName + "', " +
                "dbDescription = '" + taskDesc + "', " +
                "dbTaskDue = '" + dueDate + "'";
            return data.ExecuteNonQuery(sSQL);
        }
    }
}
