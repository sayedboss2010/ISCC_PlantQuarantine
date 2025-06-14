using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Company
{
    public class Company_NationalBLL : IGenericBLL<CompanyNationalDTO>
    {
        private UnitOfWork uow;

        public Company_NationalBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Company_National entity = uow.Repository<Company_National>().Findobject(Id);
                var empDTO = Mapper.Map<Company_National, CompanyNationalDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Company_National>().GetData().Where(p => p.User_Deletion_Id == null
              ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Company_National>().GetData().Where(a => a.User_Deletion_Id == null
                ).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<Company_National, CompanyNationalDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, bool IsTreatment, List<string> Device_Info)
        {
            try
            {
                //    var data = uow.Repository<Company_National>().GetData().Where(a => a.User_Deletion_Id == null
                //    && a.IsTreatment ==  IsTreatment
                //    ).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                //    var dataDTO = data.Select(Mapper.Map<Company_National, CompanyNationalDTO>);
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
                //
                return null;
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Company_National>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Company_National>().GetData().Where(a =>
                      (a.Name_En.Contains(enName.Trim()) || a.CommertialRecord.Contains(enName.Trim()) || a.TaxesRecord.Contains(enName.Trim())) &&
                    a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<Company_National>().GetData().Where(a =>
                       (a.Name_Ar.Contains(arName.Trim()) || a.CommertialRecord.Contains(arName.Trim()) || a.TaxesRecord.Contains(arName.Trim())) &&
                         a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Company_National>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    //data = data.Where(a => (a.Ar_Name.StartsWith(arName) || a.En_Name.StartsWith(enName))).ToList();
                    data = uow.Repository<Company_National>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName.Trim()) && a.Name_En.StartsWith(enName.Trim())) &&
                  a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                string lang = Device_Info[2];
                // var dataDto = data.OrderBy(A => (lang=="1"?A.Name_Ar:A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Company_National, CompanyNationalDTO>);

                var dataDto = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En))
                    .Skip(index).Take(pageSize).Select(x => new CompanyNationalDTO
                    {

                        ID = x.ID,
                        Center_ID = x.Center_ID,
                        Village_ID = x.Village_ID,
                        Owner_Ar = x.Owner_Ar,
                        Owner_En = x.Owner_En,
                        IsTreatment = x.IsTreatment,
                        Name_Ar = x.Name_Ar,
                        Name_En = x.Name_En,
                        Address_Ar = x.Address_Ar,
                        Address_En = x.Address_En,
                        TaxesRecord = x.TaxesRecord,
                        CommertialRecord = x.CommertialRecord,
                        IsActive = x.IsActive,
                        User_Creation_Date = x.User_Creation_Date,
                        User_Creation_Id = x.User_Creation_Id,
                        User_Deletion_Date = x.User_Deletion_Date,
                        User_Deletion_Id = x.User_Deletion_Id,
                        User_Updation_Date = x.User_Updation_Date,
                        User_Updation_Id = x.User_Updation_Id,
                        Gov_ID = x.Center_ID == null ? null : uow.Repository<Center>().GetData().Where(r => r.ID == x.Center_ID).Select(y => y.Govern_ID).FirstOrDefault()

                    }).ToList();
                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        data = data.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        data = data.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        data = data.OrderByDescending(t => t.Name_En).ToList();
                        break;
                }
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Company_National_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public Dictionary<string, object> GetAll(int pageSize, int index, string jtSorting, List<string> Device_Info)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();

        //        Int64 data_Count = 0;

        //        var data = uow.Repository<Company_National>().GetData()
        //            .Where(i => i.User_Deletion_Id == null).ToList();

        //        var dataDto = data.Select(x => new CompanyNationalDTO
        //        {
        //            ID = x.ID,
        //            Center_ID = x.Center_ID,
        //            Village_ID = x.Village_ID,
        //            Owner_Ar = x.Owner_Ar,
        //            Owner_En = x.Owner_En,
        //            IsTreatment = x.IsTreatment,
        //            Name_Ar = x.Name_Ar,
        //            Name_En = x.Name_En,
        //            Address_Ar = x.Address_Ar,
        //            Address_En = x.Address_En,
        //            TaxesRecord = x.TaxesRecord,
        //            CommertialRecord = x.CommertialRecord,
        //            IsActive = x.IsActive,
        //            User_Creation_Date = x.User_Creation_Date,
        //            User_Creation_Id = x.User_Creation_Id,
        //            User_Deletion_Date = x.User_Deletion_Date,
        //            User_Deletion_Id = x.User_Deletion_Id,
        //            User_Updation_Date = x.User_Updation_Date,
        //            User_Updation_Id = x.User_Updation_Id,
        //            Gov_ID = x.Center_ID == null ? null : uow.Repository<Center>().GetData().Where(r => r.ID == x.Center_ID).Select(y => y.Govern_ID).FirstOrDefault()
        //        }).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();

        //        data_Count = dataDto.Count();
        //        dic.Add("Count_Data", data_Count);
        //        dic.Add("Item_Data", dataDto);

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic); 
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public bool GetAny(CompanyNationalDTO entity)
        {
            var obj = entity as CompanyNationalDTO;
            return uow.Repository<Company_National>().GetAny(p => p.User_Deletion_Id == null &&
                                        p.Name_Ar == obj.Name_Ar && p.Name_En == obj.Name_En &&
                                        p.CommertialRecord == entity.CommertialRecord && p.TaxesRecord == entity.TaxesRecord
                                        && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        //public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        //{
        //    throw new NotImplementedException();
        //}

        //******************************************//
        public Dictionary<string, object> Insert(CompanyNationalDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Company_National>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Company_National_seq");

                    var model = uow.Repository<Company_National>().InsertReturn(CModel);
                    uow.SaveChanges();
                    //add contact
                    Ex_ContactDataBLL contactBLL = new Ex_ContactDataBLL();
                    int exporter_type = 6;

                    contactBLL.InsertRecords((short)entity.User_Creation_Id, model.ID, entity.User_Creation_Date, exporter_type, entity.Contacts, Device_Info);

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
        public Dictionary<string, object> Update(CompanyNationalDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();

                if (!GetAny(entity))
                {
                    var obj = entity as CompanyNationalDTO;
                    Company_National CModel = uow.Repository<Company_National>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Company_National>().Update(Co);
                    uow.SaveChanges();
                    Ex_ContactDataBLL contactBLL = new Ex_ContactDataBLL();
                    int exporter_type = 6;
                    contactBLL.UpdateRecords(entity.User_Updation_Id.Value, entity.ID, entity.User_Updation_Date.Value, exporter_type, entity.Contacts, Device_Info);
                    var empDTO = Mapper.Map<Company_National, CompanyNationalDTO>(Co);
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
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Company_National>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Company_National>().Update(Cmodel);
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Company_National>().GetData().Where(lab => lab.User_Deletion_Id == null)
                    .Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                        Value = c.ID
                    }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Company_National>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true)
                   .Select(c => new CustomOptionLongId
                   {
                       //change display lang
                       DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                       Value = c.ID
                   }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> FillDrop_Add(bool IsTreatment, List<string> Device_Info)
        {
            //fz IsTreatment was deleted !!!!!!!!!! and i returned it back 8-9-2019
            string lang = Device_Info[2];
            var data = uow.Repository<Company_National>().GetData().
                Where(lab => lab.IsTreatment == IsTreatment &&
                lab.User_Deletion_Id == null)
                    .Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                        Value = c.ID
                    }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            // return null;
        }

        public Dictionary<string, object> FillDrop_Im_Permission(List<string> Device_Info)
        {
            string lang = Device_Info[2];

            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            //                from ipi in entities.Im_RequestData rr 
            //                left join Company_National cn on rr.Importer_ID = cn.ID  and rr.ImporterType_Id = 6
            //left join   Public_Organization po on rr.Importer_ID = po.ID and rr.ImporterType_Id = 7
            //left join   Person pr on rr.Importer_ID = pr.ID and rr.ImporterType_Id = 8

            var data = (from rr in entities.Im_RequestData
                        join cn in entities.Company_National on new { a = (long)rr.Importer_ID, b = (int)rr.ImporterType_Id } equals new { a = cn.ID, b = 6 } into cn1
                        from cn in cn1.DefaultIfEmpty()
                        join po in entities.Public_Organization on new { a = (long)rr.Importer_ID, b = (int)rr.ImporterType_Id } equals new { a = po.ID, b = 7 } into po1
                        from po in po1.DefaultIfEmpty()
                        join pr in entities.People on new { a = (long)rr.Importer_ID, b = (int)rr.ImporterType_Id } equals new { a = pr.ID, b = 8 } into pr1
                        from pr in pr1.DefaultIfEmpty()
                        select new CustomOptionLongId
                        {
                            DisplayText = rr.ImporterType_Id == 6 ? cn.Name_Ar :
                                                   rr.ImporterType_Id == 7 ? po.Name_Ar :
                                                   rr.ImporterType_Id == 8 ? pr.Name : "",
                            Value = rr.ImporterType_Id == 6 ? cn.ID :
                                       rr.ImporterType_Id == 7 ? po.ID :
                                       rr.ImporterType_Id == 8 ? pr.ID : 0,
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

            //var data = uow.Repository<Company_National>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true)
            //       .Select(c => new CustomOptionLongId
            //       {
            //           //change display lang
            //           DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
            //           Value = c.ID
            //       }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //SARA
        public Dictionary<string, object> FillDrop_ByActivity(int activityId, List<string> Device_Info)
        {
            PlantQuarantineEntities entity = new PlantQuarantineEntities();
            string lang = Device_Info[2];

            var data = (from company in entity.Company_National
                        join activity in entity.CompanyActivities on company.ID equals activity.Company_ID
                        where activity.MainActivityType == activityId && company.User_Deletion_Id == null && company.IsActive == true
                        select new CustomOptionLongId
                        {
                            //change display lang
                            DisplayText = (lang == "1" ? company.Name_Ar : company.Name_En),
                            Value = company.ID
                        }).ToList();

            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }


        //reports for Company
        public Dictionary<string, object> GetCompaniesNumber(int rep, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                int x = rep;
                var data = uow.Repository<SP_GetCompaniesNumbers_DTO>().CallStored("SP_GetCompaniesNumbers", null,
                    null, Device_Info).FirstOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_Im_CheckRequest(long Outlet_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];

            PlantQuarantineEntities entities = new PlantQuarantineEntities();

            var data = (from rr in entities.Im_CheckRequest_Data
                        join cn in entities.Company_National on new { a = (long)rr.Importer_ID, b = (int)rr.ImporterType_Id } equals new { a = cn.ID, b = 6 } into cn1
                        from cn in cn1.DefaultIfEmpty()
                        join po in entities.Public_Organization on new { a = (long)rr.Importer_ID, b = (int)rr.ImporterType_Id } equals new { a = po.ID, b = 7 } into po1
                        from po in po1.DefaultIfEmpty()
                        join pr in entities.People on new { a = (long)rr.Importer_ID, b = (int)rr.ImporterType_Id } equals new { a = pr.ID, b = 8 } into pr1
                        from pr in pr1.DefaultIfEmpty()
                        where rr.Im_CheckRequest.Outlet_ID == Outlet_ID
                        select new CustomOptionLongId
                        {
                            DisplayText = rr.ImporterType_Id == 6 ? cn.Name_Ar :
                                                   rr.ImporterType_Id == 7 ? po.Name_Ar :
                                                   rr.ImporterType_Id == 8 ? pr.Name : "",
                            Value = rr.ImporterType_Id == 6 ? cn.ID :
                                       rr.ImporterType_Id == 7 ? po.ID :
                                       rr.ImporterType_Id == 8 ? pr.ID : 0,
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();


            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
//SELECT COUNT(CASE WHEN  (per.IsActive= 1 and User_Deletion_Id is null)THEN 1  END) As 'مقبول',
//COUNT(CASE WHEN(per.IsActive = 0 and User_Deletion_Id is null)THEN 1  END) As 'مرفوض',

//COUNT(CASE WHEN(per.IsActive is null and User_Deletion_Id is null)THEN 1  END) As 'تحت الطلب'

// FROM[PlantQuarantine_Test].[dbo].[Company_National] per
//where   per.IsApproved is null 

//GROUP BY  per.IsApproved;