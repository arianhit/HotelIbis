using Ibis_Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelIbis
{
    //inheritance class of staff
    public class Manager : TeamLeader
    {
         protected string staffUserName;
         protected string staffPassword;

        OutputOperators o = new OutputOperators();
        
        public void registerStaff()
        {
            List<Staff> newStaffs = new List<Staff>();
            bool newStaff = true;
            string staffFirstName;
            string staffLastName;
            string staffRole;
            

            
            //loop until adding new staff finish
            while (newStaff)
            {
                try
                {

                    Console.WriteLine("\t\t***********Register new staff****************");
                    //same as make a new booking function
                    while (true)
                    {
                        Console.WriteLine("Please Enter a staff first name");
                        staffFirstName = Console.ReadLine();
                        if (staffFirstName != null && staffFirstName.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a staff first name in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a staff Last name");
                        staffLastName = Console.ReadLine();
                        if (staffLastName != null && staffLastName.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a staff last name in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a " + staffFirstName + " Role");
                        staffRole = Console.ReadLine();
                        if ((staffRole != null && staffRole.Length < 20 && staffRole.ToLower() == "reception") || staffRole.ToLower() == "teamleader" || staffRole.ToLower() == "manager")
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a staff role in correct format (it should be Reception or TeamLeader or Manager)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a " + staffFirstName + " UserName");
                        staffUserName = Console.ReadLine();

                        if (staffUserName != null && staffUserName.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a staff user name in correct format (it should not be more than 20 charecters)");
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter a " + staffFirstName + " Password");

                        staffPassword = Console.ReadLine();

                        if (staffPassword != null && staffPassword.Length < 20)
                        {
                            break;
                        }
                        else
                        {
                            o.outPutError("Please Enter a staff password in correct format (it should not be more than 20 charecters)");
                        }
                    }

                    newStaffs.Add(new Staff(staffFirstName, staffLastName, staffRole, staffUserName, staffPassword));
                    Console.WriteLine("Would you like add more staff?(yes/no)");

                    string userAn = Console.ReadLine();
                    while (true)
                    {
                        if (userAn.ToLower() == "yes")
                        {
                            break;
                        }
                        else if (userAn.ToLower() == "no")
                        {
                            newStaff = false;
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

                string filePath = Path.GetFullPath("StaffDB.csv");

                try
                {
                    //write down the new staff on the file
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                    {

                        foreach (Staff staff in newStaffs)
                        {
                            file.WriteLine(staff.GetStaffFirstName() + "," + staff.GetStaffLastName() + "," + staff.GetStaffRole() + "," + staff.GetStaffUserName() + "," + staff.GetStaffPassword());


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
    }
}
