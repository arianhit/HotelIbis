using HotelIbis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ibis_Hotel
{
    [Serializable]
    public  class Staff
    {
        private string staffFirstName;
        private string staffLastName;
        private string staffRole;
        private string staffUserName;
        private string staffPassword;

        OutputOperators o = new OutputOperators();
        DateTime now = DateTime.Now.Date;

        public Staff(string staffFirstName, string staffLastName, string staffRole, string staffUserName, string staffPassword)
        {
            this.staffFirstName = staffFirstName;
            this.staffLastName = staffLastName;
            this.staffRole = staffRole;
            this.staffUserName = staffUserName;
            this.staffPassword = staffPassword;
            
        }
        public Staff()
        {

        }

        public string GetStaffFirstName() { return staffFirstName; }
        public string GetStaffLastName() { return staffLastName; }
        public string GetStaffRole() { return staffRole; }
        public string GetStaffUserName() { return staffUserName; }
        public string GetStaffPassword() { return staffPassword; }


        public void RecordStaff(string operation, string staffName)
        {
            DateTime now = DateTime.Now;
            string currentStaffName = staffName;
            string path = "Audit.txt";
            List<AuditStaff> staffActions = new List<AuditStaff>();
            IFormatter formatter = new BinaryFormatter();

            try
            {
                //How to read and write without file is procesesing in another program error (use using)
                //https://stackoverflow.com/questions/26741191/ioexception-the-process-cannot-access-the-file-file-path-because-it-is-being
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    staffActions = (List<AuditStaff>)formatter.Deserialize(stream);
                    stream.Close();
                }
                
                

                File.Delete(path);

                AuditStaff aus = new AuditStaff(operation, currentStaffName, now);
                staffActions.Add(aus);

                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    
                    
                    formatter.Serialize(stream, staffActions);
                    stream.Close();
                }
                ;
                


                

            }
            catch (SerializationException)
            {
                o.outPutError("Could not serilize the file");
            }

            catch (ArgumentNullException)
            {
                o.outPutError("File is empty");
            }
            catch (Exception e)
            {
                o.outPutError(e.Message);
            }
            
        }
        public void viewAudit()
        {
            Console.WriteLine("staff name\t||" + "Date and Time \t||" + "Action\t||");
            try
            {

                List<AuditStaff> staffActions = new BinaryFormatter().Deserialize(File.OpenRead("Audit.txt")) as List<AuditStaff>;

                foreach (var sf in staffActions)
                {
                    Console.Write(sf.GetCurrentStaffName() + "\t ||");
                    Console.Write(sf.GetDateAndTime() + "\t ||");
                    Console.Write(sf.GetOperation() + "\t ||");
                    Console.WriteLine("\n");
                }
                Console.WriteLine("\n\n\nFor backing to the menu please press enter:");
                Console.ReadLine();
            }
            catch (SerializationException)
            {
                o.outPutError("Could not serilize the file");
            }
            catch (ArgumentNullException)
            {
                o.outPutError("File is empty");
            }
            catch (Exception e)
            {
                o.outPutError(e.Message);
            }
        }
        public void registerStaff()
        {
            List<Staff> newStaffs = new List<Staff>();
            bool newBooking = true;
            string staffFirstName;
            string staffLastName;
            string staffRole;
            string staffPassword;
            string StaffUsername;



            while (newBooking)
            {
                try
                {

                    Console.WriteLine("\t\t***********Register new staff****************");
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
                        if (staffRole != null && staffRole.Length < 20  && staffRole.ToLower() == "reception" && staffRole.ToLower() == "teamleader" && staffRole.ToLower() == "manager")
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
                        StaffUsername = Console.ReadLine();

                        if (StaffUsername != null && StaffUsername.Length < 20)
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

                    newStaffs.Add(new Staff(staffFirstName, staffLastName, staffRole, StaffUsername, staffPassword));
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

                string filePath = Path.GetFullPath("StaffDB.csv");

                try
                {
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

