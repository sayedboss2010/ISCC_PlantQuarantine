using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
    public class Ex_ConstrainActivationProcedureDTO
    {
        public long ID { get; set; }
        public short ownerImportId { get; set; }
        public short ownerTransitId { get; set; }
       

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
        
        //eman
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

        public Procedureplants plants { get; set; }
      

    }
    //****************************************//
    public class Procedureplants
    {
        public const int IsPlant = 4;
        public long PlantId { get; set; }
        public byte statusId { get; set; }
        public byte purposeId { get; set; }
        public byte plantPartId { get; set; }
        public long? PlantCatId { get; set; }
        public List<ProcedureCustomPlantConstrain_Rows> PlantConstrain_Rows { get; set; }
        public List<ProcedureCustomPlantConstrain_Analysis> PlantConstrain_Analysis { get; set; }
        public List<ProcedureCustomPlantConstrain_Treatments> PlantConstrain_Treatments { get; set; }
        public List<ProcedureCustomPlantConstrain_ArrivalPorts> PlantConstrain_ArrivalPorts { get; set; }

    }
   
    public class ProcedureCustomPlantConstrain_Rows
    {
        public int index { get; set; }
        public long Id { get; set; }

        public Nullable<long> countryConstraintext_Id { get; set; }
        public bool IsSelected { get; set; }
        //parentid = textid
        public Nullable<long> ParentId { get; set; }
        //-----------------------------------//
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        //IsCertificate_Addtion
        public bool IsCertificate_Addtion { get; set; }
        //public byte IsAnalysis_IsTreatment { get; set; }

        //********************//
        //public long AnalysisConstrain_ID { get; set; }
        //public int AnalysisLabTypeID { get; set; }
        //public int AnalysisLab_ID { get; set; }
        //public string AnalysisLab_Name { get; set; }
        //public int AnalysisType_ID { get; set; }
        //public string AnalysisType_Name { get; set; }
        //*******************//
        //public long TreatmentConstrain_ID { get; set; }
        //public int TreatmentMainType_ID { get; set; }
        //public int TreatmentType_ID { get; set; }
        //public string TreatmentType_Name { get; set; }
        //public int TreatmentMaterial_ID { get; set; }
        //public string TreatmentMaterial_Name { get; set; }
        //public int TreatmentMethod { get; set; }
        //public string TreatmentMethod_Name { get; set; }
        //public decimal TheDose { get; set; }
        //public int Exposure_Day { get; set; }
        //public int Exposure_Minute { get; set; }
        //public int Exposure_Hour { get; set; }

        public string InSide_Certificate_Ar { get; set; }
        public string InSide_Certificate_En { get; set; }

    }
    public class ProcedureCustomPlantConstrain_Analysis
    {
        public int index { get; set; }
        public long Id { get; set; }

        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }


        //********************//
        public Nullable<long> AnalysisConstrain_ID { get; set; }
        public bool IsSelected { get; set; }
        //parentid = analysisid
        public Nullable<long> ParentId { get; set; }
        //-----//
        public int AnalysisLabTypeID { get; set; }
        public int AnalysisLab_ID { get; set; }
        public string AnalysisLab_Name { get; set; }
        public int AnalysisType_ID { get; set; }
        public string AnalysisType_Name { get; set; }
        //*******************//


    }
    public class ProcedureCustomPlantConstrain_ArrivalPorts
    {
        public int index { get; set; }
        public long Id { get; set; }

        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }


        //********************//
        public Nullable<long> ArrivalConstrain_ID { get; set; }
        public bool IsSelected { get; set; }
        //parentid = arrivalport id
        public Nullable<long> ParentId { get; set; }
        //----//
        public int PortInternationalID { get; set; }
        public short PortType_ID { get; set; }

        //*******************//


    }
    public class ProcedureCustomPlantConstrain_Treatments
    {
        public int index { get; set; }
        public long Id { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        //*******************//
        public Nullable<long> TreatmentConstrain_ID { get; set; }
        public bool IsSelected { get; set; }
        //parentid = treatmentid
        public Nullable<long> ParentId { get; set; }
        //*******************************//
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

