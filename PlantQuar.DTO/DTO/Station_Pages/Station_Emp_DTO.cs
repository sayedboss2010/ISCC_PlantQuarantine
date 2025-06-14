using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Emp_DTO
    {
        public long Id { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public long Station_Id { get; set; }
        public Nullable<long> Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Station_Name { get; set; }
        public string Company_Name { get; set; }
        public string Station_Code { get; set; }
        public string Govern_Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Date_From { get; set; }
        public Nullable<System.DateTime> Date_To { get; set; }
        public Nullable<int> Company_Type_Id { get; set; }
        public string FullName { get; set; }
        public string OutletName { get; set; }

        public string Messige_Error { get; set; }
        public Nullable<long> Station_Emp_ID { get; set; }






    }

}
