using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
    public class CustomCountryConstrain
    {

        public long ID { get; set; }
        public int ownerTypeId { get; set; }
        public short countryId { get; set; }
        public short unionId { get; set; }
        public short local = 0;
        public bool IsAnalysis { get; set; }
        public bool IsTreatment { get; set; }
        //emn
        //public bool IsCertificate_Addtion_plant { get; set; }
        public bool IsStationAccreditation_plant { get; set; }
        public bool IsFarmAccreditation_plant { get; set; }
        public bool IsCompanyAccreditation_plant { get; set; }
        //public bool IsCertificate_Addtion_product { get; set; }
        public bool IsStationAccreditation_product { get; set; }
        public bool IsFarmAccreditation_product { get; set; }
        public bool IsCompanyAccreditation_product { get; set; }
       // public bool IsCertificate_Addtion_live { get; set; }
        public bool IsStationAccreditation_live { get; set; }
        public bool IsFarmAccreditation_live { get; set; }
        public bool IsCompanyAccreditation_live { get; set; }
       // public bool IsCertificate_Addtion_notlive { get; set; }
        public bool IsStationAccreditation_notlive { get; set; }
        public bool IsFarmAccreditation_notlive { get; set; }
        public bool IsCompanyAccreditation_notlive { get; set; }
        public List<int> ArrivalPortList_plant { get; set; }
        //public List<int> ArrivalPortList_product { get; set; }
        //public List<int> ArrivalPortList_live { get; set; }
        //public List<int> ArrivalPortList_notlive { get; set; }
        //eman
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

        public plants plants { get; set; }
        //public products products { get; set; }
        //public Alives Alives { get; set; }
        //public NotAlives NotAlives { get; set; }
        //sayed
        public long Item_ShortName_id { get; set; }
        public Nullable<long> ItemCategories_ID { get; set; }
        public Nullable<bool> IsCertificate_Addtion { get; set; }

    }
    //****************************************//
    public class plants
    {
        public long Item_ShortName_id { get; set; }
        public Nullable<long> ItemCategories_ID { get; set; }
        public Nullable<bool> IsCertificate_Addtion { get; set; }
        public List<CustomPlantConstrain_Rows> PlantConstrain_Rows { get; set; }
        public List<CustomPlantConstrain_Analysis> PlantConstrain_Analysis { get; set; }
        public List<CustomPlantConstrain_Treatments> PlantConstrain_Treatments { get; set; }
        public List<CustomPlantConstrain_ArrivalPorts> PlantConstrain_ArrivalPorts { get; set; }

    }
    //public class products
    //{
    //    public const int IsPlant = 5;
    //    public long ProductId { get; set; }
    //    public byte statusId { get; set; }
    //    public byte purposeId { get; set; }
    //    public List<CustomPlantConstrain_Rows> ProductConstrain_Rows { get; set; }
    //    public List<CustomPlantConstrain_Analysis> ProductConstrain_Analysis { get; set; }
    //    public List<CustomPlantConstrain_Treatments> ProductConstrain_Treatments { get; set; }
    //    public List<CustomPlantConstrain_ArrivalPorts> ProductConstrain_ArrivalPorts { get; set; }

    //}
    //public class Alives
    //{
    //    public const int IsPlant = 16;
    //    public long aliveId { get; set; }
    //    public int statusId { get; set; }
    //    public byte purposeId { get; set; }
    //    public int biologicalPhaseId { get; set; }
    //    public List<CustomPlantConstrain_Rows> AliveConstrain_Rows { get; set; }
    //    public List<CustomPlantConstrain_Analysis> AliveConstrain_Analysis { get; set; }
    //    public List<CustomPlantConstrain_Treatments> AliveConstrain_Treatments { get; set; }
    //    public List<CustomPlantConstrain_ArrivalPorts> AliveConstrain_ArrivalPorts { get; set; }

    //}
    //public class NotAlives
    //{
    //    public const int IsPlant = 33;
    //    public long notAliveId { get; set; }
    //    public int statusId { get; set; }
    //    public byte purposeId { get; set; }

    //    public List<CustomPlantConstrain_Rows> NotAliveConstrain_Rows { get; set; }
    //    public List<CustomPlantConstrain_Analysis> NotAliveConstrain_Analysis { get; set; }
    //    public List<CustomPlantConstrain_Treatments> NotAliveConstrain_Treatments { get; set; }
    //    public List<CustomPlantConstrain_ArrivalPorts> NotAliveConstrain_ArrivalPorts { get; set; }

    //}
    public class CustomPlantConstrain_Rows
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
    public class CustomPlantConstrain_Analysis
    {
        public int index { get; set; }
        public long Id { get; set; }
        
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }


        //********************//
        public long AnalysisConstrain_ID { get; set; }
        public int AnalysisLabTypeID { get; set; }
        public int AnalysisLab_ID { get; set; }
        public string AnalysisLab_Name { get; set; }
        public int AnalysisType_ID { get; set; }
        public string AnalysisType_Name { get; set; }
        //*******************//


    }
    public class CustomPlantConstrain_ArrivalPorts
    {
        public int index { get; set; }
        public long Id { get; set; }

        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }


        //********************//
        public long ArrivalConstrain_ID { get; set; }
        public int PortInternationalID { get; set; }
        public short PortType_ID { get; set; }
       
        //*******************//


    }
    public class CustomPlantConstrain_Treatments
    {
        public int index { get; set; }
        public long Id { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        //*******************//
        public long TreatmentConstrain_ID { get; set; }
        public byte TreatmentMainType_ID { get; set; }
        public int TreatmentType_ID { get; set; }
        public string TreatmentType_Name { get; set; }
        public int TreatmentMaterial_ID { get; set; }
        public string TreatmentMaterial_Name { get; set; }
        public int TreatmentMethod { get; set; }
        public string TreatmentMethod_Name { get; set; }
        public decimal TheDose { get; set; }
        public int Exposure_Day { get; set; }
        public int Exposure_Minute { get; set; }
        public int Exposure_Hour { get; set; }


    }
    
}