using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

//********************************************************************************************************************//
//////////////////////////////////////Author ------ ---- RegistrationDB //////////////////////////////
//********************************************************************************************************************//

namespace FinalProjectDotNet
{
    class RegistrationDB
    {
        private static SqlConnection conn = new SqlConnection(@"Data Source=alice.humber.ca,9091;Initial Catalog=vshr0019DB;Persist Security Info=True;User ID=vshr0019;Password=VR-822345898");

        public static bool RegisterStudent(Student stud, Course course)
        {
            try
            {
                conn.Open();
                SqlCommand insertCommand = conn.CreateCommand();
                insertCommand.Connection = conn;
                insertCommand.CommandText = "INSERT INTO Grade(StudentId,CourseId) VALUES (@StudentId, @CourseId)";
                insertCommand.Parameters.AddWithValue("@CourseId", course.CourseID);
                insertCommand.Parameters.AddWithValue("@StudentId", stud.Studid);
                insertCommand.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.Out.WriteLine("Error:@RegisterStudent " + ex.Message + "\nRecord not added");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool UnRegisterStudent(Course course) //student obj not needed hence omitted from use
        {
            try
            {
                conn.Open();
                SqlCommand insertCommand = conn.CreateCommand();
                insertCommand.Connection = conn;
                insertCommand.CommandText = "Delete from Grade where CourseId='" + course.CourseID + "'"; ;
                insertCommand.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.Out.WriteLine("Error:@UnRegisterStudent() " + ex.Message + "\nRecord not added");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Course> GetRegisteredCourses(Student stud)
        {
            SqlDataReader reader;
            Course c;
            List<Course> list = new List<Course>();
            try
            {
                conn.Open();
                // load first list box: all courses for which
                // Noa Campbell is registered
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Grade.CourseId, Course.CourseName FROM   Grade INNER JOIN Course ON (Grade.CourseId = Course.CourseId) WHERE  (Grade.StudentId ='" + stud.Studid + "');";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c = new Course();
                    c.CourseID = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                        c.CourseNAme = reader.GetString(1);
                    list.Add(c);
                }
                reader.Close();
                return list;
            }
            catch (SqlException ex)
            {
                 Console.Out.WriteLine("Error:@GetRegisteredCourses() " + ex.Message);
                 return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Course> GetUnRegisteredCourses(Student stud)
        {
            SqlDataReader reader;
            Course c;
            List<Course> list = new List<Course>();
            try
            {
                conn.Open();
                // now load the second list box: all courses for which
                // Noa Campbell is not registered
                SqlCommand cmd = conn.CreateCommand();
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT CourseId, CourseName FROM   Course WHERE  NOT EXISTS (SELECT * FROM GRADE WHERE Grade.StudentId ='" + stud.Studid + "' AND Grade.CourseId = Course.CourseId);";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c = new Course();
                    c.CourseID = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                        c.CourseNAme = reader.GetString(1);
                    list.Add(c);
                }
                reader.Close();
                return list;
            }
            catch (SqlException se)
            {
                Console.Out.WriteLine("Error:@GetUnRegisteredCourses() " + se.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
