using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.StationNew
{
    public class FixedFeesAmountDTO
    {

        public int ID { get; set; }
        public string Name_En { get; set; }
        public string Name_Ar { get; set; }
        public string feesName { get; set; }
        public byte FeesType_Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public Nullable<double> WeightFrom { get; set; }
        public Nullable<double> WeightTo { get; set; }
        public Nullable<bool> IsPaidBefore { get; set; }
        public bool IsExport { get; set; }
        public bool IsActive { get; set; }
        public bool IsMandatory { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

    }
}
