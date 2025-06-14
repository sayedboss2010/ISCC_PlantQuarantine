using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.DataEntry.Analysis;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.DataEntry.Analysis
{
    public class AnalysisLabTypeBLL : IGenericBLL<AnalysisLabTypeDTO>
    {
        private UnitOfWork uow;

        public AnalysisLabTypeBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                AnalysisLabType entity = uow.Repository<AnalysisLabType>().Findobject(Id);
                var empDTO = Mapper.Map<AnalysisLabType, AnalysisLabTypeDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<AnalysisLabType>().GetData().Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<AnalysisLabType>().GetData(pageSize, index, A => 1 == 1).ToList();
                var dataDTO = data.Select(Mapper.Map<AnalysisLabType, AnalysisLabTypeDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAnalysisLab_ListByType(int analysisTypeId, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<AnalysisLabType>().GetData().Where(a => a.AnalysisTypeID == analysisTypeId
                 ).Select(a => new AnalysisLabDTO
                 {
                     ID = a.AnalysisLabID,
                     Name_Ar = a.AnalysisLab.Name_Ar
                 }).ToList();
                //   var dataDTO = data.Select(Mapper.Map<AnalysisLabType, AnalysisLabDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



        public Dictionary<string, object> GetAnalysisLab_ListByType_Common_Id(int analysisTypeId, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<AnalysisLabType>().GetData().Where(a => a.AnalysisTypeID == analysisTypeId
                 ).Select(a => new AnalysisLabTypeDTO
                 {
                     ID = a.ID,
                     Name_Ar = a.AnalysisLab.Name_Ar
                 }).ToList();
                //   var dataDTO = data.Select(Mapper.Map<AnalysisLabType, AnalysisLabDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();
        //        var data = new List<AnalysisLabType>();
        //        Int64 data_Count = 0;

        //        if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
        //        {
        //            data = data = uow.Repository<AnalysisLabType>().GetData().Where(a =>
        //               a.En_Name.StartsWith(enName.Trim()) &&
        //            a.User_Deletion_Id == null).ToList();

        //        }
        //        else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

        //            data = data = uow.Repository<AnalysisLabType>().GetData().Where(a =>
        //                 a.Ar_Name.StartsWith(arName.Trim()) &&
        //              a.User_Deletion_Id == null).ToList();
        //        }
        //        else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<AnalysisLabType>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
        //            data_Count = data.Count();
        //        }
        //        else
        //        {
        //            //data = data.Where(a => (a.Ar_Name.StartsWith(arName) || a.En_Name.StartsWith(enName))).ToList();
        //            data = uow.Repository<AnalysisLabType>().GetData().Where(a =>
        //            (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName)) &&
        //          a.User_Deletion_Id == null).ToList();
        //        }

        //        var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Shipment_Mean, ShipmentMeanDTO>);

        //        data_Count = data.Count();
        //        dic.Add("Count_Data", data_Count);
        //        dic.Add("Shipment_Mean_Data", dataDto);

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public bool GetAny(AnalysisLabTypeDTO entity)
        {
            var obj = entity as AnalysisLabTypeDTO;
            return uow.Repository<AnalysisLabType>().GetAny(p => (p.AnalysisLabID == obj.AnalysisLabID && p.AnalysisTypeID == obj.AnalysisTypeID && p.User_Deletion_Id == null) &&
            (obj.ID == 0 ? true : p.ID != obj.ID));
            // return false;
        }
        //******************************************//
        public Dictionary<string, object> Insert(AnalysisLabTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<AnalysisLabType>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("AnalysisLabType_SEQ");
                    uow.Repository<AnalysisLabType>().InsertRecord(CModel);
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
                uow.Repository<Object>().Save_Error(GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> Update(AnalysisLabTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as AnalysisLabTypeDTO;
                    AnalysisLabType CModel = uow.Repository<AnalysisLabType>().Findobject(obj.ID);



                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<AnalysisLabType>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<AnalysisLabType, AnalysisLabTypeDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop()
        {
            //complete code
            return null;

            //var data = uow.Repository<AnalysisLabType>().GetData()
            //    .Select(c => new CustomOption { DisplayText = c.AnalysisType.Name_Ar + "-" + c.AnalysisLab.Name_Ar, Value = c.ID }).ToList();
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public int GetAnalysisLabType(int AnalysisLabID, int AnalysisTypeID)
        {
            int AnalysisLabTypeID = uow.Repository<AnalysisLabType>().GetData().Where(x => x.AnalysisLabID == AnalysisLabID && x.AnalysisTypeID == AnalysisTypeID).Select(x => x.ID).FirstOrDefault();
            return AnalysisLabTypeID;
        }
        public void Deleterecord(AnalysisLabType obj, List<string> Device_Info)
        {
            try
            {
                //var model = Mapper.Map<List<StationActivityCountry>>(lst);
                uow.Repository<AnalysisLabType>().Update(obj);
                uow.SaveChanges();
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            }
        }
        public Dictionary<string, object> InsertRecords(short user_id, DateTime Date_Now, int AnalysisTypeID, List<int> objRecords, List<string> Device_Info)
        {
            try
            {
                AnalysisLabTypeDTO dto;
                foreach (var item in objRecords)
                {
                    dto = new AnalysisLabTypeDTO();
                    dto.AnalysisLabID = item;
                    dto.AnalysisTypeID = AnalysisTypeID;
                    dto.User_Creation_Date = Date_Now;
                    dto.User_Creation_Id = user_id;
                    Insert((dto), Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> UpdateRecords(short user_id, DateTime Date_Now, int AnalysisTypeID, List<int> objRecords, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<AnalysisLabType>().GetData().Where(x => x.AnalysisTypeID == AnalysisTypeID && x.User_Deletion_Id == null).ToList();
                var addlst = objRecords.Except(data.Select(x => x.AnalysisLabID)).ToList();
                var deletelst = data.Where(x => objRecords.IndexOf(x.AnalysisLabID) == -1).ToList();
                InsertRecords(user_id, Date_Now, AnalysisTypeID, addlst, Device_Info);
                foreach (var item in deletelst)
                {
                    item.User_Deletion_Date = Date_Now;
                    item.User_Deletion_Id = user_id;
                    Deleterecord(item, Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, objRecords);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }

        }


        //public List<byte> GetAnalysisLabTypeList(int AnalysisTypeID)
        //{
        //    return   uow.Repository<AnalysisLabType>().GetData().Where(x => x.AnalysisTypeID == AnalysisTypeID)
        //         .Select(x => x.AnalysisLabID).ToList();
        // }
    }
}