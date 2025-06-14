 
using System;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class EmployeeDTO
    {
        public long Employee_Id { get; set; }
        public Nullable<decimal> Employee_no { get; set; }
        public string Employee_name { get; set; }
        public bool ISAdmin { get; set; }
    }
}
