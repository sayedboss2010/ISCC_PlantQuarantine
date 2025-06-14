using PlantQuar.DTO.DTO.Station;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Accreditation_Data_Entry_DTO
    {
        public long ID { get; set; }
        public Nullable<byte> StationActivityType_ID { get; set; }
        public Nullable<int> Accreditation_Type_ID { get; set; }
        public string Name_AR { get; set; }
        public string Name_En { get; set; }
        public string Description_Ar { get; set; }
        public string Description_En { get; set; }
        public string DescriptionMore_AR { get; set; }
        public string DescriptionMore_EN { get; set; }
        public bool IsActive { get; set; }

        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }


        //*********************************
        public string StationActivityType_Name { get; set; }
        public string Accreditation_Type_Name { get; set; }
        //************************************//

        public int mainClass_Id { get; set; }
        public int secClass_Id { get; set; }
        public int group_Id { get; set; }
        public bool isKnown { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public Nullable<long> Item_ID { get; set; }
        public string ScientificName { get; set; }
        /// <summary>
        /// المعالجات
        /// 
        /// </summary>
        /// 
        public string MainTreatment_Name { get; set; }
        public string TreatmentType_Name { get; set; }
        public string TreatmentMethod_Name { get; set; }
        public string Message { get; set; }

        public List<Station_Accreditation_Data_CountryDTO> List_Station_Country { get; set; }
        public List<Station_Accreditation_Data_Item_ShortNameDTO> List_Station_Item_ShortName { get; set; }
        public List<Station_CheckList_DTO> List_Station_CheckList { get; set; }
        public List<A_AttachmentData_Station_DTO> List_Station_Attachment { get; set; }
    }
    public class Station_Accreditation_Data_Item_ShortNameDTO
    {
        public long ID { get; set; }
        public long Item_ShortName_ID { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }

        public string Item_Name { get; set; }
        public string ShortName_Name { get; set; }

        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }

    }
    public class Station_Accreditation_Data_CountryDTO
    {
        public Nullable<long> Id { get; set; }
        public Nullable<long> Station_Accreditation_Data_Country_ID { get; set; }
        public Nullable<long> Station_Accreditation_Data_ID { get; set; }
        public short CountryID { get; set; }
        public Nullable<short> Union_Id { get; set; }
        public bool IsActive { get; set; }
        public string Country_Name { get; set; }
        public string Union_Name { get; set; }
    }
    public class A_AttachmentData_Station_DTO
    {
        public long Id { get; set; }
        public long RowId { get; set; }
        public short A_AttachmentTableNameId { get; set; }
        public string Attachment_Number { get; set; }
        public string Attachment_TypeName { get; set; }
        public string AttachmentPath { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public byte[] AttachmentPath_Binary { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> A_AttachmentTableType_ID { get; set; }

        public long Index { get; set; }
    }

    public class Station_CheckList_DTO
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public long Station_CheckList_ID { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }

        public string Station_Constrain_Type_Name { get; set; }
        public string Station_Accreditation_Country_Item_Name { get; set; }
        public string Station_CheckList_Android_Name { get; set; }
        public string Station_Accreditation_Text_Name { get; set; }
        public string InSide_Certificate_Ar { get; set; }
    }
}
