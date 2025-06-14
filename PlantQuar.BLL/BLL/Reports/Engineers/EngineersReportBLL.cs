using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Reports.Engineers;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Reports.Engineers
{
   public class EngineersReportBLL
    {

        private UnitOfWork uow;
        public EngineersReportBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetEngineers(DateTime? from,DateTime? to,int? operationType, short? empId, short? govID, short? centerID, short? villageId, List<string> Device_Info)
        {
            try
            {
                var data = new List<EngineersReportDTO>();
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities pv = new dbPrivilageEntities();
                string lang = Device_Info[2];

                if (operationType == 78)
                {
                    data = (from ce in entities.CommitteeEmployees
                                //delegation date andstart time and reqid
                            join fc in entities.Farm_Committee on ce.Committee_ID equals fc.ID
                            //operation type
                            join sc in entities.A_SystemCode on ce.OperationType equals sc.Id
                            //committee type
                            join ct in entities.CommitteeTypes on fc.CommitteeType_ID equals ct.ID
                            join fr in entities.Farm_Request on fc.Farm_Request_ID equals fr.ID
                            join fd in entities.FarmsDatas on fr.FarmsData_ID equals fd.ID
                            join fcm in entities.Farm_Company on fd.ID equals fcm.Farm_ID
                            //company name
                            // join co in entities.Company_National on fcm.Company_ID equals co.ID
                            join Gov in entities.Governates on fd.Govern_ID equals Gov.ID
                            join cen in entities.Centers on fd.Center_Id equals cen.ID
                            join vi in entities.Villages on fd.Village_ID equals vi.ID into vis
                            from vi in vis.DefaultIfEmpty()

                            select new EngineersReportDTO
                            {
                                EmpId = ce.Employee_Id,
                                isAdmin = ce.ISAdmin,
                                Date = fc.Delegation_Date,
                                startTime = fc.StartTime,
                                operationTypeName = sc.ValueName,
                                committeeTypeName = ct.Name_Ar,
                                companyId = fcm.Company_ID,
                                requestNumber = fr.ID,
                                ExporterTypeId = fcm.ExporterType_Id,
                                governate = Gov.Ar_Name,
                                village = vi != null ? vi.Ar_Name : "",
                                center = cen.Ar_Name,
                                operationType = ce.OperationType,
                                villageId = vi.ID,
                               govID= Gov.ID,
                               centerID=vi.Center_ID

                            }).ToList();

                    if (operationType != null)
                    {
                        data = data.Where(o => o.operationType == operationType).ToList();
                    }
                    if (from != null && to != null)
                    {

                        data = data.Where(d => d.Date >= from && d.Date <= to).ToList();

                    }
                    if (empId != null)
                    {
                        data = data.Where(e => e.EmpId == empId).ToList();

                    }
                    if (govID != null)
                    {
                        data = data.Where(v => v.govID == govID).ToList();

                        if (centerID != null)
                        {
                            data = data.Where(v => v.centerID == centerID).ToList();
                            if (villageId != null)
                            {
                                data = data.Where(v => v.villageId == villageId).ToList();
                            }
                        }
                       
                    }
                           foreach(var dd in data)
                    {
                        var user = pv.PR_User.Where(r => r.Id == dd.EmpId).FirstOrDefault();
                        dd.EngineerName = user.FullName;
                        dd.EmpId = user.EmpId;
                        if (dd.ExporterTypeId == 6)
                        {
                            dd.company = uow.Repository<Company_National>().GetData().Where(a => a.ID == dd.companyId).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                           
                        }
                        else if (dd.ExporterTypeId == 7)
                        {
                            dd.company = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == dd.companyId).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                            
                        }
                        else
                        {
                            dd.company = uow.Repository<Person>().GetData().Where(a => a.ID == dd.companyId).Select(s => s.Name).FirstOrDefault();
                            
                        }
                    }

                }
               

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
