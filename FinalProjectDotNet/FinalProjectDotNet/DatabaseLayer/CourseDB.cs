using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

//********************************************************************************************************************//
//////////////////////////////////////Author ------ Sukrit Trivedi ----  CourseDB//////////////////////////////
//********************************************************************************************************************//

namespace FinalProjectDotNet
{
    class CourseDB
    {
        private static SqlConnection conn = new SqlConnection(@"Data Source=alice.humber.ca,9091;Initial Catalog=vshr0019DB;Persist Security Info=True;User ID=vshr0019;Password=VR-822345898");

        public static List<Course> GetAllCourses()
        {
            SqlDataReader reader;
            List<Course> list= new List<Course>();
            try
            {
                conn.Open();
                SqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.Connection = conn;
                sqlCmd.CommandText = "select CourseId, CourseName from Course";

                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    Course cr = new Course();
                    cr.CourseID = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                        cr.CourseNAme = reader.GetString(1);

                    list.Add(cr);
                }
                reader.Close();
                return list;
            }

            catch (SqlException ex)
            {
                Console.Out.WriteLine(ex.Message);
                return null;
            }

            finally
            {
                conn.Close();
            }
        }

        public static bool AddCourse(Course course)
        {
            string insertStatement = "INSERT INTO Course (CourseId, CourseName)"
                                     + "VALUES (@CourseId, @CourseName)";
            try
            {
                conn.Open();
                SqlCommand insertCommand = conn.CreateCommand();
                insertCommand.Connection = conn;
                insertCommand.CommandText = insertStatement;
                insertCommand.Parameters.AddWithValue("@CourseId", course.CourseID);
                insertCommand.Parameters.AddWithValue("@CourseName", course.CourseNAme);
                insertCommand.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.Out.WriteLine("Error: @AddCourse():Course \n " + ex.Message + "\nRecord not added");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool DeleteCourse(Course course)
        {
           
            try
            {
                conn.Open();
                SqlCommand updateCommand = conn.CreateCommand();
                updateCommand.Connection = conn;
                updateCommand.CommandText = "Delete from Course where CourseId='" + course.CourseID + "';";
                
                updateCommand.ExecuteNonQuery();
                return true;

            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool UpdateCourse(Course course)
        {
            try
            {
                conn.Open();
                SqlCommand updateCommand = conn.CreateCommand();
                updateCommand.Connection = conn;
                updateCommand.CommandText = "UPDATE  Course SET  CourseName='" + course.CourseNAme + "'" + " WHERE CourseId='" + course.CourseID + "'";;
                updateCommand.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.Out.WriteLine("Error: @UpdateCourse() \n " + ex.Message + "\nRecord not added");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
