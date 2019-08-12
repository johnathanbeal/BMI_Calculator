using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment3.UI.Library
{
    public static class ErrorMessages
    {
        private static int counter = 0;

        public static void ReadErrorMessageOnce(string message)
        {
            if (counter < 1)
                MessageBox.Show(message);
            counter++;
        }

        public static void ResetErrorCounter()
        {
            ResetCounter();
        }

        public static void ResetCounter()
        {
            counter = 0;
        }


    }
}
