using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequestNew
{
    public class ExRequestDetails_NewDTO
    {
        public long EX_CheckRequest_ID { get; set; }
        public string EXCheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }

        public List<Ex_Items_checkReq> itemsWithConstrains { get; set; }
    }
    public class Ex_Items_checkReq
    {
        public long ID { get; set; }
        public long? Item_ShortName_ID { get; set; }
        public string ItemShortNameAr { get; set; }
        public string ItemShortNameEn { get; set; }        
        public long Item_ID { get; set; }
        public string ItemName_Ar { get; set; }
        public string ItemName_En { get; set; }      
        public string InitiatorCountry { get; set; }
        public string InitiatorCountryEn { get; set; }

        public string qualitiveGroupName { get; set; }
        public string qualitiveGroupNameEn { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
      
        public string subPartName { get; set; }
        public List<Ex_lots> ItemCategories_lots { get; set; }

        public List<EX_TreatmentData> list_EX_TreatmentData { get; set; }
    }

    public class Ex_lots
    {
        public long ID_Lot { get; set; }
        public string packageMaterialName { get; set; }
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> Ex_checkReqItems_ID { get; set; }
        public Nullable<long> Ex_checkReqItemsCategory_ID { get; set; }
        public string categoryName { get; set; }
        public string categoryNameEn { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public string packageType { get; set; }
        public Nullable<short> packageTypeID { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public string Order_Text { get; set; }
        public string RegisterReason { get; set; }
        public Nullable<bool> IsRegister { get; set; }
        public Nullable<long> ItemCategory_ID { get; set; }
        public string itemCatGroup { get; set; }
        //public List<Lots> categoryLots { get; set; }
        public string Reason_Entry { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public string Lot_Number { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string RejectReason { get; set; }
        public Nullable<decimal> Based_Weight { get; set; }
        public Nullable<decimal> Package_Based_Weight { get; set; }
        public Nullable<decimal> Package_Net_Weight { get; set; }

        public string Container_Number { get; set; }
        public string NavigationalFluid_Number { get; set; }
        public string ShipmentPolicy_Number { get; set; }
        public string Number_Wooden_Package { get; set; }
        public string Grower_Number { get; set; }
        public string Waybill { get; set; }
        public int Check_Lot_Old_ID { get; set; }
        public string Check_Lot_Old_Name { get; set; }
        //بيانات النبات
        public long ID_IM_Item { get; set; }
        // بيانات الحاوية


        public Nullable<long> Ex_CheckRequest_ID { get; set; }

        public string containerName { get; set; }
        public string containerType { get; set; }
        public string ShipholdNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }
        public Nullable<decimal> Total_Weight { get; set; }
        public Nullable<DateTime> Delegation_Date { get; set; }
        public Nullable<byte> CommitteeResultType_ID { get; set; }

        public string Order_TextLot { get; set; }
        public string RecordedOrNot { get; set; }
        public string ItemCategoryGroup { get; set; }
        public List<Ex_Items_checkReq> Items_checkReq { get; set; }
        public List<EX_Sample_Lot> list_Committee_Sample_Lot { get; set; }
      
        public long? EX_CommitteeResult_ID { get; set; }
    }

    public class EX_Sample_Lot
    {
        public Nullable<long> LotData_ID { get; set; }
        public Nullable<int> AnalysisLabType_ID { get; set; }
        //public bigint AnalysisLabType_ID { get; set; }
        public string Analysis_Name { get; set; }
        public string Lab_Name { get; set; }
    }

    public class EX_TreatmentData
    {
        public long ID { get; set; }
        public long Ex_RequestCommittee_ID { get; set; }
        public long Ex_CountryConstrain_Treatment_ID { get; set; }
      
        public Nullable<long> Company_ID { get; set; }
        public long Ex_Request_Item_Id { get; set; }
        public Nullable<long> Ex_Request_LotData_ID { get; set; }
        public Nullable<byte> TreatmentMaterial_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Place { get; set; }
        public string Company_Place { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> ThermalSealNumber { get; set; }

        public byte TreatmentMethods_ID { get; set; }
        public string TreatmentMethods_Name { get; set; }
    }
}
