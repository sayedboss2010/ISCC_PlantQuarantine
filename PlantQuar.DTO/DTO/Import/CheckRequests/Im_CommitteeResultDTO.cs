using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
   public  class Im_CommitteeResultDTO
    {

        public long ID { get; set; }
        public long Committee_ID { get; set; }
        public long Im_Request_Item_Id { get; set; }
        public Nullable<long> LotData_ID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<byte> CommitteeResultType_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> IsAdminResult { get; set; }
        public string AdminFinalResult_Note { get; set; }
        public Nullable<double> QuantitySize { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        //  public string Text_Lot { get; set; }
        public long Item_ShortName_ID { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }

        ////public List<CommitteeResultType> committeeResultType { get; set; }
        ////public List<Im_CommitteeResult_Confirm> im_CommitteeResult_Confirm { get; set; }
        //public List<Im_RequestCommittee> im_RequestCommittee { get; set; }
       // public List<Im_CommitteeResult_Infection> im_CommitteeResult_Infection { get; set; }
        //public class Im_RequestCommittee
        //{
        //    public long ID { get; set; }
        //    public long Committee_ID { get; set; }
        //    public long Im_Request_Item_Id { get; set; }
        //    public Nullable<long> LotData_ID { get; set; }
        //    public Nullable<long> EmployeeId { get; set; }
        //    public Nullable<byte> CommitteeResultType_ID { get; set; }
        //    public Nullable<System.DateTime> Date { get; set; }
        //    public Nullable<bool> IsAdminResult { get; set; }
        //    public string AdminFinalResult_Note { get; set; }
        //    public Nullable<double> QuantitySize { get; set; }
        //    public Nullable<double> Weight { get; set; }
        //    public string Notes { get; set; }
        //    public Nullable<bool> IS_Total { get; set; }
        //    public string Text_Lot { get; set; }
        //    public Nullable<short> User_Deletion_Id { get; set; }
        //    public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        //    public Nullable<short> User_Creation_Id { get; set; }
        //    public Nullable<System.DateTime> User_Creation_Date { get; set; }
        //    public Nullable<short> User_Updation_Id { get; set; }
        //    public Nullable<System.DateTime> User_Updation_Date { get; set; }
        //}

    }
}
