// See https://aka.ms/new-console-template for more information
using HotelIbis;
using Ibis_Hotel;


class Program
{

   
    static void Main(string[] args)
    {
        Menus m = new Menus();
        OutputOperators o = new OutputOperators();
       
        try
        {
           
           
           

            if (args.Length == 0)
            {
                Console.WriteLine("Hello, and welcome to the ibis hotel software!");

                Console.WriteLine("\n\nPlease log in to your account:");

                Staff loggedInUser = m.logIn();
                string userFullname = loggedInUser.GetStaffFirstName() + loggedInUser.GetStaffLastName();
                
                if (loggedInUser.GetStaffFirstName() != "")
                {
                    Console.WriteLine("Welcome to your user menu " + loggedInUser.GetStaffFirstName() + "\n" +
                        "For using the menu please enter the number of the option (1 2 3 ..) \n\n");
                    while (m.stop == 0)
                    {
                        switch (loggedInUser.GetStaffRole())
                        {
                            case "Reception":
                                
                                
                                m.receptionMenu();
                                break;
                            case "Team Leader":

                                
                                m.teamLeader();

                                break;
                            case "General Manager":
                                

                                m.generalManager();
                                break;
                            default:
                                o.outPutError("Invalid User please login again!");
                                break;
                        }
                    }
                   
                }


            }
            else if (args[0] == "-v") { Console.WriteLine("Verssion 0.0.01"); }
            else
            {
                o.makeItColor(ConsoleColor.Yellow, "Please Enter the correct input such as (-v , -help , ..");
            }
        }
        catch (Exception ex)
        {
            o.outPutError(ex.Message);
        }
    }

}

