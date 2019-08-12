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
        
        public bool ValidateInputIsNotGreaterThanTwelve(string input, out string message)
        {
            if (Input.ParseIntegerInput(input, out message) >= 12)
            {
                message = "Invalid input. Inches should be below 12.  Please try again: ";
                return false;
            }
            else
            {
                message = "";
                return true;
            }
        }

        public bool ValidateWeight(double? _weight, bool metric, out string message)
        {
            double heaviestManUs = 1400;
            double heaviestManMetric = 650;
            string tooHeavyMessage = "Weight is too heavy";

            if (metric && _weight > heaviestManMetric)
            {
                message = tooHeavyMessage;
                return false;
            }
            else if (!metric && _weight > heaviestManUs)
            {
                message = tooHeavyMessage;
                return false;
            }
            else
            {
                message = "Weight is within acceptable parameters";
                return true;
            }         
        }

        public bool ValidateHeightIsWithinAcceptableRange(double? _height, bool _metric, out string message)
        {
            double TallestManMetric = 2.72;
            double TallestManNonMetric = (8 * 12) + 11.1;

            if ((bool)_metric && _height > TallestManMetric)
            {
                message = "Invalid input.  Height Entry is Invalid.  Please try again: ";
                
                return false;
            }

            if (!(bool)_metric && _height > TallestManNonMetric)
            {
                message = "Invalid input.  Height Entry is Invalid.  Please try again: ";
                return false;
            }

            message = "";
            return true;
        }
    }
}
