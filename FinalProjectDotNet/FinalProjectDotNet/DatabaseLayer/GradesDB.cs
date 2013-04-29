using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

//********************************************************************************************************************//
//////////////////////////////////////Author ------ Sukrit Trivedi ---- GradesDB //////////////////////////////
//********************************************************************************************************************//

namespace FinalProjectDotNet
{
    class GradesDB
    {
        private static SqlConnection conn = new SqlConnection(@"Data Source=alice.humber.ca,9091;Initial Catalog=vshr0019DB;Persist Security Info=True;User ID=vshr0019;Password=VR-822345898");

        public static List<Student> GetStudentList()
        {
            List<Student> list = new List<Student>();
            try
            {
                conn.Open();
                string querryStr = "SELECT distinct g.StudentId,s.StudentFirstName,s.StudentLastName FROM Grade g , Student s where g.StudentId=s.StudentId";
                SqlCommand cmd = new SqlCommand(querryStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Student stud;
                while (reader.Read())
                {
                    stud = new Student();
                    stud.Studid = (int)reader[0];
                    if (!reader.IsDBNull(1))
                        //     g.stud_name = reader.GetString(1);
                        stud.Fname = reader.GetString(1); 
                        stud.Lname= reader.GetString(2);
                    list.Add(stud);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error:@GetStudentsList " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Course> GetCourseList(Student stud)
        {
            SqlDataReader reader;
            List<Course> course_list = new List<Course>();
            try
            {
                conn.Open();
                string course_querryStr = "SELECT distinct g.CourseId,c.CourseName FROM Grade g , Course c  where g.CourseId=c.CourseId and g.StudentId='" + stud.Studid + "'";
                SqlCommand cmd = new SqlCommand(course_querryStr, conn);
                reader = cmd.ExecuteReader();
                Course course;
                while (reader.Read())
                {
                    course = new Course();
                    course.CourseID = (int)reader[0];
                    if (!reader.IsDBNull(1))
                        course.CourseNAme = reader.GetString(1);

                    course_list.Add(course);
                }
                return course_list;
            }
            catch (SqlException se)
            {
                Console.Out.WriteLine("Error:GetCourseList " + se.Message + "\nProblem while populating course");
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static ArrayList GetGrades(Course course, Student stud)
        {
            ArrayList list = new ArrayList();
            
            try
            {
                conn.Open();
                string course_querryStr = "SELECT MidTermExam, FinalExam, Assignments FROM Grade where CourseId='" + course.CourseID + "' AND StudentId='" + stud.Studid + "'";
                SqlCommand cmd = new SqlCommand(course_querryStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < 3; i++)
			        {
			            list.Add(reader[i]);
			        }
                }
                return list;
            }
            catch (Exception se)
            {
                Console.Out.WriteLine("Error:@GetGrades "+se.Message + "\nProblem while populating grades");
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool UpdateGrades(Course course,Student stud,int midterm, int final, int assignment)
        {
            try
            {
                conn.Open();
                string querryStr = "UPDATE Grade set MidTermExam=@MidTermExam, FinalExam=@FinalExam, Assignments=@Assignments where CourseId='" + course.CourseID + "' AND StudentId='" + stud.Studid + "'";
                SqlCommand cmd = new SqlCommand(querryStr, conn);
                cmd.Parameters.AddWithValue("@MidTermExam", midterm);
                cmd.Parameters.AddWithValue("@FinalExam", final);
                cmd.Parameters.AddWithValue("@Assignments", assignment);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error:@UpdateGrades "+ex.Message + "\nProblem while insserting grades to database");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
