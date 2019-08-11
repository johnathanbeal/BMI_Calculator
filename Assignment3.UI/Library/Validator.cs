using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment3.UI.Library
{
    public class Validator
    {
        
        public bool ValidateInputIsNotGreaterThanTwelve(string input)
        {
            if (Input.ParseIntegerInput(input) >= 12)
            {
                MessageBox.Show("Invalid input. Inches should be below 12.  Please try again: ");
                return false;
            }
            else
            { return true; }
        }

        public bool ValidateHeightIsWithinAcceptableRange(double? _height, bool _metric)
        {
            double TallestManMetric = 2.72;
            double TallestManNonMetric = (8 * 12) + 11.1;

            if ((bool)_metric && _height > TallestManMetric)
            {
                Input.ReadErrorMessageOnce("Invalid input.  Height Entry is Invalid.  Please try again: ");
                
                return false;
            }

            if (!(bool)_metric && _height > TallestManNonMetric)
            {
                Input.ReadErrorMessageOnce("Invalid input.  Height Entry is Invalid.  Please try again: ");
                return false;
            }
            return true;
        }

        

    }
}
