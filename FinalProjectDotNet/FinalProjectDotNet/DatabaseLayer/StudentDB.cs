using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

//********************************************************************************************************************//
//////////////////////////////////////Author ------ Sukrit Trivedi ---- StudentDB //////////////////////////////
//********************************************************************************************************************//

namespace FinalProjectDotNet
{
    class StudentDB
    {
        private static SqlConnection conn = new SqlConnection(@"Data Source=alice.humber.ca,9091;Initial Catalog=vshr0019DB;Persist Security Info=True;User ID=vshr0019;Password=VR-822345898");
       
        public static List<Student> GetAllStudents(){

            SqlDataReader reader;
             List<Student> studList = new List<Student>();
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Student;";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student s = new Student();
                    //Province p = new Province();

                    s.Studid = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                    {
                        s.Fname = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        s.Lname = reader.GetString(2);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        s.Address = reader.GetString(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        s.City = reader.GetString(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        s.Province=reader.GetString(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        s.Zip = reader.GetString(6);
                    }
                    if (!reader.IsDBNull(7))
                    {
                        s.Phno = reader.GetString(7);
                    }
                    if (!reader.IsDBNull(8))
                    {
                        s.Dob = reader.GetDateTime(8).ToString();

                    }
                    if (!reader.IsDBNull(9))
                    {
                        s.Email = reader.GetString(9);

                    }
                   

                    studList.Add(s);
                   // listBox1.Items.Add(s);
                    
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.Out.WriteLine("Error: ", ex.Message + "\nTable was not fully loaded");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                
            }
            return studList;
        }

        public static bool AddStudent(Student stud){
            Console.Beep();
            try
            {

                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Student (StudentId,StudentFirstName,StudentLastName,StudentAddress,StudentCity,StudentProvince,StudentZipCode,StudentPhone,StudentDateOfBirth,StudentEmail) VALUES (@StudentId,@StudentFirstName,@StudentLastName,@StudentAddress,@StudentCity,@StudentProvince,@StudentZipCode,@StudentPhone,@StudentDateOfBirth,@StudentEmail)";
                cmd.Parameters.AddWithValue("@StudentId", stud.Studid);
                cmd.Parameters.AddWithValue("@StudentFirstName", stud.Fname);
                cmd.Parameters.AddWithValue("@StudentLastName", stud.Lname);
                cmd.Parameters.AddWithValue("@StudentAddress", stud.Address);
                cmd.Parameters.AddWithValue("@StudentCity", stud.City);
                cmd.Parameters.AddWithValue("@StudentProvince", stud.Province);
                cmd.Parameters.AddWithValue("@StudentZipCode", stud.Zip);
                cmd.Parameters.AddWithValue("@StudentPhone", stud.Phno);
                cmd.Parameters.AddWithValue("@StudentDateOfBirth", stud.Dob);
                cmd.Parameters.AddWithValue("@StudentEmail", "abc@gmail.com");
                cmd.ExecuteNonQuery();
                Console.Beep();
                return true;
            }
            catch (SqlException ex)
            {
                Console.Out.WriteLine("Error: ", ex.Message + "\nStudent not added");
                return false;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();

            }

        }

        public static bool DeleteStudent(Student stud)
        {
            string deletegradeStatement = "Delete from Grade where StudentId='" + stud.Studid + "';";

            String deletestudentStatement = "Delete from Student where StudentId='" + stud.Studid + "';";
             try
                {
                    
                    conn.Open();
                    SqlCommand updateCommand = conn.CreateCommand();
                    updateCommand.Connection = conn;
                    updateCommand.CommandText = deletegradeStatement;
                   
                    updateCommand.CommandText = deletestudentStatement;
                    
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

        public static bool UpdateStudent(Student stud)
        {
            string updateStatement = "UPDATE  Student SET  StudentFirstName='" + stud.Fname + "'" + ", StudentLastName='" + stud.Lname + "'" + ", StudentAddress='" + stud.Address + "'" + ", StudentCity='" + stud.City + "'" + ", StudentProvince='" + stud.Province + "'" + ", StudentZipCode='" + stud.Zip + "'" + ", StudentPhone='" + stud.Phno + "'" + ", StudentDateOfBirth='" + stud.Dob + "'" + ", StudentEmail='" + stud.Email + "'" + " WHERE StudentId='" + stud.Studid + "'";

            try
            {
                conn.Open();
                SqlCommand updateCommand = conn.CreateCommand();
                updateCommand.Connection = conn;
                updateCommand.CommandText = updateStatement;
                
                updateCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Console.Out.WriteLine("Error: @UpdateStudent() " + ex.Message + "\nRecord not added");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
