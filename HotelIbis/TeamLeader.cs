using Ibis_Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelIbis
{
    //inheritance class of staff
    public class TeamLeader : Staff
    {

        OutputOperators o = new OutputOperators();
        string path = "Audit.txt";
       
        IFormatter formatter = new BinaryFormatter();
        //As this function is for audit and we want to record every single action even when program crashed
        //every action that staff does record 
        //otherwise I could run this functon with all action when user finished his work so there was no need of doing a same thing many times
        public void RecordStaff(string operation, string userFullName)
        {
            DateTime now = DateTime.Now;
            //get the current staff name
            string currentStaffName = userFullName;
            List<AuditStaff> staffActions = new List<AuditStaff>();

            try
            {
                //How to read and write without file is procesesing in another program error (use using)
                //https://stackoverflow.com/questions/26741191/ioexception-the-process-cannot-access-the-file-file-path-because-it-is-being
                //Deserialize all data from before in the file and store them as a list of Audit staff object 
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    staffActions = (List<AuditStaff>)formatter.Deserialize(stream);
                    stream.Close();
                }


                //close the file
                File.Delete(path);

                //Record the action as a Audit staff object and add it to the list
                AuditStaff aus = new AuditStaff(operation, currentStaffName, now);
                staffActions.Add(aus);

                //Serialize all data which recorded and write them on a file
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

                List<AuditStaff> staffActions = new List<AuditStaff>();

                //Serialize all data which recorded and store them to the list of audit staff 
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    staffActions = (List<AuditStaff>)formatter.Deserialize(stream);
                    stream.Close();
                }
                //output each record on console
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

    }
}

