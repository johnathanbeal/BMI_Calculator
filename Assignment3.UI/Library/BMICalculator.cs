using System;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment3.UI.Library
{
    internal class BMICalculator
    {
        private string name;
        
        private double _height;
        private double _weight;

        private UnitTypes unit;
        private Validator valid;



        public BMICalculator()
        {
            valid = new Validator();
        }

        //public Person ProcessBmiInput(string firstName, string lastName, string height, string inches, string weight, bool? metric)
        //{
        //    //Person invalidInput = new Person() { BMI = null, Status = "Please try again with valid input" };

        //    inches = ConvertEmptyToZero(inches);

        //    //var _height = HeightSubProcess(height, inches, metric);
        //    //if (_height == null)
        //    //    return invalidInput;
        //    //var _weight = Input.ReadDoubleFromWpfTextbox(weight);
        //    //if (_weight == null)
        //    //    return invalidInput;

        //    //bool inchesValid = ValidateInputIsNotGreaterThanTwelve(inches);
        //    //if (!inchesValid)
        //    //    return invalidInput;

        //    //bool heightIsValid = ValidateHeightIsWithinAcceptableRange(_height, (bool)metric);
        //    //if (!heightIsValid)
        //    //    return invalidInput;

        //    //Person validInput = new Person(firstName, lastName, _height, _weight);

        //    //if ((bool)metric)
        //    //{
        //    //    validInput.BMI = CalculateMetricBMI(_height, _weight);
        //    //    validInput.NormalWeightHigh = ReverseCalculateHighWeightBMI(_height);
        //    //    validInput.NormalWeightLow = ReverseCalculateLowWeightBMI(_height);
        //    //}
        //    //else
        //    //{
        //    //    validInput.BMI = CalculateAmericanBMI(_height, _weight);
        //    //    validInput.NormalWeightHigh = ReverseCalculateAmericanHighWeightBMI(_height);
        //    //    validInput.NormalWeightLow = ReverseCalculateAmericanLowWeightBMI(_height);
        //    //}
        //    //validInput.Status = CalculateNutritionalStatusFromBMI(validInput.BMI);
        //    //if((firstName == "" && lastName == ""))
        //    //{
        //    //    validInput.Name = "Thomas Anderson";
        //    //}
            
        //    //return validInput;
        //}

        public string GetName(string firstname, string lastname)
        {
            name = firstname + " " + lastname;
            return name;
        }

        public Tuple<double, double> GetHeightAndWeight(string feetOrInches, string inches, string weight, bool metric)
        {
            _height = (double)ProcessHeight(feetOrInches, inches, metric);
            _weight = (double)ProcessWeight(weight);
            return new Tuple<double, double>(_height, _weight);
        }

        public double? ProcessWeight(string weight)
        {
            var _weight = Input.ReadDoubleFromWpfTextbox(weight);
            return _weight;
        }

        public bool CalculateIfWeightAndHeightAreNotNull(double? _weight, double? _height)
        {
            if (_weight == null || _height == null)
            {
                Input.ReadErrorMessageOnce();
                return false;
            }
            else
            {
                return true;
            }
        }

        private string ConvertEmptyToZero(string input)
        {
            Regex regexCharacters = new Regex("[A-Za-z0-9 _.,!\"'/$]*");
            Regex regexNumbers = new Regex("^[0 - 9]*");
            bool isNumber = regexNumbers.IsMatch(input);

            if (isNumber)
            {
                return input;
            }
            else if (input == "")
            {                
                return "0";
            }
            else if (!isNumber)
            {
                Input.ReadErrorMessageOnce();
                return "not a number";
            }
            else
            {
                return "unknown";
            }
        }

        public double? ProcessHeight(string height, string inches, bool? metric)
        {
            if (!(bool)metric)
            {
                inches = ConvertEmptyToZero(inches);
                var validInches = valid.ValidateInputIsNotGreaterThanTwelve(inches);
                if (!validInches)
                    return null;
            }

            var subProHeight = HeightSubProcess(height, inches, metric);
            if (subProHeight == null)
                return null;

            var validHeightRange = valid.ValidateHeightIsWithinAcceptableRange(subProHeight, (bool)metric);
            if (!validHeightRange)
                return null;

            if (subProHeight == null)
                return null;
            
            if (subProHeight != null)
                return subProHeight;
            else
                return null;
        }

        public double? HeightSubProcess(string height, string inches, bool? metric)
        {
            int? usHeight = 0;

            if (!(bool)metric)
            {
                usHeight = ((Input.ParseIntegerInput(height)) * 12);
                if (usHeight != null)
                {
                    usHeight = usHeight + (Input.ParseIntegerInput(inches));
                }
            }            
            
            var metricHeight = Input.ReadDoubleFromWpfTextbox(height);

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

        public void ResetErrorCounter()
        {
            Input.ResetCounter();
        }

    }
}
