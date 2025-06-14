 
using System;

namespace PlantQuar.DTO.DTO.Station
{
    public class EmployeeDTO
    {
        public decimal Employee_Id { get; set; }
        public Nullable<decimal> Employee_no { get; set; }
        public string Employee_name { get; set; }
        public bool ISAdmin { get; set; }
    }
}
