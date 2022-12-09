using CsvHelper;
using Ibis_Hotel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HotelIbis
{
    public class Menus
    {

        public int stop = 0;
        OutputOperators o = new OutputOperators();
        Staff staff = new Staff();
        Hotel hotel = new Hotel();

        string userFullName;
        public Staff logIn()
        {
            HashSet<Staff> staffs = new HashSet<Staff>();
            OutputOperators o = new OutputOperators();
            Staff user = new Staff("", "", "", "", "");
            int numberOfLginTrials = 0;
            bool userNamesContains;
            string userFirstname = "";
            string userLastName = "";
            string userUserName = "";
            string userRole = "";
            string userPassword = "";
            string userEmail = "";

            bool validLogin = false;
            while (numberOfLginTrials < 4)
            {
                try
                {
                    string filePath = Path.GetFullPath("StaffDB.csv");
               

                    string[] lines = File.ReadAllLines(filePath);

                    foreach(var line in lines)
                    {
                        var values = line.Split(',');
                        staffs.Add(new Staff( values[0], values[1], values[2], values[3], values[4]));
                    }


                    Console.WriteLine("*-*-*-*-*-*-*-*-*    Log in Menu   *-*-*-*-*-*-*-*-*\n");

                    Console.WriteLine("Enter your user name :");
                    string userName = Console.ReadLine();


                    Console.WriteLine("Enter your password :");
                    string userPassWord = Console.ReadLine();


                    foreach (Staff staff in staffs)
                    {
                        if (userName == staff.GetStaffUserName() && userPassWord == staff.GetStaffPassword())
                        {
                            userFirstname = staff.GetStaffFirstName();
                            user = staff;
                            validLogin = true;
                            this.userFullName = staff.GetStaffFirstName()+ " "+staff.GetStaffLastName();
                            break;
                        }

                    }
                }

                catch (FileNotFoundException)
                {
                    o.outPutError("File not found - please provide valid path of file");
                }
                catch (Exception ex)
                {
                    o.outPutError("Something went wrong please try again");
                }


                if (validLogin)
                {
                    o.makeItColor(ConsoleColor.Green, "You have successfully logged in " + userFirstname + "!!!\n\n\n");

                    return user;
                }
                else if (!validLogin)
                {

                    switch (numberOfLginTrials)
                    {
                        case 0:
                            o.outPutError("****************ACCES DENIED WRONG USERNAME OR PASSWORD************** \nTRY AGAIN\nYOU HAVE JUST ");
                            o.makeItColor(ConsoleColor.Red, "*3* ");
                            Console.Write("ATTEMPTS\n");
                            numberOfLginTrials++;
                            break;
                        case 1:

                            o.outPutError("****************ACCES DENIED WRONG USERNAME OR PASSWORD************** \nTRY AGAIN\nYOU HAVE JUST ");
                            o.makeItColor(ConsoleColor.Red, "*2* ");
                            Console.Write("ATTEMPTS\n");
                            numberOfLginTrials++;
                            break;
                        case 2:
                            o.outPutError("****************ACCES DENIED WRONG USERNAME OR PASSWORD************** \nTRY AGAIN\nTHIS IS YOUR ");
                            o.makeItColor(ConsoleColor.Red, " LAST ATTEMPT ");
                            Console.Write("BE CARFUL!\n");
                            numberOfLginTrials++;
                            break;
                        case 3:
                            o.makeItColor(ConsoleColor.Red, "[ERROR]:****************ACCES DENIED WRONG USERNAME OR PASSWORD************** \nYOU HAD 3 ATTEMPTS AND ");
                            o.makeItColor(ConsoleColor.Red, "YOU ARE NOT ALLOW LOGIN AGAIN\n");
                            numberOfLginTrials++;
                            break;
                    }
                }



            }

            return user;
        }
        public string GetUserFullName()
        {
            return userFullName;
        }

        public void receptionMenu()
        {
           

          
            

           

            Console.WriteLine("\tReception Menu:  ");
            Console.WriteLine("\t\t1.List guests in house");
            Console.WriteLine("\t\t2.Arrival List");
            Console.WriteLine("\t\t3.Departures");
            Console.WriteLine("\t\t4.Make a booking");
            Console.WriteLine("\t\t5.check in");
            Console.WriteLine("\t\t6.check out");
            Console.WriteLine("\t\t7.View the booking");
            Console.WriteLine("\t\t0.Exit");
            int userOptionNum = Convert.ToInt32(Console.ReadLine());
           
            switch (userOptionNum)
            {
                case 0:
                    Console.Clear();
                    stop = 1;
                    break;

                case 1:
                    Console.Clear();
                    hotel.GetListInHouse();
                    staff.RecordStaff("in house list got", userFullName);
                    break;
                case 2:
                    Console.Clear();

                    hotel.GetArrivalList();
                    staff.RecordStaff("arrival list got", userFullName);
                    break;
                case 3:
                    Console.Clear();

                    hotel.GetDepartures();
                    staff.RecordStaff("departure list got", userFullName);
                    break;
                case 4:
                    Console.Clear();
                    hotel.CreatNewBooking();
                    staff.RecordStaff("Booking Created", userFullName);
                    break;
                case 5:
                    Console.Clear();
                    
                    hotel.CheckIn();
                    staff.RecordStaff("Checked in", userFullName);
                    break;
                case 6:
                    Console.Clear();
                    hotel.CheckOut();
                    staff.RecordStaff("Checked Out", userFullName);

                    break;
                case 7:
                    Console.Clear();
                    hotel.viewBooking();
                    staff.RecordStaff("Booking viewed", userFullName);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter valid number!");
                    break ;
            }

        }
        public void teamLeader()
        {
            Console.WriteLine("\tReception Menu:  ");
            Console.WriteLine("\t\t1.List guests in house");
            Console.WriteLine("\t\t2.Arrival List");
            Console.WriteLine("\t\t3.Departures");
            Console.WriteLine("\t\t4.Make a booking");
            Console.WriteLine("\t\t5.check in");
            Console.WriteLine("\t\t6.check out");
            Console.WriteLine("\t\t7.View the booking");
            Console.WriteLine("\t\t8.Audit staffs");
            Console.WriteLine("\t\t0.Exit");
            int userOptionNum = Convert.ToInt32(Console.ReadLine());

            switch (userOptionNum)
            {
                case 0:
                    Console.Clear();
                    stop = 1;
                    break;

                case 1:
                    Console.Clear();
                    hotel.GetListInHouse();
                    staff.RecordStaff("in house list got", userFullName);
                    break;
                case 2:
                    Console.Clear();

                    hotel.GetArrivalList();
                    staff.RecordStaff("arrival list got", userFullName);
                    break;
                case 3:
                    Console.Clear();

                    hotel.GetDepartures();
                    staff.RecordStaff("departure list got", userFullName);
                    break;
                case 4:
                    Console.Clear();
                    hotel.CreatNewBooking();
                    staff.RecordStaff("Booking Created", userFullName);
                    break;
                case 5:
                    Console.Clear();

                    hotel.CheckIn();
                    staff.RecordStaff("Checked in", userFullName);
                    break;
                case 6:
                    Console.Clear();
                    hotel.CheckOut();
                    staff.RecordStaff("Checked Out", userFullName);

                    break;
                case 7:
                    Console.Clear();
                    hotel.viewBooking();
                    staff.RecordStaff("Booking viewed", userFullName);
                    break;
                case 8:
                    Console.Clear();
                    staff.viewAudit();
                    staff.RecordStaff("Audit viewd", userFullName);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter valid number!");
                    break;
            }

        }
        public void generalManager()
        {
            Console.WriteLine("\tReception Menu:  ");
            Console.WriteLine("\t\t1.List guests in house");
            Console.WriteLine("\t\t2.Arrival List");
            Console.WriteLine("\t\t3.Departures");
            Console.WriteLine("\t\t4.Make a booking");
            Console.WriteLine("\t\t5.check in");
            Console.WriteLine("\t\t6.check out");
            Console.WriteLine("\t\t7.View the booking");
            Console.WriteLine("\t\t8.Audit staffs");
            Console.WriteLine("\t\t9.Register staff");
            Console.WriteLine("\t\t0.Exit");

            int userOptionNum = Convert.ToInt32(Console.ReadLine());
            switch (userOptionNum)
            {
                case 0:
                    Console.Clear();
                    stop = 1;
                    break;

                case 1:
                    Console.Clear();
                    hotel.GetListInHouse();
                    staff.RecordStaff("in house list got", userFullName);
                    break;
                case 2:
                    Console.Clear();

                    hotel.GetArrivalList();
                    staff.RecordStaff("arrival list got", userFullName);
                    break;
                case 3:
                    Console.Clear();

                    hotel.GetDepartures();
                    staff.RecordStaff("departure list got", userFullName);
                    break;
                case 4:
                    Console.Clear();
                    hotel.CreatNewBooking();
                    staff.RecordStaff("Booking Created", userFullName);
                    break;
                case 5:
                    Console.Clear();

                    hotel.CheckIn();
                    staff.RecordStaff("Checked in", userFullName);
                    break;
                case 6:
                    Console.Clear();
                    hotel.CheckOut();
                    staff.RecordStaff("Checked Out", userFullName);

                    break;
                case 7:
                    Console.Clear();
                    hotel.viewBooking();
                    staff.RecordStaff("Booking viewed", userFullName);
                    break;
                case 8:
                    Console.Clear();
                    staff.viewAudit();
                    staff.RecordStaff("Audit viewd", userFullName);
                    break;
                case 9:
                    Console.Clear();
                    staff.registerStaff();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter valid number!");
                    break;
            }
        }
    }

}
