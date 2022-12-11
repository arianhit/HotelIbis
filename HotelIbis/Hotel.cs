using Ibis_Hotel;
using Microsoft.Data.SqlClient;
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

        //to occupie a room whith a guest
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
            //show the arrival list so user can see all details and can shose the guest id easier
            GetArrivalList();
            string[] allGuestLines = { "" };
            string[] allRoomsLines = { "" };

            try
            {
                //same method just this time as a loop to check that the guest id is valid
                while (true)
                {
                    Console.WriteLine("Please Enter guest ID that you want to check in:");

                    userGuestNumb = Convert.ToInt32(Console.ReadLine());

                    string filePath = Path.GetFullPath("GuestDB.csv");
                    //store all guests line in an array
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
                //show the rooms which are occupied so user can chose the room easier
                GetRooms();
               //ask for the room number 
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
                    //if the guest number match one of the guests on arrival list
                    if (Convert.ToInt32(values[0]) == userGuestNumb)
                    {
                        
                        //make a guest to make a room easier
                        guest = new Guest(Convert.ToInt32(values[0]), values[1].ToString(), values[2].ToString(), Convert.ToDateTime(values[3]), Convert.ToDateTime(values[4]), Convert.ToInt32(values[5]), values[6].ToString(), Convert.ToInt32(values[7]));


                        //make a room for writing and saving
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
                


                try
                {
                    //write down the new room with a new guest on the file
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
        //remove guest from list in house
        public void CheckOut()
        {
            int userGuestNumb = 0;
            int userRoomNumb = 0;




            Room newRoom = new Room();
            //as I want to get all rooms from the file and then clear the file and fill it with new rooms without the chosen room
            //I used hashset instead of list as we will not have empty indext to make problem during writing
            HashSet<string> newLines = new HashSet<string>();
            Console.WriteLine("\t\t***********checking Out****************");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            //store all rooms an an array
            string[] allLines = GetRooms();
            try
            {

                //check if the room is occupied and in correct format as a loop
                while (true)
                {
                    Console.WriteLine("Please Enter room number that you want to check out the guest :");
                    userRoomNumb = Convert.ToInt32(Console.ReadLine());
                    List<int> roomNumbers = new List<int>();

                    foreach (string line in allLines)
                    {
                        var values = line.Split(',');
                        //add all room numbers to the list so we can check that if the user entered number is in our list or not
                        roomNumbers.Add(int.Parse(values[0]));
                    }
                    //check if the room is occupied and in correct format
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

                //get all rooms lines and add them to the hashset
                foreach (string line in allLines)
                {
                    newLines.Add(line);
                }
                //clear the file
                File.WriteAllText(filePathOfRooms, "");

                //remove the room that wants to check out from our hashset
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
                    //write down all rooms from the hashset 
                    foreach (string line in newLines)
                    {
                        var values = line.Split(',');
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePathOfRooms, false))
                        {

                            file.WriteLine(values[0] + "," + values[1] + "," + values[2] + "," + values[3] + "," + values[4] + "," + values[5] + "," + values[6] + "," + values[7] + "," + values[8] + "," + values[9]);

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
                //get the full path of RoomsDB.csv file
                string filePath = Path.GetFullPath("RoomsDB.csv");
                //store all lines in array
                string[] allLines = File.ReadAllLines(filePath);
                //list of all lines to count them
                List<string> allRoomLines = new List<string>();
                DateTime now = DateTime.Now.Date;

                Console.WriteLine("\t\t***********Rooms****************");
                Console.WriteLine("RoomID\t||" + "Status\t||" + "Guest\t||");
                //acces to each line in lines  
                foreach (var line in allLines)
                {
                    //add them to the list
                    allRoomLines.Add(line);
                }
                //out put all rooms 
                foreach (string room in allRoomLines)
                {
                    Console.WriteLine("");
                    var values = room.Split(",");
                    //acces to each value of the line
                    foreach (var value in values)
                    {
                        //out put the values
                        Console.Write(value.ToString() + "\t||");
                    }
                    Console.WriteLine("");
                }
                //if the file is emty
                if (allRoomLines.Count == 0)
                {
                    o.makeItColor(ConsoleColor.Yellow, "\nNo room in house\n");
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
        //list of people who accomodated
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
        //list of people that check in today
        public void GetArrivalList()
        {
            try
            {
                //today date
                DateTime now = DateTime.Now.Date;

                //same method used
                string filePath = Path.GetFullPath("GuestDB.csv");
                string[] allLines = File.ReadAllLines(filePath);

                Console.WriteLine("\t\t***********Arrival List****************");
                Console.WriteLine("GuestID\t||" + "FirstName\t||" + "LastName\t||" + "CheckinDate\t||" + "CheckoutDate\t|| " + "People\t||" + "PaySts\t||" + "PhoneNumber");


                bool guestFound = false;

                //same method
                foreach (string line in allLines)
                {


                    var values = line.Split(',');

                    //if the check in date is same as today as we want arrival list
                    if (Convert.ToDateTime(values[3]) == now)
                    {
                        //out put them
                        Console.WriteLine("");
                        foreach (var value in values)
                        {
                            Console.Write(value + "\t||");
                        }
                        Console.WriteLine("");
                        //at least one guest found
                        guestFound = true;

                    }



                }
                if (!guestFound)
                {
                    //no guest founded
                    o.makeItColor(ConsoleColor.Yellow, "\nNo Guest Found\n");

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
        //list of people who are checking out today
        public void GetDepartures()
        {
            try
            {
                //same method
                string filePath = Path.GetFullPath("GuestDB.csv");
                string[] allLines = File.ReadAllLines(filePath);
                DateTime now = DateTime.Now.Date;
                Console.WriteLine("\t\t***********Departures****************");
                Console.WriteLine("GuestID\t||" + "FirstName\t||" + "LastName\t||" + "CheckinDate\t||" + "CheckoutDate\t|| " + "People\t||" + "PaySts\t||" + "PhoneNumber");

                bool guestFound = false;

                foreach (var line in allLines)
                {
                    var values = line.Split(',');
                    //check that check out date is today
                    if (Convert.ToDateTime(values[4]) == now)
                    {

                        Console.WriteLine("");
                        foreach (var value in values)
                        {
                            Console.Write(value + "\t||");
                        }
                        Console.WriteLine("");
                        //at least one guest found
                        guestFound = true;

                    }

                }

                if (!guestFound)
                { 
                    //no guest founded
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
        //make a new guest and write it down in GuestDB
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
            //ask for new booking as a loop so user can add as many booking as he wants
            while (newBooking)
            {
                try
                {
                    
                    Console.WriteLine("\t\t***********Make a booking****************");
                    //ask for the first name as a loop to ask correct format of it  
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
                    //ask for the first name as a loop to ask correct format of it 
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest Last name");
                        guestLastName = Console.ReadLine();
                        if (guestLastName != null && guestLastName.Length < 20)
                        {
                            //correct format recieved so break the loop
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest last name in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    //ask for the check in date as a loop to ask correct format of it 
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest check-in date (it should not be as a date like dd/mm/yyyy)");
                        guestCheckIn = Console.ReadLine();
                        //https://stackoverflow.com/questions/48563292/c-sharp-date-format-uk-and-us checking date format
                        //check the date format as the GB format
                        if (guestCheckIn != null && DateTime.TryParseExact(guestCheckIn, formats, new CultureInfo("en-GB"), DateTimeStyles.None, out dateTime))

                        {
                            //correct format recieved so break the loop
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a a guest check-in date in correct format (it should not be as a date like dd/mm/yyyy)");
                        }
                    }
                    //ask for the check out date as a loop to ask correct format of it 
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest check-out date (it should not be as a date like dd/mm/yyyy)");
                        guestCheckOut = Console.ReadLine();
                        if (guestCheckOut != null && DateTime.TryParseExact(guestCheckOut, formats, new CultureInfo("en-GB"), DateTimeStyles.None, out dateTime))

                        {
                            //correct format recieved so break the loop
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest check-out date correct format (it should not be as a date like dd/mm/yyyy)");
                        }
                    }
                    //ask for the number of the people as a loop to ask correct format of it 
                    while (true)
                    {
                        Console.WriteLine("Please Enter a number of the guests (it should not be more than 2 or less than 1)");
                        guestsNumber = Convert.ToInt32(Console.ReadLine());
                        if (guestsNumber != null && guestsNumber <= 2 && guestsNumber >= 1)
                        {
                            //correct format recieved so break the loop
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a number of the guests in correct format (it should not be more than 2 or less than 1)");
                        }
                    }
                    //ask if its paid or not as a loop to ask correct format of it 
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest payment status (it should be \"paid\" or \"not paid\"");
                        guestPaymentStatus = Console.ReadLine();
                        if (guestPaymentStatus != null && guestPaymentStatus.ToLower() == "paid" || guestPaymentStatus.ToLower() == "not paid")
                        {
                            //correct format recieved so break the loop
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest payment status in correct format (it should be \"paid\" or \"not paid\"");
                        }
                    }
                    //ask for phone number  as a loop to ask correct format of it 
                    while (true)
                    {
                        Console.WriteLine("Please Enter a guest phone number (it should like \"1234567890\")");
                        guestPhoneNumber = Convert.ToInt32(Console.ReadLine());
                        if (guestPhoneNumber != null && guestPhoneNumber.ToString().Length == 10)
                        {
                            //correct format recieved so break the loop
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a guest phone number in correct format (it should like \"1234567890\")");
                        }
                    }

                    //add the new guest to the list
                    //guest id will make by system as a random number
                    newBookings.Add(new Guest(random.Next(1000, 9999), guestFirstName, guestLastName, Convert.ToDateTime(guestCheckIn), Convert.ToDateTime(guestCheckOut), guestsNumber, guestPaymentStatus, guestPhoneNumber));
                    Console.WriteLine("Would you like add more booking?(yes/no)");

                    string userAn = Console.ReadLine();
                    //check the user anwser of the question yes or no
                    while (true)
                    {
                        if (userAn.ToLower() == "yes")
                        {
                            //contine to get new booking
                            break;
                        }
                        else if (userAn.ToLower() == "no")
                        {
                            
                            //break the loop of getting new booking
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
                //store all new bookings order by guest ID
                IEnumerable<Guest> newGuests = newBookings.OrderBy(g => g.GetGuestId());

                //get the file path
                string filePath = Path.GetFullPath("GuestDB.csv");

                try
                {
                    //to write on GuestDB
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath, true))
                    {
                        //write each guest in the new bookings oredered list
                        foreach (Guest guest in newGuests)
                        {
                            writer.WriteLine(guest.GetGuestId() + "," + guest.GetGuestFirstName() + "," + guest.GetGuestLastName() + "," + guest.GetGuestCheckInDate() + "," + guest.GetGuestCheckOutDate()
                                + "," + guest.GetGuestsNumber() + "," + guest.GetGuestPaymentStatus() + "," + guest.GetGuestPhoneNum());

                        }
                        //close it to avoid file procesing error
                        writer.Close();
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
        //view the booking by searching the guest ID
        public void viewBooking()
        {
            try
            {
                //same method
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

                    //if the guest number that user entered match on of the guest numbers output that guest
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
                    
                    o.makeItColor(ConsoleColor.Yellow, "\nNo booking founded\n");
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
