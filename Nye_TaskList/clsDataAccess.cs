using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Nye_TaskList
{
    public class clsDataAccess
    {
        /// <summary>
        /// Stores Connection string to database
        /// </summary>
        private string sConnectionString;
        
        /// <summary>
        /// Build Connection String
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder("Data Source = task-list-project.czca6afzbdvq.us-east-2.rds.amazonaws.com,1433; Initial Catalog = dbTaskList; Integrated Security = False; Persist Security Info = True; User ID = admin; Password = $Steelisstrong32;");
            return builder.ToString();
        }

        /// <summary>
        /// Executes A Non Query
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns>INumRows</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                sConnectionString = GetConnectionString();
                int iNumRows;
                using(SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = sConnectionString;
                    SqlDataAdapter adapt = new SqlDataAdapter();
                    conn.Open();
                    adapt.SelectCommand = new SqlCommand(sSQL, conn);
                    adapt.SelectCommand.CommandTimeout = 0;

                    iNumRows = adapt.SelectCommand.ExecuteNonQuery();
                }
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Execute a scalar statement
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns>obj.tostring</returns>
        public string ExecuteScalarSQL(string sSQL)
        {
            try { 
                object obj;
                sConnectionString = GetConnectionString();
                using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = sConnectionString;
                        SqlDataAdapter adapt = new SqlDataAdapter();
                        conn.Open();
                        adapt.SelectCommand = new SqlCommand(sSQL, conn);
                        adapt.SelectCommand.CommandTimeout = 0;
                        obj = adapt.SelectCommand.ExecuteScalar();
                    }

                    //See if the object is null
                    if (obj == null)
                    {
                        //Return a blank
                        return "";
                    }
                    else
                    {
                        //Return the value
                        return obj.ToString();
                    }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Executes SQL Statements
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="iRetVal"></param>
        /// <returns>Dataset ds</returns>
        public DataSet ExecuteSQLStatment(string sSQL, ref int iRetVal)
        {
            try
            {
                sConnectionString = GetConnectionString();
                //Create a new DataSet
                DataSet ds = new DataSet();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = sConnectionString;
                    SqlDataAdapter adapt = new SqlDataAdapter();
                    conn.Open();
                    adapt.SelectCommand = new SqlCommand(sSQL, conn);
                    adapt.SelectCommand.CommandTimeout = 0;
                    adapt.Fill(ds);
                }
                iRetVal = ds.Tables[0].Rows.Count;
                return ds;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
