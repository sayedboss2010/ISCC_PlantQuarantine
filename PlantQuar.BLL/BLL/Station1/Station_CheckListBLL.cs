using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station
{
    public class Station_CheckListBLL
    {
        private UnitOfWork uow;

        public Station_CheckListBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(long Id, List<string> Device_Info)
        {
            try
            {

                Station_CheckList entity = uow.Repository<Station_CheckList>().Findobject(Id);
                var empDTO = Mapper.Map<Station_CheckList, Station_CheckListDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {

            var count = uow.Repository<Station_CheckList>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);

        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Station_CheckList>().GetData().Where(p => p.User_Deletion_Id == null).
                    OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).Skip(index).Take(pageSize).ToList();
                //var data = uow.Repository<AnalysisLab>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<Station_CheckList, Station_CheckListDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Station_CheckList>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Station_CheckList>().GetData().Where(a =>
                      a.ConstrainText_En.StartsWith(enName.Trim()) &&
                   a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = data.Where(a => a.ConstrainText_Ar.StartsWith(arName.Trim())).ToList();

                    data = uow.Repository<Station_CheckList>().GetData().Where(a =>
                        a.ConstrainText_Ar.StartsWith(arName.Trim()) &&
                     a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Station_CheckList>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = data.Where(a => (a.ConstrainText_Ar.StartsWith(arName) || a.ConstrainText_En.StartsWith(enName))).ToList();
                    data = uow.Repository<Station_CheckList>().GetData().Where(a =>
                    (a.ConstrainText_Ar.StartsWith(arName.Trim()) && a.ConstrainText_En.StartsWith(enName.Trim())) &&
                  a.User_Deletion_Id == null).ToList();
                }
                string lang = Device_Info[2];

                //var dataDto = data.OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Station_CheckList, Station_CheckListDTO>);

                switch (jtSorting)
                {
                    case "ConstrainText_Ar ASC":
                        data = data.OrderBy(t => t.ConstrainText_Ar).ToList();
                        break;
                    case "ConstrainText_Ar DESC":
                        data = data.OrderByDescending(t => t.ConstrainText_Ar).ToList();
                        break;
                    case "ConstrainText_En ASC":
                        data = data.OrderBy(t => t.ConstrainText_En).ToList();
                        break;
                    case "ConstrainText_En DESC":
                        data = data.OrderByDescending(t => t.ConstrainText_En).ToList();
                        break;
                }
                switch (jtSorting)
                {
                    case "ConstrainText_Ar ASC":
                        data = data.OrderBy(t => t.ConstrainText_Ar).ToList();
                        break;
                    case "ConstrainText_Ar DESC":
                        data = data.OrderByDescending(t => t.ConstrainText_Ar).ToList();
                        break;
                    case "ConstrainText_En ASC":
                        data = data.OrderBy(t => t.ConstrainText_En).ToList();
                        break;
                    case "ConstrainText_En DESC":
                        data = data.OrderByDescending(t => t.ConstrainText_En).ToList();
                        break;
                }
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Station_CheckList_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Station_CheckList>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Station_CheckList>().Update(Cmodel);
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

        public bool GetAny(Station_CheckListDTO obj)
        {
            return uow.Repository<Station_CheckList>().GetAny(p => (p.User_Deletion_Id == null &&
                                         (p.ConstrainText_Ar == obj.ConstrainText_Ar.Trim() || p.ConstrainText_En == obj.ConstrainText_En.Trim())) && (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        //******************************************//
        public Dictionary<string, object> Insert(Station_CheckListDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_CheckList_seq");
                  //  entity.ID = int.Parse(id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<Station_CheckList>(entity);
                    uow.Repository<Station_CheckList>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Station_CheckListDTO obj, List<string> Device_Info)
        {
            try
            {
                //  obj.ConstrainText_Ar = obj.ConstrainText_Ar.Trim();
                //  obj.ConstrainText_En = obj.ConstrainText_En.Trim();

                if (!GetAny(obj))
                {
                    Station_CheckList CModel = uow.Repository<Station_CheckList>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    obj.User_Updation_Date = CModel.User_Updation_Date;
                    obj.User_Updation_Id = CModel.User_Updation_Id;

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Station_CheckList>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Station_CheckList, Station_CheckListDTO>(Co);
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

        // ADD FUNCTIONS TO FILL DROPS
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Station_CheckList>().GetData().Where(lab => lab.User_Deletion_Id == null).
                Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),

                    Value = c.ID
                }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            // 
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Station_CheckList>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOptionLongId
            {
                //change display lang
                DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
    }
}
