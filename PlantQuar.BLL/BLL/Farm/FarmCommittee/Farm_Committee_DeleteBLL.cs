using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmCommittee
{
    public class Farm_Committee_DeleteBLL
    {
        private UnitOfWork uow;

        public Farm_Committee_DeleteBLL()
        {

            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetAll(List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var dd = (from fd in entities.FarmsDatas
                          join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                          join fc in entities.Farm_Committee on fr.ID equals fc.Farm_Request_ID
                          join it in entities.Items on fd.Item_ID equals it.ID
                          where fc.Status == false && fc.Is_Start_Android == null
                          // fd.IsActive == true
                          select new FarmCommitteeDeleteDTO
                          {
                              Farm_Committee_ID = fc.ID,
                              Farm_ID = fd.ID,
                              Farm_Name_Ar = fd.Name_Ar,
                              Item_Name_Ar = it.Name_Ar,
                              Farm_FarmCode_14 = fd.FarmCode_14,
                              Is_Start_Android = fc.Is_Start_Android,

                          }).ToList();
                //  Dictionary<string, object> dic = new Dictionary<string, object>();
                //   var data = new List<FarmCommitteeDeleteDTO>();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dd);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



        public Dictionary<string, object> delete(List<long> deleted_lst, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                foreach (var x in deleted_lst)
                {
                    // Delete From Farm_Committee_Final_Result
                    var deletedFarmCommitteeFinalResultRow = (from fcfr in entities.Farm_Committee_Final_Result
                                                              where fcfr.FarmCommittee_ID == 33
                                                              select fcfr).ToList();
                    foreach (var i in deletedFarmCommitteeFinalResultRow)
                    { entities.Farm_Committee_Final_Result.Remove(i); }


                    // Delete From Farm_Committee_CheckList
                    var deletedFarmCommitteeCheckListRow = (from fccl in entities.Farm_Committee_CheckList
                                                            where fccl.FarmCommittee_ID == x
                                                            select fccl).ToList();
                    foreach (var i in deletedFarmCommitteeCheckListRow)
                    { entities.Farm_Committee_CheckList.Remove(i); }


                    //// Delete From Farm_Committee_Shift
                    var deletedFarmCommitteeShiftRow = (from fcs in entities.Farm_Committee_Shift
                                                        where fcs.Farm_Committee_ID == x
                                                        select fcs).ToList();
                    foreach (var i in deletedFarmCommitteeShiftRow)
                    { entities.Farm_Committee_Shift.Remove(i); }


                    //// Delete From Farm_Committee_Examination
                    var deletedFarmCommitteeExaminationRow = (from fce in entities.Farm_Committee_Examination
                                                              where fce.FarmCommittee_ID == x
                                                              select fce).ToList();
                    foreach (var i in deletedFarmCommitteeExaminationRow)
                    { entities.Farm_Committee_Examination.Remove(i); }


                    //// Delete From Farm_SampleData_Item
                    var deletedFarmSampleDataItemRow = (from fsi in entities.Farm_SampleData_Item
                                                        where fsi.FarmCommittee_ID == x
                                                        select fsi).ToList();
                    foreach (var i in deletedFarmSampleDataItemRow)
                    { entities.Farm_SampleData_Item.Remove(i); }


                    //// Delete From Farm_Committee_Constrain
                    var deletedFarmcommitteeConstrainRow = (from fcc in entities.Farm_Committee_Constrain
                                                            where fcc.Farm_Committee_ID == x
                                                            select fcc).ToList();
                    foreach (var i in deletedFarmcommitteeConstrainRow)
                    { entities.Farm_Committee_Constrain.Remove(i); }
                    //entities.Farm_Committee_Constrain.Remove(deletedFarmcommitteeConstrainRow);

                    //// Delete CommitteeEmployee
                    var deleted_CommitteeEmployee_Row = (from fcc in entities.CommitteeEmployees
                                                            where fcc.Committee_ID == x && fcc.OperationType==78
                                                            select fcc).ToList();
                    foreach (var i in deleted_CommitteeEmployee_Row)
                    { entities.CommitteeEmployees.Remove(i); }


                    Farm_Committee CModel = uow.Repository<Farm_Committee>().Findobject(x);
                    CModel.User_Updation_Date = null;
                    CModel.User_Updation_Id = null;
                    CModel.CommitteeType_ID = null;
                    CModel.Delegation_Date = null;
                    CModel.StartTime = null;
                    CModel.EndTime = null;
                    CModel.IsApproved = null; // المفروض تكون حسب التاريخ اللى جاي مع الركوست لو بينها تبقى 1 لو لا تبقى null
                    CModel.Status = null;
                    //context.SaveChanges();
                    uow.Repository<Farm_Committee>().Update(CModel);
                    uow.SaveChanges();
                    entities.SaveChanges();
                }


                //return null;
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 1);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
