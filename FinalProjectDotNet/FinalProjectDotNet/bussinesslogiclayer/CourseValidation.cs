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
    class CourseValidation
    {
        [ValueConversion(typeof(int), typeof(string))]
        public class CourseNameConversion : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    String str = value.ToString();
                    str = str.ToUpper();
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

        public class CourseIdValidation : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                Regex regex = new Regex(@"^\\d{3}$");   
                if (!regex.IsMatch(value.ToString()) == true)
                {
                    return new ValidationResult(false, "Please enter course id containing only numbers and 3 digits in length");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }
    }
}
