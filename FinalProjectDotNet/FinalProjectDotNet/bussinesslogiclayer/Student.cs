using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;


namespace FinalProjectDotNet
{
     
    public class Student:IValueConverter
    {
        /////////////////////---------Author------Riju Vashisht----822345898------(Student class)-------------------/////////////
        private string fname, lname, address, city, zip, phno, email,fullName ;
        Int32 studid;
       String dob;
       String province;

       public Student()
       {

       }
       public Student(string f_name, string l_name, string add_ress, string ci_ty, String prov_ince, string z_ip, String d_ob, string ph_no, Int32 studid, string ema_il)
        {
            this.fname = f_name;
            this.lname = l_name;
            this.address = add_ress;
            this.city = ci_ty;
            this.province = prov_ince;
            this.zip = z_ip;
            this.dob = d_ob;
            this.phno = ph_no;
            this.studid = studid;
            this.email=ema_il;
        }
             public string FullName {

                 get { return fullName; }
                 set { fullName = value; }
             }
        public String Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Lname
        {
            get { return lname; }
            set { lname = value; }
        }

        public string Fname
        {
            get { return fname; }
            set { fname = value; }
        }

        public string Phno
        {
            get { return phno; }
            set { phno = value; }
        }

        public Int32 Studid
        {
            get { return studid; }
            set { studid = value; }
        }

        public String Province
        {
            get { return province; }
            set { province = value; }

        }
      
       
            public string Email
        {
            get { return email ; }
            set { email = value; }
        }

        public override string ToString()
        {
            return fname + " " + lname;
        }



        public Object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            phno = value.ToString();
            if ((phno.Length <= 10))
            {


                try
                {
                    phno = Phno.Insert(3, "-");
                    phno = Phno.Insert(7, "-");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                return "10 digits only";
            }
            return phno;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
    /// <summary>
    /// Class to convert zip code in proper format
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class ZipCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string zipCode = value.ToString();
            if (zipCode.Length == 6)
            {
                zipCode = zipCode.Insert(3, " ");
            }
            return zipCode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

   
   
  

    public class Students
    {
        public List<Student> GetStudents()
        {
            List<Student> students = StudentDB.GetAllStudents();
           
            return students;
        }
    }
}

