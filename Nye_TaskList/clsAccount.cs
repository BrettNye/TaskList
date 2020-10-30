using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nye_TaskList
{
    /// <summary>
    /// Model to database dbUser Table
    /// </summary>
    public class clsAccount
    {

        private int iUser_id;
        private string sUsername;
        private string sEmail;
        private string sPasswordHash;
        private string sPasswordSalt;
        private string sLastUpdatedBy;
        private string dLastUpdatedDate;

        public int User_id
        {
            get { return iUser_id; }
            set { iUser_id = value; }
        }

        public string Username
        {
            get { return sUsername; }
            set { sUsername = value; }
        }

        public string Email
        {
            get { return sEmail; }
            set { sEmail = value; }
        }

        public string PasswordHash
        {
            get { return sPasswordHash; }
            set { sPasswordHash = value; }
        }

        public string PasswordSalt
        {
            get { return sPasswordSalt; }
            set { sPasswordSalt = value; }
        }

        public string LastUpdateBy
        {
            get { return sLastUpdatedBy; }
            set { sLastUpdatedBy = value; }
        }

        public string LastUpdatedDate
        {
            get { return dLastUpdatedDate; }
            set { dLastUpdatedDate = value; }
        }
    }
}
