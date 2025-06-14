using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Final_Result_DTO
    {
        public List<Station_Accreditation_Data_ALL_DTO> List_Station_Accreditation_Data_ALL { get; set; }
        public List<Station_Check_Quarantine_DTO> List_Station_Check_Quarantine { get; set; }
        public List<Station_Accreditation_Request_TypeDTO> List_Station_Accreditation_Request_Type { get; set; }
        public List<Station_Check_AdminDTO> List_Station_Check_Admin { get; set; }
        public List<Station_ConfirmDTO> List_Station_Confirm { get; set; }
        public List<Station_Check_ImgeDTO> List_Station_Check_Imge { get; set; }
        public List<Station_Fees_DTO> List_Station_Fees { get; set; }

        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }



        public long Station_ID { get; set; }
        public long Station_Accreditation_Request_ID { get; set; }
        public byte Station_Accreditation_Request_Type_ID { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public Nullable<bool> IsAccepted_Quarantine { get; set; }
        public string Notes_Quarantine { get; set; }
        public Nullable<System.DateTime> StartDate_Quarantine { get; set; }
        public Nullable<System.DateTime> EndDate_Quarantine { get; set; }

        public long Station_Accreditation_Committee { get; set; }
        public string Station_Name { get; set; }
        public string station_Address { get; set; }
        public string StationCode { get; set; }
        public string Governate_Name { get; set; }
        public string Center_Name { get; set; }
        public string Village_name { get; set; }
        
        public string Station_Accreditation_Data_Description { get; set; }
 
        public string Station_CheckList_name { get; set; }
        public long Station_CheckList_ID { get; set; }
        public Nullable<bool> IsAccepted_Band_Admin { get; set; }
        public string Notes_Ar_Band_Admin { get; set; }
        public string Notes_En_Band_Admin { get; set; }
        public Nullable<bool> IsAccepted_final_Admin { get; set; }
        public string Notes_Ar_final_Admin { get; set; }
        public Nullable<System.DateTime> Date_final_Admin { get; set; }
        public Nullable<bool> IsAccepted_Confirm { get; set; }
        public string Notes_Confirm { get; set; }
        public Nullable<System.DateTime> Date_Confirm { get; set; }
      
        public long Station_Committee_ID { get; set; }
        public Nullable<System.DateTime> Station_Committee_Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> Station_Committee_StartTime { get; set; }
        public Nullable<System.TimeSpan> Station_Committee_EndTime { get; set; }
        public byte[] Station_Committee_Imge_Binary { get; set; }
        public Nullable<long> Station_Committee_Imge_ID { get; set; }
        public long Station_ID1 { get; set; }
        public long Committee_ID { get; set; }
        public long Employee_Id { get; set; }
        public bool ISAdmin { get; set; }
        public int OperationType { get; set; }
        public short Id { get; set; }
        public string FullName { get; set; }
        public string Station_Refuse_Reason_Nots { get; set; }
        public string Committee_position { get; set; }
    }

    public class Station_Accreditation_Data_ALL_DTO
    {
        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }
    } 
    public class Station_Check_Quarantine_DTO
    {
        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }
        public long Station_Accreditation_Committee_ID { get; set; }
        public long Station_ID { get; set; }
        public long Station_Accreditation_Request_ID { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public Nullable<bool> IsAccepted_Quarantine { get; set; }
        public string Notes_Quarantine { get; set; }
        public Nullable<System.DateTime> StartDate_Quarantine { get; set; }
        public Nullable<System.DateTime> EndDate_Quarantine { get; set; }
        public Nullable<bool> IsAccepted_final_Admin { get; set; }
        public string Station_Refuse_Reason_Nots { get; set; }
        public string Committee_position { get; set; }
    }  
    public class Station_Accreditation_Request_TypeDTO
    {
        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }        
        public long Station_Accreditation_Request_ID { get; set; }
        public byte Station_Accreditation_Request_Type_ID { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public string Station_Refuse_Reason_Nots { get; set; }

    }
    public class Station_Check_AdminDTO
    {
        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public long Station_Accreditation_Committee_ID { get; set; }
        public Nullable<System.DateTime> Station_Committee_Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> Station_Committee_StartTime { get; set; }
        public Nullable<System.TimeSpan> Station_Committee_EndTime { get; set; }
        public long Station_CheckList_ID { get; set; }
        public Nullable<bool> IsAccepted_Band_Admin { get; set; }
        public string Notes_Ar_Band_Admin { get; set; }
        public string Notes_En_Band_Admin { get; set; }
        public string FullName { get; set; }
        public string Station_CheckList_name { get; set; }

        public Nullable<bool> IsAccepted_final_Admin { get; set; }
        public string Notes_Ar_final_Admin { get; set; }
        public string Station_Refuse_Reason_Nots { get; set; }

    }
    public class Station_ConfirmDTO
    {
        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public long Station_Accreditation_Committee_ID { get; set; }
        public Nullable<bool> IsAccepted_Confirm { get; set; }
        public string Notes_Confirm { get; set; }
        public Nullable<System.DateTime> Date_Confirm { get; set; }
        public string FullName { get; set; }

    }
    public class Station_Check_ImgeDTO
    {
        public long Station_Accreditation_Request_ID { get; set; }
        public long Station_Accreditation_Committee_ID { get; set; }
        //public string Station_Accreditation_Request_Type_Name { get; set; }
        public byte[] Station_Committee_Imge_Binary { get; set; }
        public Nullable<long> Station_Committee_Imge_ID { get; set; }

        public string Infection_Comment { get; set; }
    }

    public class Station_Fees_DTO
    {
        
        public long row_number { get; set; }
        public long ID { get; set; }
        public string Name_AR { get; set; }
        public string Name_AR_Type { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<decimal> fess_Requst { get; set; }
        public Nullable<decimal> fess_Shift { get; set; }
        public Nullable<decimal> Committee_eng_fess { get; set; }
        public Nullable<bool> Committee_IsPaid { get; set; }
     
    }
}
