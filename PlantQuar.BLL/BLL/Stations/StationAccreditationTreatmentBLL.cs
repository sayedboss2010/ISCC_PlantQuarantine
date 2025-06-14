using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Stations
{
    public class StationAccreditationTreatmentBLL : IGenericBLL<Station_AccreditationTreatmentDTO>
    {
        private UnitOfWork uow;

        public StationAccreditationTreatmentBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Station_AccreditationTreatment entity = uow.Repository<Station_AccreditationTreatment>().Findobject(Id);
                var empDTO = Mapper.Map<Station_AccreditationTreatment, Station_AccreditationTreatmentDTO>(entity);
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
            var count = uow.Repository<Station_AccreditationTreatment>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Station_Accreditation.User_Deletion_Id == null
                && p.TreatmentType.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Station_AccreditationTreatment>().GetData(pageSize, index, A => 1 == 1).Where
                    (p =>
                 // get undeleted parent
                 p.Station_Accreditation.User_Deletion_Id == null
                && p.TreatmentType.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<Station_AccreditationTreatment, Station_AccreditationTreatmentDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Station_AccreditationTreatmentDTO obj)
        {
            return uow.Repository<Station_AccreditationTreatment>().GetAny(p => p.User_Deletion_Id == null &&
                                        p.Station_AccreditationID == obj.Station_AccreditationID && p.Treatment_Id == obj.Treatment_Id && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(Station_AccreditationTreatmentDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Station_AccreditationTreatment>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Station_AccreditationTreatment_Seq");
                    uow.Repository<Station_AccreditationTreatment>().InsertRecord(CModel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(Station_AccreditationTreatmentDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    Station_AccreditationTreatment CModel = uow.Repository<Station_AccreditationTreatment>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Station_AccreditationTreatment>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Station_AccreditationTreatment, Station_AccreditationTreatmentDTO>(Co);
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
        public Dictionary<string, object> Insert(byte treatment_id, short user_id, DateTime Date_Now, long StationAccrediationID, List<string> Device_Info)
        {
            try
            {
                var CModel = uow.Repository<Station_AccreditationTreatment>().GetData().Where(x => x.Treatment_Id == treatment_id && x.Station_AccreditationID == StationAccrediationID && x.User_Deletion_Id == null).SingleOrDefault();
                if (CModel == null)
                {
                    Station_AccreditationTreatment Model = new Station_AccreditationTreatment();
                    Model.User_Creation_Date = Date_Now;
                    Model.User_Creation_Id = user_id;
                    Model.Station_AccreditationID = StationAccrediationID;
                    Model.Treatment_Id = treatment_id;
                    Model.IsActive = true;
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Station_AccreditationTreatment_Seq");

                    uow.Repository<Station_AccreditationTreatment>().InsertRecord(Model);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, StationAccrediationID);
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
        public Dictionary<string, object> Update(byte? treatment_id, short user_id, DateTime Date_Now, long StationAccrediationID, List<string> Device_Info)
        {
            try
            {
                var Model = uow.Repository<Station_AccreditationTreatment>().GetData().Where(x => x.Station_AccreditationID == StationAccrediationID && x.User_Deletion_Id == null).SingleOrDefault();
                if (treatment_id == null)
                {
                    DeleteParameters param = new DeleteParameters();
                    param.id = Model.ID;
                    param.Userid = user_id;
                    param._DateNow = Date_Now;
                    Delete(param, Device_Info);
                }
                else if(Model==null)
                {
                    Insert(treatment_id.Value, user_id, Date_Now, StationAccrediationID, Device_Info);
                }
                else
                {
                    Model.User_Updation_Date = Date_Now;
                    Model.User_Updation_Id = user_id;
                    Model.Treatment_Id = treatment_id.Value;
                    uow.Repository<Station_AccreditationTreatment>().Update(Model);
                    uow.SaveChanges();
                }
                    var _DTO = Mapper.Map<Station_AccreditationTreatment, Station_AccreditationTreatmentDTO>(Model);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, _DTO);
                
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Station_AccreditationTreatment>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Station_AccreditationTreatment>().Update(Cmodel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
