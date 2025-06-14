using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class A_AttachmentDataBLL 
    {
        private UnitOfWork uow;

        public A_AttachmentDataBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(long requestId,int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<A_AttachmentData>();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                string lang = Device_Info[2];
                Int64 data_Count = 0;

                data = uow.Repository<A_AttachmentData>().GetData().Where(a => a.RowId == requestId && a.User_Creation_Id != null && a.User_Deletion_Id == null).OrderBy(x=>x.Id).Skip(index).Take(pageSize).ToList();
                

                var dataDTO = data.Select(Mapper.Map<A_AttachmentData, ex_A_AttachmentDataDTO>);
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("A_AttachmentData", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(ex_A_AttachmentDataDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(ex_A_AttachmentDataDTO entity, List<string> Device_Info)
        {
            var CModel = Mapper.Map<A_AttachmentData>(entity);
            CModel.Id = uow.Repository<Object>().GetNextSequenceValue_Long("A_AttachmentData_seq");

            uow.Repository<A_AttachmentData>().InsertRecord(CModel);
            uow.SaveChanges();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
        }

        public Dictionary<string, object> InsertList(List<ex_A_AttachmentDataDTO> files, List<string> Device_Info)
        {
            try
            {
                foreach (var entity in files)
                {
                    var CModel = Mapper.Map<A_AttachmentData>(entity);
                    CModel.Id = uow.Repository<Object>().GetNextSequenceValue_Long("A_AttachmentData_seq");

                    uow.Repository<A_AttachmentData>().InsertRecord(CModel);
                    uow.SaveChanges();
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, files.FirstOrDefault());
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        //public Dictionary<string, object> Update(A_AttachmentDataDTO entity, List<string> Device_Info)
        //{
        //    try
        //    {
                
        //            var obj = entity;
        //        A_AttachmentData CModel = uow.Repository<A_AttachmentData>().Findobject(obj.Id);
        //        if(entity.AttachmentPath =="")
        //        {
        //            obj.AttachmentPath = CModel.AttachmentPath;
        //        }
        //            var Co = Mapper.Map(obj, CModel);
        //            uow.Repository<A_AttachmentData>().Update(Co);
        //            uow.SaveChanges();

        //            var empDTO = Mapper.Map<A_AttachmentData, A_AttachmentDataDTO>(Co);
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<A_AttachmentData>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<A_AttachmentData>().Update(Cmodel);
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

        public Dictionary<string, object> Update(ex_A_AttachmentDataDTO entity, List<string> Device_Info)
        {
            try
            {

                var obj = entity;
                A_AttachmentData CModel = uow.Repository<A_AttachmentData>().Findobject(obj.Id);
                if (entity.AttachmentPath == "")
                {
                    obj.AttachmentPath = CModel.AttachmentPath;

                }
                obj.User_Creation_Id = CModel.User_Creation_Id;
                obj.User_Creation_Date = CModel.User_Creation_Date;
                var Co = Mapper.Map(obj, CModel);
                uow.Repository<A_AttachmentData>().Update(Co);
                uow.SaveChanges();

                var empDTO = Mapper.Map<A_AttachmentData, ex_A_AttachmentDataDTO>(Co);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}