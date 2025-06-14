using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.Committee
{
    public class farmReqAnalysisTypeBLL
    {
        private UnitOfWork uow;

        public farmReqAnalysisTypeBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetAll(long farmCommitteeId, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Farm_SampleData>().GetData().Where(f => f.FarmCommittee_ID == farmCommitteeId && f.User_Deletion_Id == null).ToList();

                var dataDTO = data.Select(v => new Farm_SampleData2DTO
                {
                    ID = v.ID,
                    AnalysisLabType_ID = v.AnalysisLabType_ID,
                    FarmCommittee_ID = v.FarmCommittee_ID,
                    Farm_Request_ItemCategories_ID = v.Farm_Request_ItemCategories_ID,
                    categoryName = uow.Repository<Farm_Request_ItemCategories>().GetData()
                    .Include(i=>i.Farm_ItemCategories.ItemCategory).Where(a => a.ID == v.Farm_Request_ItemCategories_ID)
                    .FirstOrDefault().Farm_ItemCategories.ItemCategory.Name_Ar,
                    ListAnalysisType_Id = uow.Repository<AnalysisLabType>().GetData().Where(a => a.ID == v.AnalysisLabType_ID).FirstOrDefault().AnalysisTypeID,
                }).Distinct().ToList();
                Int64 data_Count = 0;
                Dictionary<string, object> dic = new Dictionary<string, object>();
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Item_Data", dataDTO);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Farm_SampleData2DTO entity)
        {
            var obj = entity as Farm_SampleData2DTO;
            if(obj.ID ==0)
            {
                return uow.Repository<Farm_SampleData>().GetAny(p => (p.FarmCommittee_ID == entity.FarmCommittee_ID && p.AnalysisLabType_ID == entity.AnalysisLabType_ID && p.User_Deletion_Id == null) &&
            (obj.ID == 0 ? true : p.ID != obj.ID));
            }
            else
            {
                return uow.Repository<Farm_SampleData>().GetAny(p => (p.FarmCommittee_ID == entity.FarmCommittee_ID && p.AnalysisLabType_ID == entity.AnalysisLabType_ID && p.Farm_Request_ItemCategories_ID == entity.Farm_Request_ItemCategories_ID && p.User_Deletion_Id == null) &&
            (obj.ID == 0 ? true : p.ID != obj.ID));
            }
            
            // return false;
        }
        //******************************************//
        public Dictionary<string, object> Insert(Farm_SampleData2DTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    // var CModel = Mapper.Map<Farm_SampleData>(entity);
                    // loop and save for each category
                    //get far id 

                    // var farmId = uow.Repository<Farm_Committee>().GetData().Include(f => f.Farm_Request).Where(c => c.ID == entity.FarmCommittee_ID).FirstOrDefault().Farm_Request.FarmsData_ID;
                    var farmReqId = uow.Repository<Farm_Committee>().GetData().Where(c => c.ID == entity.FarmCommittee_ID).FirstOrDefault().Farm_Request_ID;
                    //eslam new temp for Barcode
                    var empID = uow.Repository<CommitteeEmployee>().GetData().Where(c => c.Committee_ID == entity.FarmCommittee_ID && c.User_Deletion_Id == null && c.User_Deletion_Date == null && c.OperationType == 78 && c.ISAdmin == true).FirstOrDefault();
                    //get categories

                    var farmcategories = uow.Repository<Farm_Request_ItemCategories>().GetData().Where(f => f.Farm_Request_ID == farmReqId).ToList();

                    //int length = 5;
                    // string empID_string = empID.Employee_Id.ToString("D" + length);

   Random rd = new Random();
                    Farm_SampleData2DTO dto;
                    foreach (var cat in farmcategories)
                    {
                        dto = new Farm_SampleData2DTO();
                        dto = entity;
                        var id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_SampleData_seq");
                        dto.ID = id;
                        dto.Farm_Request_ItemCategories_ID = cat.ID;
                        //eslam new temp for Barcode
                        //create 5 digit random for barcode
                     
                        string rand = rd.Next(0, 100000).ToString("D5");
                        // save barcode
                        var dayofyear = "000" + DateTime.Now.DayOfYear;

                        var zx = DateTime.Now.Year.ToString().Substring(2);
                        var hour = (DateTime.Now.Hour).ToString("D" + 2);
                        var min = (DateTime.Now.Minute).ToString("D" + 2);
                        var sec = (DateTime.Now.Second).ToString("D" + 2);
                        string barcode = "78" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
                        dto.Sample_BarCode = barcode;

                        var obj = Mapper.Map<Farm_SampleData>(dto);

                        uow.Repository<Farm_SampleData>().InsertRecord(obj);
                        uow.SaveChanges();
                    }


                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> Update(Farm_SampleData2DTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as Farm_SampleData2DTO;
                   Farm_SampleData CModel = uow.Repository<Farm_SampleData>().Findobject(obj.ID);

                    //Farm_SampleData CModel = uow.Repository<Farm_SampleData>().GetData().Where(h=>h.FarmCommittee_ID == entity.FarmCommittee_ID && )

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Farm_SampleData>().Update(Co);
                    uow.SaveChanges();

                    //var farmcategories = uow.Repository<Farm_SampleData>().GetData().Where(f => f.FarmCommittee_ID == entity.FarmCommittee_ID).ToList();
                    //foreach (var cat in farmcategories)
                    //{
                    //    Farm_SampleData vv = new Farm_SampleData();
                    //    vv = Co;

                    //    vv.ID = cat.ID;
                    //    vv.Farm_ItemCategories_ID = cat.Farm_ItemCategories_ID;
                    //    uow.Repository<Farm_SampleData>().Update(vv);
                    //    uow.SaveChanges();
                    //}



                    var empDTO = Mapper.Map<Farm_SampleData, Farm_SampleData2DTO>(Co);
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
        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Farm_SampleData>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Farm_SampleData>().Update(Cmodel);
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

    }
}
