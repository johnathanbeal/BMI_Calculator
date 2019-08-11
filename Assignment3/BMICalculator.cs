using System;


namespace Assignment3
{
    public class BMICalculator
    {
        private string name = "Jason Bourne";
        private double height = 0;
        private double weight = 0;
        private UnitTypes unit;

        private double CalculateAmericanBMI(double heightUSA, double weightUSA)
        {
            double bmiUSA =  703 * (weightUSA / (heightUSA * heightUSA));
            return Math.Round(bmiUSA, 1);
        }

        private double CalculateMetricBMI(double heightMetric, double weightMetric)
        {

        }

        private string CalculateNutritionalStatusFromBMI(double bmi)
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
    }
}
