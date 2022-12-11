using CsvHelper;
using Ibis_Hotel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
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
        Manager manager = new Manager();
        TeamLeader teamLeader = new TeamLeader();

        string userFullName;
        public Staff logIn()
        {
            //to store all staffs in a hashset for checking them
            HashSet<Staff> staffs = new HashSet<Staff>();
            OutputOperators o = new OutputOperators();
            //make an emty staff to fill it by loged in staff for returning it
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
                    //get the path of staffDB.csv file
                    string filePath = Path.GetFullPath("StaffDB.csv");
               
                    //Read all the lines and store all lines in an array
                    string[] lines = File.ReadAllLines(filePath);

                    //for each line in lines
                    foreach(var line in lines)
                    {
                        //acces to the all variables of the line by spiliting them by "," as it is a csv file
                        var values = line.Split(',');
                        //make new staff according to each line
                        staffs.Add(new Staff( values[0], values[1], values[2], values[3], values[4]));
                    }


                    Console.WriteLine("*-*-*-*-*-*-*-*-*    Log in Menu   *-*-*-*-*-*-*-*-*\n");
                   
                    while (true)
                    {
                        //ask for the username
                        Console.WriteLine("Enter your user name :");
                        userUserName = Console.ReadLine();

                        //check the username that user cannot enter null value or bigger that 20 chars
                        if (userUserName != null && userUserName.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            //Out put the error in a format of error
                            o.outPutError("Please Enter a user name in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    while (true)
                    {
                        //ask for the password
                        Console.WriteLine("Enter your password :");
                        userPassword = Console.ReadLine();
                        //check the password that user cannot enter null value or bigger that 20 chars
                        if (userPassword != null && userPassword.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            //Out put the error in a format of error
                            o.outPutError("Please Enter a password in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    

                    //check each staff in our hashset collection
                    foreach (Staff staff in staffs)
                    {
                        //if username and password which user entered match the username and password of one of the staff
                        if (userUserName == staff.GetStaffUserName() && userPassword == staff.GetStaffPassword())
                        {
                            //login is valid
                            userFirstname = staff.GetStaffFirstName();
                            //pass the staff that loged in to the user so we will be able to return user
                            user = staff;
                            validLogin = true;
                            this.userFullName = staff.GetStaffFirstName() + " " + staff.GetStaffLastName();
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

                //if login was valid
                if (validLogin)
                {
                    //freindly message that user logged in
                    o.makeItColor(ConsoleColor.Green, "You have successfully logged in " + userFirstname + "!!!\n\n\n");
                    //return the current staff that logged in
                    return user;
                }
                //if login was not valid
                else if (!validLogin)
                {

                    switch (numberOfLginTrials)
                    {
                        //first try
                        case 0:
                            o.outPutError("****************ACCES DENIED WRONG USERNAME OR PASSWORD************** \nTRY AGAIN\nYOU HAVE JUST ");
                            o.makeItColor(ConsoleColor.Red, "*3* ");
                            Console.Write("ATTEMPTS\n");
                            numberOfLginTrials++;
                            break;
                            //second try
                        case 1:

                            o.outPutError("****************ACCES DENIED WRONG USERNAME OR PASSWORD************** \nTRY AGAIN\nYOU HAVE JUST ");
                            o.makeItColor(ConsoleColor.Red, "*2* ");
                            Console.Write("ATTEMPTS\n");
                            numberOfLginTrials++;
                            break;
                            //third and last try 
                        case 2:
                            o.outPutError("****************ACCES DENIED WRONG USERNAME OR PASSWORD************** \nTRY AGAIN\nTHIS IS YOUR ");
                            o.makeItColor(ConsoleColor.Red, " LAST ATTEMPT ");
                            Console.Write("BE CARFUL!\n");
                            numberOfLginTrials++;
                            break;
                            //kick user out of the software with freindly text
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
            //recive the input from user as a number
            int userOptionNum = Convert.ToInt32(Console.ReadLine());
           
            switch (userOptionNum)
            {
                //if they enter 0 exit the software
                case 0:
                    Console.Clear();
                    stop = 1;
                    break;
                //before each proces clear the console to deliver better interface
                case 1:
                    Console.Clear();
                    hotel.GetListInHouse();
                    //record what is user doing and who is the user
                    teamLeader.RecordStaff("in house list got", userFullName);

                    break;
                case 2:
                    Console.Clear();

                    hotel.GetArrivalList();
                    teamLeader.RecordStaff("arrival list got", userFullName);
                    break;
                case 3:
                    Console.Clear();

                    hotel.GetDepartures();
                    teamLeader.RecordStaff("departure list got", userFullName);
                    break;
                case 4:
                    Console.Clear();
                    hotel.CreatNewBooking();
                    teamLeader.RecordStaff("Booking Created", userFullName);
                    break;
                case 5:
                    Console.Clear();
                    
                    hotel.CheckIn();
                    teamLeader.RecordStaff("Checked in", userFullName);
                    break;
                case 6:
                    Console.Clear();
                    hotel.CheckOut();
                    teamLeader.RecordStaff("Checked Out", userFullName);

                    break;
                case 7:
                    Console.Clear();
                    hotel.viewBooking();
                    teamLeader.RecordStaff("Booking viewed", userFullName);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter valid number!", userFullName);
                    break ;
            }

        }
        public void teamLeaderMenu()
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
                    teamLeader.RecordStaff("in house list got", userFullName);
                    break;
                case 2:
                    Console.Clear();

                    hotel.GetArrivalList();
                    teamLeader.RecordStaff("arrival list got", userFullName);
                    break;
                case 3:
                    Console.Clear();

                    hotel.GetDepartures();
                    teamLeader.RecordStaff("departure list got", userFullName);
                    break;
                case 4:
                    Console.Clear();
                    hotel.CreatNewBooking();
                    teamLeader.RecordStaff("Booking Created", userFullName);
                    break;
                case 5:
                    Console.Clear();

                    hotel.CheckIn();
                    teamLeader.RecordStaff("Checked in", userFullName);
                    break;
                case 6:
                    Console.Clear();
                    hotel.CheckOut();
                    teamLeader.RecordStaff("Checked Out", userFullName);

                    break;
                case 7:
                    Console.Clear();
                    hotel.viewBooking();
                    teamLeader.RecordStaff("Booking viewed", userFullName);
                    break;
                case 8:
                    //only team leader and manager can do this
                    Console.Clear();
                    teamLeader.viewAudit();
                    teamLeader.RecordStaff("Audit viewd", userFullName);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter valid number!", userFullName);
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
                    manager.RecordStaff("in house list got", userFullName);
                    break;
                case 2:
                    Console.Clear();

                    hotel.GetArrivalList();
                    manager.RecordStaff("arrival list got", userFullName);
                    break;
                case 3:
                    Console.Clear();

                    hotel.GetDepartures();
                    manager.RecordStaff("departure list got", userFullName);
                    break;
                case 4:
                    Console.Clear();
                    hotel.CreatNewBooking();
                    manager.RecordStaff("Booking Created", userFullName);
                    break;
                case 5:
                    Console.Clear();

                    hotel.CheckIn();
                    manager.RecordStaff("Checked in", userFullName);
                    break;
                case 6:
                    Console.Clear();
                    hotel.CheckOut();
                    manager.RecordStaff("Checked Out", userFullName);

                    break;
                case 7:
                    Console.Clear();
                    hotel.viewBooking();
                    manager.RecordStaff("Booking viewed", userFullName);
                    break;
                case 8:
                    //only team leader and manager can do this
                    Console.Clear();
                    manager.viewAudit();
                    manager.RecordStaff("Audit viewd", userFullName);
                    break;
                case 9:
                    //only manager can do this
                    Console.Clear();
                    manager.registerStaff();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter valid number!", userFullName);
                    break;
            }
        }
    }

}
