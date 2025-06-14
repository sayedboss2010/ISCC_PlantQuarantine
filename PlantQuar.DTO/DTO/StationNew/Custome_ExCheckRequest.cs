using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlantQuar.DTO.DTO.Station
{
    public class Custome_ExCheckRequest
    {
        public SelectList outlet_List { get; set; }
        public SelectList station_List { get; set; }
        public SelectList gov_List { get; set; }
        public SelectList center_List { get; set; }
        public SelectList country_List { get; set; }
        public SelectList portInternational_List { get; set; }
        public SelectList portNational_List { get; set; }
        public SelectList shipmentMean_List { get; set; }
        public SelectList transportationMean_List { get; set; }
        public SelectList company_List { get; set; }
        public SelectList exporterType_List { get; set; }
        public SelectList publicOrg_List { get; set; }
        public SelectList personIdType_List { get; set; }
        public SelectList portType_List { get; set; }
        public SelectList generalAdmin_List { get; set; }


        public SelectList portOrganizational_List { get; set; }
        //المحافظة
        [Required(ErrorMessage = "*")]
        public int govID { get; set; }
        public int centerId { get; set; }
        //إدارة عليا
        [Required(ErrorMessage = "*")]
        public byte generalAdminID { get; set; }
        //المنفذ       
        public byte outletID { get; set; }
        //ميناء الشحن
        [Required(ErrorMessage = "*")]
        public int shipmentPortId { get; set; }
        //نوع المصدر
        [Required(ErrorMessage = "*")]
        public int exporterTypeId { get; set; }

        //المصدر        
        //*****************شركة**********************         
        public Nullable<long> exportCompanyId { get; set; }
        //صاحب الرسالة       
        public string ownerName_company { get; set; }
        public string ownerAddress_company { get; set; }
        //مندوب صاحب الرسالة      
        public string delegateName_company { get; set; }
        public string delegateAddress_company { get; set; }

        //*****************هيئة**********************        
        public Nullable<long> exportPublicOrgId { get; set; }
        //صاحب الرسالة        
        public string ownerName_publicOrg { get; set; }
        public string ownerAddress_publicOrg { get; set; }
        //مندوب صاحب الرسالة        
        public string delegateName_publicOrg { get; set; }
        public string delegateAddress_publicOrg { get; set; }

        //*****************شخص*********************       
        public string personName { get; set; }
        public string personAddress { get; set; }
        public Nullable<short> personNationality { get; set; }
        public string personJob { get; set; }
        public Nullable<int> personIdType { get; set; }
        public string personIDNum { get; set; }
        public Nullable<int> personPhone { get; set; }
        [EmailAddress]
        public string personMail { get; set; }
        //الشركة المستوردة        
        public string importerCompany { get; set; }
        public string recieverName { get; set; }
        public string importerCompanyAddress { get; set; }
        //مسار الرسالة
        [Required(ErrorMessage = "*")]
        public short imporeterCountryId { get; set; }
        [Required(ErrorMessage = "*")]
        public int arrivalPortTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        public int arrivalPortId { get; set; }
        public Nullable<int> transitCountryId { get; set; }
        public Nullable<int> transitPortTypeId { get; set; }
        public Nullable<int> transitPortId { get; set; }
        [Required(ErrorMessage = "*")]
        public byte shipmentMeanId { get; set; }
        [Required(ErrorMessage = "*")]
        public byte transportMeanId { get; set; }

        public string shipName { get; set; }

        //مكان الفحص
        public bool IsStation { get; set; }
        public Nullable<int> stationId { get; set; }
        public string checkplaceAddress { get; set; }

        //****************//
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

        public List<Custom_ExPlants> plants { get; set; }
        public List<Custom_ExProducts> products { get; set; }
        public List<Custom_ExAliveLiableItems> aliveItems { get; set; }
        public List<Custom_ExNotAliveLiableItems> notAliveItems { get; set; }
        public List<Custom_Attatchment> filesAttached { get; set; }
        public List<Custom_CheckRequest_Fees> feesList { get; set; }
        public List<Custom_ExImportCompanies> ImpCompanies { get; set; }
    }
    public class Custom_ExImportCompanies
    {
        public int Index { get; set; }
        public string ImportCompany { get; set; }
        public string ImporeterCompanyAddress { get; set; }
        public string Reciever_Name { get; set; }
    }

    public class Custom_ExPlants
    {
        public Custom_ExPlants()
        {
            lotData = new List<LotData>();
        }
        public const int IsPlant = 4;
        public int index { get; set; }
        public long Plant_ID { get; set; }
        public string PlantShortName { get; set; }
        public Nullable<long> PlantCat_ID { get; set; }
        public byte PlantPartType_ID { get; set; }
        public byte ProductStatus_ID { get; set; }
        public byte Purpose_ID { get; set; }
        public string HSCODE { get; set; }
        public List<LotData> lotData { get; set; }
    }
    public class Custom_ExProducts
    {
        public Custom_ExProducts()
        {
            lotData = new List<LotData>();
        }
        public const int IsPlant = 5;
        public int index { get; set; }
        public long Plant_ID { get; set; }
        public long Product_ID { get; set; }
        public string HSCode { get; set; }
        public byte ProductStatus_ID { get; set; }
        public byte Purpose_ID { get; set; }
        public List<LotData> lotData { get; set; }
    }
    public class Custom_ExAliveLiableItems
    {
        public Custom_ExAliveLiableItems()
        {
            lotData = new List<LotData>();
        }
        //syscode=16
        public const int IsPlant = 16;
        public int index { get; set; }
        public int alive_ID { get; set; }
        public byte Purpose_ID { get; set; }
        public int Status_ID { get; set; }
        public int BiologicalPhase { get; set; }
        public int Strain_ID { get; set; }
        public string ShortName { get; set; }
        public string HSCODE { get; set; }
        public List<LotData> lotData { get; set; }
    }
    public class Custom_ExNotAliveLiableItems
    {
        public Custom_ExNotAliveLiableItems()
        {
            lotData = new List<LotData>();
        }
        //syscode=33
        public const int IsPlant = 33;
        public int index { get; set; }
        public int notAlive_ID { get; set; }
        public byte Purpose_ID { get; set; }
        public int Status_ID { get; set; }
        public string ShortName { get; set; }
        public string HSCODE { get; set; }
        public List<LotData> lotData { get; set; }
    }
    public class LotData
    {
        public int LotIndex { get; set; }
        public string Lot_Number { get; set; }
        public short Package_Material_ID { get; set; }
        public short Package_Type_ID { get; set; }
        public int Package_Count { get; set; }
        public Nullable<long> Farm_ID { get; set; }
        public string farmAddress { get; set; }//ناحية الزراعة
                                               //***************************************//
        public decimal Package_Weight { get; set; }
        public decimal Package_Weight_Ton { get; set; }
        public decimal Package_Weight_Kilo { get; set; }
        public decimal Package_Weight_Gram { get; set; }
        //***//
        public decimal Gross_Weight { get; set; }
        public decimal Gross_Weight_Ton { get; set; }
        public decimal Gross_Weight_Kilo { get; set; }
        public decimal Gross_Weight_Gram { get; set; }
        //***//
        public decimal Net_Weight { get; set; }
        public decimal Net_Weight_Ton { get; set; }
        public decimal Net_Weight_Kilo { get; set; }
        public decimal Net_Weight_Gram { get; set; }
        //**************************************//
        public int govID { get; set; }
        public int centerID { get; set; }
        public int villageID { get; set; }
    }
    public class Custom_Attatchment
    {
        public int Index { get; set; }
        public long RowId { get; set; }
        public short A_AttachmentTableNameId { get; set; }
        public string AttachmentPath { get; set; }
        //public HttpPostedFileBase AttachmentPath_Content { get; set; }
        public string Attachment_Number { get; set; }
        public string Attachment_TypeName { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public class Custom_Constrains
    {
        //c.ConstrainText_Ar,c.CountryConstrain_Type , ConstrainOwner_ID
        public string ConstrainText_Ar { get; set; }
        public Int32 CountryConstrain_Type { get; set; }
        public Int16 ConstrainOwner_ID { get; set; }
        public string union_Name { get; set; }
    }
    public class Custom_CheckRequest_Fees
    {
        public int FixedFeesAmount_ID { get; set; }
        public string FeesTypeName { get; set; }
        public Nullable<decimal> FeeValue { get; set; }
    }
}
