using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Admin
{
    public class Transation_LogsDTO
    {
        public long ID { get; set; }

        public Nullable<System.DateTime> Action_Date { get; set; }
        public Nullable<long> User_Id { get; set; }
        public Nullable<long> User_Id_CheckRequest { get; set; }
        public string Staus_Name { get; set; }
        public string User_Name { get; set; }

        public Nullable<int> User_Type_ID { get; set; }
        public string User_Type_Name { get; set; }
        public string Notes { get; set; }


        public short ID_Table_Action { get; set; }
        public Nullable<long> ID_TableActionValue { get; set; }

        public string NOTS { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
    }
}
