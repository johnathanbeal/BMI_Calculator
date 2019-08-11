using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.UI.Library
{
    //I was going to use this class, but then decided against it
    class Person
    {

        private string _firstname;
        private string _lastname;
        private double? _height;
        private double? _weight;

        public string Name
        {
            get { return _firstname + " " + _lastname; }
            set { string[] names = value.Split(' ');
                _firstname = names[0];
                _lastname = names[1];
            }
        }
        public double NormalWeightHigh { get; set; }
        public double NormalWeightLow { get; set; }
        public double? BMI { get; set; }

        public string Status { get; set; }

        public Person()
        {

        }

        public Person(string firstname, string lastname, double? height, double? weight)
        {
            _firstname = firstname;
            _lastname = lastname;
           
            _height = height;
            _weight = weight;
        }

        

    }
}
