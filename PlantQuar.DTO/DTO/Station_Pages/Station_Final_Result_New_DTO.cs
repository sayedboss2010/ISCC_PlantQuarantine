using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Final_Result_New_DTO
    {
        public long Station_ID { get; set; }
       
        public string Station_Name { get; set; }
        public string station_Address { get; set; }
        public string StationCode { get; set; }
        public string Governate_Name { get; set; }
        public string Center_Name { get; set; }
        public string Village_name { get; set; }
        public List<Station_Accreditation_Data_NewDTO> List_Station_Accreditation_Data { get; set; }
        public List<Station_Fees_DTO> List_Station_Fees { get; set; }
    }

    public class Station_Accreditation_Data_NewDTO
    {
        public long Station_ID { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }
        public Nullable<bool> Is_final_Status { get; set; }
        public long Station_Accreditation_Request_ID { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public List<Request_Type_DTO> List_Station_Accreditation_Request_Type { get; set; }
        public List<Station_Accreditation_Request_Hagre_DTO> List_Station_Accreditation_Request_Hagre { get; set; }
    }

    public class Request_Type_DTO
    {
        public byte Station_Accreditation_Request_Type_ID { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }      
        public List<Station_Request_Committee_DTO> List_Station__Request_Committee { get; set; }
    }
    public class Station_Accreditation_Request_Hagre_DTO
    {
        public byte Station_Accreditation_Request_Type_ID { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public long Station_Accreditation_Request_ID { get; set; }

        public Nullable<bool> IsAccepted_Quarantine { get; set; }
        public string Notes_Quarantine { get; set; }
        public string Status_Quarantine { get; set; }
        public Nullable<System.DateTime> StartDate_Quarantine { get; set; }
        public Nullable<System.DateTime> EndDate_Quarantine { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public string Full_Name_Quarantine { get; set; }

        public List<Station_Request_Committee_DTO> List_Station__Request_Committee { get; set; }
    }
    public class Station_Request_Committee_DTO
    {
        public long Station_Accreditation_Request_ID { get; set; }
        public long Station_Committee_ID { get; set; }
        public Nullable<System.DateTime> Station_Committee_Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> Station_Committee_StartTime { get; set; }
        public Nullable<System.TimeSpan> Station_Committee_EndTime { get; set; }
        public string Station_Refuse_Reason_Nots { get; set; }
        public string Committee_position { get; set; }
        public string Station_Accreditation_Request_Type_Name { get; set; }
        public Nullable<bool> IsAccepted_final_Admin { get; set; }
        public Nullable<bool> Is_final_Status { get; set; }
        public string Notes_Ar_final_Admin { get; set; }
        public string FullName_Admin { get; set; }
        public List<Station_Request_Committee_Admin_DTO> List_Station_Request_Committee_Admin { get; set; }
        public List<Station_Request_Committee_Confirm_DTO> List_Station_Request_Committee_Confirm { get; set; }
        public List<Station_Request_Committee_Imge_DTO> List_Station_Request_Committee_Imge { get; set; }
    }

    public class Station_Request_Committee_Admin_DTO
    {       
        public long Station_CheckList_ID { get; set; }
        public Nullable<bool> IsAccepted_Band_Admin { get; set; }
        public string Notes_Ar_Band_Admin { get; set; }
        public string Notes_En_Band_Admin { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Station_CheckList_name { get; set; }

        public Nullable<bool> IsAccepted_final_Admin { get; set; }
        public string Notes_Ar_final_Admin { get; set; }
        public string Station_Refuse_Reason_Nots { get; set; }

    }
    public class Station_Request_Committee_Confirm_DTO
    {
        public Nullable<bool> IsAccepted_Confirm { get; set; }
        public string Notes_Confirm { get; set; }
        public Nullable<System.DateTime> Date_Confirm { get; set; }
        public Nullable<long> Employee_Id_Confirm { get; set; }
        public string FullName_Confirm { get; set; }

    }
    public class Station_Request_Committee_Imge_DTO
    {
        public long Station_Accreditation_Committee_ID { get; set; }
        public byte[] Station_Committee_Imge_Binary { get; set; }
        public Nullable<long> Station_Committee_Imge_ID { get; set; }
        public string Infection_Comment { get; set; }
    }

}
