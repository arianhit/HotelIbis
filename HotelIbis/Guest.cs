using HotelIbis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibis_Hotel
{
    public class Guest
    {
        //all variables protected and not accesable
        protected int guestID;
        protected string guestFirstName;
        protected string guestLastName;
        protected DateTime guestCheckInDate;
        protected DateTime guestCheckOutDate;
        protected int guestsNumber;
        protected string guestPaymentStatus;
        protected int guestPhoneNum;
        
        //constructure
        public Guest(int guestID, string guestFirstName, string guestLastName, DateTime guestCheckInDate, DateTime guestCheckOutDate, int guestsNumber, string guestPaymentStatus, int guestPhoneNum)
        {
            this.guestID = guestID;
            this.guestFirstName = guestFirstName;
            this.guestLastName = guestLastName;
            this.guestCheckInDate = guestCheckInDate;
            this.guestCheckOutDate = guestCheckOutDate;
            if (guestsNumber > 2)
            {
                this.guestsNumber = 2;
            }
            else
            {
                this.guestsNumber = guestsNumber;
            }
            this.guestPaymentStatus = guestPaymentStatus;
            this.guestPhoneNum = guestPhoneNum;
        }

        public Guest()
        {

        }

        //get each variable if needed
        public int GetGuestId() { return guestID; }
        public string GetGuestFirstName() { return guestFirstName; }
        public string GetGuestLastName() { return guestLastName; }
        public DateTime GetGuestCheckInDate() { return guestCheckInDate; }
        public DateTime GetGuestCheckOutDate() { return guestCheckOutDate; }
        public int GetGuestsNumber() { return guestsNumber; }
        public string GetGuestPaymentStatus() { return guestPaymentStatus; }
        public int GetGuestPhoneNum() { return guestPhoneNum; }

    }
}
