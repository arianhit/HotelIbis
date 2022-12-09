using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelIbis
{
    public class OutputOperators
    {
        public void outPutError(string message)
        {
            ConsoleColor beforeRed = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR]: ");
            Console.ForegroundColor = beforeRed;
            Console.WriteLine(message);

        }
        public void makeItColor(ConsoleColor color,string text)
        {
            ConsoleColor beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = beforeColor;
        }
        
    }
}
