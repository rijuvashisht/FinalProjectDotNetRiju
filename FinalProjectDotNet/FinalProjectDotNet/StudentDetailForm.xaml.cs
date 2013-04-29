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

namespace FinalProjectDotNet
{
    /// <summary>
    /// Interaction logic for StudentDetailForm.xaml
    /// </summary>
    public partial class StudentDetailForm : Window
    {

        public Mode mode;
        public Student st;
        public bool formResult = false;
        Province p;

        public StudentDetailForm(Mode mode, Student st)
        {
            InitializeComponent();
            this.mode = mode;
            this.st = st;

        }

        public void setValues(Mode mode, Student st)
        {
           
            if (mode == Mode.Add)
            {
                try
                {
                    st.Studid = Int32.Parse(txtId.Text);
                }
                catch (FormatException ex)
                {
                }
                st.Fname = txtfname.Text;
                st.Lname = txtlname.Text;
                st.Address = txtaddress.Text;
                st.City = txtcity.Text;
                st.Province = cmbprovince.Text;
                st.Zip = txtzip.Text;
                st.Fname = txtfname.Text;
                st.Fname = txtfname.Text;
                st.Fname = txtfname.Text;
                try
                {
                    st.Dob = txtdob.DisplayDate.ToString();
                }
                catch (Exception ex)
                {
                }

                st.Phno = txtphone.Text;

               
            }
            else if (mode == Mode.Delete)
            {
            }
            else if (mode == Mode.Update)
            {
            }
        }


        private void CloseStudentDetailForm(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }



        private void ok_Click(object sender, RoutedEventArgs e)
        {
        }



        private void TextBox_Error_1(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());

            }
            else
            {
                System.Diagnostics.Trace.WriteLine("Validation Error Cleared");

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setValues(mode,st);
            MessageBox.Show("set values called");
        }

        private void ok_Click_1(object sender, RoutedEventArgs e)
        {

            if (mode == Mode.Add)
            {
                bool result = StudentDB.AddStudent(st);
                if (result)
                {
                    formResult = true;
                    new MainWindow().listboxStudent.Items.Add(st);
                }
                else
                {
                    formResult = false;
                    MessageBox.Show("Student is not added due to database problem");
                }
            }
            else if (mode == Mode.Delete)
            {
                bool result = StudentDB.DeleteStudent(st);
                if (result)
                {
                    formResult = true;
                    new MainWindow().listboxStudent.Items.Remove(st);
                }
                else
                {
                    formResult = false;
                    MessageBox.Show("Student is not added due to database problem");
                }
            }
            else if (mode == Mode.Update)
            {
                bool result = StudentDB.UpdateStudent(st);
                if (result)
                {
                    formResult = true;

                }
                else
                {
                    formResult = false;
                    MessageBox.Show("Student is not added due to database problem");
                }
            }

        }






    }
    public class NullStringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            string str = value.ToString();
            if (str.Equals("") || str.Equals(null))
            {
                return new ValidationResult(false, "All fields are mandatory");


            }

            return new ValidationResult(true, "Validation lifted");

        }
    }

    /// <summary>
    /// Check if it is only number
    /// </summary>
    public class NumberOnly : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string number = value.ToString();
            string numpattern = "^[0-9]{8}$";


            Regex regularExpression = new Regex(numpattern);

            if (!(regularExpression.IsMatch(number)))
            {
                return new ValidationResult(false, "Please enter number  in proper format (Ex: 989899299 Only numbers)");
            }


            return new ValidationResult(true, "");
        }
    }
    /// <summary>
    /// Class to validate zip code in canadian format
    /// </summary>
    public class ZipCodeValidation : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string zipCode = value.ToString();
            string canZipPattern = "^[A-Z]{1}[0-9]{1}[A-Z]{1}[0-9]{1}[A-Z]{1}[0-9]{1}$";


            Regex regularExpression = new Regex(canZipPattern);

            if (!(regularExpression.IsMatch(zipCode)))
            {
                return new ValidationResult(false, "Please enter zip code in proper format (Ex: X9X9X9)");
            }


            return new ValidationResult(true, "");

        }
    }

    /// <summary>
    /// Class to validate date of birth, it cannot be in future
    /// </summary>
    public class DateOfBirthValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            DateTime date = (DateTime)value;
            if (date >= DateTime.Today)
            {
                return new ValidationResult(false, "Date of birth should be less than today");
            }
            return new ValidationResult(true, "");
        }
    }

    /// <summary>
    /// Class to validate phone number, it should contain 10 digits, no spaces, parenthesis or dashes
    /// </summary>
    public class PhoneNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string phoneNumber = value.ToString();
            if (phoneNumber.Length != 10)
            {
                return new ValidationResult(false, "Phone number should consist 10 digits");
            }
            else
            {
                foreach (char c in phoneNumber)
                {
                    if (c < 48 || c > 57)
                    {
                        return new ValidationResult(false, "Phone number should not contain any spaces,"
                            + "dashes or parenthesis");
                    }
                }
            }
            return new ValidationResult(true, "");
        }
    }


}
