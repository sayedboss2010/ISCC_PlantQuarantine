using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmConstrain
{
    public class Farm_CheckList_Constrain_DTO
    {
        //Farm_Constrain
        public long ID_Farm_CheckList { get; set; }
        public long ID_Farm_Country_CheckList { get; set; }
        public Nullable<long> Item_ID { get; set; }
        public Nullable<short> Country_Id { get; set; }
        public long Farm_Constrain_Text_ID { get; set; }
        public Nullable<bool> Is_Preview { get; set; }
        public Nullable<int> AnalysisType_ID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<byte> Count_Visit { get; set; }

        public string Item_Name { get; set; }
        public string Country_Name { get; set; }

        //Farm_CheckList

        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        public string Description_Ar { get; set; }
        public string Description_En { get; set; }

    }

    public class Farm_CheckListDTO
    {
        public long ID { get; set; }
        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        public string Description_Ar { get; set; }
        public string Description_En { get; set; }
        public bool IsActive { get; set; }
        public bool IsMotataleb { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
    }
}
