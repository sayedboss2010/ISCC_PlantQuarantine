using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Fees
{
    public class Im_Fees_ActionsBLL
    {
        private UnitOfWork uow;

        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        public Im_Fees_ActionsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find_Fees_Request_Actions(List<string> Device_Info)
        {
                             var permissions = (from r in entities.Item_ShortName.Where(a => a.User_Deletion_Id==null)
                                                join p in entities.Fees_Action.Where(a=> a.User_Deletion_Id == null&&a.IsActive==true && a.FeesType_Id==6) on
                               r.ID equals p.Item_Shift_Treatment_ID 
                               into ps
                               from p in ps.DefaultIfEmpty()                                   
                            group p by new
                            {
                                Id = r.ID,
                                ShortName_Ar = r.ShortName_Ar,
                                item_Name_Ar = r.Item.Name_Ar,
                                Is_ImportTaxFree=r.Is_ImportTaxFree,
                                QualitativeGroup=r.QualitativeGroup.Name_Ar,

                            } into grp

                            select new Im_Fees_ActionsDTO
                            {
                                Id = grp.Key.Id,
                                ShortName_Ar = grp.Key.ShortName_Ar,
                                item_Name_Ar = grp.Key.item_Name_Ar,
                                Is_ImportTaxFree=grp.Key.Is_ImportTaxFree,
                                QualitativeGroup=grp.Key.QualitativeGroup,
                                Fees_Import = grp.Sum(p => p.Feer_Type_Action_ID == 9 ? p.Amount : 0),

                                Fees_Tranzet = grp.Sum(p => p.Feer_Type_Action_ID == 8 ? p.Amount : 0)

                            }).OrderByDescending(x => x.Id).ToList();


            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, permissions);
        }

        public Dictionary<string, object> FillDrop_Fees_Money_List(List<string> Device_Info)
        {
            var values = (from m in entities.Fees_Money
                               select new Im_Fees_ActionsDTO
                               {
                                  
                                   Fees_Import = m.Fees,
                                   Fees_Tranzet=m.Fees,
                                   Type =m.Type,
                                   Id_Check = m.ID,
                               }
                  ).ToList();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, values);
            //return null;
        }


        public Dictionary<string, object> Insert(List<Fees_ActionDTO> entity, List<string> Device_Info)  
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();

            try
            {
                foreach (var item in entity)
                {
                    var Fees_Action_Update = entities.Fees_Action.Where(a => a.FeesType_Id == 6 
                    && a.Item_Shift_Treatment_ID == item.Item_Shift_Treatment_ID && a.Feer_Type_Action_ID == item.Feer_Type_Action_ID);

                    foreach (var Action_Update in Fees_Action_Update)
                    {

                        Action_Update.IsActive = false;
                    }

                    entities.SaveChanges();

                    long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Fees_Action_seq");
                    item.ID = CommitteResult_ID;
                    var Co = Mapper.Map<Fees_ActionDTO, Fees_Action>(item);
                    uow.Repository<Fees_Action>().InsertReturn(Co);
                    uow.SaveChanges();
                }


                
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}



//var q =
//    from c in categories
//    join p in products on c.Category equals p.Category into ps
//    from p in ps.DefaultIfEmpty()
//    select new { Category = c, ProductName = p == null ? "(No products)" : p.ProductName };