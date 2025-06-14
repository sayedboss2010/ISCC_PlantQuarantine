using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Farm.FarmData
{
    public class Farm_CompanyBLL
    {
        private UnitOfWork uow;

        public Farm_CompanyBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Farm_Company entity = uow.Repository<Farm_Company>().Findobject(Id);
                var _DTO = Mapper.Map<Farm_Company, Farm_CompanyDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Farm_Company>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Farm_Company>().GetData(pageSize, index, A => 1 == 1)
                    .Where(a => a.User_Deletion_Id == null).ToList();
                var dataDTO = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Farm_Company, Farm_CompanyDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Farm_CompanyDTO obj)
        {
            return uow.Repository<Farm_Company>().GetAny(p => (p.User_Deletion_Id == null)
            && (obj.Company_ID == p.Company_ID && obj.Farm_ID == p.Farm_ID)
            && p.Start_Date == obj.Start_Date && p.End_Date == obj.End_Date &&
            (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(Farm_CompanyDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Farm_Company>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Company_Seq");
                    uow.Repository<Farm_Company>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Farm_CompanyDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    Farm_Company CModel = uow.Repository<Farm_Company>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<Farm_Company>().Update(Co);
                    uow.SaveChanges();

                    var _DTO = Mapper.Map<Farm_Company, Farm_CompanyDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, _DTO);
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