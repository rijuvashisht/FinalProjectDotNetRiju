using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace StudentProjectWPF
{
    class StudentValidation
    {
        [ValueConversion(typeof(int), typeof(string))]
        public class IdConversion : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    return String.Format("{0:###-###-###}", Int64.Parse(value.ToString()));
                }
                catch (Exception)
                {
                    return "";
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        [ValueConversion(typeof(int), typeof(string))]
        public class ZipConversion : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    String str = value.ToString();
                    str = str.ToUpper();
                    str = str.Insert(3, " ");
                    return str;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        [ValueConversion(typeof(int), typeof(string))]
        public class PhoneConversion : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    return String.Format("{0:(###) ###-####}", Int64.Parse(value.ToString()));
                }
                catch (Exception)
                {
                    return "";
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        [ValueConversion(typeof(int), typeof(string))]
        public class DOBConversion : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    DateTime dt = (DateTime)value;
                    string month = dt.Month.ToString();
                    string day = dt.Day.ToString();
                    string year = dt.Year.ToString();
                    return day + "-" + month + "-" + year;
                }
                catch (Exception)
                {
                    return "";
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        [ValueConversion(typeof(int), typeof(string))]
        public class EmailConversion : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    String str = value.ToString();
                    str = str.ToLower();
                    return str;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        [ValueConversion(typeof(int), typeof(string))]
        public class AgeCalculation : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    DateTime dt = (DateTime)value;
                    int todayyear = DateTime.Today.Year;
                    int year = dt.Year;
                    int age = todayyear - year;
                    return age.ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class NotNullStringValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                string val = value.ToString();
                if (val == "")
                {
                    return new ValidationResult(false, "Please enter, string cannot be empty");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }

         public class IdValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                Regex regex = new Regex(@"^\\d{9}$");   //^[A-Z]{2}\d{6}$
                if (!regex.IsMatch(value.ToString()) == true)
                {
                    return new ValidationResult(false, "Please enter student id containing only numbers and 9 digits in length");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }

         public class NameValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                Regex regex = new Regex(@"^[a-zA-Z]{0,19}$");
                if (!regex.IsMatch(value.ToString()) == true)
                {
                    return new ValidationResult(false, "Please enter  name containing only letters and length between 1 and 20 without any space between them ");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }

        public class ZipValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                Regex regex = new Regex("^[a-zA-Z][0-9][a-zA-Z][0-9][a-zA-Z][0-9]$");
                if (!regex.IsMatch(value.ToString()) == true)
                {
                    return new ValidationResult(false, "Please enter a valid code");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }
        
        public class PhoneValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                Regex regex = new Regex("^\\d{10}$");
                if (!regex.IsMatch(value.ToString()) == true)
                {
                    return new ValidationResult(false, "Please enter a valid phone number");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }

        public class DOBValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                try
                {
                    DateTime value1 = Convert.ToDateTime(value);
                    DateTime today1 = DateTime.Today;
                    if (value1 >= today1)
                    {
                        return new ValidationResult(false, "Do not enter today's date");
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }
                catch (Exception)
                {
                    return new ValidationResult(false, "Do not enter today's date");
                }
            }
        }

        public class EmailValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                Regex regex = new Regex(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");  
                if (!regex.IsMatch(value.ToString()) == true)
                {
                    return new ValidationResult(false, "Please enter a valid email address");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }
    }
}