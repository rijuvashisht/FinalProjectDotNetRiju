using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FinalProjectDotNet
{
    public class Province
    {
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public override string ToString()
        {
            return ProvinceName;
        }
    }
    public class Provinces
    {
        public List<Province> GetProvinces()
        {
            List<Province> provinces = ProvinceDB.GetProvinceList();

            return provinces;
        }
    }
   
}
