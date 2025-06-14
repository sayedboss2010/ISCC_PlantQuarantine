
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.Permissions
{
    public class ImRequestDetails_NewDTO
    {
        public long Im_CheckRequest_ID { get; set; }
        public string ImCheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }

        public List<Items_checkReq_New> itemsWithConstrains { get; set; }
    }
        public class Items_checkReq_New
    {
      //  public long ID { get; set; }
        public long? Item_ShortName_ID { get; set; }
        public string ItemShortNameAr { get; set; }
        public string ItemShortNameEn { get; set; }
        public long Im_Initiator_ID { get; set; }
        //public Nullable<long> ImcheckReqItem_ID { get; set; }
        //public Nullable<long> ImcheckReqshippedMethod_ID { get; set; }

        //public string Item_checkReq_Number { get; set; }
        //public Nullable<decimal> Fees { get; set; }
        public long Item_ID { get; set; }
        public string ItemName_Ar { get; set; }
        public string ItemName_En { get; set; }

     //   public Nullable<long> Im_Initiator_ID { get; set; }
        public string InitiatorCountry { get; set; }
        public string InitiatorCountryEn { get; set; }

        public string qualitiveGroupName { get; set; }
        public string qualitiveGroupNameEn { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }

        //public string ScientificNameAr { get; set; }
        //public string ScientificNameEn { get; set; }
        //public string Status { get; set; }
        //public string Purpose { get; set; }
        //public string StatusEn { get; set; }
        //public string PurposeEn { get; set; }
        //public string subPartName { get; set; }

        //public string subPartNameEn { get; set; }
        //public Nullable<int> SubPart_id { get; set; }
        //public string InitiatorCountry { get; set; }
        //public string InitiatorCountryEn { get; set; }

        //public constrains Itemconstrains { get; set; }
        //public Nullable<double> Size { get; set; }
        //public Nullable<int> Package_Count { get; set; }
        //public Nullable<decimal> Package_Weight { get; set; }
        //public Nullable<int> Units_Number { get; set; }
        //public string packageType { get; set; }
        //public string packageTypeEn { get; set; }
        //public string packageMaterial { get; set; }
        //public string packageMaterialEn { get; set; }
        //public Nullable<short> packageMaterialID { get; set; }
        //public Nullable<short> packageTypeID { get; set; }


        //public string Order_Text { get; set; }
        // public List<Lots> ItemLots { get; set; }

        public string subPartName { get; set; }
        public List<categories_lots_New> ItemCategories_lots { get; set; }

    }


    public class categories_lots_New
    {
        
        public long ID_Lot { get; set; }
        public string packageMaterialName { get; set; }
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> Im_checkReqItems_ID { get; set; }
        public Nullable<long> Im_checkReqItemsCategory_ID { get; set; }
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

     
        public Nullable<long> Im_CheckRequest_ID { get; set; }

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
        public List<Items_checkReq> Items_checkReq { get; set; }
        public List<Committee_Sample_Lot> list_Committee_Sample_Lot { get; set; }
        public List<Committee_Treatment_Lot> list_Committee_Treatment_Lot { get; set; }

        public List<Lot_Result_Status> list_Lot_Result_Status { get; set; }
        public long? Im_CommitteeResult_ID { get; set; }
        public Nullable<int> Lot_Result_Status { get; set; }
       

    }

    public class Committee_Sample_Lot
    {

        public Nullable<long> LotData_ID { get; set; }
        public int AnalysisLabType_ID { get; set; }
        public string Analysis_Name { get; set; }
        public string Lab_Name { get; set; }


    }

    public class Committee_Treatment_Lot
    {

        public long ID { get; set; }
        public long Im_RequestCommittee_ID { get; set; }
        public long Im_Request_Item_Id { get; set; }
        public Nullable<long> Im_Request_LotData_ID { get; set; }
        public Nullable<byte> TreatmentType_ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Place { get; set; }
        public byte TreatmentMethod_ID { get; set; }
        public Nullable<byte> TreatmentMat_ID { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> ThermalSealNumber { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
        public Nullable<bool> IS_From_Android { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public string Procedures { get; set; }
        public string Is_Paid_Treatment { get; set; }

        public string TreatmentMethod_Name { get; set; }
        public string TreatmentType_Name { get; set; }
        public string TreatmentMat_Name { get; set; }
        public Nullable<decimal> Treatment_Amount { get; set; }


    }

    public class Lot_Result_Status
    {

        public Nullable<long> LotData_ID { get; set; }
        public Nullable<byte> commite_No { get; set; }
        
        public string Status_Name { get; set; }
        public string Nots_Result_Status { get; set; }
        public Nullable<int> IS_Status { get; set; }
        public Nullable<int> IS_Status_Committee { get; set; }

        public Nullable<int> Is_Continue { get; set; }
    }




}
