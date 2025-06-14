using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmConstrain;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmRequest
{
    public class Farm_Final_Result_BLL
    {
        private UnitOfWork uow;
        public Farm_Final_Result_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetFarmCommitteeType(long FarmCommittee_ID)
        {
            byte? committeeType = uow.Repository<Farm_Committee>().GetData().Where(f => f.ID == FarmCommittee_ID).FirstOrDefault().CommitteeType_ID;
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, committeeType);

        }

        #region Examination
        public Dictionary<string, object> GetAll_Farm_Committee_Examination(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
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

                                Area_AcresFarm = fic.Area_Acres, //fic المساحة الخاصه بالعميل
                                Area_AcresAndoid = fce.Area_Acres,//fce المساحة الخاصه android
                                Quantity_Ton = fce.Quantity_Ton,//fce الكمية الخاصه android
                            }).ToList();
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
                                            //حجر
                                            IsAccepted_Quarantine = fcom.IsAccepted_Quarantine,
                                            //IsAccepted_Quarantine = fcom.IsAccepted,
                                            EmployeeId_Quarantine = fcom.EmployeeId_Quarantine,
                                            //AdminName= priv.PR_User.Where(p => p.Id == fcom.EmployeeId).Select(e => e.FullName).FirstOrDefault(),
                                            // ConfirmName= priv.PR_User.Where(p => p.Id == fconf.EmployeeId).Select(e => e.FullName).FirstOrDefault()
                                        }).Distinct().OrderBy(m => m.IsAccepted_Quarantine).ToList();
                foreach (var item in _Farm_Check_List)
                {
                    item.AdminNameCheckList = priv.PR_User.Where(p => p.Id == item.AdminEmployeeId).Select(a => a.FullName).FirstOrDefault();
                    item.Employee_Name_Quarantine = priv.PR_User.Where(p => p.Id == item.EmployeeId_Quarantine).Select(a => a.FullName).FirstOrDefault();
                    item.ConfirmName = priv.PR_User.Where(p => p.Id == item.ConfirmEmployeeId).Select(a => a.FullName).FirstOrDefault();
                }


                //Notes For sayed
                var _Farm_Check_List_All_Note = entities.Farm_Committee_Final_Result.Where(p => p.FarmCommittee_ID == FarmCommittee_ID
                 && p.User_Deletion_Date == null && p.User_Deletion_Id == null)
                    .Select(e => new Farm_Check_List_All_NoteDTO
                    {
                        Notes_Type = e.ISAdmin == true ? "الادمن" : e.ISAdmin == false ? "المساعد" : "الحجر",
                        Notes_Ar = e.Notes_CheckList == null ? " " : e.Notes_CheckList,
                        EmployeeId = e.EmployeeId,
                        //IsAccepted = e.,
                        // AdminName= priv.PR_User.Where(p => p.EmpId ==e.EmployeeId).Select(a => a.FullName).FirstOrDefault()
                    }).OrderBy(a => a.Notes_Type).ToList();
                foreach (var item in _Farm_Check_List_All_Note)
                {
                    item.Name = priv.PR_User.Where(p => p.Id == item.EmployeeId).Select(a => a.FullName).FirstOrDefault();

                }
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

                dic.Add("Farm_Check_List_All_Note", _Farm_Check_List_All_Note);
                //dic.Add("FillFarm_Check_List_Admin_Note", _Farm_Check_List_Admin_Note);
                //dic.Add("FillFarm_Check_List_Confirm_Note", _Farm_Check_List_Confirm_Note);
                //dic.Add("Farm_Check_List_AdminQuarantine_Note", _Farm_Check_List_AdminQuarantine_Note);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update_Examination(Farm_Committee_ExaminationDTO entity, List<string> Device_Info)
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
                fcommittee.EmployeeId = user_Id;// CheckListStatus.Select(a=>a.User_Updation_Id).FirstOrDefault();
                fcommittee.User_Creation_Id = user_Id;// CheckListStatus.Select(a => a.User_Updation_Id).FirstOrDefault();
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
                    CModel.Area_Acres_Quarant = item.Area_Acres_Quarant;
                    CModel.IsActive = item.IsActive;
                    //CModel.isacc = item.ISAccepted;
                    CModel.StartDate = item.StartDate;
                    CModel.EndDate = item.EndDate;


                    CModel1.User_Updation_Id = user_id;
                    CModel1.User_Updation_Date = DateTime.Now;
                    CModel1.StartDate =item.StartDate;
                    CModel1.EndDate = item.EndDate;
                    CModel1.IsActive = item.IsActive;
                    CModel1.IsAcceppted = true;
                    CModel1.Quantity_Ton__Export = item.Quantity_Ton__Export;
                    CModel1.Quantity_Ton__Quarant = item.Quantity_Ton__Quarant;
                    CModel1.Area_Acres_Quarant = item.Area_Acres_Quarant;

                    uow.Repository<Farm_Request_ItemCategories>().Update(CModel);
                    uow.Repository<Farm_ItemCategories>().Update(CModel1);
                    uow.SaveChanges();

                    // save to Farm_ItemCategories

                    //Farm_ItemCategories CModel12 = uow.Repository<Farm_ItemCategories>().Findobject(item.Farm_ItemCategories_ID);
                    //CModel12.User_Updation_Id = user_id;
                    //CModel12.User_Updation_Date = DateTime.Now;
                    //CModel12.Quantity_Ton__Export = item.Quantity_Ton__Export;
                    //CModel12.Quantity_Ton__Quarant = item.Quantity_Ton__Quarant;
                    //CModel12.Area_Acres_Quarant = item.Area_Acres_Quarant;
                    //CModel12.StartDate = item.StartDate;
                    //CModel12.EndDate = item.EndDate;
                    //uow.Repository<Farm_ItemCategories>().Update(CModel12);
                    //uow.SaveChanges();









                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, FinalItemCategoryAreaforAll);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        #endregion

        #region Farm_SampleData

        public Dictionary<string, object> GetAll_Farm_SampleData(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                //make chech about the committee 12 and  constrains for all countries if null that doesnot have samples 
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from fs in entities.Farm_SampleData_Item
                            join itg in entities.ItemCategories on fs.Farm_Request_ItemCategories.Farm_ItemCategories.ItemCategories_ID equals itg.ID into itg1
                            from itg in itg1.DefaultIfEmpty()
                            join it in entities.Items on fs.Farm_Committee.Farm_Request.FarmsData.Item_ID equals it.ID
                            join alt in entities.AnalysisLabTypes on fs.AnalysisLabType_ID equals alt.ID
                            where fs.FarmCommittee_ID == FarmCommittee_ID
                            select new Farm_SampleDataDTO
                            {
                                ID = fs.ID,
                                Item_Name_Ar = it.Name_Ar,
                                ItemCategories_Name_Ar = itg.Name_Ar,
                                //check admin res
                                Sample_BarCode = fs.Sample_BarCode,
                                IsPrint = fs.IsPrint,
                                AnalysisType_Name = alt.AnalysisType.Name_Ar,
                                AnalysisLab_Name = alt.AnalysisLab.Name_Ar,
                                IsRejectedAll = alt.AnalysisType.IsRejectedAll == false ? "" : "مرفوض كليا",
                                Notes_Ar = fs.Notes_Ar,
                                SampleRatio = fs.SampleRatio,
                                SampleSize = fs.SampleSize,
                                WithdrawDate = fs.WithdrawDate,
                                //IsAccepted_Confirm =ii.IsAccepted,
                                // Notes_Confirm = ii.Notes,
                                Admin_Confirmation = fs.Admin_Confirmation,
                                //lab result
                                IsAccepted = fs.IsAccepted,
                                RejectReason_Ar = fs.RejectReason_Ar,

                                //IsAccepted_Admin = fs.IsAccepted,
                                //Notes_Confirm = ii == null ? null : ii.Notes,
                                //IsAccepted_Confirm = ii == null ? null : (Nullable<bool>)ii.IsAccepted,

                            }).ToList();
                //eman
                foreach (var dd in data)
                {
                    var image = uow.Repository<A_AttachmentData>().GetData().FirstOrDefault(c => c.RowId == dd.ID && c.A_AttachmentTableNameId == 10);
                    if (image != null)
                    {
                        dd.imageUrl = image.AttachmentPath;
                    }
                }
                //getno of employee for committee
                var emps = uow.Repository<CommitteeEmployee>().GetData().Where(c => c.Committee_ID == FarmCommittee_ID && c.OperationType == 78 && c.ISAdmin == false).ToList();
                var noEmp = emps.Count();
                dbPrivilageEntities priv = new dbPrivilageEntities();
                //get emps result
                foreach (var exam in data)
                {
                    //eman admin name  edit Eslam
                    var adminname = uow.Repository<CommitteeEmployee>().GetData().Where(c => c.Committee_ID == FarmCommittee_ID && c.OperationType == 78 && c.ISAdmin == true && c.User_Deletion_Date == null && c.User_Deletion_Id == null).FirstOrDefault();
                    exam.AdminName = priv.PR_User.Where(p => p.Id == adminname.Employee_Id).Select(e => e.FullName).FirstOrDefault();

                    var empsres = uow.Repository<Farm_SampleData_Confirm_Item>().GetData().Where(c => c.Farm_SampleData_Item_ID == exam.ID).ToList();
                    exam.IsTotalRes = false;

                    if (empsres.Count == noEmp)
                    {
                        if (exam.Sample_BarCode != null && exam.IsAccepted != null)
                        {
                            exam.IsTotalRes = true;
                        }
                    }
                    exam.employeeRes = empsres.Select(v => new empResult
                    {
                        Notes_Confirm = v.Notes,
                        IsAccepted_Confirm = v.IsAccepted,
                        EmployeeId = v.EmployeeId,
                        Date = v.Date,
                        empName = priv.PR_User.Where(p => p.Id == v.EmployeeId).Select(e => e.FullName).FirstOrDefault()
                    }).ToList();

                }
                //data = uow.Repository<Farm_Committee_Examination>().GetData()
                //.Where(a => a.FarmCommittee_ID == FarmCommittee_ID).ToList();
                var ifAllConfirmed = 1;
                var notConfirm = data.Where(n => n.Admin_Confirmation == null).ToList();
                if (notConfirm.Count > 0)
                {
                    ifAllConfirmed = 0;
                }
                var status = 0;
                var statusRequest = uow.Repository<Farm_Committee>().GetData().Include(f => f.Farm_Request).Where(c => c.ID == FarmCommittee_ID).Select(s => s.Farm_Request.IsStatus).FirstOrDefault();
                if (statusRequest == true)
                {
                    status = 1;
                }
                if (statusRequest == false)
                {
                    status = 2;
                }
                var ifAppearCategories = 1;
                if (data.Where(b => b.IsAccepted == null).Count() > 0)
                {
                    ifAppearCategories = 0;

                }
                string lang = Device_Info[2];
                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Farm_Committee_Examination_Data", data.OrderByDescending(m => m.IsAccepted));
                dic.Add("ifAllConfirmed", ifAllConfirmed);
                dic.Add("statusRequest", status);
                dic.Add("ifAppearCategories", ifAppearCategories);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update_Farm_SampleData(Farm_SampleDataDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_SampleData_Item CModel = uow.Repository<Farm_SampleData_Item>().Findobject(entity.ID);
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
        #endregion

        #region Farm_Country

        public Dictionary<string, object> GetAll_Farm_Country(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;



                //var data = uow.Repository<Country>().GetData()
                //.Join(uow.Repository<Farm_Country>().GetData(), c => c.ID, fco => fco.Country_ID, (c, fco) => new { c, fco })
                //.Join(uow.Repository<Farm_Request>().GetData(), fco => fco.fco.Farm_Request_ID, fr => fr.ID, (fco, fr) => new { fco, fr })
                //.Join(uow.Repository<Farm_Committee>().GetData(), fr => fr.fr.ID, fc => fc.Farm_Request_ID, (fr, fc) => new { fr, fc })
                //.Join(uow.Repository<Farm_Committee_Constrain>().GetData(), fr => fr.fr.fr.ID, fcc => fcc.Farm_Committee_ID, (fr, fcc) => new { fr, fcc })
                //.Join(uow.Repository<Farm_Constrain>().GetData(), fcc => fcc.fcc.Farm_Constrain_ID, fcon => fcon.ID, (fcc, fcon) => new { fcc, fcon })
                //.Where(a => a.fcc.fr.fr.fr.ID == FarmCommittee_ID )
                //.Select(a => new Farm_CountryDTO {
                //    ID = a.fcc.fr.fr.fco.fco.ID,
                //    Ar_Name = a.fcc.fr.fr.fco.c.Ar_Name,
                //    En_Name = a.fcc.fr.fr.fco.c.En_Name,
                //    Country_ID = a.fcc.fr.fr.fco.c.ID,
                //    Farm_Request_ID = a.fcc.fr.fr.fco.fco.Farm_Request_ID,
                //    Farm_ID = a.fcc.fr.fr.fr.FarmsData_ID ,
                //    status = a.fcc.fr.fr.fr.IsStatus,
                //    IsActive = a.fcc.fr.fr.fco.fco.IsActive,
                //    IsAcceppted = a.fcc.fr.fr.fco.fco.IsAcceppted,
                //    Start_Date = a.fcc.fr.fr.fco.fco.Start_Date,
                //    End_Date = a.fcc.fr.fr.fco.fco.End_Date,
                //    Farm_Visit_Count=a.fcc.fcc.Farm_Constrain.Count_Visit,
                //    Num_Of_Visit_For_Farm= a.fcc.fr.fr.fr.FarmsData_ID

                //}).ToList();
                PlantQuarantineEntities entities = new PlantQuarantineEntities();


                //*****
                //                select
                //fc.ID,
                //c.Ar_Name,
                //c.En_Name,
                //Country_ID = c.ID,
                //Farm_Request_ID = fr.ID,
                // fr.FarmsData_ID,
                //Farm_ID = fr.FarmsData_ID,
                //status = fr.IsStatus,
                //fc.IsActive,
                //fc.IsAcceppted,
                //fc.Start_Date,
                //fc.End_Date,
                //max(fcon.Count_Visit) Count_Visit
                //var data222 = (from c  in entities.Countries
                //            join fc in entities.Farm_Country  on c.ID equals fc.Country_ID into fc1
                //            from fc in fc1.DefaultIfEmpty()
                //            join fr in entities.Farm_Request  on fc.Farm_Request_ID equals fr.ID into fr1
                //               from fr in fr1.DefaultIfEmpty()
                //            join fcom in entities.Farm_Committee  on fr.ID equals fcom.Farm_Request_ID
                //            join fd in entities.FarmsDatas  on fr.FarmsData_ID equals fd.ID
                //            join fcon in entities.Farm_Constrain  on new { a= fd.Item_ID , b= fc.Country_ID  } equals new {a= fcon.Item_ID,b= fcon.Country_Id }
                //               where fcom.ID == FarmCommittee_ID

                //               group new { fr, c,fc, fcon } by new
                //               {
                //                   fr.FarmsData_ID,
                //                   fc.ID,
                //                   Ar_Name = c.Ar_Name,
                //                   En_Name = c.En_Name,
                //                   Country_ID = c.ID,
                //                   Farm_Request_ID=fr.ID,
                //                   Farm_ID = fr.FarmsData_ID,
                //                   status=fr.IsStatus,
                //                   fc.IsActive,
                //                   fc.IsAcceppted,
                //                   fc.Start_Date,
                //                   fc.End_Date
                //               }into grp                              
                //               select new Farm_CountryDTO
                //               {
                //                   ID = grp.Key.ID,
                //                   Ar_Name = grp.Key.Ar_Name,
                //                   En_Name = grp.Key.En_Name,
                //                   Country_ID = grp.Key.Country_ID,
                //                   Farm_Request_ID = grp.Key.Farm_Request_ID,
                //                   Farm_ID = grp.Key.Farm_ID,
                //                   status = grp.Key.status,
                //                   IsActive = grp.Key.IsActive,
                //                   IsAcceppted = grp.Key.IsAcceppted,
                //                   Start_Date = grp.Key.Start_Date,
                //                   End_Date = grp.Key.End_Date,
                //                   Farm_Visit_Count = grp.Max(a => a.fcon.Count_Visit)
                //               }).ToList();
                //***************
                //join Farm_Constrain fcn on fcn.Item_ID = fd.Item_ID and(fcn.Country_Id = fc.Country_ID OR fcn.Country_Id is null)
                var data2 = (from c in entities.Countries
                             join fc in entities.Farm_Country on c.ID equals fc.Country_ID into fc1
                             from fc in fc1.DefaultIfEmpty()
                             join fr in entities.Farm_Request on fc.Farm_Request_ID equals fr.ID
                             // into fr1 from fr in fr1.DefaultIfEmpty()
                             join fcom in entities.Farm_Committee on fr.ID equals fcom.Farm_Request_ID
                             join fd in entities.FarmsDatas on fr.FarmsData_ID equals fd.ID
                             // join fcon in entities.Farm_Constrain on fd.Item_ID equals fcon.Item_ID
                             //on new { a = fd.Item_ID, b = fc.Country_ID } equals new { a = fcon.Item_ID, b = fcon.Country_Id }
                             from fcon in entities.Farm_Constrain.Where(x => fd.Item_ID == x.Item_ID && (fc.Country_ID == x.Country_Id || x.Country_Id == null)).DefaultIfEmpty()
                                 //into fg from fcon in fg.DefaultIfEmpty()
                                 // new { a = fd.Item_ID, b = fc.Country_ID } equals new { a = fcon.Item_ID, b = fcon.Country_Id }
                             where fcom.ID == FarmCommittee_ID
                             //&& fcon.Country_Id == null
                             group new { fr, fcon } by new
                             {
                                 fc.ID,
                                 fc.Country.Ar_Name,
                                 fc.Country.En_Name,
                                 Country_ID = c.ID,
                                 Farm_Request_ID = fr.ID,
                                 Farm_ID = fr.FarmsData_ID,
                                 status = fr.IsStatus,
                                 fc.IsActive,
                                 fc.IsAcceppted,
                                 fc.Start_Date,
                                 fc.End_Date,
                                 //fcon.Count_Visit

                             } into grp
                             select new Farm_CountryDTO
                             {
                                 ID = grp.Key.ID,
                                 Ar_Name = grp.Key.Ar_Name,
                                 En_Name = grp.Key.En_Name,
                                 Country_ID = grp.Key.Country_ID,
                                 Farm_Request_ID = grp.Key.Farm_Request_ID,
                                 Farm_ID = grp.Key.Farm_ID,
                                 status = grp.Key.status,
                                 IsActive = grp.Key.IsActive,
                                 IsAcceppted = grp.Key.IsAcceppted,
                                 Start_Date = grp.Key.Start_Date,
                                 End_Date = grp.Key.End_Date,
                                 Farm_Visit_Count = grp.Max(a => a.fcon.Count_Visit)
                             }).ToList();


                //var  data3 = (from fc in entities.Farm_Constrain
                //          join it in entities.Items on fc.Item_ID equals it.ID
                //          //join Un in entities.Unions on  fc.Union_Id equals Un.ID
                //          join co in entities.Countries on fc.Country_Id equals co.ID into co1
                //          from co in co1.DefaultIfEmpty()
                //          join fct in entities.Farm_Constrain_Text on fc.Farm_Constrain_Text_ID equals fct.ID
                //          join at in entities.AnalysisTypes on fc.AnalysisType_ID equals at.ID into ats
                //          from at in ats.DefaultIfEmpty()
                //          where
                //           fc.User_Deletion_Id == null && fc.IsActive == true
                //          && it.ID == Item_ID
                //          && (co.ID == Country_Id || fc.Country_Id == null)
                //          select new Farm_CountryDTO
                //          {
                //              ID = grp.Key.ID,
                //              Ar_Name = grp.Key.Ar_Name,
                //              En_Name = grp.Key.En_Name,
                //              Country_ID = grp.Key.Country_ID,
                //              Farm_Request_ID = grp.Key.Farm_Request_ID,
                //              Farm_ID = grp.Key.Farm_ID,
                //              status = grp.Key.status,
                //              IsActive = grp.Key.IsActive,
                //              IsAcceppted = grp.Key.IsAcceppted,
                //              Start_Date = grp.Key.Start_Date,
                //              End_Date = grp.Key.End_Date,
                //              Farm_Visit_Count = grp.Max(a => a.fcon.Count_Visit)

                //              ID = fc.ID,
                //              Ar_Name = fc.Country.Ar_Name,
                //              En_Name = fc.Country.En_Name,
                //              Country_ID = c.ID,
                //              Farm_Request_ID = fr.ID,
                //              Farm_ID = fr.FarmsData_ID,
                //              status = fr.IsStatus,
                //              fc.IsActive,
                //              fc.IsAcceppted,
                //              fc.Start_Date,
                //              fc.End_Date,

                //          })





                var farmID = data2.Select(a => a.Farm_ID).FirstOrDefault();
                var data3 = (from c in entities.Countries
                             join fc in entities.Farm_Country on c.ID equals fc.Country_ID
                             join fr in entities.Farm_Request on fc.Farm_Request_ID equals fr.ID
                             join fcom in entities.Farm_Committee on fr.ID equals fcom.Farm_Request_ID

                             where fcom.ID == FarmCommittee_ID //fr.FarmsData_ID == farmID //farmDataID الشروط قابلة للزياده يتم التاكد من انتهاء اللجنة
                             group fr by new
                             {
                                 Country_ID = c.ID,
                                 fr.ID
                             } into grp
                             select new Farm_CountryDTO
                             {
                                 Country_ID = grp.Key.Country_ID,

                                 Farm_Visit_Count_Actual = grp.Count(a => a.ID != null)
                             }).ToList();
                var data = (from d1 in data2
                            join d2 in data3 on d1.Country_ID equals d2.Country_ID into j
                            from d2 in j.DefaultIfEmpty()
                            select new Farm_CountryDTO
                            {
                                ID = d1.ID,
                                Ar_Name = d1.Ar_Name,
                                En_Name = d1.En_Name,
                                Country_ID = d1.Country_ID,
                                Farm_Request_ID = d1.Farm_Request_ID,
                                Farm_ID = d1.Farm_ID,
                                status = d1.status,
                                IsActive = d1.IsActive,
                                IsAcceppted = d1.IsAcceppted,
                                Start_Date = d1.Start_Date,
                                End_Date = d1.End_Date,
                                Farm_Visit_Count = d1.Farm_Visit_Count,
                                Farm_Visit_Count_Actual = d2.Farm_Visit_Count_Actual
                            }
                               ).ToList();
                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data.Where(a => a.User_Deletion_Id == null && a.En_Name.StartsWith(enName)).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data.Where(a => a.User_Deletion_Id == null && a.Ar_Name.StartsWith(arName)).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data.Where(a => a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data.ToList();
                }
                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.Ar_Name).ToList();
                        break;
                    case "Ar_Name DESC":
                        data = data.OrderByDescending(t => t.Ar_Name).ToList();
                        break;
                    case "En_Name ASC":
                        data = data.OrderBy(t => t.En_Name).ToList();
                        break;
                    case "En_Name DESC":
                        data = data.OrderByDescending(t => t.En_Name).ToList();
                        break;


                }
                string lang = Device_Info[2];
                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Country_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public Dictionary<string, object> GetAll_Farm_Country(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)

        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();
        //        Int64 data_Count = 0;
        //        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        //        var data2 = (from c in entities.Countries
        //                     join fc in entities.Farm_Country on c.ID equals fc.Country_ID into fc1
        //                     from fc in fc1.DefaultIfEmpty()
        //                     join fr in entities.Farm_Request on fc.Farm_Request_ID equals fr.ID 
        //                    // into fr1 from fr in fr1.DefaultIfEmpty()
        //                     join fcom in entities.Farm_Committee on fr.ID equals fcom.Farm_Request_ID
        //                     join fd in entities.FarmsDatas on fr.FarmsData_ID equals fd.ID
        //                     join fcon in entities.Farm_Constrain on fd.Item_ID equals fcon.Item_ID //&&  ( fd.Country_ID  equals fcon.Country_Id )
        //                     //into fg from fcon in fg.DefaultIfEmpty()
        //                     // new { a = fd.Item_ID, b = fc.Country_ID } equals new { a = fcon.Item_ID, b = fcon.Country_Id }
        //        where fcom.ID == FarmCommittee_ID
        //                     &&(fcon.Country_Id == fc.Country_ID || fcon.Country_Id != null)
        //                     group new { fr, fcon } by new
        //                     {
        //                         fc.ID,
        //                         fc.Country.Ar_Name,
        //                         fc.Country.En_Name,
        //                         Country_ID = c.ID,
        //                         Farm_Request_ID = fr.ID,
        //                         Farm_ID = fr.FarmsData_ID,
        //                         status = fr.IsStatus,
        //                         fc.IsActive,
        //                         fc.IsAcceppted,
        //                         fc.Start_Date,
        //                         fc.End_Date
        //                         //fcon.Count_Visit
        //                     } into grp
        //                     select new Farm_CountryDTO
        //                     {
        //                         ID = grp.Key.ID,
        //                         Ar_Name = grp.Key.Ar_Name,
        //                         En_Name = grp.Key.En_Name,
        //                         Country_ID = grp.Key.Country_ID,
        //                         Farm_Request_ID = grp.Key.Farm_Request_ID,
        //                         Farm_ID = grp.Key.Farm_ID,
        //                         status = grp.Key.status,
        //                         IsActive = grp.Key.IsActive,
        //                         IsAcceppted = grp.Key.IsAcceppted,
        //                         Start_Date = grp.Key.Start_Date,
        //                         End_Date = grp.Key.End_Date,
        //                         Farm_Visit_Count = grp.Max(a => a.fcon.Count_Visit)
        //                     }).ToList();
        //        var farmID = data2.Select(a => a.Farm_ID).FirstOrDefault();
        //        var data3 = (from c in entities.Countries
        //                     join fc in entities.Farm_Country on c.ID equals fc.Country_ID
        //                     join fr in entities.Farm_Request on fc.Farm_Request_ID equals fr.ID
        //                     join fcom in entities.Farm_Committee on fr.ID equals fcom.Farm_Request_ID

        //                     where fcom.ID == FarmCommittee_ID //fr.FarmsData_ID == farmID //farmDataID الشروط قابلة للزياده يتم التاكد من انتهاء اللجنة
        //                     group fr by new
        //                     {
        //                         Country_ID = c.ID,
        //                         fr.ID
        //                     } into grp
        //                     select new Farm_CountryDTO
        //                     {
        //                         Country_ID = grp.Key.Country_ID,

        //                         Farm_Visit_Count_Actual = grp.Count(a => a.ID != null)
        //                     }).ToList();
        //        var data = (from d1 in data2
        //                    join d2 in data3 on d1.Country_ID equals d2.Country_ID into j
        //                    from d2 in j.DefaultIfEmpty()
        //                    select new Farm_CountryDTO
        //                    {
        //                        ID = d1.ID,
        //                        Ar_Name = d1.Ar_Name,
        //                        En_Name = d1.En_Name,
        //                        Country_ID = d1.Country_ID,
        //                        Farm_Request_ID = d1.Farm_Request_ID,
        //                        Farm_ID = d1.Farm_ID,
        //                        status = d1.status,
        //                        IsActive = d1.IsActive,
        //                        IsAcceppted = d1.IsAcceppted,
        //                        Start_Date = d1.Start_Date,
        //                        End_Date = d1.End_Date,
        //                        Farm_Visit_Count = d1.Farm_Visit_Count,
        //                        Farm_Visit_Count_Actual = d2.Farm_Visit_Count_Actual
        //                    }
        //                       ).ToList();
        //        if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
        //        {
        //            data.Where(a => a.User_Deletion_Id == null && a.En_Name.StartsWith(enName)).ToList();
        //        }
        //        else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data.Where(a => a.User_Deletion_Id == null && a.Ar_Name.StartsWith(arName)).ToList();
        //        }
        //        else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data.Where(a => a.User_Deletion_Id == null).ToList();
        //        }
        //        else
        //        {
        //            data.ToList();
        //        }
        //        switch (jtSorting)
        //        {
        //            case "Ar_Name ASC":
        //                data = data.OrderBy(t => t.Ar_Name).ToList();
        //                break;
        //            case "Ar_Name DESC":
        //                data = data.OrderByDescending(t => t.Ar_Name).ToList();
        //                break;
        //            case "En_Name ASC":
        //                data = data.OrderBy(t => t.En_Name).ToList();
        //                break;
        //            case "En_Name DESC":
        //                data = data.OrderByDescending(t => t.En_Name).ToList();
        //                break;
        //        }
        //        string lang = Device_Info[2];
        //        //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
        //        data_Count = data.Count();

        //        dic.Add("Count_Data", data_Count);
        //        dic.Add("Country_Data", data);

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}


        public Dictionary<string, object> Update_Farm_Country(Farm_CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Country CModel = uow.Repository<Farm_Country>().Findobject(entity.ID);
                CModel.IsActive = entity.IsActive;
                CModel.IsAcceppted = entity.IsAcceppted;
                CModel.Start_Date = entity.Start_Date;
                CModel.End_Date = entity.End_Date;
                CModel.User_Updation_Date = entity.User_Updation_Date;
                CModel.User_Updation_Id = entity.User_Updation_Id;


                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> UpdateAllCountries(Farm_CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                var farmReqId = uow.Repository<Farm_Committee>().GetData().SingleOrDefault(c => c.ID == entity.ID).Farm_Request_ID;
                var allCountries = uow.Repository<Farm_Country>().GetData().Where(r => r.Farm_Request_ID == farmReqId).ToList();
                foreach (var coun in allCountries)
                {
                    //requestId1.Contains(fr.ID)

                    if (entity.No_Insert_Farm_Country != null)
                    {

                        //if (!entity.No_Insert_Farm_Country.Where(a => a.Country_ID == coun.ID).Any())
                        //{
                            coun.IsActive = entity.IsActive;
                            coun.IsAcceppted = entity.IsAcceppted;
                            coun.Start_Date = entity.Start_Date;
                            coun.End_Date = entity.End_Date;
                            coun.User_Updation_Date = entity.User_Updation_Date;
                            coun.User_Updation_Id = entity.User_Updation_Id;
                            uow.SaveChanges();
                        //}
                    }
                    else
                    {
                        //coun.IsActive = entity.IsActive;
                        coun.IsAcceppted = entity.IsAcceppted;
                        coun.Start_Date = entity.Start_Date;
                        coun.End_Date = entity.End_Date;
                        coun.User_Updation_Date = entity.User_Updation_Date;
                        coun.User_Updation_Id = entity.User_Updation_Id;
                        uow.SaveChanges();
                    }
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        #endregion

        #region FarmRequestItem_Categorie
        public Dictionary<string, object> GetAll_FarmRequestItem_Categories(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();


                Int64 data_Count = 0;
                var farmReqId = uow.Repository<Farm_Committee>().GetData().Where(c => c.ID == FarmCommittee_ID).FirstOrDefault().Farm_Request_ID;

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from fic in entities.Farm_ItemCategories
                            join fs in entities.Farm_Request_ItemCategories on fic.ID equals fs.Farm_ItemCategories_ID
                            join fce in entities.Farm_Committee_Examination on fs.ID equals fce.Farm_Request_ItemCategories_ID
                            join itg in entities.ItemCategories on fic.ItemCategories_ID equals itg.ID

                            //from ii in iis.DefaultIfEmpty()
                            where fs.Farm_Request_ID == farmReqId

                            select new Farm_Request_ItemCategoriesDTO
                            {
                                ID = fs.ID,
                                Farm_ItemCategories_ID = fic.ID,
                                IsActive = fs.IsActive,
                                categoryName = itg.Name_Ar,
                                Area_Acres = fic.Area_Acres,
                                Area_AcresAndroid = fce.Area_Acres,
                                Quantity_Ton = fce.Quantity_Ton,
                                Area_Acres_Quarant = fs.Area_Acres_Quarant,
                                Quantity_Ton__Quarant = fs.Quantity_Ton__Quarant,
                                Quantity_Ton__Export = fs.Quantity_Ton__Export,
                                StartDate = fce.StartDate,
                                EndDate = fce.EndDate,
                            }).ToList();

                string lang = Device_Info[2];
                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
                data_Count = data.Count();
                var allIsActive = 0;
                if (data.Where(d => d.IsActive == null).ToList().Count > 0)
                {
                    allIsActive = 1;
                }
                var status = 0;
                var statusRequest = uow.Repository<Farm_Committee>().GetData().Include(f => f.Farm_Request).Where(c => c.ID == FarmCommittee_ID).Select(s => s.Farm_Request.IsStatus).FirstOrDefault();
                if (statusRequest == true)
                {
                    status = 1;
                }
                if (statusRequest == false)
                {
                    status = 2;
                }
                dic.Add("Count_Data", data_Count);
                dic.Add("Farm_Committee_Examination_Data", data);
                dic.Add("allIsActive", allIsActive);
                dic.Add("statusRequest", status);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }

        public Dictionary<string, object> Update_FarmRequestItem_Categorie(Farm_Request_ItemCategoriesDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Request_ItemCategories CModel = uow.Repository<Farm_Request_ItemCategories>().Findobject(entity.ID);

                CModel.IsActive = entity.IsActive;

                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        #endregion


        #region FarmsData

        public Dictionary<string, object> GetAll_FarmsData(long FarmsData_ID, long FarmCommittee_ID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from fd in entities.FarmsDatas
                            join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                            join fc in entities.Farm_Committee on fr.ID equals fc.Farm_Request_ID

                            where fc.ID == FarmCommittee_ID
                            select new FarmCommitteeExaminationAndSampleDataVM
                            {
                                FarmCode_14 = fd.FarmCode_14,
                                print_text = fr.Print_Text,
                                IsActive = fd.IsActive,
                                IsApproved = fd.IsApproved,
                            }).ToList();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update_Farm_Data(FarmsDataDTO entity, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                FarmsData CModel = uow.Repository<FarmsData>().Findobject(entity.ID);

                var Count_Farm_Center = db.FarmsDatas.Where(a => a.Center_Id == CModel.Center_Id).Count();
                var _year = DateTime.Now.Year.ToString().Substring(2);
                string _FarmCode_14 = (int.Parse(CModel.Govern_ID.ToString())).ToString("D" + 2)
                    + (int.Parse(CModel.Center_Id.ToString())).ToString("D" + 5)
                    + (int.Parse(Count_Farm_Center.ToString())).ToString("D" + 4)
                    + CModel.Item.Item_Code.ToString()
                    + DateTime.Now.Year.ToString().Substring(2);
                //_FarmCode_14.ToString("D" + 5);
                //CModel.Center_Id.ToString("D" + 5);
                //CModel.Item.Item_Code;
                //DateTime.Now.Year
                //    DateTime.Now.Year.ToString().Substring(2)
                CModel.FarmCode_14 = _FarmCode_14;
                CModel.IsApproved = entity.IsApproved;
                CModel.IsActive = entity.IsActive;
                CModel.User_Updation_Date = CModel.User_Updation_Date;
                CModel.User_Updation_Id = CModel.User_Updation_Id;
                uow.SaveChanges();

                Farm_Request CModel1 = uow.Repository<Farm_Request>().Findobject(entity.Farm_Request_ID);
                CModel1.Print_Text = entity.print_text;

                CModel1.User_Updation_Date = DateTime.Now;
                //  CModel.User_Updation_Id = Dto.user_Id;
                uow.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "");


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        #endregion
        public Dictionary<string, object> Update_Farm_Request_IsStatus(FarmRequestDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Request CModel = uow.Repository<Farm_Request>().Findobject(entity.ID);
                CModel.IsStatus = entity.IsStatus;
                //if(entity.IsStatus ==false)
                CModel.Is_Final_requst = true;

                CModel.User_Updation_Date = entity.User_Updation_Date;
                CModel.User_Updation_Id = entity.User_Updation_Id;
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



    }
}
