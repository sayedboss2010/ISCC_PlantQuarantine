using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;

namespace PlantQuar.DTO.DTO.Farm.FarmData
{
    public class FarmsDataDTO
    {
        public FarmsDataDTO()
        {

            ownerData = new Farm_CompanyDTO();
            plantList = new List<Farm_Request_ItemCategoriesDTO>();
            requestLst = new List<FarmRequestDTO>();
            attachmentList = new List<A_AttachmentDataDTO>();
        }

        public long ID { get; set; }
        public long Farm_Request_ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string FarmCode_14 { get; set; }
        public string print_text { get; set; }
        public string ispayment { get; set; }
        public Nullable<short> Village_ID { get; set; }
        public short Govern_ID { get; set; }
        public short Center_Id { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public string ThePivot { get; set; }
        public string GPSRead { get; set; }
        public Nullable<bool> Status { get; set; }
        public string FileUpload { get; set; }
        public string FileUpload_File { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        //*************************sayed
        public Nullable<bool> IsActive { get; set; }
        public string ScientificName { get; set; }
        //*************************************//

        public Nullable<bool> IS_OnlineOffline { get; set; }

        public Nullable<long> Item_ID { get; set; }

        //*******************************************//
        public string GoveName { get; set; }
        public string CenterName { get; set; }
        public string VillageName { get; set; }

        //************************************//
        public string PlantName { get; set; }

        public int mainClass_Id { get; set; }
        public int secClass_Id { get; set; }
        public int group_Id { get; set; }
        public bool isKnown { get; set; }
        public string Print_Text { get; set; }
        public Farm_CompanyDTO ownerData { get; set; }
        public List<Farm_Request_ItemCategoriesDTO> plantList { get; set; }
        public List<FarmRequestDTO> requestLst { get; set; }
        public List<A_AttachmentDataDTO> attachmentList { get; set; }

        public List<Farm_Requst_ListDTO> _Farm_Requst_List { get; set; }
        public List<Farm_Requst_RefuseReason_ListDTO> Farm_Requst_RefuseReason_List { get; set; }

        //************************************//
        public Nullable<long> RequestId { get; set; }
        public Nullable<bool> requestAccepted { get; set; }
        public Nullable<bool> requestPaid { get; set; }
    }
    public class Farm_Requst_RefuseReason_ListDTO
    {
        public long ID { get; set; }
        public long Farm_Request_ID { get; set; }
        public Nullable<short> Refuse_Reason_ID { get; set; }
        public string Nots { get; set; }
        public string Refuse_Reason { get; set; }

        public List<short> refuseReasonsIds { get; set; }

    }
    public class UpdateFarmModelDTO
    {
        public long ID { get; set; }
        public string FarmCode_14 { get; set; }
        public string print_text { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public long Farm_Request_ID { get; set; }


    }

}
