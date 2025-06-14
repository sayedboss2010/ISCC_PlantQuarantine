using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.DataEntry.Analysis;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.Repository;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Analysis
{

    public class AnalysisTypeBLL : IGenericBLL<AnalysisTypeDTO>
    {
        private UnitOfWork uow;

        public AnalysisTypeBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<AnalysisType>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                var data = uow.Repository<AnalysisType>().GetData(pageSize, index).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<AnalysisType, AnalysisTypeDTO>);
                Int64 data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("AnalysisType_Data", dataDTO);


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<AnalysisType>().GetData()
                  .Select(c => new CustomOption
                  {
                      //change display lang
                      DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                      Value = c.ID
                  }).OrderBy(a => a.DisplayText).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string>Device_Info )
        {
             string lang = Device_Info[2];

            var data = uow.Repository<AnalysisType>().GetData().Where(x => x.IsActive == true && x.User_Deletion_Id == null)
                 .Select(c => new CustomOption
                 {
                     //change display lang
                     DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                     Value = c.ID
                 }).OrderBy(a => a.DisplayText).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> FillDrop_AnalysisType_ByConstrain_EX(long EX_requestId, List<string> Device_Info)
        {
           // بناءا على الطلب اجيب الاشتراطات بتاعت الطلب ومنها اجيب المعامل
            string lang = Device_Info[2];
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            var Get_AnalysisType=(from At in entities.AnalysisTypes
                                  join cal in entities.Ex_CountryConstrain_AnalysisLabType on At.ID equals cal.AnalysisTypeID
                                  join exc in entities.Ex_ContactData on cal.CountryConstrain_ID equals exc.Exporter_ID
                                  where exc.Exporter_ID== EX_requestId
                                  && At.IsActive == true 
                                  && At.User_Deletion_Id == null
                                  select new CustomOption
                                  {
                                      DisplayText = (lang == "1" ? At.Name_Ar : At.Name_En),
                                      Value = At.ID
                                  }).OrderBy(a => a.DisplayText).ToList();
            //            = (from cal .AnalysisTypeID

            //from Ex_CheckRequest ex
            //inner join Ex_CheckRequest_Constran exc on ex.ID = exc.Ex_CheckRequest_ID
            //inner join Ex_CountryConstrain_AnalysisLabType cal on exc.Ex_CountryConstrain_ID = cal.CountryConstrain_ID
            //where ex.ID = 393
            //            var data = uow.Repository<AnalysisType>().GetData().Where(x => x.IsActive == true && x.User_Deletion_Id == null)
            //                 .Select(c => new CustomOption
            //                 {                     
            //                     DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
            //                     Value = c.ID
            //                 }).OrderBy(a => a.DisplayText).ToList();

            Get_AnalysisType.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Get_AnalysisType);
        }

        //public Data_Count GetAll(string arName = "", string enName = "", int pageSize = 0, int index = 0)
        //{
        //    Data_Count dic = new Data_Count();

        //    try
        //    {
        //        var data = new List<AnalysisType>();
        //        Int64 data_Count = 0;

        //        if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<AnalysisType>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
        //            data_Count = data.Count();
        //        }
        //        else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<AnalysisType>().GetData().Where(a => a.User_Deletion_Id == null &&
        //                                    a.Name_Ar.StartsWith(arName)).ToList();
        //            data_Count = data.Count();
        //        }
        //        else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<AnalysisType>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
        //            data_Count = data.Count();
        //        }
        //        else
        //        {
        //            data = uow.Repository<AnalysisType>().GetData().Where(a => a.User_Deletion_Id == null &&
        //                     (a.Name_Ar.StartsWith(arName) || a.Name_En.StartsWith(enName))).ToList();
        //            data_Count = data.Count();
        //        }
        //        var dataDTO = data.Select(Mapper.Map<AnalysisType, AnalysisTypeDTO>);

        //        // dic.Add("Count_Data", data_Count);
        //        //dic.Add("AnalysisType_Data", dataDTO);


        //        //dic.L_dataDTO = dataDTO;
        //        //dic.count = (int)data_Count;
        //        return uow.Repository<Object>().DataReturn2((int)DTO.HelperClasses.Enums.Success.Insert, dataDTO, Convert.ToInt32(data_Count));
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, null);
        //        //uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null)
        //        return null;
        //    }
        //}
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index,
            string jtSorting, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<AnalysisType>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<AnalysisType>().GetData().Where(a =>
                       a.Name_En.StartsWith(enName.Trim()) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<AnalysisType>().GetData().Where(a =>
                         a.Name_Ar.StartsWith(arName.Trim()) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<AnalysisType>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    //data = data.Where(a => (a.Ar_Name.StartsWith(arName) || a.En_Name.StartsWith(enName))).ToList();
                    data = uow.Repository<AnalysisType>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }

                var dataDto = data.OrderBy(A => (lang =="1"?A.Name_Ar:A.Name_En)).Skip(index).Take(pageSize).Select(a => new AnalysisTypeDTO
                {
                    ID = a.ID,
                    Name_Ar = a.Name_Ar,
                    Name_En = a.Name_En,
                    IsActive = a.IsActive,
                    IsRejectedAll = a.IsRejectedAll,
                    ListAnalysisLab_Id = uow.Repository<AnalysisLabType>().GetData().Where(x => x.AnalysisTypeID == a.ID && x.User_Deletion_Id == null).Select(x => x.AnalysisLabID).ToList()
                });
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

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("AnalysisType_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(AnalysisTypeDTO entity)
        {
            return uow.Repository<AnalysisType>().GetAny(p => (p.User_Deletion_Id == null &&
                                       (p.Name_Ar == entity.Name_Ar || p.Name_En == entity.Name_En)) &&
                                       (entity.ID == 0 ? true : p.ID != entity.ID));
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                AnalysisType entity = uow.Repository<AnalysisType>().Findobject(Id);
                var empDTO = Mapper.Map<AnalysisType, AnalysisTypeDTO>(entity);
                //#region  get analysisLabtype
                //AnalysisLabTypeBLL AnalysisLabType = new AnalysisLabTypeBLL();
                //empDTO.ListAnalysisLab_Id = AnalysisLabType.GetAnalysisLabTypeList(entity.ID);

                //#endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Insert(AnalysisTypeDTO entity, List<string> Device_Info)
        {

            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<AnalysisType>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("AnalysisType_seq");

                    var CreatedModel = uow.Repository<AnalysisType>().InsertReturn(CModel);
                    uow.SaveChanges();

                    #region Save ListAnalysisLab
                    AnalysisLabTypeBLL AnalysisLabType = new AnalysisLabTypeBLL();
                    AnalysisLabTypeDTO AnalysisLabTypeDto = new AnalysisLabTypeDTO();
                    AnalysisLabTypeDto.AnalysisTypeID = CModel.ID;
                    AnalysisLabType.InsertRecords(entity.User_Creation_Id, entity.User_Creation_Date, CreatedModel.ID, entity.ListAnalysisLab_Id, Device_Info);

                    #endregion
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CModel);
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
        public Dictionary<string, object> Update(AnalysisTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    AnalysisType CModel = uow.Repository<AnalysisType>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<AnalysisType>().Update(Co);
                    uow.SaveChanges();
                    #region update ListAnalysisLab
                    AnalysisLabTypeBLL AnalysisLabType = new AnalysisLabTypeBLL();
                    AnalysisLabType.UpdateRecords(Co.User_Updation_Id.Value, Co.User_Updation_Date.Value, Co.ID, entity.ListAnalysisLab_Id, Device_Info);
                    #endregion
                    //#region Save ListAnalysisLab


                    //AnalysisLabTypeBLL AnalysisLabType = new AnalysisLabTypeBLL();
                    //AnalysisLabTypeDTO AnalysisLabTypeDto = new AnalysisLabTypeDTO();
                    //AnalysisLabType.UpdateRecords(Co.User_Updation_Id.Value, Co.User_Updation_Date.Value, Co.ID, entity.ListAnalysisLab_Id, Device_Info);
                    //AnalysisLabTypeDto.AnalysisTypeID = CModel.ID;
                    //foreach (var item in entity.ListAnalysisLab_Id)
                    //{
                    //    AnalysisLabTypeDto.AnalysisLabID = item;
                    //    //complete code 7-5-2019
                    // //    AnalysisLabType.Update(AnalysisLabTypeDto, Device_Info) as Dictionary<string, object>;
                    //    AnalysisLabType.Update(AnalysisLabTypeDto, Device_Info);
                    //}
                    //#endregion

                    var empDTO = Mapper.Map<AnalysisType, AnalysisTypeDTO>(Co);
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
                var Cmodel = uow.Repository<AnalysisType>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id =(short) obj.Userid;
                    uow.Repository<AnalysisType>().Update(Cmodel);
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

        public Dictionary<string, object> FillDrop_Edit_Ex(long ExportCountry_Id, long shortnameId,List<string> Device_Info)
        {
            string lang = Device_Info[2];
            // var req = uow.Repository<Farm_Request>().GetData().Include(f => f.FarmsData).Where(r => requestId.Contains(r.ID)).FirstOrDefault();
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            var data = (from anl in entities.AnalysisTypes
                        join ex in entities.Ex_CountryConstrain_AnalysisLabType on anl.ID equals ex.AnalysisTypeID
                        join constr in entities.Ex_CountryConstrain on ex.CountryConstrain_ID equals constr.ID
                        where anl.IsActive == true && anl.User_Deletion_Id == null
                        && ex.IsAcive == true && ex.User_Deletion_Id == null
                        && constr.IsActive == true && constr.User_Deletion_Id == null
                        && constr.Import_Country_ID == ExportCountry_Id
                        && constr.Item_ShortName_id == shortnameId
                        && constr.ItemCategories_ID == null
                        select  new CustomOption
                              {
                                  //change display lang
                                  DisplayText = (lang == "1" ? anl.Name_Ar : anl.Name_En),
                                  Value = anl.ID
                              }).OrderBy(a => a.DisplayText).Distinct().ToList();


            //var data = uow.Repository<AnalysisType>().GetData().Where(x => x.IsActive == true && x.User_Deletion_Id == null)
            //     .Select(c => new CustomOption
            //     {
            //         //change display lang
            //         DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
            //         Value = c.ID
            //     }).OrderBy(a => a.DisplayText).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

    }
}
