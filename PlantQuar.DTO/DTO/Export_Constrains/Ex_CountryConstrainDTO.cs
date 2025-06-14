using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
   
    public class Ex_CountryConstrainDTO
    {
        public long ID { get; set; }
        public Nullable<short> Country_Id { get; set; }
        public Nullable<short> Union_Id { get; set; }              
        public Nullable<bool> IsCertificate_Addtion { get; set; }
        public bool IsExport { get; set; }
        public bool IsAnalysis { get; set; }
        public bool IsTreatment { get; set; }              
        public Nullable<long> Parent_ID { get; set; }              
        public Nullable<short> CountryID { get; set; }
        public List<int> ArrivalPortList { get; set; }
        
        public string ConstrainOwner_Name { get; set; }
        public string TransportCountry_Name { get; set; }
        public string CountryConstrain_TypeName { get; set; }
        public bool IsActive_Action { get; set; }

       
        public Nullable<short> Import_Country_ID { get; set; }
        public Nullable<short> TransportCountry_ID { get; set; }
        public long Item_ShortName_id { get; set; }      
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
       
        public List<Ex_CountryConstrain_TextDTO> CountryConstrain_TextDTO { set; get; }
        public List<Ex_CountryConstrain_AnalysisLabTypeDTO> AnalysisLabType { set; get; }
        public List<Ex_CountryConstrain_ArrivalPortDTO> ConstraintAirPortInternational { set; get; }
        public List<Ex_CountryConstrain_TreatmentDTO> Constraint_Treatment { set; get; }

    }

    public class Ex_CountryConstrain_TextDTO
    {
        public long ID { get; set; }
        public long CountryConstrain_ID { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        // العرض
        public Nullable<long> EX_Constrain_Text_ID { get; set; }

        
        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        public string InSide_Certificate_Ar { get; set; }
        public string InSide_Certificate_En { get; set; }
        public Nullable<bool> IsCertificate_Addtion { get; set; }

        public string Ar_Name_Constrain_Type { get; set; }
        public string En_Name_Constrain_Type { get; set; }
    }

    public class Ex_CountryConstrain_AnalysisLabTypeDTO
    {
        public long ID { get; set; }
        public long CountryConstrain_ID { get; set; }
        public bool IsAcive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public int AnalysisTypeID { get; set; }

        public long ExConstrainsLabsAndTypID { get; set; }
      
        public string TypeName_Ar { get; set; }
        public string TypeName_En { get; set; }
             
    }

    public class Ex_CountryConstrain_ArrivalPortDTO
    {
        public long Id { get; set; }
        public long Ex_CountryConstrain_Id { get; set; }
        public int Port_International_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public Nullable<bool> IsActive { get; set; }

        // العرض
        public long ExConstrainsAirPortAndCountryID { get; set; }
        public string CountryName_Ar { get; set; }
        public string CountryLabName_En { get; set; }
        public string AirPortName_Ar { get; set; }
        public string AirPortName_En { get; set; }
       
    }


    public class Ex_CountryConstrain_TreatmentDTO
    {
        public long ID { get; set; }
        public long CountryConstrain_ID { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public bool IsAcive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public Nullable<bool> IS_Optional { get; set; }
        public byte TreatmentMethods_ID { get; set; }

        // العرض
        public string TreatmentMethod_Ar_Name { get; set; }
        public string TreatmentMethod_En_Name { get; set; }

        public string TreatmentType_Ar_Name { get; set; }
        public string TreatmentType_En_Name { get; set; }

        public string TreatmentMainType_Ar_Name { get; set; }
        public string TreatmentMainType_En_Name { get; set; }
    }

}
