using PlantQuar.DTO.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class Ex_ListDTO
    {
        public Nullable<long> Outlet_User_ID { get; set; }
        public string Outlet_User_Name { get; set; }
        public Nullable<short> Center_ID { get; set; }
        public long Ex_CheckRequest_ID { get; set; }
        public string ImCheckRequest_Number { get; set; }
        public Nullable<System.DateTime> Creation_Date { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ExportCountryName { get; set; }
        public long Importer_ID { get; set; }
        public int ImporterType_Id { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public string ImporterTypeName { get; set; }
        public string ImporterName { get; set; }
        public Nullable<short> f { get; set; }
        public long g { get; set; }
        public Nullable<long> Outlet_Examination_ID { get; set; }
        public string Outlet_Examination_Name { get; set; }
        public Nullable<long> Station_Examination_ID { get; set; }
        public Nullable<long> Outlet_Genshi_ID { get; set; }
        public string Outlet_Genshi_Name { get; set; }
        public Nullable<long> Station_Genshi_ID { get; set; }
        public Nullable<int> Closed_Request { get; set; }
        public int Final_Result_ID { get; set; }
        public string Final_Result_Name { get; set; }
        public string Station_Examination_Name { get; set; }
        public string Station_Genshi_Name { get; set; }
        public int? TotalResults { get; set; }
        public int? TotalPages { get; set; }
        //public List<SearchFilterDTO> SearchFilterDTO { get; set; }
    }
}
