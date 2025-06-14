using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
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

namespace PlantQuar.BLL.BLL.Farm.FarmCommittee
{
    public class Farm_SampleDataBLL : IGenericBLL<Farm_SampleDataDTO>
    {
        private UnitOfWork uow;

        public Farm_SampleDataBLL()
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
                var dataDTO = new List<Farm_SampleDataDTO>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    dataDTO = uow.Repository<Farm_SampleDataDTO>().GetData().Select(a => new Farm_SampleDataDTO
                    {
                        AnalysisLabType_ID = a.AnalysisLabType_ID,
                        FarmCommittee_ID = a.FarmCommittee_ID,
                        Farm_ItemCategories_ID = a.Farm_ItemCategories_ID,
                        WithdrawDate = a.WithdrawDate,
                        Sample_BarCode = a.Sample_BarCode,
                        SampleSize = a.SampleSize,
                        SampleRatio = a.SampleRatio,
                        IsAccepted = a.IsAccepted,
                        Notes_Ar = a.Notes_Ar,
                        RejectReason_Ar = a.RejectReason_Ar,
                        RejectReason_En = a.RejectReason_En,
                        Notes_En = a.Notes_En,
                    }).ToList();
                }
                else
                {
                    dataDTO = uow.Repository<Farm_SampleData_Item>().GetData().
                        Select(a => new Farm_SampleDataDTO
                        {
                            AnalysisLabType_ID = a.AnalysisLabType_ID,
                            FarmCommittee_ID = a.FarmCommittee_ID,
                            Farm_ItemCategories_ID = a.Farm_Request_ItemCategories_ID,
                            WithdrawDate = a.WithdrawDate,
                            Sample_BarCode = a.Sample_BarCode,
                            SampleSize = a.SampleSize,
                            SampleRatio = a.SampleRatio,
                            IsAccepted = a.IsAccepted,
                            Notes_Ar = a.Notes_Ar,
                            RejectReason_Ar = a.RejectReason_Ar,
                            RejectReason_En = a.RejectReason_En,
                            Notes_En = a.Notes_En,
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
                var data = (from fic in entities.Farm_ItemCategories
                            join ff in entities.Farm_Request_ItemCategories on fic.ID equals ff.Farm_ItemCategories_ID
                            join fs in entities.Farm_SampleData_Item on ff.ID equals fs.Farm_Request_ItemCategories_ID
                            // join fsc in entities.Farm_SampleData_Confirm_Item on fs.ID equals fsc.Farm_SampleData_ID into iis

                            join itg in entities.ItemCategories on fic.ItemCategories_ID equals itg.ID into itg1
                            from itg in itg1.DefaultIfEmpty()
                            join it in entities.Items on itg.Item_ID equals it.ID
                            join alt in entities.AnalysisLabTypes on fs.AnalysisLabType_ID equals alt.ID
                            join at in entities.AnalysisTypes on alt.AnalysisTypeID equals at.ID
                            join al in entities.AnalysisLabs on alt.AnalysisLabID equals al.ID
                            //from ii in iis.DefaultIfEmpty()
                            where fs.FarmCommittee_ID == FarmCommittee_ID
                            select new Farm_SampleDataDTO
                            {
                                ID = fs.ID,
                                Item_Name_Ar = it.Name_Ar,
                                ItemCategories_Name_Ar = itg.Name_Ar,
                                //check admin res
                                Sample_BarCode = fs.Sample_BarCode,
                                IsPrint = fs.IsPrint,
                                AnalysisType_Name = at.Name_Ar,
                                AnalysisLab_Name = al.Name_Ar,
                                IsRejectedAll = at.IsRejectedAll == false ? "" : "مرفوض كليا",
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
                dic.Add("Farm_Committee_Examination_Data", data);
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
            //try
            //{
            //    Dictionary<string, object> dic = new Dictionary<string, object>();

            //    var data = new List<Farm_SampleData_Item>();
            //    Int64 data_Count = 0;

            //    if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
            //    {
            //        data = uow.Repository<Farm_SampleData_Item>().GetData().Where(a => a.User_Deletion_Id == null && a.FarmCommittee_ID == FarmCommittee_ID &&
            //        a.RejectReason_En.StartsWith(enName)
            //     && a.User_Deletion_Id == null).ToList();
            //    }
            //    else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
            //    {
            //        data = uow.Repository<Farm_SampleData_Item>().GetData().Where(a => a.User_Deletion_Id == null && a.FarmCommittee_ID == FarmCommittee_ID &&
            //                          a.RejectReason_Ar.StartsWith(arName)  // get undeleted parent
            //                       && a.User_Deletion_Id == null).ToList();
            //    }
            //    else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
            //    {
            //        data = uow.Repository<Farm_SampleData_Item>().GetData().Where(a => a.User_Deletion_Id == null && a.FarmCommittee_ID == FarmCommittee_ID
            //                       // get undeleted parent
            //                       && a.User_Deletion_Id == null).ToList();
            //    }
            //    else
            //    {
            //        data = uow.Repository<Farm_SampleData_Item>().GetData().Where(a => a.User_Deletion_Id == null && a.FarmCommittee_ID == FarmCommittee_ID &&
            //                 (a.RejectReason_Ar.StartsWith(arName) && a.RejectReason_En.StartsWith(enName))
            //                      // get undeleted parent
            //                      && a.User_Deletion_Id == null).ToList();
            //    }
            //    switch (jtSorting)
            //    {
            //        case "Ar_Name ASC":
            //            data = data.OrderBy(t => t.RejectReason_Ar).ToList();
            //            break;
            //        case "Ar_Name DESC":
            //            data = data.OrderByDescending(t => t.RejectReason_Ar).ToList();
            //            break;
            //        case "En_Name ASC":
            //            data = data.OrderBy(t => t.RejectReason_En).ToList();
            //            break;
            //        case "En_Name DESC":
            //            data = data.OrderByDescending(t => t.RejectReason_En).ToList();
            //            break;


            //    }
            //    string lang = Device_Info[2];
            //    //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
            //    data_Count = data.Count();

            //    dic.Add("Count_Data", data_Count);
            //    dic.Add("Fees_Action_Data", data);

            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }
        public Dictionary<string, object> GetFarmCommitteeType(long FarmCommittee_ID)
        {
            byte? committeeType = uow.Repository<Farm_Committee>().GetData().Where(f => f.ID == FarmCommittee_ID).FirstOrDefault().CommitteeType_ID;
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, committeeType);

        }
        public Dictionary<string, object> Insert(Farm_SampleDataDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Farm_SampleDataDTO entity)
        {
            var obj = entity as Farm_SampleDataDTO;
            return uow.Repository<Farm_SampleData_Item>().GetAny(p => obj.ID == 0 ? true : p.ID != obj.ID);
        }

        public Dictionary<string, object> Update(Farm_SampleDataDTO entity, List<string> Device_Info)
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
    }
}
