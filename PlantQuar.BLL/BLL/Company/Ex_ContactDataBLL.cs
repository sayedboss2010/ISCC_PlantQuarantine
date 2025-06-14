using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Company
{
    public class Ex_ContactDataBLL
    {
        private UnitOfWork uow;

        public Ex_ContactDataBLL()
        {

            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Ex_ContactData>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.ContactType.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Ex_ContactData entity = uow.Repository<Ex_ContactData>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_ContactData, Exporter_ContactDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Ex_ContactData>().GetData().Where(a => a.User_Deletion_Id == null
             // get undeleted parent
             && a.ContactType.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<Ex_ContactData, Exporter_ContactDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                //Complete Code

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(Exporter_ContactDTO obj)
        {
            // return true;
            return uow.Repository<Ex_ContactData>().GetAny(a => a.Exporter_ID == obj.Exporter_ID && a.ContactType_ID == obj.ContactType_ID && a.ExporterType_Id == obj.ExporterType_Id && a.Value == obj.Value && a.User_Deletion_Id == null && a.ID != obj.ID);
        }
        //******************************************//
        public Dictionary<string, object> Insert(Exporter_ContactDTO entity, List<string> Device_Info)
        {
            try
            {


                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Ex_ContactData>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_ContactData_seq");
                    //entity.ID =int.Parse( id.ToString());

                    uow.Repository<Ex_ContactData>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Exporter_ContactDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    Ex_ContactData CModel = uow.Repository<Ex_ContactData>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Ex_ContactData>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Ex_ContactData, Exporter_ContactDTO>(Co);
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
                var Cmodel = uow.Repository<Ex_ContactData>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Ex_ContactData>().Update(Cmodel);
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
        public Dictionary<string, object> InsertRecords(short user_id, long Exporter_ID, DateTime Date_Now, int ExporterType_Id, List<Exporter_ContactDTO> objRecords, List<string> Device_Info)
        {
            try
            {
                foreach (var item in objRecords)
                {
                    if (item.DeleteCheck != 1)
                    {
                        var CModel = Mapper.Map<Ex_ContactData>(item);
                        CModel.User_Creation_Date = Date_Now;
                        CModel.User_Creation_Id = user_id;
                        CModel.ExporterType_Id = ExporterType_Id;
                        CModel.Exporter_ID = Exporter_ID;
                        CModel.IsActive = true;

                        Insert(Mapper.Map<Exporter_ContactDTO>(CModel), Device_Info);
                    }

                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> UpdateRecords(short user_id, long Exporter_ID, DateTime Date_Now, int ExporterType_Id, List<Exporter_ContactDTO> objRecords,
            List<string> Device_Info)
        {
            try
            {
                var CDTO = Mapper.Map<List<Exporter_ContactDTO>>(objRecords);
                var lstAddEx_ContactDatas = CDTO.Where(a => (a.ID == 0)).ToList();
                //// Add///
                if (lstAddEx_ContactDatas.Count > 0)
                {
                    InsertRecords(user_id, Exporter_ID, Date_Now, ExporterType_Id, Mapper.Map<List<Exporter_ContactDTO>>(lstAddEx_ContactDatas), Device_Info);

                }
                // Edit
                var lstEditEx_ContactDatas = CDTO.Where(a => (a.DeleteCheck != 1 && a.ID > 0)).ToList();

                if (lstEditEx_ContactDatas.Count() > 0)
                {
                    foreach (var item in lstEditEx_ContactDatas)
                    {
                        item.User_Updation_Date = Date_Now;
                        item.User_Updation_Id = user_id;
                        item.Exporter_ID = Exporter_ID;
                        item.ExporterType_Id = ExporterType_Id;
                        item.IsActive = true;
                        Update(Mapper.Map<Exporter_ContactDTO>(item), Device_Info);
                    }
                }
                // Delete
                var lstDeleteEx_ContactDatas = CDTO.Where(a => (a.DeleteCheck == 1 && a.ID > 0)).ToList();

                if (lstDeleteEx_ContactDatas.Count() > 0)
                {
                    foreach (var item in lstDeleteEx_ContactDatas)
                    {
                        item.User_Deletion_Date = Date_Now;
                        item.User_Deletion_Id = user_id;
                        item.Exporter_ID = Exporter_ID;
                        item.ExporterType_Id = ExporterType_Id;
                        Update(Mapper.Map<Exporter_ContactDTO>(item), Device_Info);
                    }
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }

        }
    }
}
