using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FinalProjectDotNet
{
   public class Course
    {
        private int courseId;
        private String courseName;

        public Course()
        {
        }

        public Course(int courseId, string courseName)
        {
            this.courseId = courseId;
            this.courseName = courseName;
        }
        public int CourseID { 
            get 
            { 
                return courseId;
            }
            set 
            { courseId = value;
            }
        }
        public String CourseNAme { 
            get { 
                return courseName; }
            set
            {
                 courseName=value;
            }
        }

       public override String ToString()
       {
           return courseName;

       }
    }

   public class Courses
   {
       public List<Course> GetStudents()
       {
           List<Course> courses = CourseDB.GetAllCourses();

           return courses;
       }
   }
}
