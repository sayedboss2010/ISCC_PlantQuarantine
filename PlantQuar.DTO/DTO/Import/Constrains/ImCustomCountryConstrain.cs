using System;
using System.Collections.Generic;

namespace PlantQuar.DTO.DTO.Import.Constrains
{
    public class ImCustomCountryConstrain
    {
        public long ID { get; set; }
        public Nullable<byte> Group_ID { get; set; }
        public Nullable<short> QualGroup_ID { get; set; }
        //public bool IsCertificate_Addtion_plant { get; set; }
        public long? ItemShortNameId { get; set; }
        public long? ItemId { get; set; }
        public long? ProductId { get; set; }
        public List<long> InitiatorList_Items { get; set; }
        public List<long> InitiatorList_QualGroup { get; set; }

        //eman
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

        public Im_Items items { get; set; }
        public ImQualGroup QualitiveGroup { get; set; }
    }
    //****************************************//
    public class Im_Items
    {
        public long itemId { get; set; }
        public byte statusId { get; set; }
        public byte purposeId { get; set; }
        public byte subPartId { get; set; }
        public long? PlantCatId { get; set; }

        public List<ImCustomItemConstrain_Rows> ItemConstrain_Rows { get; set; }
        public List<ImCustomItemConstrain_ArrivalPorts> ItemConstrain_ArrivalPorts { get; set; }
    }
    public class ImQualGroup
    {
        public List<ImCustomItemConstrain_Rows> QualGConstrain_Rows { get; set; }
        public List<ImCustomItemConstrain_ArrivalPorts> QualGConstrain_ArrivalPorts { get; set; }
    }

    public class ImCustomItemConstrain_Rows
    {
        public int index { get; set; }
        public long Id { get; set; }
        public byte conTypeId { get; set; }
        public string countryName { get; set; }
        public long countryConstraintext_Id { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        //IsCertificate_Addtion
        //public bool IsCertificate_Addtion { get; set; }

        public string InSide_Certificate_Ar { get; set; }
        public string InSide_Certificate_En { get; set; }

    }

    public class ImCustomItemConstrain_ArrivalPorts
    {
        public int index { get; set; }
        public long Id { get; set; }

        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

        //********************//
        public long ArrivalConstrain_ID { get; set; }
        public Nullable<int> PortnationalID { get; set; }
        public short PortType_ID { get; set; }


        public Nullable<int> PortorganizationalID { get; set; }

        //*******************//
    }
}