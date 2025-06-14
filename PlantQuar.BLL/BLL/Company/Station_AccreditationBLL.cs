using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Company
{
    public class Station_AccreditationBLL : IGenericBLL<Station_AccreditationDTO>
    {
        private UnitOfWork uow;
        public Station_AccreditationBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Station_Accreditation entity = uow.Repository<Station_Accreditation>().Findobject(Id);
                var empDTO = Mapper.Map<Station_Accreditation, Station_AccreditationDTO>(entity);
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
            var count = uow.Repository<Station_Accreditation>().GetData().Where(p => p.User_Deletion_Id == null
            // get undeleted parent
               && p.StationActivity.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Station_Accreditation>().GetData().Where(p => p.User_Deletion_Id == null
                // get undeleted parent
               && p.StationActivity.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Station_Accreditation, Station_AccreditationDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Station_AccreditationDTO obj)
        {
            return uow.Repository<Station_Accreditation>().GetAny(p => p.User_Deletion_Id == null &&
                                       p.StationActivityID == obj.StationActivityID &&
                                       p.Item_ID == obj.Item_ID &&
                                       p.StartDate == obj.StartDate &&
                                       p.EndDate == obj.EndDate &&
                                       (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Station_Accreditation>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Station_Accreditation>().Update(Cmodel);
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

        //******************************************//
        public Dictionary<string, object> Insert(Station_AccreditationDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Station_Accreditation>(entity);
                    // var obj = entity as Station_AccreditationDTO;
                    //get system code of product/plant
                    /*  string ValueName = "";
                      if (obj.bridge == 1)
                      {
                          ValueName = "منتج";
                          CModel.ProdPlant_ID = obj.Product_ID;
                      }
                      else
                      {
                          ValueName = "نبات";
                          CModel.ProdPlant_ID = obj.Plant_ID;
                      }*/
                    //  int SysCode = uow.Repository<A_SystemCode>().GetData().Where(a => a.SystemCodeTypeId == 2 && a.ValueName == ValueName).Select(a=>a.Id).FirstOrDefault();
                    // CModel.IsPlant = SysCode;
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_seq");
                    var data = uow.Repository<Station_Accreditation>().InsertReturn(CModel);
                    uow.SaveChanges();
                    Station_AccreditationDTO _data = new Station_AccreditationDTO();
                    _data = Mapper.Map<Station_AccreditationDTO>(data);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _data);
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
        public Dictionary<string, object> Update(Station_AccreditationDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    Station_Accreditation CModel = uow.Repository<Station_Accreditation>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Station_Accreditation>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Station_Accreditation, Station_AccreditationDTO>(Co);
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
        public Dictionary<string, object> GetStationAccredition(int stationid, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                Int64 data_Count = 0;
                var data = uow.Repository<Station_Accreditation>().GetData().Where(x => x.StationActivity.Station_ID == stationid && x.User_Deletion_Date == null).
                    Select(x => new Station_AccreditationDTO
                    {
                        CountryID = x.StationAccrediationCountries.Where(c => c.User_Deletion_Id == null).Select(c => c.CountryID).ToList(),
                        EndDate = x.EndDate,
                        FileUpload = x.FileUpload,
                        ID = x.ID,
                        IsActive = x.IsActive,
                        Item_ID = x.Item_ID,
                        StartDate = x.StartDate,
                        StationActivityID = x.StationActivityID,
                        Treatment_Id = x.Station_AccreditationTreatment.Where(c => c.User_Deletion_Id == null).Select(c => c.Treatment_Id).FirstOrDefault(),
                        User_Creation_Date = x.User_Creation_Date,
                        User_Creation_Id = x.User_Creation_Id,
                        User_Deletion_Date = x.User_Deletion_Date,
                        User_Deletion_Id = x.User_Deletion_Id,
                        User_Updation_Date = x.User_Updation_Date,
                        User_Updation_Id = x.User_Updation_Id,
                        TreatmentCheck = (x.Station_AccreditationTreatment.Where(c => c.User_Deletion_Id == null && c.Station_AccreditationID == x.ID).Select(c => c.Treatment_Id).FirstOrDefault()) == 0 ? 0 : 1,
                        TreatmentMain_Id = x.Station_AccreditationTreatment.Where(c => c.User_Deletion_Id == null).Select(c => c.TreatmentType.MainType_ID).FirstOrDefault()
                    });
                data_Count = data.Count();
                var _data = data.ToList();
                dic.Add("Count_Data", data_Count);
                dic.Add("Station_Data", _data);
                //   var _DTO = entity.Select(Mapper.Map<Station_Accreditation, Station_AccreditationDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetProdORPlant(int ProdPlant, List<string> Device_Info)
        {
            try
            {
                //check product or plant 
                if (ProdPlant == 4)//is plant
                {
                    var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
                }
                else if (ProdPlant == 5)//is product
                {
                    var data = uow.Repository<Product>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
                }
                else if (ProdPlant == 16)//is بند خاضع  حي
                {
                    var data = uow.Repository<LiableItem>().GetData().Where(a => a.User_Deletion_Id == null && a.IsAlive == 14 && a.IsActive == true).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
                }
                else //is بند خاضع غير حي 33
                {
                    var data = uow.Repository<LiableItem>().GetData().Where(a => a.User_Deletion_Id == null && a.IsAlive == 15 && a.IsActive == true).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
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
