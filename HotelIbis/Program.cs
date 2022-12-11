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

                //Get the current staff from login Menu
                Staff loggedInUser = m.logIn();
                string userFullname = loggedInUser.GetStaffFirstName() + loggedInUser.GetStaffLastName();
                
                if (loggedInUser.GetStaffFirstName() != "")
                {
                    Console.WriteLine("Welcome to your user menu " + loggedInUser.GetStaffFirstName() + "\n" +
                        "For using the menu please enter the number of the option (1 2 3 ..) \n\n");
                    //stop the loop if user entered 0 on menu
                    while (m.stop == 0)
                    {
                        switch (loggedInUser.GetStaffRole().ToLower())
                        {
                            case "reception":
                                
                                
                                m.receptionMenu();
                                break;
                            case "teamleader":

                                
                                m.teamLeaderMenu();

                                break;
                            case "generalmanager":
                                

                                m.generalManager();
                                break;
                            default:
                                o.outPutError("Invalid User please login again!");
                                m.stop=0;
                                break;
                        }
                    }
                   
                }


            }
            else if (args[0] == "-v") { Console.WriteLine("Verssion 0.0.01"); }
            else if (args[0] == "-help") { Console.WriteLine("You can see the version by entering -v and if you want to use software just enter 0"); }

            else
            {
                //to make the text in yellow
                o.makeItColor(ConsoleColor.Yellow, "Please Enter the correct input such as (-v , -help , ..");
            }
        }
        catch (Exception ex)
        {
            o.outPutError(ex.Message);
        }
    }

}

