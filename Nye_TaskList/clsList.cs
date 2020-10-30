using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nye_TaskList
{
    /// <summary>
    /// Model for database dbLists
    /// </summary>
    public class clsList
    {
        private int iListID;
        private string sListName;

        public int ListID
        {
            get { return iListID; }
            set { iListID = value; }
        }

        public string ListName
        {
            get { return sListName; }
            set { sListName = value; }
        }
    }
}
