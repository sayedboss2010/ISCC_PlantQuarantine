
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class ExRequestDetails_NewDTO
    {
        public long Ex_CheckRequest_ID { get; set; }
        public string Ex_CheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }

        public List<Ex_Items_checkReq_New> itemsWithConstrains { get; set; }
    }
        public class Ex_Items_checkReq_New
    {
      //  public long ID { get; set; }
        public long? Item_ShortName_ID { get; set; }
        public string ItemShortNameAr { get; set; }
        public string ItemShortNameEn { get; set; }
       
        //public Nullable<long> Ex_checkReqItem_ID { get; set; }
        //public Nullable<long> Ex_checkReqshippedMethod_ID { get; set; }
     
        //public string Item_checkReq_Number { get; set; }
        //public Nullable<decEx_al> Fees { get; set; }
        public long Item_ID { get; set; }
        public string ItemName_Ar { get; set; }
        public string ItemName_En { get; set; }

     //   public Nullable<long> Ex__Initiator_ID { get; set; }
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
        public List<Ex_categories_lots_New> ItemCategories_lots { get; set; }

    }


    public class Ex_categories_lots_New
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
        public long ID_Ex_Item { get; set; }
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
        public List<Items_checkReq> Items_checkReq { get; set; }
        public List<Ex_Committee_Sample_Lot> list_Committee_Sample_Lot { get; set; }
        public long? Ex_CommitteeResult_ID { get; set; }
    }

    public class Ex_Committee_Sample_Lot
    {

        public Nullable<long> LotData_ID { get; set; }
        public int AnalysisLabType_ID { get; set; }
        public string Analysis_Name { get; set; }
        public string Lab_Name { get; set; }


    }

}
