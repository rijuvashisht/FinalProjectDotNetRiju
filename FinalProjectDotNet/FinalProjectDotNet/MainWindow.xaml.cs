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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;

namespace FinalProjectDotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Mode mode;
        Student st;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void radiofirstname_Checked(object sender, RoutedEventArgs e)
        {
            ICollectionView view = GetStudentsfromView();
            if (view.SortDescriptions.Count != 0)
            {

                view.SortDescriptions.RemoveAt(0);
            }
         
             view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Fname", ListSortDirection.Ascending));

        }

        private void radiolastname_Checked(object sender, RoutedEventArgs e)
        {
            ICollectionView view = GetStudentsfromView();
            if (view.SortDescriptions.Count != 0)
            {

                view.SortDescriptions.RemoveAt(0);
            }
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Lname", ListSortDirection.Ascending));
        }

        private void radionone_Checked(object sender, RoutedEventArgs e)
        {

        }
        ICollectionView GetStudentsfromView()
        {
            ICollectionView myView =  CollectionViewSource.GetDefaultView(listboxStudent.ItemsSource);
            return myView;;
        }

        private void OpenStudentDetail(object sender, ExecutedRoutedEventArgs e)
        {
            Student st = new Student();
            StudentDetailForm studentdetail = new StudentDetailForm(Mode.Add,st);
            studentdetail.ShowDialog();
        }

      
        private void ExitStudentDetail(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenCourseDetail(object sender, ExecutedRoutedEventArgs e)
        {
            CourseDetail crs = new CourseDetail();
            crs.ShowDialog();
        }

        private void ExitCourseDetail(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitMainAppExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        //********************************************************************************************************************//
        //////////////////////////////////////Author ------ Sukrit Trivedi ---- Grades Tab ----- Start //////////////////////////////
        //********************************************************************************************************************//


        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            List<Student> studList = new List<Student>();
            studList = GradesDB.GetStudentList();

            foreach (Student stud in studList)
            {
                cmbStudent.Items.Add(stud);
            }
            cmbStudent.SelectedIndex = 0;
            load();
        }
        void load()
        {
            cmbStudent_DropDownClosed(this, null);
        }

        private void cmbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ArrayList list = new ArrayList();
                list = GradesDB.GetGrades(((Course)cmbCourse.SelectedItem), ((Student)cmbStudent.SelectedItem));
                if (list != null)
                {
                    object[] arr = new object[3];

                    arr = list.ToArray();
                    if (!arr.Length.Equals(0))
                    {
                        txtMidTerm.Text = Convert.ToString(arr[0]);
                        txtFinalTerm.Text = Convert.ToString(arr[1]);
                        txtAssignment.Text = Convert.ToString(arr[0]);
                    }
                    txtTotal.Text = (0.3 * Convert.ToInt32(txtMidTerm.Text) + 0.3 * Convert.ToInt32(txtFinalTerm.Text) + 0.4 * Convert.ToInt32(txtAssignment.Text)).ToString();
                }
            }
            catch (NullReferenceException)
            {

            }
            catch (FormatException)
            {
                txtTotal.Text = "";
            }
        }

        private void cmbStudent_DropDownClosed(object sender, EventArgs e)
        {
            cmbCourse.Items.Clear();
            List<Course> courseList = new List<Course>();
            courseList = GradesDB.GetCourseList((Student)cmbStudent.SelectedItem);
            foreach (Course course in courseList)
            {
                cmbCourse.Items.Add(course);
            }
            cmbCourse.SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int assign = Convert.ToInt32(txtAssignment.Text);
                int final = Convert.ToInt32(txtFinalTerm.Text);
                int mid = Convert.ToInt32(txtMidTerm.Text);
                if (assign > 100 || final > 100 || mid > 100)
                {
                    MessageBox.Show("The grades cannot be grater than 100");
                }
                else
                {
                    bool result = GradesDB.UpdateGrades((Course)cmbCourse.SelectedItem, (Student)cmbStudent.SelectedItem, mid, final, assign);
                    if (result)
                    {
                        MessageBox.Show("updated");
                    }
                    else
                    {
                        MessageBox.Show("error");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("invalid input");
            }
        }


        //********************************************************************************************************************//
        //////////////////////////////////////Author ------ Sukrit Trivedi ---- Grades Tab----- END //////////////////////////////
        //********************************************************************************************************************//



        private void Registration_Loaded(object sender, RoutedEventArgs e)
        {
            List<Student> studList = new List<Student>();
            studList = StudentDB.GetAllStudents();

            foreach (Student stud in studList)
            {
                cmbStudent_reg.Items.Add(stud);
            }
            cmbStudent_reg.SelectedIndex = 0;
        }

        private void cmbStudent_reg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxReg.Items.Clear();
            List<Course> regList = new List<Course>();
            regList= RegistrationDB.GetRegisteredCourses((Student)cmbStudent_reg.SelectedItem);
            foreach (Course course in regList)
            {
                listBoxReg.Items.Add(course);
            }
        }
        private void refresh()
        {

        }

        private void listBoxReg_Loaded(object sender, RoutedEventArgs e)
        {
            listBoxUNReg.Items.Clear();
            List<Course> regList = new List<Course>();
            regList = RegistrationDB.GetUnRegisteredCourses((Student)cmbStudent_reg.SelectedItem);
            foreach (Course course in regList)
            {
                listBoxUNReg.Items.Add(course);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listboxStudent.ItemsSource=null;
            loadStudent.IsEnabled = true;
            clearStudent.IsEnabled = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Students students = new Students();
            listboxStudent.ItemsSource = students.GetStudents();
            loadStudent.IsEnabled = false;
            clearStudent.IsEnabled = true;
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            bool isregistered = false;
            Student st=(Student)cmbStudent.SelectedItem;
            Course cs = (Course)listBoxUNReg.SelectedItem;
            if (cs != null)
            {
               
               isregistered= RegistrationDB.RegisterStudent(st, cs);
               
               reloadunreg();
               reloadreg();
            }
            else
            {
                MessageBox.Show("Please select  a student from unregistered courses");
                listBoxReg.Focus();
            }
        }

        private void btnUnReg_Click(object sender, RoutedEventArgs e)
        {
            bool isunregistered = false;
            Student st = (Student)cmbStudent.SelectedItem;
            Course cs = (Course)listBoxReg.SelectedItem;
            if (cs != null)
            {
              
              isunregistered=  RegistrationDB.UnRegisterStudent(cs);
                
              reloadreg();
              reloadunreg();
            }
            else
            {
                MessageBox.Show("Please select  a student from registered courses");
                listBoxUNReg.Focus();
            }
        }
        public void reloadreg()
        {
            listBoxReg.Items.Clear();
            List<Course> regList = new List<Course>();
            regList = RegistrationDB.GetRegisteredCourses((Student)cmbStudent_reg.SelectedItem);
            foreach (Course course in regList)
            {
                listBoxReg.Items.Add(course);
            }
        }

        public void reloadunreg()
        {
            listBoxUNReg.Items.Clear();
            List<Course> regList = new List<Course>();
            regList = RegistrationDB.GetUnRegisteredCourses((Student)cmbStudent_reg.SelectedItem);
            foreach (Course course in regList)
            {
                listBoxUNReg.Items.Add(course);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateStudentDetail(object sender, ExecutedRoutedEventArgs e)
        {
            
            if (listboxStudent.SelectedItem != null)
            {
                StudentDetailForm studentdetail = new StudentDetailForm(Mode.Update, st);
                studentdetail.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a student");
            }
        }

        private void DeleteStudentDetail(object sender, ExecutedRoutedEventArgs e)
        {
            Student st = (Student)listboxStudent.SelectedItem;
            if (listboxStudent.SelectedItem != null)
            {
                StudentDetailForm studentdetail = new StudentDetailForm(Mode.Update, st);
                studentdetail.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a student");
            }
        }

        private void listboxStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            st = (Student)listboxStudent.SelectedItem;
            
        }
 
    }
}
