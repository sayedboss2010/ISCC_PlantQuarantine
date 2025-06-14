using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmCommittee
{
    public class FarmRequestItem_CategoriesBLL
    {
        private UnitOfWork uow;

        public FarmRequestItem_CategoriesBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetAll(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();


                Int64 data_Count = 0;
                var farmReqId = uow.Repository<Farm_Committee>().GetData().Where(c => c.ID == FarmCommittee_ID).FirstOrDefault().Farm_Request_ID;

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from fic in entities.Farm_ItemCategories
                            join fs in entities.Farm_Request_ItemCategories on fic.ID equals fs.Farm_ItemCategories_ID
                            join fce in entities.Farm_Committee_Examination on fs.ID equals fce.Farm_Request_ItemCategories_ID
                            join itg in entities.ItemCategories on fic.ItemCategories_ID equals itg.ID

                            //from ii in iis.DefaultIfEmpty()
                            where fs.Farm_Request_ID == farmReqId

                            select new Farm_Request_ItemCategoriesDTO
                            {
                                ID = fs.ID,
                                Farm_ItemCategories_ID = fic.ID,
                                IsActive = fs.IsActive,
                                categoryName = itg.Name_Ar,
                                Area_Acres = fic.Area_Acres,
                                Quantity_Ton = fce.Quantity_Ton,

                            }).ToList();

                string lang = Device_Info[2];
                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
                data_Count = data.Count();
                var allIsActive = 0;
                if (data.Where(d => d.IsActive == null).ToList().Count > 0)
                {
                    allIsActive = 1;
                }
                var status = 0;
                var statusRequest = uow.Repository<Farm_Committee>().GetData().Include(f => f.Farm_Request).Where(c => c.ID == FarmCommittee_ID).Select(s => s.Farm_Request.IsStatus).FirstOrDefault();
                if (statusRequest == true)
                {
                    status = 1;
                }
                if (statusRequest == false)
                {
                    status = 2;
                }
                dic.Add("Count_Data", data_Count);
                dic.Add("Farm_Committee_Examination_Data", data);
                dic.Add("allIsActive", allIsActive);
                dic.Add("statusRequest", status);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }
        public Dictionary<string, object> Update(Farm_Request_ItemCategoriesDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Request_ItemCategories CModel = uow.Repository<Farm_Request_ItemCategories>().Findobject(entity.ID);

                CModel.IsActive = entity.IsActive;

                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
