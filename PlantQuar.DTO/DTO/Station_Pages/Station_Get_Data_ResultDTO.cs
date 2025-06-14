using PlantQuar.DTO.DTO.Export_CheckRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Get_Data_ResultDTO
    {
        public long StationId { get; set; }
        public string station_Status { get; set; }
        public int station_btn { get; set; }
        public string StationName { get; set; }
        public string StationAddress { get; set; }
        public string villageName { get; set; }
        public string centerName { get; set; }
        public string governorateName { get; set; }
        public string ItemName { get; set; }
        public string countryName { get; set; }
        public long requestId { get; set; }
        public Nullable<bool> IsAccepted_Request { get; set; }
        public Nullable<bool> IsPaid_Request { get; set; }
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
        public Nullable<long> Company_ID { get; set; }
        public Nullable<byte> Seasonal_Annual { get; set; }
        public Nullable<int> Year_Creation { get; set; }
        public Nullable<long> Manager_ID { get; set; }
        public string Manager_Ar_Name { get; set; }
        public string Company_National_Owner_Ar { get; set; }
        public string Manager_En_Name { get; set; }
        public string Manager_Address_Ar { get; set; }
        public string Manager_Address_En { get; set; }
        public string Manager_Mobile { get; set; }
        public string Managing_Director_NID { get; set; }
        public string contactType { get; set; }
        public string contactValue { get; set; }
        public Nullable<int> contactID { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string StationActivityType_Name { get; set; }
        public string Station_Request_Type_Name { get; set; }
        public byte StationActivityType_ID { get; set; }
        public Nullable<int> Accreditation_Type_ID { get; set; }
        public Nullable<long> Station_Committee_ID { get; set; }
        public Nullable<bool> Station_Committee_Is_Cancel { get; set; }
        public Nullable<System.DateTime> Station_Committee_Delegation_Date { get; set; }
        public Nullable<bool> Station_Committee_IsPaid { get; set; }
        public Nullable<bool> Station_Committee_IsApproved { get; set; }
        public Nullable<bool> Station_Committee_STATUS { get; set; }
        public Nullable<bool> Station_Committee_Is_Start_Android { get; set; }
        public Nullable<long> Request_Fees_ID { get; set; }
        public Nullable<decimal> Request_Fees_Value { get; set; }
        public string Station_Managing_Director_Ar_Name { get; set; }
        public string Station_Managing_Director_En_Name { get; set; }
        public string ImporterTypeName { get; set; }
        public string ImporterName { get; set; }
        public string Importer_Adrres { get; set; }
        public string Importer_CommertialRecord { get; set; }
        public string Importer_TaxesRecord { get; set; }
        public string Importer_Phone { get; set; }
        public string Importer_Email { get; set; }
        public decimal Facility_area { get; set; }
        public decimal The_num_of_storage_refrigerators { get; set; }
        public decimal Storage_fridge_capacity { get; set; }
        public decimal Fast_cooling_refrigerators { get; set; }
        public decimal The_num_of_production_lines { get; set; }
        public decimal Production_capacity { get; set; }

        //تاب البيانات الاساسية
        //public long StationId { get; set; }
        //public string station_Status { get; set; }
        //public int station_btn { get; set; }
        //public long Company_ID { get; set; }
        //public string StationName { get; set; }
        //public string StationAddress { get; set; }
        //public string villageName { get; set; }
        //public string centerName { get; set; }
        //public string governorateName { get; set; }
        public string companyName { get; set; }

        //public string ItemName { get; set; }
        //public string countryName { get; set; }
        //public long requestId { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        //public Nullable<bool> IsAccepted_Request { get; set; }
        //public Nullable<bool> IsActive_Request { get; set; }
        //public Nullable<bool> IsPaid_Request { get; set; }


        //public string TaxesRecord { get; set; }
        //public string CommertialRecord { get; set; }

        //public string Industrial_License_Num { get; set; }


        //public string Ar_Name { get; set; }
        //public string En_Name { get; set; }
        //public string Address_Ar { get; set; }
        //public string Address_En { get; set; }
        //public string StationCode { get; set; }

        //public Nullable<System.DateTime> StartDate_Industrial_License_Num { get; set; }
        //public Nullable<System.DateTime> EndDate_Industrial_License_Num { get; set; }
        //public Nullable<int> Average_number_workers { get; set; }

        //public Nullable<int> Number_working_Days { get; set; }
        //public Nullable<int> Number_Shifts { get; set; }
        //public Nullable<int> working_hours { get; set; }
        //public Nullable<int> Year_Creation { get; set; }
        //public Nullable<byte> Seasonal_Annual { get; set; }

        ////تاب المدير المسؤول
        //public Nullable<long> Manager_ID { get; set; }
        //public string Manager_Ar_Name { get; set; }
        //public string Manager_En_Name { get; set; }
        //public string Manager_Address_Ar { get; set; }
        //public string Manager_Address_En { get; set; }

        //public string Manager_Mobile { get; set; }
        //public string Managing_Director_NID { get; set; }

        //// تاب وساثل الاتصال

        //public string contactValue { get; set; }
        //public string contactType { get; set; }
        //public Nullable<int> contactID { get; set; }

        ////تاب الاعتماد
        //public long Station_Accreditation_Data_ID { get; set; }
        //public string Station_Accreditation_Data_Name { get; set; }
        //public Nullable<System.DateTime> Start_Date { get; set; }
        //public Nullable<System.DateTime> End_Date { get; set; }
        //public string StationActivityType_Name { get; set; }
        //public string Station_Request_Type_Name { get; set; }

        //public Nullable<byte> StationActivityType_ID { get; set; }
        //public Nullable<int> Accreditation_Type_ID { get; set; }

        public Nullable<int> Station_Fees_Id { get; set; }
        public Nullable<decimal> Station_Fees_Money { get; set; }
        public Nullable<int> Station_Fees_Id_Type { get; set; }
        public List<Station_Request_Fees_DTO> List_Station_Request_Fees { get; set; }
        //// اللجنة
        //public Nullable<long> Station_Committee_ID { get; set; }
        ////تابة الرسوم
        //public Nullable<long> Request_Fees_ID { get; set; }
        //public Nullable<decimal> Request_Fees_Value { get; set; }


        //public Nullable<bool> IsActive_Station { get; set; }



        //public Nullable<bool> Is_Final_requst { get; set; }




        ////الشركة 
        //public string Station_Managing_Director_Ar_Name { get; set; }
        //public string Station_Managing_Director_En_Name { get; set; }
        //public string ImporterName { get; set; }
        //public string Importer_Adrres { get; set; }

        //public string Importer_TaxesRecord { get; set; }
        //public string Importer_CommertialRecord { get; set; }

        //public string Importer_Phone { get; set; }
        //public string Importer_Email { get; set; }


        //public Nullable<bool> Station_Committee_Is_Cancel { get; set; }
        //public Nullable<System.DateTime> Station_Committee_Delegation_Date { get; set; }
        //public Nullable<bool> Station_Committee_IsPaid { get; set; }
        //public Nullable<bool> Station_Committee_STATUS { get; set; }
        //public Nullable<bool> Station_Committee_Is_Start_Android { get; set; }


        //////////////////// details of station 
        //public string Facility_area { get; set; }
        //public string The_num_of_storage_refrigerators { get; set; }
        //public string Storage_fridge_capacity { get; set; }
        //public string Fast_cooling_refrigerators { get; set; }
        //public string The_num_of_production_lines { get; set; }
        //public string Production_capacity { get; set; }

        ////////// attach
        public List<Attachments> Attachments { get; set; }


        public List<CompanyActivityDTO> _CompanyActivitys { get; set; }
        public List<ContactTypeDTO> ImporterContacts { get; set; }

        public string attaches { get; set; }
        public string Notes_Reject { get; set; }


    }
}