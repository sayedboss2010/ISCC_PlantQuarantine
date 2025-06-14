using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmData
{
    public class FarmPlantBLL
    {
        private UnitOfWork uow;
        public FarmPlantBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Fill_PlantCategories(List<string> Device_Info, int Item_ID)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategory>().GetData().Where(a => a.User_Deletion_Id == null && a.Item_ID == Item_ID
            //13-5-2020 fz removed as in constrain don't need it
            // && a.IsForbidden == false
            ).
                Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> GetAll(Int64 ItemID, Int64 Farm_ID, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Farm_ItemCategories>();
                Int64 data_Count = 0;
                data = uow.Repository<Farm_ItemCategories>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               //&& p.Plant_ID == ItemID
               && p.Farm_ID == Farm_ID
               && p.User_Deletion_Id == null
           && p.User_Deletion_Id == null).ToList();
                //Complete Code
                var dataDto = data.OrderBy(A => A.ID).Select(Mapper.Map<Farm_ItemCategories, FarmPlantDTO>);

                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("ItemPart_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(FarmPlantDTO entity)
        {
            return uow.Repository<Farm_ItemCategories>().GetAny(p => (p.User_Deletion_Id == null)
            && p.Farm_ID == entity.Farm_ID && p.Area_Acres == entity.Area_Acres &&
            p.StartDate == entity.StartDate && p.EndDate == entity.EndDate 
            //&& p.PlantCat_ID == entity.PlantCat_ID &&
            //p.Plant_ID == entity.Plant_ID 
            && p.Quantity_Ton == entity.Quantity_Ton &&
            (entity.ID == 0 ? true : p.ID != entity.ID)
            );
        }
        //******************************************//

        public Dictionary<string, object> Insert(FarmPlantDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Farm_ItemCategories>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("FarmPlant_Seq");
                    uow.Repository<Farm_ItemCategories>().InsertRecord(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(FarmPlantDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as FarmPlantDTO;
                    Farm_ItemCategories CModel = uow.Repository<Farm_ItemCategories>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Farm_ItemCategories>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Farm_ItemCategories, FarmPlantDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Farm_ItemCategories>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Farm_ItemCategories>().Update(Cmodel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
