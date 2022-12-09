using Ibis_Hotel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelIbis
{
    public class Hotel
    {
        OutputOperators o = new OutputOperators();
        DateTime now = DateTime.Now.Date;
        Guest guest = new Guest();
        public void CheckIn()
        {
            int userGuestNumb = 0;
            int userRoomNumb = 0;




            Room newRoom = new Room();
            bool checkGuestId = false;
            List<string> newLines = new List<string>();
            Console.WriteLine("\t\t***********checking In****************");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            GetArrivalList();
            string[] allGuestLines = { "" };
            string[] allRoomsLines = { "" };

            try
            {

                while (true)
                {
                    Console.WriteLine("Please Enter guest ID that you want to check in:");

                    userGuestNumb = Convert.ToInt32(Console.ReadLine());

                    string filePath = Path.GetFullPath("GuestDB.csv");

                    allGuestLines = File.ReadAllLines(filePath);

                    foreach (string line in allGuestLines)
                    {
                        var values = line.Split(",");

                        if (Convert.ToInt32(values[0]) == userGuestNumb)
                        {
                            checkGuestId = true;

                        }

                    }
                    if (userGuestNumb != null && userGuestNumb.ToString().Length == 4 && checkGuestId)
                    {
                        break;
                    }
                    if (!checkGuestId)
                    {
                        o.outPutError("Guest Not founded or it is not in arrival list to check in");

                    }
                    else
                    {
                        o.outPutError("Please Enter guest ID in correct format (it should like \"1234\")");
                    }
                }
                GetRooms();
                while (true)
                {
                    Console.WriteLine("Please Enter room number that you want to check in the guest :");
                    userRoomNumb = Convert.ToInt32(Console.ReadLine());
                    if (userRoomNumb != null && userRoomNumb.ToString().Length == 3)
                    {
                        break;
                    }
                    else
                    {
                        o.outPutError("Please Enter room number in correct format (it should like \"123\")");

                    }
                }

            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
            }
            try
            {

                foreach (string line in allGuestLines)
                {
                    var values = line.Split(",");

                    if (Convert.ToInt32(values[0]) == userGuestNumb)
                    {
                        checkGuestId = true;
                        guest = new Guest(Convert.ToInt32(values[0]), values[1].ToString(), values[2].ToString(), Convert.ToDateTime(values[3]), Convert.ToDateTime(values[4]), Convert.ToInt32(values[5]), values[6].ToString(), Convert.ToInt32(values[7]));



                        newRoom = new Room(userRoomNumb, "Occupied", guest);
                    }

                }

            }

            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
            }

            try
            {

                string filePathOfRooms = Path.GetFullPath("RoomsDB.csv");
                string guestInText = newRoom.GetGuest().GetGuestInString();


                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePathOfRooms, true))
                    {

                        file.WriteLine(newRoom.GetRoomNumber() + "," + newRoom.GetRoomStatus() + "," + newRoom.GetGuest().GetGuestId() + "," + newRoom.GetGuest().GetGuestFirstName() + "," + newRoom.GetGuest().GetGuestLastName() + "," + newRoom.GetGuest().GetGuestCheckInDate() + "," + newRoom.GetGuest().GetGuestCheckOutDate()
                                + "," + newRoom.GetGuest().GetGuestsNumber() + "," + newRoom.GetGuest().GetGuestPaymentStatus() + "," + newRoom.GetGuest().GetGuestPhoneNum());

                        file.Close();
                    }
                    Console.WriteLine(newRoom.GetGuest().GetGuestFirstName() + " " + newRoom.GetGuest().GetGuestLastName() + " Checked in ");
                }
                catch (Exception ex)
                {
                    o.outPutError("Something went wrong please try again");
                }


            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
            }

        }
        public void CheckOut()
        {
            int userGuestNumb = 0;
            int userRoomNumb = 0;




            Room newRoom = new Room();
            HashSet<string> newLines = new HashSet<string>();
            Console.WriteLine("\t\t***********checking Out****************");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string[] allLines = GetRooms();
            try
            {


                while (true)
                {
                    Console.WriteLine("Please Enter room number that you want to check out the guest :");
                    userRoomNumb = Convert.ToInt32(Console.ReadLine());
                    List<int> roomNumbers = new List<int>();

                    foreach (string line in allLines)
                    {
                        var values = line.Split(',');

                        roomNumbers.Add(int.Parse(values[0]));
                    }

                    if (userRoomNumb != null && userRoomNumb.ToString().Length == 3 && roomNumbers.Contains(userRoomNumb))
                    {
                        break;
                    }
                    if (roomNumbers.Contains(userRoomNumb))
                    {
                        o.outPutError("This room is not occupied");
                    }
                    else
                    {
                        o.outPutError("Room not found as an occupied room");

                    }
                }

            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
            }
            try
            {

                string filePathOfRooms = Path.GetFullPath("RoomsDB.csv");


                foreach (string line in allLines)
                {
                    newLines.Add(line);
                }

                File.WriteAllText(filePathOfRooms, "");

                foreach (string line in newLines)
                {
                    var values = line.Split(',');
                    if (values[0] == userRoomNumb.ToString())
                    {


                        newLines.Remove(line);
                        Console.WriteLine(values[3] + " " + values[4] + " Checkeded out ");
                    }
                }



                try
                {

                    foreach (string line in newLines)
                    {
                        var values = line.Split(',');
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePathOfRooms, false))
                        {

                            file.WriteLine(values[0]+","+ values[1]+"," + values[2]+"," + values[3]+"," + values[4]+"," + values[5]+"," + values[6]+"," + values[7]+"," + values[8] + "," + values[9]);

                            file.Close();
                        }

                    }





                }
                catch (Exception ex)
                {
                    o.outPutError(ex.Message);
                }


            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
            }

        }
        public string[] GetRooms()
        {
            try
            {
                string filePath = Path.GetFullPath("RoomsDB.csv");
                string[] allLines = File.ReadAllLines(filePath);
                List<string> allRoomLines = new List<string>();
                DateTime now = DateTime.Now.Date;

                Console.WriteLine("\t\t***********Rooms****************");
                Console.WriteLine("RoomID\t||" + "Status\t||" + "Guest\t||");

                foreach (var line in allLines)
                {
                    allRoomLines.Add(line);
                }

                foreach (string room in allRoomLines)
                {
                    Console.WriteLine("");
                    var values = room.Split(",");
                    foreach (var value in values)
                    {
                        Console.Write(value.ToString() + "\t||");
                    }
                    Console.WriteLine("");
                }
                if(allRoomLines.Count == 0)
                {
                    o.makeItColor(ConsoleColor.Yellow, "No room in house");
                }

                return allRoomLines.ToArray();

            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
                return null;
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
                return null;
            }
        }

        public void GetListInHouse()
        {
            try
            {



                Console.WriteLine("\t\t***********List guests in house****************");

                GetRooms();



            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {

                o.outPutError(ex.Message);
            }
        }
        public void GetArrivalList()
        {
            try
            {
                DateTime now = DateTime.Now.Date;
                string filePath = Path.GetFullPath("GuestDB.csv");
                string[] allLines = File.ReadAllLines(filePath);

                Console.WriteLine("\t\t***********Arrival List****************");
                Console.WriteLine("GuestID\t||" + "FirstName\t||" + "LastName\t||" + "CheckinDate\t||" + "CheckoutDate\t|| " + "People\t||" + "PaySts\t||" + "PhoneNumber");


                bool guestFound = false;


                foreach (string line in allLines)
                {


                    var values = line.Split(',');
                    //arrivalGuests = new Guest(int.Parse(values[0]), values[1], values[2], DateTime.Parse(values[3]), DateTime.Parse(values[4]), int.Parse(values[5]), values[6], int.Parse(values[7]));

                    if (values[3] != null && values[3] != "''")
                    {
                        if (Convert.ToDateTime(values[3]) == now)
                        {

                            Console.WriteLine("");
                            foreach (var value in values)
                            {
                                Console.Write(value + "\t||");
                            }
                            Console.WriteLine("");
                            guestFound = true;

                        }

                    }


                }
                if (!guestFound)
                {
                    o.makeItColor(ConsoleColor.Yellow, "No Guest Found");

                }

            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {

            }
        }
        public void GetDepartures()
        {
            try
            {
                string filePath = Path.GetFullPath("GuestDB.csv");
                string[] allLines = File.ReadAllLines(filePath);
                DateTime now = DateTime.Now.Date;
                Console.WriteLine("\t\t***********Departures****************");
                Console.WriteLine("GuestID\t||" + "FirstName\t||" + "LastName\t||" + "CheckinDate\t||" + "CheckoutDate\t|| " + "People\t||" + "PaySts\t||" + "PhoneNumber");

                bool guestFound = false;

                foreach (var line in allLines)
                {
                    var values = line.Split(',');

                    if (Convert.ToDateTime(values[4]) == now)
                    {

                        Console.WriteLine("");
                        foreach (var value in values)
                        {
                            Console.Write(value + "\t||");
                        }
                        Console.WriteLine("");
                        guestFound = true;

                    }

                }

                if (!guestFound)
                {
                    o.makeItColor(ConsoleColor.Yellow, "No Guest Found");
                }

            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {

            }

        }
        public void CreatNewBooking()
        {
            Random random = new Random();
            List<Guest> guests = new List<Guest>();
            List<Guest> newBookings = new List<Guest>();
            bool newBooking = true;
            string guestFirstName;
            string guestLastName;
            string guestCheckIn;
            string guestCheckOut;
            int guestsNumber;
            int guestPhoneNumber;
            string guestPaymentStatus;
            DateTime dateTime;
            //all correct format of date
            string[] formats = {"d/M/yyyy h:mm:ss tt", "d/M/yyyy h:mm tt",
                   "dd/MM/yyyy hh:mm:ss", "d/m/yyyy h:mm:ss",
                   "d/M/yyyy hh:mm tt", "d/M/yyyy hh tt",
                   "d/M/yyyy h:mm", "d/M/yyyy h:mm",
                   "dd/MM/yyyy hh:mm", "d/MM/yyyy hh:mm","dd/MM/yyyy","d/M/yyyy","dd/M/yyyy","d/MM/yyyy","dd/M/yy","d/M/yy","MM/d/yy","d/MM/yy"};
            while (newBooking)
            {
                try
                {
                    //TO DO TRY CATSCH
                    Console.WriteLine("\t\t***********Make a booking****************");
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest first name");
                        guestFirstName = Console.ReadLine();
                        if (guestFirstName != null && guestFirstName.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest first name in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest Last name");
                        guestLastName = Console.ReadLine();
                        if (guestLastName != null && guestLastName.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest last name in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest check-in date");
                        guestCheckIn = Console.ReadLine();
                        //https://stackoverflow.com/questions/48563292/c-sharp-date-format-uk-and-us checking date format
                        if (guestCheckIn != null && DateTime.TryParseExact(guestCheckIn, formats, new CultureInfo("en-GB"), DateTimeStyles.None, out dateTime))

                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a a guest check-in date in correct format (it should not be as a date like 00/00/0000)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest check-out date");
                        guestCheckOut = Console.ReadLine();
                        if (guestCheckOut != null && DateTime.TryParseExact(guestCheckOut, formats, new CultureInfo("en-GB"), DateTimeStyles.None, out dateTime))

                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest check-out date correct format (it should not be as a date like 00/00/0000)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a number of the guests");
                        guestsNumber = Convert.ToInt32(Console.ReadLine());
                        if (guestsNumber != null && guestsNumber <= 2 && guestsNumber >= 1)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a number of the guests in correct format (it should not be more than 2 or less than 1)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest payment status");
                        guestPaymentStatus = Console.ReadLine();
                        if (guestPaymentStatus != null && guestPaymentStatus.ToLower() == "paid" || guestPaymentStatus.ToLower() == "not paid")
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest payment status in correct format (it should be \"paid\" or \"not paid\"");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest phone number");
                        guestPhoneNumber = Convert.ToInt32(Console.ReadLine());
                        if (guestPhoneNumber != null && guestPhoneNumber.ToString().Length == 10)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest phone number in correct format (it should like \"1234567890\")");
                        }
                    }


                    newBookings.Add(new Guest(random.Next(1000, 9999), guestFirstName, guestLastName, Convert.ToDateTime(guestCheckIn), Convert.ToDateTime(guestCheckOut), guestsNumber, guestPaymentStatus, guestPhoneNumber));
                    Console.WriteLine("Would you like add more booking?(yes/no)");

                    string userAn = Console.ReadLine();
                    while (true)
                    {
                        if (userAn.ToLower() == "yes")
                        {
                            break;
                        }
                        else if (userAn.ToLower() == "no")
                        {
                            newBooking = false;
                            break;
                        }
                        else
                        {
                            o.makeItColor(ConsoleColor.Yellow, "Please Enter yes or no");
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    o.outPutError(e.Message);
                }

            }
            try
            {
                IEnumerable<Guest> newGuests = newBookings.OrderBy(g => g.GetGuestId());
                string filePath = Path.GetFullPath("GuestDB.csv");

                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                    {

                        foreach (Guest guest in newGuests)
                        {
                            file.WriteLine(guest.GetGuestId() + "," + guest.GetGuestFirstName() + "," + guest.GetGuestLastName() + "," + guest.GetGuestCheckInDate() + "," + guest.GetGuestCheckOutDate()
                                + "," + guest.GetGuestsNumber() + "," + guest.GetGuestPaymentStatus() + "," + guest.GetGuestPhoneNum());

                        }
                        file.Close();
                    }
                }
                catch (Exception ex)
                {
                    o.outPutError("Something went wrong please try again");
                }


            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
            }

        }
        public void viewBooking()
        {
            try
            {
                string filePath = Path.GetFullPath("GuestDB.csv");

                string[] allLines = File.ReadAllLines(filePath);
                int userGuestNumb;
                bool bookingFound = false;

                while (true)
                {
                    Console.WriteLine("Please Enter guest ID that you want to see the his booking:");

                    userGuestNumb = Convert.ToInt32(Console.ReadLine());
                    if (userGuestNumb != null && userGuestNumb.ToString().Length == 4)
                    {
                        break;
                    }
                    else
                    {
                        o.outPutError("Please Enter guest ID in correct format (it should like \"1234\")");
                    }
                }
                Console.WriteLine("GuestID\t||" + "FirstName\t||" + "LastName\t||" + "CheckinDate\t||" + "CheckoutDate\t|| " + "People\t||" + "PaySts\t||" + "PhoneNumber");

                foreach (string line in allLines)
                {
                    var values = line.Split(",");

                    
                    if (Convert.ToInt32(values[0]) == userGuestNumb)
                    {
                        Console.WriteLine("");
                        foreach (var value in values)
                        {
                            Console.Write(value + "\t||");
                        }
                        Console.WriteLine("");

                        bookingFound = true;
                    }
                    Console.WriteLine("");

                }
                if (!bookingFound)
                {
                    o.makeItColor(ConsoleColor.Yellow, "No booking founded");
                }
                Console.WriteLine("\n\n\nFor backing to the menu please press enter:");
                Console.ReadLine();
            }
            catch (FileNotFoundException)
            {
                o.outPutError("File not found - please provide valid path of file");
            }
            catch (Exception ex)
            {
                o.outPutError(ex.Message);
            }
        }
    }
}
