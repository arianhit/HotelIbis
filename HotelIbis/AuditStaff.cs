using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelIbis
{
    [Serializable]
    public class AuditStaff
    {
        private string operation;

        private string currentStaffName;

        private DateTime dateAndTime;

        public AuditStaff(string operation, string currentStaffName, DateTime dateAndTime)
        {
            this.operation = operation;
            this.currentStaffName = currentStaffName;
            this.dateAndTime = dateAndTime;
        }

        public string GetOperation() { return operation; }
        public string GetCurrentStaffName() { return currentStaffName; }

        public DateTime GetDateAndTime() { return dateAndTime; }
    }
}
