using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmData
{
    public class FarmRequestDTO
    {
        public long ID { get; set; }
        public Nullable<long> FarmsData_ID { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public Nullable<bool> IsStatus { get; set; }
        //eslam
        public Nullable<System.DateTime> End_Date_Request { get; set; }
        public Nullable<System.DateTime> Start_Date_Request { get; set; }
        public Nullable<decimal> Fees { get; set; }
        public Nullable<decimal> Fees_Actual { get; set; }
        public string Print_Text { get; set; }

        public List<FarmCountryDTO> countryLst { get; set; }
    }

    public class FarmCountryDTO
    {
        public string country_Name_En;

        public long ID { get; set; }
        public Nullable<long> Farm_Request_ID { get; set; }
        public Nullable<short> Country_ID { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public string country_Name { get; set; }
        public string union_Name { get; set; }
        public Nullable<int> UnionId { get; set; }
        public string country_Name_Ar { get; set; }
    }
}
