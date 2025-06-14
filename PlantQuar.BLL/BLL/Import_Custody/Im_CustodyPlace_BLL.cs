using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.Committee;
using PlantQuar.DTO.DTO.Import_Custody;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import_Custody
{
    public class Im_CustodyPlace_BLL
    {
        private UnitOfWork uow;
        public Im_CustodyPlace_BLL()
        {
            uow = new UnitOfWork();
        }

        //Find AnalysisLab by Id 
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Im_CustodyPlace entity = uow.Repository<Im_CustodyPlace>().Findobject(Id);
                var empDTO = Mapper.Map<Im_CustodyPlace, Im_CustodyPlace_DTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get AnalysisLab Count where User_Deletion_Id is null
        public Dictionary<string, object> GetCount(List<string> Device_Info)
        {


            var count = uow.Repository<Im_CustodyPlace>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        //Get AnalysisLab List where User_Deletion_Id is null
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Im_CustodyPlace>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<Im_CustodyPlace, Im_CustodyPlace_DTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get AnalysisLab List by ARName or ENName  where User_Deletion_Id is null
        public Dictionary<string, object> GetAll(string permissionId, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Im_CustodyPlace>();
                // var dataICR = new List<Im_CheckRequest>();
                Int64 data_Count = 0;

                //var _Im_CustodyPlace = (from icp in dataICp
                //                  //join ICR in dataICR on icp.Im_CheckRequest_ID equals ICR.ID
                //                  where icp.Im_CheckRequest_ID ==5// decimal.Parse( permissionId)
                //                  select new
                //                  {
                //                      icp.ID,
                //                      icp.PlaceType
                //                  }).ToList();

                //data = data = uow.Repository<Im_CustodyPlace>().GetData().Where(a =>
                //       a.Im_CheckRequest_ID.ToString().StartsWith(permissionId.Trim()) &&
                //    a.User_Deletion_Id == null).ToList();          
                data = uow.Repository<Im_CustodyPlace>().GetData().Where(a => a.User_Deletion_Id == null).ToList();

                //  data = uow.Repository<Im_CustodyPlace>().GetData().Where(a =>a.Im_CheckRequest_ID == a.Im_CheckRequest.ID).ToList();

                var dataDto = data.Select(Mapper.Map<Im_CustodyPlace, Im_CustodyPlace_DTO>).ToList();

                data_Count = dataDto.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Im_CustodyPlace_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any AnalysisLab  where User_Deletion_Id=null
        public bool GetAny(Im_CustodyPlace_DTO entity)
        {
            //    var obj = entity;
            //    return uow.Repository<Im_CustodyPlace>().GetAny(p => (p.User_Deletion_Id == null &&
            //                                (p.Im_CheckRequest_ID == obj.Im_CheckRequest_ID)) && (obj.ID == 0 ? true : p.ID != obj.ID));
            //
            return false;
        }
        //******************************************//

        //Create  AnalysisLab 
        public Dictionary<string, object> Insert(Im_CustodyPlace_DTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {

                    var CModel = Mapper.Map<Im_CustodyPlace>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CustodyPlace_seq");
                    long ID_Im_CheckRequest = uow.Repository<Im_CheckRequest>().GetData().Where(a =>
                       a.CheckRequest_Number == entity.Im_CheckRequest_ID.ToString()).FirstOrDefault().ID;
                    //CModel.Im_CheckRequest_ID=ID_Im_CheckRequest;
                    byte PlaceType = entity.PlaceType;
                    CModel.Im_CustodyPlaceType = PlaceType;
                    uow.Repository<Im_CustodyPlace>().InsertRecord(CModel);
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


        //Update  AnalysisLab 
        public Dictionary<string, object> Update(Im_CustodyPlace_DTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity;
                    Im_CustodyPlace CModel = uow.Repository<Im_CustodyPlace>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Im_CustodyPlace>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Im_CustodyPlace, Im_CustodyPlace_DTO>(Co);
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

        //Delete AnalysisLab
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Im_CustodyPlace>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Im_CustodyPlace>().Update(Cmodel);
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
