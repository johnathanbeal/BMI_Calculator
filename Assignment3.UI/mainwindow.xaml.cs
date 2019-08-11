using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assignment3.UI.Library;

namespace Assignment3.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();
            InitializeGui();
        }
        
        private void InitializeGui()
        {
            if((bool)NonMetric_RadioBtn.IsChecked)
            {
                Height_Input_Inches.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Height_Input_Inches.Visibility = System.Windows.Visibility.Hidden;
            }

            FirstName_Input.Text = "";
            LastName_Input.Text = "";
            NonMetric_RadioBtn.IsChecked = true;
            Height_Input.Text = "";
            Height_Input_Inches.Text = "";
            Weight_Input.Text = "";
            NormalWeightLabel.Content = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var personBMI = bmicalc.ProcessBmiInput(FirstName_Input.Text, LastName_Input.Text, Height_Input.Text, Height_Input_Inches.Text, Weight_Input.Text, Metric_RadioBtn.IsChecked); ;
            BMICalculator bmicalc = new BMICalculator();
            var feetOrMeters = Height_Input.Text;
            var inches = Height_Input_Inches.Text;
            var metric = Metric_RadioBtn.IsChecked;
            var unit = bmicalc.GetUnitType((bool)metric);
            double? weight = null;
            double? height;
            double? bmi = null;
            string status = "";
            string range = "enter valid height and weight to display range";
            string name = FirstName_Input.Text + " " + LastName_Input.Text;
            if ((name.Trim() == "") || (name == null))
            {
                name = "Jan Alleman";
            }
            ResultsGroupBox.Header = "Results: " + name;
            height = bmicalc.ProcessHeight(feetOrMeters, inches, metric);
            weight = bmicalc.ProcessWeight(Weight_Input.Text);

            if (bmicalc.CalculateIfWeightAndHeightAreNotNull(weight, height))
            {               
                bmi = bmicalc.CalculateBMI(height, weight, unit);
                status = bmicalc.CalculateNutritionalStatusFromBMI(bmi);
                range = bmicalc.ProcessNormalBMIRange(height, unit, status); 
            }
            else
            {
                Height_Input.Text = "";
                Height_Input_Inches.Text = "";
                if (weight == null)
                {
                    Weight_Input.Text = "";
                }
            }

            Bmi_Results.Content = bmi;
            Bmi_Category.Content = status;

            NormalWeightLabel.Content = range;
            bmicalc.ResetErrorCounter();
        }

        private void NonMetric_RadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            //For some reason each time I tried to put code inside this method an error was thrown
        }

        private void Metric_RadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            Height_Input_Inches.Visibility = System.Windows.Visibility.Hidden;
            Height_Input_Inches_Label.Visibility = System.Windows.Visibility.Hidden;
            Height_Input_Inches_Label.Content = "";
            Height_Input.Text = "";
            Height_Input_Inches.Text = "";
            Weight_Input.Text = "";
            NormalWeightLabel.Content = "";
            ResultsGroupBox.Header = "";
            Bmi_Results.Content = "";
            Bmi_Category.Content = "";
        }

        private void NonMetric_RadioBtn_Click(object sender, RoutedEventArgs e)
        {
            Height_Input_Inches.Visibility = System.Windows.Visibility.Visible;
            Height_Input_Inches_Label.Visibility = System.Windows.Visibility.Visible;
            Height_Input_Inches_Label.Visibility = System.Windows.Visibility.Visible;
            Height_Input_Inches_Label.Content = "Inches";
            Height_Label.Content = "Feet";
            Weight_Label.Content = "Pounds";
            Weight_Input.Text = "";
            Height_Input.Text = "";
            Height_Input_Inches.Text = "";
            ResultsGroupBox.Header = "";
            Bmi_Results.Content = "";
            Bmi_Category.Content = "";
            NormalWeightLabel.Content = "";
        }

        private void Metric_RadioBtn_Click(object sender, RoutedEventArgs e)
        {
            Height_Input_Inches.Visibility = System.Windows.Visibility.Hidden;
            Height_Input_Inches_Label.Visibility = System.Windows.Visibility.Hidden;
            Height_Input_Inches_Label.Content = "";
            Height_Label.Content = "Meters";
            Weight_Label.Content = "Kilograms";
            ResultsGroupBox.Header = "";

        }

        private void FirstName_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LastName_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Weight_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Height_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
