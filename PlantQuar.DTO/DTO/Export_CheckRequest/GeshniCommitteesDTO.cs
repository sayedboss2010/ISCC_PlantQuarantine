using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class GeshniCommitteesDTO
    {
    
        public Nullable<long> Ex_CheckRequest_ID { get; set; }

        public Nullable<byte> Ex_CommitteeCheckLocation_ID { get; set; }
  
        public long? Station_Examination_ID { get; set; }
        public string Station_Examination { get; set; }
        public long? PortNational_ID { get; set; }

        public string PortNationalName { get; set; }
        public string OutletNameGashni { get; set; }
      
        public string Examination_location { get; set; }
        public string CenterName { get; set; }
        public string GovernName { get; set; }
        public string Station_Geshni_Name { get; set; }
    }
    public class GeshniPortsDTO


    {
        public string Ex_CheckRequest_Number { get; set; }
        public Nullable<int> NewPortGeshni_Id { get; set; }

        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

    }
    public class GeshniStationDTO


    {
        public string Ex_CheckRequest_Number { get; set; }
        public Nullable<long> NewStationGeshni_Id { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

    }
    public class stationsGeashni
    {
        public long IDStation { get; set; }
        public string Ar_Name { get; set; }
        public long Item_ShortName_ID { get; set; }
    }
    public class ExdChechRequestItemGeshni
    {
        public long IDCheckrequest { get; set; }
        public long? Item_ShortName_ID { get; set; }
        public string CheckRequest_Number { get; set; }
    }


    public class EmployeeGeshniChangeDTO
    {

        public Nullable<long> Ex_CheckRequest_ID2 { get; set; }

        public Nullable<long> Emp_ID2 { get; set; }


        public string EmpName2 { get; set; }
        public string Notes2 { get; set; }
        
      public Nullable<System.DateTime> User_Creation_Date2 { get; set; }
        //  public string User_Creation_Date2 { get; set; }
  

    }
}
