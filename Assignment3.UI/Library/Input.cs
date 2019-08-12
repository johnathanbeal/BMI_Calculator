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
        public static int? ParseIntegerInput (string wpfInput, out string message)
        {
            // Reads from the console until a correct integer is received
            bool goodNumber = false;
            int convertedValue = 0;

                goodNumber = int.TryParse(wpfInput, out convertedValue );

                if (!goodNumber)
                {
                    message = "Please re-enter weight and/or height";
                    return null;
                }
                else
                {
                    message = "";
                    return convertedValue;
                }                               
        }

        public static double? ReadDoubleFromWpfTextbox(string textInput, out string message)
        {
            // Reads from the console until a correct decimal is received
            double input = default(double);
            if (double.TryParse(textInput, out input))
            {
                message = "";
                return input;
            }
            else
            {
                message = "Please re-enter weight and/or height";
                return null;
            }
        }
    }
}
