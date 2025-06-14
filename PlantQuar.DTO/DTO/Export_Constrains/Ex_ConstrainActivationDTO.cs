using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
   public class Ex_ConstrainActivationDTO
    {
        public long ID { get; set; }
        public Nullable<short> Import_Country_ID { get; set; }
        public Nullable<short> TransportCountry_ID { get; set; }
        public long Item_ShortName_id { get; set; }
        public Nullable<long> ItemCategories_ID { get; set; }
        public Nullable<bool> IsStationAccreditation { get; set; }
        public Nullable<bool> IsFarmAccreditation { get; set; }
        public Nullable<bool> IsCompanyAccreditation { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }



        public Nullable<short> CountryID { get; set; }

        public List<int> ArrivalPortList { get; set; }

        public string ConstrainOwner_Name { get; set; }
        public string TransportCountry_Name { get; set; }
        public string CountryConstrain_TypeName { get; set; }
        public Nullable<bool> IsActive_Action { get; set; }      
        public Nullable<bool> IsCertificate_Addtion { get; set; }

    }
}
