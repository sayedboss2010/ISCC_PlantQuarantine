using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlantQuar.DTO.DTO.Station
{
    public class StationDTO
    {
        public StationDTO()
        {
            Contacts = new List<StationContactDTO>();
        }
        public long ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public string StationCode { get; set; }
        public string TaxesRecord { get; set; }
        public string CommertialRecord { get; set; }
        public string Industrial_License_Num { get; set; }
        public string FileUpload { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<short> Gov_Id { get; set; }
        public Nullable<short> Center_Id { get; set; }
        public Nullable<short> Village_Id { get; set; }
       
       

        public List<StationContactDTO> Contacts { get; set; }
        public HttpPostedFileBase file { get; set; }

    }
}
