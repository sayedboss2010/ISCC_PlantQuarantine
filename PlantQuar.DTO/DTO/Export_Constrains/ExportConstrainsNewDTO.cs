using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
    public class ExportConstrainsNewDTO
    {
        //Ex_CountryConstrain
        public long ID { get; set; }
        public Nullable<short> ConstrainOwner_ID { get; set; }
        public int CountryConstrain_Type { get; set; }
        public Nullable<short> TransportCountry_ID { get; set; }
        public long Item_ShortName_id { get; set; }
        public bool IsExport { get; set; }
        public Nullable<bool> IsStationAccreditation { get; set; }
        public Nullable<bool> IsFarmAccreditation { get; set; }
        public Nullable<bool> IsCompanyAccreditation { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> ItemCategories_ID { get; set; }
        public Nullable<bool> IsCertificate_Addtion { get; set; }

        public List<EX_CustomItemConstrain_Rows> List_ItemConstrain_Rows { get; set; }
    }

    public class EX_CustomItemConstrain_Rows
    {
        public int index { get; set; }
        public long Id { get; set; }

        public long countryConstraintext_Id { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        //IsCertificate_Addtion
        public bool IsCertificate_Addtion { get; set; }

        public string InSide_Certificate_Ar { get; set; }
        public string InSide_Certificate_En { get; set; }

    }
}
