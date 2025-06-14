using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.labResult
{
    public class LaboratoryResult_ReportDTO
    {
        public long ID { get; set; }
        public long? farmSampleId { get; set; }
        public Nullable<bool> IsPrint { get; set; }
        public string labName { get; set; }
        public string analysisType { get; set; }
        public string analysisType_Ar { get; set; }
        public string analysisType_En { get; set; }
        public int analysisType_ID { get; set; }

        public Nullable<System.DateTime> WithdrawDate { get; set; }
        public string Sample_BarCode { get; set; }
        public Nullable<double> SampleSize { get; set; }
        public Nullable<double> SampleRatio { get; set; }
        public string farmName { get; set; }
        public string Syl_ALkhatima_Number { get; set; }
        public string Notes_Ar { get; set; }
        public string RejectReason_Ar { get; set; }
        public string RejectReason_En { get; set; }
        public string Notes_En { get; set; }
        public string farmCode { get; set; }
        public string itemName { get; set; }
        public string Scientific_Name { get; set; }
        public string item_Type_Name { get; set; }
        public string ShortName { get; set; }

        public string itemCategoryName { get; set; }

        public string SubPart_Name { get; set; }
        public string Item_Status_Name { get; set; }
        public string Item_Purpose_Name { get; set; }

        public string Grower_Number { get; set; }

        public string ExaminAddress { get; set; }
        public string companyName { get; set; }
        public string company_Address_Ar { get; set; }
        public string farmAddress { get; set; }
        public string village { get; set; }
        public string center { get; set; }
        public string governate { get; set; }
        public string Outlet_Name { get; set; }
        public string Station_Name { get; set; }
        public string FarmCode { get; set; }
        public string Examination_Place { get; set; }
        public string Origin_Countery { get; set; }
        public string Destination_Countery { get; set; }
        public string Station_Code { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }

        public Nullable<int> Count_Sample { get; set; }
        // public string Lot_Number { get; set; }
        //  public Nullable<double> Size { get; set; }
        public List<CommitteList> CommitteList { get; set; }
        public List<ItemEx_CheckRequestDTO> ItemEx_CheckRequest { get; set; }
    }

    public class CommitteList
    {


        //public List<QuickList_Shift> QuickList_Shift { get; set; }
        //public List<QuickcommitteeEmployee_Name> QuickcommitteeEmployee_Name { get; set; }
        //public List<QuickcommitteeEmployee_NameADMIN> QuickcommitteeEmployee_NameADMIN { get; set; }
        //public List<QuickcommitteeEmployee_NameConfirm> QuickcommitteeEmployee_NameConfirm { get; set; }


        public string Lot_Number { get; set; }
        public string committeeFullEmployee_Name { get; set; }
        public Nullable<long> Committee_Result_Lot_ID { get; set; }

        public string ResultTypes_Name { get; set; }

        public Nullable<long> Committee_ID { get; set; }

        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> Status { get; set; }

        public Nullable<System.DateTime> Date { get; set; }

        public double Weight { get; set; }

        public string Notes { get; set; }

        public Nullable<bool> IsAdminFinalResult { get; set; }

        public Nullable<bool> ISAdmin { get; set; }

        public double QuantitySize { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }

        public Nullable<bool> Is_Result_Finch { get; set; }


        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }
    }

    public class ItemEx_CheckRequestDTO
    {
        public List<Item_lots> item_lots { get; set; }
        public List<Item_lots2> item_lots2 { get; set; }
        public string ItemNameAr { get; set; }
        public string ItemNameEn { get; set; }

        public string ItemShortNameAr { get; set; }
        public string ItemShortNameEn { get; set; }
        public Nullable<int> Package_Count { get; set; }

        public Nullable<int> Units_Number { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> NetWeight { get; set; }

        public string packageType { get; set; }
        public string Agriculture_Hand { get; set; }
        public string packageTypeEn { get; set; }
        public string packageMaterial { get; set; }
        public string PackageMaterialEn { get; set; }

    }

    public class Item_lots
    {
        public string Package_Type_Name { get; set; }


        public long ID { get; set; }
        public long LotCount { get; set; }
        public string packageMaterialName { get; set; }
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> Ex_CheckRequest_Items_ID { get; set; }
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
        public string Order_TextLot { get; set; }
        public string RegisterReason { get; set; }

        public Nullable<long> ItemCategory_ID { get; set; }
        public string ItemCategory { get; set; }
        public string ItemCategoryGroup { get; set; }
        public string RecordedOrNot { get; set; }
        public string itemCatGroup { get; set; }

        public string Reason_Entry { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Net_Weight_Final { get; set; }
        public Nullable<decimal> Gross_Weight_Final { get; set; }
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
        public string ItemcategoryName { get; set; }


    }
    public class Item_lots2
    {
        public string Lot_Number { get; set; }
        public string Package_Type_Name { get; set; }
        public string packageMaterialName { get; set; }
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
        public string ItemcategoryName { get; set; }
        public Nullable<double> Size { get; set; }

    }
}
