using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Pallet_Export_CheckRequest
{
    public class Pallet_EXCheckRequestListDTO
    {
        public long row_number { get; set; }
        public int Final_Result_ID { get; set; }
        public long? Outlet_ID { get; set; }
        public long Ex_CheckRequest_ID { get; set; }
        //public long? CommitteeID { get; set; }
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
        public Nullable<int> Closed_Request { get; set; }
        //public Nullable<byte> Is_Cancel { get; set; }
        //public Nullable<bool> StatusRequestCommittee { get; set; }
        //public Nullable<int> Ex_Final_Result_ID { get; set; }
        //public string Ex_Final_ResultName { get; set; }
        //public long Ex_CertificatesRequests_ID { get; set; }

        public Nullable<int> countPrint { get; set; }

    }
}
