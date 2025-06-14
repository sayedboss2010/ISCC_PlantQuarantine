using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Get_EN_Print_DTO
    {
        public long StationId { get; set; }
        public string StationName_AR { get; set; }
        public string StationName_En { get; set; }
        public string StationAddress_AR { get; set; }
        public string StationAddress_EN { get; set; }
        public string villageName_AR { get; set; }
        public string villageName_EN { get; set; }
        public string centerName_AR { get; set; }
        public string centerName_EN { get; set; }
        public string governorateName_AR { get; set; }
        public string governorateName_EN { get; set; }
        public string ItemName { get; set; }
        public string countryName { get; set; }
        public long requestId { get; set; }
        public Nullable<bool> IsAccepted_Request { get; set; }
        public Nullable<bool> IsActive_Request { get; set; }
        public Nullable<bool> Is_Final_requst { get; set; }
        public Nullable<bool> IsActive_Station { get; set; }
        public string TaxesRecord { get; set; }
        public string CommertialRecord { get; set; }
        public string Industrial_License_Num { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public string StationCode { get; set; }
        public Nullable<System.DateTime> StartDate_Industrial_License_Num { get; set; }
        public Nullable<System.DateTime> EndDate_Industrial_License_Num { get; set; }
        public Nullable<int> Average_number_workers { get; set; }
        public Nullable<int> working_hours { get; set; }
        public Nullable<int> Number_working_Days { get; set; }
        public Nullable<int> Number_Shifts { get; set; }
        public Nullable<byte> Seasonal_Annual { get; set; }
        public Nullable<int> Year_Creation { get; set; }
        public Nullable<long> Manager_ID { get; set; }
        public string Manager_Ar_Name { get; set; }
        public string Manager_En_Name { get; set; }
        public string Manager_Address_Ar { get; set; }
        public string Manager_Address_En { get; set; }
        public string Manager_Mobile { get; set; }
        public string Managing_Director_NID { get; set; }
        public string contactType { get; set; }
        public string contactValue { get; set; }
        public Nullable<int> contactID { get; set; }
        public string Station_Accreditation_Data_Name_AR { get; set; }
        public string Station_Accreditation_Data_Name_EN { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string StationActivityType_Name_AR { get; set; }
        public string StationActivityType_Name_En { get; set; }
        public string Station_Request_Type_Name { get; set; }
        public byte StationActivityType_ID { get; set; }
        public Nullable<int> Accreditation_Type_ID { get; set; }
        public Nullable<long> Station_Committee_ID { get; set; }
        public Nullable<long> Request_Fees_ID { get; set; }
        public Nullable<decimal> Request_Fees_Value { get; set; }
        public string Station_Managing_Director_Ar_Name { get; set; }
        public string Station_Managing_Director_En_Name { get; set; }
        public string ImporterTypeName_AR { get; set; }
        public string ImporterTypeName_EN { get; set; }
        public string ImporterName_AR { get; set; }
        public string ImporterName_EN { get; set; }
        public string Importer_Adrres_AR { get; set; }
        public string Importer_Adrres_EN { get; set; }
        public string Importer_CommertialRecord { get; set; }
        public string Importer_TaxesRecord { get; set; }
        public string Importer_Phone { get; set; }
        public string Importer_Email { get; set; }

        public string Description_Ar { get; set; }
        public string Description_En { get; set; }
        public string DescriptionMore_AR { get; set; }
        public string DescriptionMore_EN { get; set; }
        public string GPS { get; set; }
    }
}
