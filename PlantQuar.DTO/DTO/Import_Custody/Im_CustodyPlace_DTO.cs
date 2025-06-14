using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import_Custody
{
    public class Im_CustodyPlace_DTO
    {
        public long ID { get; set; }
        public long Im_CheckRequest_ID { get; set; }
        public string En_Desc { get; set; }
        public string Ar_Desc { get; set; }
        public byte PlaceType { get; set; }
        public double Storage_capacity { get; set; }
        public short Center_Id { get; set; }
        public string Address { get; set; }
        public string Owner_Name { get; set; }
        public string NationalID { get; set; }
        public string Phone { get; set; }
        public double PreviewQuantityDuration { get; set; }
        public Nullable<System.DateTime> DateStored { get; set; }
        public double Quantity { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }       
    }
}
