using Ibis_Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelIbis
{
    [Serializable]
    public class AuditStaff : Staff
    {
        //all variables protected and not accesable
        private string operation;

        private string currentStaffName;

        private DateTime dateAndTime;

        //constructure
        public AuditStaff(string operation, string currentStaffName, DateTime dateAndTime)
        {
            this.operation = operation;
            this.currentStaffName = currentStaffName;
            this.dateAndTime = dateAndTime;
        }
        //get each variable if needed
        public string GetOperation() { return operation; }
        public string GetCurrentStaffName() { return currentStaffName; }

        public DateTime GetDateAndTime() { return dateAndTime; }
    }
}
