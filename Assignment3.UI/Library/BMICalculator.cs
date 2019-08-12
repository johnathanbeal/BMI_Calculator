using System;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment3.UI.Library
{
    internal class BMICalculator
    {
       
        private Validator valid;
  
        public BMICalculator()
        {
            valid = new Validator();
        }

        public double? ProcessWeight(string weight, bool metric, out string proWeightMessage)
        {
            string readDoubleMessage;
            var _weight = Input.ReadDoubleFromWpfTextbox(weight, out readDoubleMessage);
            proWeightMessage = readDoubleMessage;
            string validWeightMessage;

            var validWeight = valid.ValidateWeight(_weight, metric, out validWeightMessage);
            if (!validWeight)
            {
                proWeightMessage = validWeightMessage;
                return null;
            }

            return _weight;
        }

        public bool CalculateIfWeightAndHeightAreNotNull(double? _weight, double? _height)
        {
            if (_weight == null || _height == null)
            {
                //Input.ReadErrorMessageOnce();
                return false;
            }
            else
            {
                return true;
            }
        }

        private string ConvertEmptyToZero(string input, out string convertEmptyToZeroMessage)
        {
            Regex regexCharacters = new Regex("[A-Za-z0-9 _.,!\"'/$]*");
            Regex regexNumbers = new Regex("^[0 - 9]*");
            bool isNumber = regexNumbers.IsMatch(input);

            if (isNumber && input != "")
            {
                convertEmptyToZeroMessage = "string is a number";
                return input;
            }
            else if (input == "")
            {
                convertEmptyToZeroMessage = "string was empty and has been converted to a 0";
                return "0";
            }
            else if (!isNumber)
            {
                convertEmptyToZeroMessage = "input cannot be converted to a number";
                return "";
            }
            else
            {
                convertEmptyToZeroMessage = "input cannot be converted to a number";
                return "";
            }
        }

        public double? ProcessHeight(string height, string inches, bool? metric, out string message)
        {
            if (!(bool)metric)
            {
                string convertEmptyToZeroMessage;
                inches = ConvertEmptyToZero(inches, out convertEmptyToZeroMessage);
                if (inches == "" || convertEmptyToZeroMessage == "input cannot be converted to a number")
                {
                    message = convertEmptyToZeroMessage;
                    return null;
                }

                string validateInputLessThanTwelve;
                var validInches = valid.ValidateInputIsNotGreaterThanTwelve(inches, out validateInputLessThanTwelve);
                if (!validInches)
                {
                    message = validateInputLessThanTwelve;
                    return null;
                }
            }

            string _heightSubProMessage;
            var subProHeight = HeightSubProcess(height, inches, metric, out _heightSubProMessage);
            message = _heightSubProMessage;
            if (subProHeight == null)
            {            
                return null;
            }

            string validHeightRangeMessage;
            var validHeightRange = valid.ValidateHeightIsWithinAcceptableRange(subProHeight, (bool)metric, out validHeightRangeMessage);
            message = validHeightRangeMessage;
            if (!validHeightRange)
                return null;

            if (subProHeight != null)
                return subProHeight;
            else
                return null;
        }

        public double? HeightSubProcess(string height, string inches, bool? metric, out string heightSubProMessage)
        {
            int? usHeight = 0;

            if (!(bool)metric)
            {
                string footOrMeterMessage;
                usHeight = ((Input.ParseIntegerInput(height, out footOrMeterMessage)) * 12);
                if (usHeight != null)
                {
                    string inchesMessage;
                    usHeight = usHeight + (Input.ParseIntegerInput(inches, out inchesMessage));
                    heightSubProMessage = footOrMeterMessage + inchesMessage;
                }
                else
                {
                    heightSubProMessage = footOrMeterMessage;
                }
            }

            string metricMessage;
            var metricHeight = Input.ReadDoubleFromWpfTextbox(height, out metricMessage);
            heightSubProMessage = metricMessage;
            if (!(bool)metric && usHeight != null)
            {              
                return usHeight;
            }
            else if ((bool)metric && metricHeight != null)
            {
                return metricHeight;
            }
            else return null;
        }

        public double? CalculateBMI(double? height, double? weight, UnitTypes unitType)
        {
            if (unitType == UnitTypes.Metric)
            {
                return CalculateMetricBMI(height, weight);
            }
            else if (unitType == UnitTypes.American)
            {
                return CalculateAmericanBMI(height, weight);
            }
            else
            {
                return null;
            }
        }

        private double CalculateAmericanBMI(double? heightUSA, double? weightUSA)
        {
            double? bmiUSA =  703 * (weightUSA / (heightUSA * heightUSA));
            return Math.Round(Convert.ToDouble(bmiUSA), 1);
        }

        private double CalculateMetricBMI(double? heightMetric, double? weightMetric)
        {
            double? bmiWorld = weightMetric / (heightMetric * heightMetric);
            return Math.Round(Convert.ToDouble(bmiWorld), 1);
        }

        public string ProcessNormalBMIRange(double? height, UnitTypes unit, string status)
        {
            string invalidInputResponse = "Please try again with valid input";

            var range = CalculateNormalBMIRange(height, unit);

            if (range.Item2 > 0 && range.Item1 > 0 && status != invalidInputResponse)
            {
                return "Normal weight should be between " + range.Item2 + " and " + range.Item1 + ".";
            }
            else
            {
                return "";
            }
        }

        public Tuple<int,int> CalculateNormalBMIRange(double? height, UnitTypes unitType)
        {
            int high;
            int low;

            if (unitType == UnitTypes.Metric)
            {
                high = ReverseCalculateHighWeightBMI(height);
                low = ReverseCalculateLowWeightBMI(height);
            }
            else if (unitType == UnitTypes.American)
            {
                high = ReverseCalculateAmericanHighWeightBMI(height);
                low = ReverseCalculateAmericanLowWeightBMI(height);
            }
            else
            {
                high = 0;
                low = 0;
            }

            return new Tuple<int, int>(high, low);
        }

        private int ReverseCalculateAmericanHighWeightBMI(double? heightUSA)
        {
            double? weightUSA = 24.9 * (heightUSA * heightUSA) / 703;
            return Convert.ToInt32(weightUSA);
        }

        private int ReverseCalculateAmericanLowWeightBMI(double? heightUSA)
        {
            double? weightUSA = 18.5 * (heightUSA * heightUSA)/703;
            return Convert.ToInt32(weightUSA);
        }

        private int ReverseCalculateHighWeightBMI(double? heightMetric)
        {
            double? weightMetric = 24.9 * (heightMetric * heightMetric);
            return Convert.ToInt32(weightMetric);
        }

        private int ReverseCalculateLowWeightBMI(double? heightMetric)
        {
            double? weightMetric = 18.5 * (heightMetric * heightMetric);
            return Convert.ToInt32(weightMetric);
        }

        public string CalculateNutritionalStatusFromBMI(double? bmi)
        {
            if(bmi < 18.5)
            {
                return "Underweight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                return "Normal weight";
            }
            else if (bmi >= 25.0 && bmi <= 29.9)
            {
                return "Overweight (Pre-obesity)";
            }
            else if (bmi >= 30.0 && bmi < 34.9)
            {
                return "Overweight (Obesity class 1)";
            }
            else if (bmi >= 35.0 && bmi <= 39.9)
            {
                return "Overweight (Obesity class 2)";
            }
            else if (bmi > 40)
            {
                return "Overweight (Obesity class 3)";
            }
            else
            {
                return "Unknown input";
            }
        }

        public UnitTypes GetUnitType(bool metric)
        {
            if (metric)
                return UnitTypes.Metric;
            else
                return UnitTypes.American;
        }

    }
}
