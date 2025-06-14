using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmCommittee
{
    public class Farm_Committee_ExaminationBLL : IGenericBLL<Farm_Committee_ExaminationDTO>
    {
        private UnitOfWork uow;

        public Farm_Committee_ExaminationBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var dataDTO = new List<Farm_Committee_ExaminationDTO>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    dataDTO = uow.Repository<Farm_Committee_ExaminationDTO>().GetData().Select(a => new Farm_Committee_ExaminationDTO
                    {
                        FarmCommittee_ID = a.FarmCommittee_ID,
                        Farm_Request_ItemCategories_ID = a.Farm_Request_ItemCategories_ID,
                        Notes = a.Notes,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        AdminFinalResult_Note = a.AdminFinalResult_Note,
                        Quantity_Ton = a.Quantity_Ton,
                        IsAccepted = a.IsAccepted,
                        IsAdminFinalResult = a.IsAdminFinalResult,
                    }).ToList();

                    //(lang == "1" ? (a.IsAccepted == "true" ? "مقبول" : a.IsAccepted == "false" ? "مرفوض" : "لم يتم العمل") :
                    //   (a.IsAccepted == "true" ? "Accept" : a.IsAccepted == "false" ? "Reject" : "Not Worked")),
                }
                else
                {
                    dataDTO = uow.Repository<Farm_Committee_Examination>().GetData().
                        Select(a => new Farm_Committee_ExaminationDTO
                        {
                            FarmCommittee_ID = a.FarmCommittee_ID,
                            Farm_Request_ItemCategories_ID = a.Farm_Request_ItemCategories_ID,
                            Notes = a.Notes,
                            StartDate = a.StartDate,
                            EndDate = a.EndDate,
                            AdminFinalResult_Note = a.AdminFinalResult_Note,
                            Quantity_Ton = a.Quantity_Ton,
                            IsAccepted = a.IsAccepted,
                            IsAdminFinalResult = a.IsAdminFinalResult,
                        }).Skip(index).Take(pageSize).ToList();
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;


                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities priv = new dbPrivilageEntities();
                var data = (from fic in entities.Farm_ItemCategories
                            join ff in entities.Farm_Request_ItemCategories on fic.ID equals ff.Farm_ItemCategories_ID
                            join fce in entities.Farm_Committee_Examination on ff.ID equals fce.Farm_Request_ItemCategories_ID
                            //join fcec in entities.Farm_Committee_Examination_Confirm on fce.ID equals fcec.Farm_Committee_Exmination_ID into iis

                            join itg in entities.ItemCategories on fic.ItemCategories_ID equals itg.ID
                            join it in entities.Items on itg.Item_ID equals it.ID
                            // from fcec in iis.DefaultIfEmpty()
                            where fce.FarmCommittee_ID == FarmCommittee_ID
                            select new Farm_Committee_ExaminationDTO
                            {
                                ID = fce.ID,
                                Item_Name_Ar = it.Name_Ar,
                                ItemCategories_Name_Ar = itg.Name_Ar,
                                AdminFinalResult_Note = fce.Notes,
                                IsAccepted_Admin = fce.IsAccepted,//== true?"مقبول":"مرفوض",
                                                                  // Notes_Confirm = fcec == null ? null : fcec.Notes,
                                                                  //IsAccepted_Confirm = fcec == null ? null : (Nullable<bool>)fcec.IsAccepted,
                                Admin_Confirmation = fce.Admin_Confirmation,
                                //**** الجديد
                                StartDate = fce.StartDate,
                                EndDate = fce.EndDate,
                                Quantity_Ton = fce.Quantity_Ton,
                                Area_AcresFarm = fic.Area_Acres

                            }).ToList();

                //sayed 
                // var requestId1 = requestId.Select(a => a.reqId).ToArray();
                //var _Farm_Constrain_CheckList = (from fccl in entities.Farm_Country_CheckList
                //                                 where fccl.Farm_CheckList.IsActive == true
                //                                && fccl.IsActive == true
                //                                 select new Farm_Country_CheckList_DTO
                //                                 {
                //                                     Farm_Country_ID = fccl.Country_ID,
                //                                     Constrain_CheckList_ID = fccl.ID,
                //                                     Constrain_CheckList_text = fccl.Farm_CheckList.ConstrainText_Ar,

                //                                 }).Distinct().ToList();
                //foreach (var item in _Farm_Constrain_CheckList)
                //{
                //    var List_Farm_Country = (from fc in entities.Farm_Country
                //                             join fr in entities.Farm_Request on fc.Farm_Request_ID equals fr.ID
                //                             join fcm in entities.Farm_Committee on fr.ID equals fcm.Farm_Request_ID
                //                             where fcm.ID== FarmCommittee_ID
                //                             select new Farm_Country_CheckList_DTO
                //                             {
                //                                 farm_Id = fc.Farm_Request.FarmsData_ID,
                //                                 farm_Name = fc.Farm_Request.FarmsData.Name_Ar,
                //                                 Country_Name = fc.Country.Ar_Name,
                //                                 Farm_Committee_ID = fcm.ID
                //                             }).Distinct().FirstOrDefault();
                //    if (List_Farm_Country == null)
                //    {
                //        foreach (var item1 in Req_List)
                //        {
                //            item.Constrain_CheckList_text = item.Constrain_CheckList_text;
                //            item.Constrain_CheckList_ID = item.Constrain_CheckList_ID;

                //            item.farm_Id = item1.farm_Id;
                //            item.Farm_Committee_ID = item1.Farm_Committee_ID;
                //        }

                //    }
                //    else
                //    {
                //        item.farm_Id = List_Farm_Country.farm_Id;
                //        item.Farm_Committee_ID = List_Farm_Country.Farm_Committee_ID;
                //        item.farm_Name = List_Farm_Country.farm_Name;
                //        item.Country_Name = List_Farm_Country.Country_Name;
                //    }
                //}


                //var List_Farm_Constrain_CheckList = (from cl in _Farm_Constrain_CheckList

                //                                     select new Farm_Country_CheckList_DTO
                //                                     {
                //                                         farm_Id = cl.farm_Id,
                //                                         Constrain_CheckList_ID = cl.Constrain_CheckList_ID,
                //                                         Constrain_CheckList_text = cl.Constrain_CheckList_text,
                //                                         farm_Name = cl.farm_Name,
                //                                         Country_Name = cl.Country_Name,
                //                                         Farm_Committee_ID = cl.Farm_Committee_ID
                //                                     }).Distinct().ToList();
                //eslam 

                var _Farm_Check_List = (from fc in entities.Farm_Committee
                                        join fcom in entities.Farm_Committee_CheckList on fc.ID equals fcom.FarmCommittee_ID
                                        join fcl in entities.Farm_Country_CheckList on fcom.Farm_Country_CheckList_ID equals fcl.ID
                                        join fconf in entities.Farm_Committee_CheckList_Confirm on fcom.ID equals fconf.Farm_Committee_CheckList_ID into fconf1
                                        from fconf in fconf1.DefaultIfEmpty()
                                            //join fcfr in entities.Farm_Committee_Final_Result on fc.ID equals fcfr.FarmCommittee_ID into fcfr1
                                            // from fcfr in fcfr1.DefaultIfEmpty()
                                        where fcom.FarmCommittee_ID == FarmCommittee_ID
                                        select new Farm_Committee_CheckList_ListDTO
                                        {
                                            CheckList_ID = fcom.ID,
                                            Delegation_Date = fc.Delegation_Date,
                                            StartTime = fc.StartTime,
                                            EndTime = fc.EndTime,

                                            Constrain_Ar = fcl.Farm_CheckList.ConstrainText_Ar,
                                            Farm_Country_CheckList = fcl.Country.Ar_Name,
                                            //admin
                                            IsAcceptedAdmin = fcom.IsAccepted,
                                            AdminEmployeeId = fcom.EmployeeId,
                                            //مساعد
                                            ConfirmEmployeeId = fconf.EmployeeId,
                                            IsAcceptedConfirm = fconf.IsAccepted,
                                            //AdminName= priv.PR_User.Where(p => p.Id == fcom.EmployeeId).Select(e => e.FullName).FirstOrDefault(),
                                            // ConfirmName= priv.PR_User.Where(p => p.Id == fconf.EmployeeId).Select(e => e.FullName).FirstOrDefault()
                                        }).Distinct().ToList();
                foreach (var item in _Farm_Check_List)
                {
                    item.AdminNameCheckList = priv.PR_User.Where(p => p.Id == item.AdminEmployeeId).Select(a => a.FullName).FirstOrDefault();
                    item.ConfirmName = priv.PR_User.Where(p => p.Id == item.ConfirmEmployeeId).Select(a => a.FullName).FirstOrDefault();
                }
                //Notes For Admin Android
                var _Farm_Check_List_Admin_Note = entities.Farm_Committee_Final_Result.Where(p => p.FarmCommittee_ID == FarmCommittee_ID && p.ISAdmin == true && p.User_Deletion_Date == null && p.User_Deletion_Id == null)
                    .Select(e => new Farm_Check_List_Admin_NoteDTO
                    {
                        Notes_ArAdmin = e.Notes_CheckList,
                        AdminEmployeeId = e.EmployeeId,
                        // AdminName= priv.PR_User.Where(p => p.EmpId ==e.EmployeeId).Select(a => a.FullName).FirstOrDefault()
                    }).ToList();
                foreach (var item in _Farm_Check_List_Admin_Note)
                {
                    item.AdminName = priv.PR_User.Where(p => p.Id == item.AdminEmployeeId).Select(a => a.FullName).FirstOrDefault();

                }
                //Notes For Quarantine
                var _Farm_Check_List_AdminQuarantine_Note = entities.Farm_Committee_Final_Result.Where(p => p.FarmCommittee_ID == FarmCommittee_ID && p.ISAdmin == null && p.User_Deletion_Date == null && p.User_Deletion_Id == null)
                .Select(e => new Farm_Check_List_AdminQuarantine_NoteDTO
                {
                    Notes_ArAdminQuarantine = e.Notes_CheckList,
                    AdminQuarantineEmployeeId = e.EmployeeId,
                    // AdminName= priv.PR_User.Where(p => p.EmpId ==e.EmployeeId).Select(a => a.FullName).FirstOrDefault()
                }).ToList();
                foreach (var item in _Farm_Check_List_AdminQuarantine_Note)
                {
                    item.QuarantineAdminName = priv.PR_User.Where(p => p.Id == item.AdminQuarantineEmployeeId).Select(a => a.FullName).FirstOrDefault();

                }
                //End Notes Quarantine
                //Notes For Confirm_Android
                var _Farm_Check_List_Confirm_Note = entities.Farm_Committee_Final_Result.Where(p => p.FarmCommittee_ID == FarmCommittee_ID && p.ISAdmin == false && p.User_Deletion_Date == null && p.User_Deletion_Id == null)
               .Select(e => new _Farm_Check_List_Confirm_NoteDTO
               {
                   Notes_ArConfirm = e.Notes_CheckList,
                   ConfirmEmployeeId = e.EmployeeId,
                   //ConfirmName= priv.PR_User.Where(p => p.Id ==e.EmployeeId).Select(a => a.FullName).FirstOrDefault()
               }).ToList();
                foreach (var item in _Farm_Check_List_Confirm_Note)
                {
                    item.ConfirmName = priv.PR_User.Where(p => p.Id == item.ConfirmEmployeeId).Select(a => a.FullName).FirstOrDefault();
                }



                //  var AdminName = priv.PR_User.Where(p => p.Id == _Farm_Check_List.AdminEmployeeId).Select(e => e.FullName).ToList();



                //eslam 

                //eman
                //getno of employee for committee
                var emps = uow.Repository<CommitteeEmployee>().GetData().Where(c => c.Committee_ID == FarmCommittee_ID && c.OperationType == 78 && c.ISAdmin == false && c.User_Deletion_Date == null && c.User_Deletion_Id == null).ToList();
                var noEmp = emps.Count();


                //get emps result
                foreach (var exam in data)
                {
                    //eman admin name 
                    var adminname = uow.Repository<CommitteeEmployee>().GetData().Where(c => c.Committee_ID == FarmCommittee_ID && c.OperationType == 78 && c.ISAdmin == true && c.User_Deletion_Date == null && c.User_Deletion_Id == null).FirstOrDefault();
                    exam.AdminName = priv.PR_User.Where(p => p.Id == adminname.Employee_Id).Select(e => e.FullName).FirstOrDefault();


                    var empsres = uow.Repository<Farm_Committee_Examination_Confirm>().GetData().Where(c => c.Farm_Committee_Exmination_ID == exam.ID).ToList();
                    exam.IsTotalRes = false;

                    if (empsres.Count == noEmp)
                    {
                        if (exam.IsAccepted_Admin != null)
                        {
                            exam.IsTotalRes = true;
                        }
                    }
                    exam.employeeRes = empsres.Select(v => new empResult
                    {
                        Notes_Confirm = v.Notes,
                        IsAccepted_Confirm = v.IsAccepted,
                        Date = v.Date,
                        EmployeeId = v.EmployeeId,
                        empName = priv.PR_User.Where(p => p.Id == v.EmployeeId).Select(e => e.FullName).FirstOrDefault()
                    }).ToList();

                }
                //check if all confirmed
                var ifAllConfirmed = 1;
                var notConfirm = data.Where(n => n.Admin_Confirmation == null).ToList();
                if (notConfirm.Count > 0)
                {
                    ifAllConfirmed = 0;
                }
                var ifAppearCategories = 1;
                if (data.Where(b => b.IsTotalRes == false).Count() > 0)
                {
                    ifAppearCategories = 0;

                }

                string lang = Device_Info[2];
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Farm_Committee_Examination_Data", data);
                dic.Add("ifAllConfirmed", ifAllConfirmed);
                dic.Add("ifAppearCategories", ifAppearCategories);
                dic.Add("Fill_Farm_Check_List", _Farm_Check_List);

                dic.Add("FillFarm_Check_List_Admin_Note", _Farm_Check_List_Admin_Note);
                dic.Add("FillFarm_Check_List_Confirm_Note", _Farm_Check_List_Confirm_Note);
                dic.Add("Farm_Check_List_AdminQuarantine_Note", _Farm_Check_List_AdminQuarantine_Note);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert(Farm_Committee_ExaminationDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Farm_Committee_ExaminationDTO entity)
        {
            var obj = entity as Farm_Committee_ExaminationDTO;
            return uow.Repository<Farm_Committee_Examination>().GetAny(p => obj.ID == 0 ? true : p.ID != obj.ID);
        }

        public Dictionary<string, object> Update(Farm_Committee_ExaminationDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Committee_Examination CModel = uow.Repository<Farm_Committee_Examination>().Findobject(entity.ID);
                CModel.Admin_Confirmation = entity.Admin_Confirmation;
                CModel.Admin_User = entity.Admin_User;
                CModel.Admin_Date = DateTime.Now;
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //SaveFinalNotesCheckList   
        public Dictionary<string, object> SaveFinalNotesCheckList(short user_Id, long farmCommitteeId, string notes, List<Farm_Committee_CheckList_DTO> CheckListStatus, List<string> Device_Info)
        {
            try
            {
                var id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_Final_Result_SEQ");
                Farm_Committee_Final_ResultDTO fcommittee = new Farm_Committee_Final_ResultDTO();
                fcommittee.ID = id;

                fcommittee.FarmCommittee_ID = farmCommitteeId;
                fcommittee.Notes_CheckList = notes;
                fcommittee.ISAdmin = null;
                fcommittee.EmployeeId = CheckListStatus.Select(a => a.User_Updation_Id).FirstOrDefault();
                fcommittee.User_Creation_Id = CheckListStatus.Select(a => a.User_Updation_Id).FirstOrDefault();
                fcommittee.User_Creation_Date = DateTime.Now;
                var CModel = Mapper.Map<Farm_Committee_Final_Result>(fcommittee);
                uow.Repository<Farm_Committee_Final_Result>().InsertRecord(CModel);
                uow.SaveChanges();

                foreach (var item in CheckListStatus)
                {




                    var ID = item.FarmCommittee_ID;
                    Farm_Committee_CheckList CModel3 = uow.Repository<Farm_Committee_CheckList>().Findobject(ID);
                    CModel3.IsAccepted_Quarantine = item.IsAccepted_Quarantine;
                    CModel3.User_Updation_Id = user_Id;
                    CModel3.User_Updation_Date = DateTime.Now;
                    uow.Repository<Farm_Committee_CheckList>().Update(CModel3);
                    uow.SaveChanges();

                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, fcommittee);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> SaveAreaAndWightFarmRequestQaurList(short user_id, List<Farm_Request_ItemCategoriesDTO> FinalItemCategoryAreaforAll, List<string> Device_Info)
        {
            try
            {
                foreach (var item in FinalItemCategoryAreaforAll)
                {
                    Farm_Request_ItemCategories CModel = uow.Repository<Farm_Request_ItemCategories>().Findobject(item.ID);
                    Farm_ItemCategories CModel1 = uow.Repository<Farm_ItemCategories>().Findobject(item.Farm_ItemCategories_ID);

                    CModel.User_Updation_Id = user_id;
                    CModel.User_Updation_Date = DateTime.Now;
                    CModel.Quantity_Ton__Export = item.Quantity_Ton__Export;
                    CModel.Quantity_Ton__Quarant = item.Quantity_Ton__Quarant;
                    CModel.IsActive = item.IsActive;
                    CModel.Area_Acres_Quarant = item.Area_Acres_Quarant;
                    CModel1.User_Updation_Id = user_id;
                    CModel1.User_Updation_Date = DateTime.Now;
                    CModel1.IsActive = item.IsActive;
                    CModel1.IsAcceppted =item.ISAccepted;
                    CModel1.Quantity_Ton__Export = item.Quantity_Ton__Export;
                    CModel1.Quantity_Ton__Quarant = item.Quantity_Ton__Quarant;
                    CModel1.Area_Acres_Quarant = item.Area_Acres_Quarant;
                    uow.Repository<Farm_Request_ItemCategories>().Update(CModel);
                    uow.Repository<Farm_ItemCategories>().Update(CModel1);
                    uow.SaveChanges();

                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, FinalItemCategoryAreaforAll);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
