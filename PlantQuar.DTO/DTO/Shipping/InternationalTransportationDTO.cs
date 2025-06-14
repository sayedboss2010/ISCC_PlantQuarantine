using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Shipping
{
   public class InternationalTransportationDTO
    {

        public long ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string TransferMethod { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<byte> Transport_Mean_Id { get; set; }
        public Nullable<byte> Shipment_Mean_Id { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       // public virtual ICollection<Im_CheckRequest_Data> Im_CheckRequest_Data { get; set; }
    }
}
