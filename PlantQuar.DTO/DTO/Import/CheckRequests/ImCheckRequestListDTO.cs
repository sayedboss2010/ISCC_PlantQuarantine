using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
   public class ImCheckRequestListDTO
    {
        public long row_number { get; set; }
        public long? Outlet_ID { get; set; }
        public long Im_CheckRequest_ID { get; set; }
        public long? CommitteeID { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public string ImCheckRequest_Number { get; set; }
        public System.DateTime Creation_Date { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public long Importer_ID { get; set; }
        public int ImporterType_Id { get; set; }
        // public short ExportCountry_Id { get; set; }
       
        public string ExportCountryName { get; set; }
        public string ImporterTypeName { get; set; }
        public string ImporterName { get; set; }

        public bool Is_Finch_All { get; set; }
        public Nullable<bool> Closed_Request { get; set; }

        public Nullable<int> Im_Final_Result_ID { get; set; }

        public string Name_Final_Result { get; set; }
        public string Current_Status { get; set; }

        public Nullable<System.DateTime> User_Creation_Date { get; set; }

        public Nullable<long> Im_CheckRequest_Final_Result_ID { get; set; }


    }
}
