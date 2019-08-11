using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//MAKE SURE TO CHANGE THE namespace to the one of yours (if not Assignment2). Look at your
//other files (program.cs) to see which namespace you have.
namespace Assignment3.UI.Library
{

    /// <summary>
    /// This is a class with shared functions that are used to check the user's input
    /// and make sure that it belongs to the class that the calling function needs.
    /// </summary>
    /// <remarks></remarks>
    /// 
    public class Input
    {
        private static int counter = 0;
        public static int? ParseIntegerInput (string wpfInput)
        {
            // Reads from the console until a correct integer is received
            bool goodNumber = false;
            int convertedValue = 0;

                goodNumber = int.TryParse(wpfInput, out convertedValue );

                if (!goodNumber)
                {
                    ReadErrorMessageOnce();
                    return null;
                }             
                    
            return convertedValue;
        }

        public static double? ReadDoubleFromWpfTextbox(string textInput)
        {
            // Reads from the console until a correct decimal is received
            double input = default(double);
            if (double.TryParse(textInput, out input))
            {
                return input;
            }
            else
            {
                ReadErrorMessageOnce();

                return null;
            }
        }

        public static void ReadErrorMessageOnce()
        {
            if (counter < 1)
                MessageBox.Show("Please re-enter weight and/or height");
            counter++;
        }

        public static void ReadErrorMessageOnce(string message)
        {
            if (counter< 1)
                MessageBox.Show(message);
            counter++;
        }

        public static void ResetCounter()
        {
            counter = 0;
        }
    }


}
