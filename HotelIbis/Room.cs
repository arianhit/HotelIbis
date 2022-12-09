﻿using HotelIbis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibis_Hotel
{
    public class Room
    {
        private int roomNumber;
        private string roomStatus;
        private Guest guest;
        OutputOperators o = new OutputOperators();
        DateTime now = DateTime.Now.Date;

        public Room(int roomNumber, string roomStatus, Guest guest)
        {
            this.roomNumber = roomNumber;
            this.roomStatus = roomStatus;
            this.guest = guest;
        }
        public Room()
        {

        }

        public int GetRoomNumber() { return roomNumber; }
        public string GetRoomStatus() { return roomStatus; }

        public Guest GetGuest() { return guest; }

    }
}
