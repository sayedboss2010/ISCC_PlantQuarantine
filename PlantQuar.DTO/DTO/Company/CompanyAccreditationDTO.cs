using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Company
{
    public class CompanyAccreditationDTO
    {
        public long ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<short> Country_ID { get; set; }
     
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<long> Item_ShortName_id { get; set; }
        public Nullable<int> itemTypeLst { get; set; }
        public Nullable<int> groupLst { get; set; }
        public Nullable<long> plantLst { get; set; }
    }
}
