using HotelIbis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ibis_Hotel
{
    [Serializable]
    public class Staff
    {

        //all variables protected and not accesable 
        protected string staffFirstName;
        protected string staffLastName;
        protected string staffRole;
        protected string staffUserName;
        protected string staffPassword;

        //contsructure
        public Staff(string staffFirstName, string staffLastName, string staffRole, string staffUserName, string staffPassword)
        {
            this.staffFirstName = staffFirstName;
            this.staffLastName = staffLastName;
            this.staffRole = staffRole;
            this.staffUserName = staffUserName;
            this.staffPassword = staffPassword;

        }
        public Staff()
        {

        }
        //get each variable if needed
        public string GetStaffFirstName() { return staffFirstName; }
        public string GetStaffLastName() { return staffLastName; }
        public string GetStaffRole() { return staffRole; }
        public string GetStaffUserName() { return staffUserName; }
        public string GetStaffPassword() { return staffPassword; }


    }
}

