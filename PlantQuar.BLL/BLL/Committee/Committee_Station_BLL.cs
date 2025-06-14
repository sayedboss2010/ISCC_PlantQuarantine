using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Committee
{
    public class Committee_Station_BLL
    {
        private UnitOfWork uow;

        public Committee_Station_BLL()
        {
            uow = new UnitOfWork();
        }
              
        public Dictionary<string, object> Insert_Committee(Committee_Station_DTO entity, List<string> Device_Info)
        {
            try
            {
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        var operationType = 79; //ask
                                                //long Committe_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");
                        short _User_Creation_Id = 1;
                        var _User_Creation_Date = DateTime.Now;
                        foreach (var item in entity.List_Committee_Station)
                        {
                            _User_Creation_Id = item.User_Creation_Id;
                            //#region Station_Request                                
                            //Station_Accreditation_Request CModel_Station_Request = uow.Repository<Station_Accreditation_Request>().Findobject(item.Station_Request_ID);
                            //CModel_Station_Request.IsStatus = false;                                                      
                            //uow.Repository<Station_Accreditation_Request>().Update(CModel_Station_Request);
                            //uow.SaveChanges();
                            //#endregion

                            #region Station_Committee
                            var id_Comm = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Committee_seq");
                            var Station_Committee = new Station_Accreditation_Committee
                            {
                                ID = id_Comm,
                                Station_Accreditation_Request_ID=item.Station_Request_ID,
                                User_Updation_Date = item.User_Updation_Date,
                                User_Updation_Id = item.User_Updation_Id,
                                CommitteeType_ID = item.CommitteeType_ID,
                                Delegation_Date = item.Delegation_Date = item.Delegation_Date,
                                StartTime = item.StartTime,
                                EndTime = item.EndTime,
                                //IsApproved = true, 
                                Status = false,
                                User_Creation_Id = item.User_Creation_Id,
                                User_Creation_Date = item.User_Creation_Date,
                            };
                            context.Station_Accreditation_Committee.Add(Station_Committee);
                            context.SaveChanges();
                            #endregion

                            #region Employee              
                            if (entity.List_emp.Count > 0)
                            {
                                foreach (var item_Emp in entity.List_emp)
                                {
                                    long _Employee_Id = long.Parse(item_Emp.Employee_Id.ToString());

                                    var Comm_Employee = new CommitteeEmployee
                                    {
                                        Committee_ID = id_Comm,
                                        Employee_Id = _Employee_Id,
                                        ISAdmin = item_Emp.ISAdmin,
                                        OperationType = operationType,
                                        User_Creation_Id = item.User_Creation_Id,
                                        User_Creation_Date = item.User_Creation_Date,
                                    };
                                    context.CommitteeEmployees.Add(Comm_Employee);
                                    context.SaveChanges();
                                }
                            }
                            #endregion

                            #region Shift

                            if (entity.List_ShiftTiming != null)
                            {
                                if (entity.List_ShiftTiming.Count > 0)
                                {
                                    foreach (var item_Shift in entity.List_ShiftTiming)
                                    {
                                        var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Committee_Shift_seq");
                                        var _Amount = decimal.Parse(item_Shift.Amount.ToString());
                                        var Committee_Shift = new Station_Accreditation_Committee_Shift
                                        {
                                            ID = id,
                                            Station_Accreditation_Committee_ID = id_Comm,
                                            ShiftTiming_ID = item_Shift.ShiftTiming_ID,
                                            Count = item_Shift.Count,
                                            Amount = _Amount,
                                            User_Creation_Id = item.User_Creation_Id,
                                            User_Creation_Date = item.User_Creation_Date,
                                        };
                                        context.Station_Accreditation_Committee_Shift.Add(Committee_Shift);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Fees

                            if (entity.List_Station_Request_Fees != null)
                            {
                                if (entity.List_Station_Request_Fees.Count > 0)
                                {
                                    foreach (var item_Fees in entity.List_Station_Request_Fees)
                                    {
                                        var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Request_Fees_ENG_seq");
                                        var _Value = decimal.Parse(item_Fees.Value.ToString());
                                        var Committee_Request_Fees_ENG = new Station_Accreditation_Request_Fees_ENG
                                        {
                                            ID = id,
                                            Station_Accreditation_Committee_ID = id_Comm,
                                            Station_Fees_Type_ID = item_Fees.Station_Fees_Type_ID,
                                            //Count = item_Fees.Count,
                                            Num_Eng =item_Fees.Num_Eng,
                                            Value = _Value,
                                            User_Creation_Id = item.User_Creation_Id,
                                            User_Creation_Date = item.User_Creation_Date,
                                        };
                                        context.Station_Accreditation_Request_Fees_ENG.Add(Committee_Request_Fees_ENG);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion
                            // محتاجة تعديل
                            #region Station_Committee_CheckList
                            var List_CheckList = (from sar in context.Station_Accreditation_Request 
                              join sad in context.Station_Accreditation_Data  on sar.Station_Accreditation_Data_ID equals sad.ID
                              join sacl in context.Station_Accreditation_CheckList  on sad.ID equals sacl.Station_Accreditation_Data_ID                             
                              where sar.ID == item.Station_Request_ID
                                        && sacl.Station_CheckList.Is_Androud == true
                                        && sacl.IsActive == true
                                                  select new  { sacl.ID }).ToList();

                            int Complete_Count = 0;
                            if (List_CheckList.Count() > 0)
                            {
                                foreach (var CheckList in List_CheckList)
                                {
                                    var ID_Committee_CheckList = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Committee_CheckList_seq");
                                    var Committee_CheckList = new Station_Accreditation_Committee_CheckList
                                    {
                                        ID = ID_Committee_CheckList,
                                        Committee_ID = id_Comm,
                                        Station_Accreditation_CheckList_ID = CheckList.ID,
                                        User_Creation_Id = _User_Creation_Id,
                                        User_Creation_Date = _User_Creation_Date,
                                    };
                                    context.Station_Accreditation_Committee_CheckList.Add(Committee_CheckList);
                                    context.SaveChanges();
                                    Complete_Count = 1;
                                }
                            }
                            #endregion
                            if (Complete_Count > 0)
                            {
                                entity.Message = "تم تشكيل لجنة";
                                trans.Commit();
                            }
                            else
                            {
                                entity.Message = "لا يمكن عمل تشكيل لعدم وجود متطلبات";
                            }

                        }                                
                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                var err = ex;
                //foreach (var item in err)
                //{
 uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.ToString(), MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                //}
               
            }
        }
    }
}
