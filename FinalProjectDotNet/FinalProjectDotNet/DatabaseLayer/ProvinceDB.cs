using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


//********************************************************************************************************************//
//////////////////////////////////////Author ------ Riju Vashisht---------- //////////////////////////////
//********************************************************************************************************************//

namespace FinalProjectDotNet
{
    class ProvinceDB
    {
        private static SqlConnection conn = new SqlConnection(@"Data Source=alice.humber.ca,9091;Initial Catalog=vshr0019DB;Persist Security Info=True;User ID=vshr0019;Password=VR-822345898");

        public static List<Province> GetProvinceList()
        {
            SqlDataReader reader;
            Province p;
            List<Province> list = new List<Province>();
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Province";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    p = new Province();
                    p.ProvinceId = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        p.ProvinceName = reader.GetString(1);
                    list.Add(p);
                }
                reader.Close();
                return list;
            }
            catch (SqlException se)
            {
                Console.Out.WriteLine(se.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
