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
        private int guestID;
        private string guestFirstName;
        private string guestLastName;
        private DateTime guestCheckInDate;
        private DateTime guestCheckOutDate;
        private int guestsNumber;
        private string guestPaymentStatus;
        private int guestPhoneNum;
        

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
        

        public int GetGuestId() { return guestID; }
        public string GetGuestFirstName() { return guestFirstName; }
        public string GetGuestLastName() { return guestLastName; }
        public DateTime GetGuestCheckInDate() { return guestCheckInDate; }
        public DateTime GetGuestCheckOutDate() { return guestCheckOutDate; }
        public int GetGuestsNumber() { return guestsNumber; }
        public string GetGuestPaymentStatus() { return guestPaymentStatus; }
        public int GetGuestPhoneNum() { return guestPhoneNum; }

        public string GetGuestInString()
        {
            string guestInString;

            guestInString = "" + "\t" +this.guestID.ToString() + "\t" + this.guestFirstName + "\t" + this.guestLastName + "\t" + this.guestCheckInDate.ToString() + "\t" + guestCheckOutDate.ToString()
                + "\t" + this.guestsNumber.ToString() + "\t" + this.guestPaymentStatus + "\t" + guestPhoneNum.ToString()+ "" ;


            return guestInString ;
        }
        
    }
}
