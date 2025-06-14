using System;

namespace PlantQuar.DTO.DTO.DataEntry.Items.ItemData
{
    public class Item_ShortNameDTO
    {
        public long ID { get; set; }
        public Nullable<long> Item_ID { get; set; }
        public Nullable<long> SubPart_ID { get; set; }
        public Nullable<int> Item_Status_ID { get; set; }
        public Nullable<int> Item_Purpose_ID { get; set; }
        public string ShortName_Ar { get; set; }
        public string ShortName_En { get; set; }
        public bool ExportStatus { get; set; }
        public bool ImportStatus { get; set; }
        public string Reason { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<bool> IS_ShortName { get; set; }
        public Nullable<short> QualitativeGroup_Id { get; set; }
        public Nullable<byte> Item_Type_ID { get; set; }
        public bool Is_ImportTaxFree { get; set; }
        public Nullable<long> Product_ID { get; set; }
        public Nullable<long> totItems { get; set; }
        public bool IsKnown { get; set; }
        public Nullable<long> ItemCategories_Group_ID { get; set; }

        /// <summary>
        /// SARA
        /// </summary>
        public string SubPart_Name { get; set; }
        public string Status_Name { get; set; }
        public string Purpose_Name { get; set; }
    }
}