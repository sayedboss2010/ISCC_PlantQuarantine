using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.ExportRequest
{

    public class CommitteeTypeBLL : IGenericBLL<ex_CommitteeTypeDTO>
    {
        private UnitOfWork uow;

        public CommitteeTypeBLL()
        {
            uow = new UnitOfWork();
        }

        //Find CommitteeType Obj
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                CommitteeType entity = uow.Repository<CommitteeType>().Findobject(Id);
                var empDTO = Mapper.Map<CommitteeType, ex_CommitteeTypeDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Count CommitteeType
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<CommitteeType>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        //Get CommitteeType List 
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info) 
        {
            try
            {

                var data = new List<CommitteeType>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<CommitteeType>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<CommitteeType>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<CommitteeType, ex_CommitteeTypeDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get CommitteeType List Search by ArName & EnName
        public Dictionary<string, object>  GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)

        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<CommitteeType>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<CommitteeType>().GetData().Where(a =>
                       a.Name_En.StartsWith(enName.Trim()) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<CommitteeType>().GetData().Where(a =>
                         a.Name_Ar.StartsWith(arName.Trim()) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<CommitteeType>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    //data = data.Where(a => (a.Ar_Name.StartsWith(arName) || a.En_Name.StartsWith(enName))).ToList();
                    data = uow.Repository<CommitteeType>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }
                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        data = data.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        data = data.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        data = data.OrderByDescending(t => t.Name_En).ToList();
                        break;


                }
                string lang = Device_Info[2];
                var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<CommitteeType, ex_CommitteeTypeDTO>);

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("CommitteeType_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any CommitteeType
        public bool GetAny(ex_CommitteeTypeDTO entity)
        {
            var obj = entity;
            return uow.Repository<CommitteeType>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        //Create CommitteeType
        public Dictionary<string, object> Insert(ex_CommitteeTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();

                if (!GetAny(entity))
                {
                    entity.ID = uow.Repository<Object>().GetNextSequenceValue_Byte("CommitteeType_seq");
                    var CModel = Mapper.Map<CommitteeType>(entity);
                    uow.Repository<CommitteeType>().InsertRecord(CModel);
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

        //Update CommitteeType
        public Dictionary<string, object> Update(ex_CommitteeTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();

                if (!GetAny(entity))
                {
                    var obj = entity as ex_CommitteeTypeDTO;
                    CommitteeType CModel = uow.Repository<CommitteeType>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<CommitteeType>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<CommitteeType, ex_CommitteeTypeDTO>(Co);
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

        //Delete CommitteeType
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<CommitteeType>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<CommitteeType>().Update(Cmodel);
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

        //DROPS
        //Get CommitteeType List DDL
        public Dictionary<string, object> FillCommitteeType_List(int Lst, List<string> Device_Inf)
        {

            var data = uow.Repository<CommitteeType>().GetData().Where(x => x.User_Deletion_Id == null)
                .Select(c => new CustomOptionShortId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //Get CommitteeType Create & Update DDL
        public Dictionary<string, object> FillCommitteeType_AddEdit(List<string> Device_Inf)
        {
            string lang = Device_Inf[2];
            var data = uow.Repository<CommitteeType>().GetData().Where(x => x.User_Deletion_Id == null).Select(c => new CustomOptionShortId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            CustomOptionShortId empty = new CustomOptionShortId();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

    }
}
