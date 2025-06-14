using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Admin
{
    public class userPriviledgeBLL
    {
        private UnitOfWork uow;
        dbPrivilageEntities entities1 = new dbPrivilageEntities();

        public userPriviledgeBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> getUserPermisions(int userID,int menuId, int modelID, int GroupId, List<string> Device_Info)
        {
          
            var permissions1 = (
                 from cc in entities1.PR_GroupModuleMenuPrivilage
                     // join rr in entities1.PR_UserGroup
                     // on cc.PR_GroupId equals rr.PR_GroupId

                 where
                 cc.PR_MenuId == menuId && cc.PR_ModuleId == modelID
                 && cc.PR_GroupId == GroupId && cc.PR_User_id == userID
                 select new userPriviledgeDTO
                 {

                     CanPrint = cc.CanPrint,
                     CanView = cc.CanView

                 }
                 ).FirstOrDefault();

            //var per = entities1.PR_GroupModuleMenuPrivilage.
            //    Where(p=>p.PR_User_Id==userID && p.PR_MenuId == menuId && 
            //    p.PR_ModuleId == modelID &&p.PR_GroupId==GroupId).Select(x=> new userPriviledgeDTO
            //{

            //    CanPrint = x.CanPrint,
            //    CanView = x.CanView

            //}).FirstOrDefault();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, permissions1);


        }
    }
}
