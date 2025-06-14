using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Committee_Delete_DTO
    {
        public long Station_Accreditation_Committee_ID { get; set; }
        public string Station_Ar_Name { get; set; }
        public long Station_ID { get; set; }

        public string Station_Code { get; set; }

        public string Station_Accreditation_Data_Name { get; set; }
        public Nullable<bool> Is_Start_Android { get; set; }
        public Nullable<int> Accreditation_Type_ID { get; set; }
        public Nullable<short> Station_Gov_Id { get; set; }

        public Nullable<short> Station_Center_Id { get; set; }
    }

    public class Outlit_Data_DTO
    {
        public int IsExport { get; set; }
        public long Outlet_ID { get; set; }

        public short Outlet_Center_ID { get; set; }

        public short Outlet_Gov_Id { get; set; }
    }

}
