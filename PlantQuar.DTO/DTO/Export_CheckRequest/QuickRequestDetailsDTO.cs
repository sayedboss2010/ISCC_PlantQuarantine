using System;
using System.Collections.Generic;


namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class QuickRequestDetailsDTO
    {
        public Nullable<decimal> Amount { get; set; }
        public long Ex_CheckRequest_ID { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public string CheckRequest_Number { get; set; }
        public string CommitteeNotFound { get; set; }
        public string ExportCompany { get; set; }
        public long Ex_CheckRequest_Items_ID { get; set; }
        public Nullable<int> SubPart_id { get; set; }
        public Nullable<short> Package_Material_ID { get; set; }
        public Nullable<short> Package_Type_ID { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public Nullable<bool> Is_LotDivision { get; set; }
        public Nullable<double> Size { get; set; }
        public string Order_Text { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public long ID { get; set; }

        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Based_Weight { get; set; }

        public Nullable<decimal> Package_Based_Weight { get; set; }
        public Nullable<decimal> Package_Net_Weight { get; set; }

        public string Reason_Entry { get; set; }
        public string Lot_Number { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string RejectReason { get; set; }
        public string Grower_Number { get; set; }
        public long Item_ShortName_ID { get; set; }
        public string ShortName_Ar { get; set; }
        public string ShortName_En { get; set; }
        public long Item_ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Scientific_Name { get; set; }




        public string packageType { get; set; }
        public Nullable<int> ImporterType_Id { get; set; }
        public string ImporterName { get; set; }
        public string CompanyAddress { get; set; }
        public string ArrivePortName { get; set; }
        public string ExportPortName { get; set; }
        public string ExportCountryName { get; set; }
        public string Farm_Agriculture_Hand { get; set; }
        public string Ship_Name { get; set; }
        public DateTime? Shipment_Date { get; set; }

        public string ExportCompanyAddress { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }

        public List<ItemEx_CheckRequestDTO> ItemEx_CheckRequest { get; set; }
        public List<QuickList_Port> QuickList_Port { get; set; }
        public List<QuickCommitteList> QuickCommitteList { get; set; }
    }
    public class QuickCommitteList
    {

        //Eslam
        // 
        public List<QuickList_Shift> QuickList_Shift { get; set; }
        public List<QuickcommitteeEmployee_Name> QuickcommitteeEmployee_Name { get; set; }
        public List<QuickcommitteeEmployee_NameADMIN> QuickcommitteeEmployee_NameADMIN { get; set; }
        public List<QuickcommitteeEmployee_NameConfirm> QuickcommitteeEmployee_NameConfirm { get; set; }


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
    public class QuickList_Shift
    {

        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

        public Nullable<decimal> Shift_Count { get; set; }
        public Nullable<decimal> Shift_Amount { get; set; }

        public Nullable<decimal> Shift_Sum_All { get; set; }

    }
    public class QuickcommitteeEmployee_NameADMIN
    {
        public string Employee_NameAdmin { get; set; }
    }
    public class QuickcommitteeEmployee_NameConfirm
    {
        public long Employee_Id { get; set; }
        public bool ISAdmin { get; set; }
        public int OperationType { get; set; }


        public string Employee_NameConfirm { get; set; }
    }
    public class QuickcommitteeEmployee_Name
    {
        public long Committee_ID { get; set; }
        public long Employee_Id { get; set; }
        public bool ISAdmin { get; set; }
        public int OperationType { get; set; }

        public string Employee_Name { get; set; }
    }



    public class QuickList_Port
    {
        public int ReqPortType_ID { get; set; }
        public string TransportPortType { get; set; }

        public string ExportCountryName { get; set; }

        public string ArrivePortName { get; set; }
        public string TransportPortName { get; set; }
        public string ArrivePortType { get; set; }



        public string TransitPortType { get; set; }
        public string TransitPort { get; set; }
        public string TransitCountry { get; set; }

        public string govNameAR { get; set; }
        public string govNameEN { get; set; }
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
}
