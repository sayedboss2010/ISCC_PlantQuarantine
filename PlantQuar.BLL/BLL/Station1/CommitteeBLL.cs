using AutoMapper;
using PlantQuar.BLL.BLL.Farm.FarmRequest;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
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
    public class CommitteeBLL
    {
        private UnitOfWork uow;
        public CommitteeBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> FindStation(object Id, List<string> Device_Info)
        {

            try
            {
                Station_Accreditation_Committee entity = uow.Repository<Station_Accreditation_Committee>().Findobject(Id);

                var empDTO = Mapper.Map<Station_Accreditation_Committee, Station_Accreditation_CommitteeDTO>(entity);
               // empDTO.stationcode = entity.Station_Accreditation.StationActivity.Station.StationCode;

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillCommitteeType_List(int Lst, List<string> Device_Inf)
        {

            var data = uow.Repository<CommitteeType>().GetData().Where(x => x.User_Deletion_Id == null)
                .Select(c => new CustomOptionShortId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> FindReqNumber(object Id, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest entity = uow.Repository<Ex_CheckRequest>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_CheckRequest, Export_CheckRequestDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO.CheckRequest_Number);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FindCreationDate(object Id, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest entity = uow.Repository<Ex_CheckRequest>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_CheckRequest, Export_CheckRequestDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO.User_Creation_Date);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(Ex_RequestCommitteeDTO entity)
        {
            var obj = entity as Ex_RequestCommitteeDTO;
            return uow.Repository<Ex_RequestCommittee>().GetAny(p => (p.User_Deletion_Id == null) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public void Send_Committe_Employee(long Committee_ID, int OperationType,
            DateTime Create_date, long? Employee_Id,
            List<EmployeeDTO> com_emp, List<string> Device_Info)
        {
            CommitteeEmployeeDTO committeeEmployeeDTO = new CommitteeEmployeeDTO();

            CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();
            committeeEmployeeDTO.User_Creation_Id = Employee_Id;
            committeeEmployeeDTO.User_Creation_Date = Create_date;

            foreach (var item in com_emp)
            {
                committeeEmployeeDTO.Employee_Id = (long)item.Employee_Id;
                committeeEmployeeDTO.ISAdmin = item.ISAdmin;
                committeeEmployeeDTO.Committee_ID = Committee_ID;
                committeeEmployeeDTO.OperationType = OperationType;
                committeeEmployeeBLL.Insert(committeeEmployeeDTO, Device_Info);
            }
        }

        public Dictionary<string, object> Insert(Ex_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Ex_RequestCommittee>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_seq");

                    var req_com = uow.Repository<Ex_RequestCommittee>().InsertReturn(CModel);
                    uow.SaveChanges();

                    #region Employee
                    if (entity.com_emp != null)
                    {
                        Send_Committe_Employee(req_com.ID, entity.OperationType,
                        entity.User_Creation_Date, entity.User_Creation_Id,
                        entity.com_emp, Device_Info);
                    }

                    #endregion


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
    }
}
