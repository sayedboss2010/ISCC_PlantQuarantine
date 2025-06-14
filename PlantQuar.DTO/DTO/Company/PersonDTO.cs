using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Company
{
    public class PersonDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Person_IDType { get; set; }
        public string IDNumber { get; set; }
        public Nullable<short> Country_ID { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //************************//
        public string personIdType_Name { get; set; }
        public string nationality { get; set; }
        
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public bool IsActive { get; set; }
    }
}