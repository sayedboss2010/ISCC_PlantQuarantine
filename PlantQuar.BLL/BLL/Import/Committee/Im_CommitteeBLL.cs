using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Import.CheckRequests;
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
    public class Im_CommitteeBLL
    {
        private UnitOfWork uow;

        public Im_CommitteeBLL()
        {
            uow = new UnitOfWork();
        }

        public bool GetAny(Im_RequestCommitteeDTO entity)
        {
            //var obj = entity as Ex_RequestCommitteeDTO;
            //return uow.Repository<Ex_RequestCommittee>().GetAny(p => (p.User_Deletion_Id == null )&& (obj.ID == 0 ? true : p.ID != obj.ID));
            return false;
        }
        //******************************************//
        public Dictionary<string, object> Insert(Im_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            //try
            //{

            //    if (!GetAny(entity))
            //    {
            //        var CModel = Mapper.Map<Im_RequestCommittee>(entity);
            //        CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");

            //        var req_com = uow.Repository<Im_RequestCommittee>().InsertReturn(CModel);
            //        uow.SaveChanges();



            //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
            //    }
            //    else
            //    {
            //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
            return null;
        }


        //get committee type for request
        public Dictionary<string, object> getCommitteeTypeForrequest(long requestId, List<string> Device_Info)
        {
            try
            {
                string constrain = "";
                var req = uow.Repository<Farm_Request>().GetData().Include(f => f.FarmsData).Where(r => r.ID == requestId).FirstOrDefault();
                var itemId = req.FarmsData.Item_ID;
                var countriesReq = uow.Repository<Farm_Country>().GetData().Include(c => c.Country).Where(c => c.Farm_Request_ID == requestId).ToList();
                if (countriesReq.Count > 0)
                {
                    constrain = "لا بد من ادخال دول على الطلب  :";
                }
                    var ispreview = false;
                    var isAnalysis = false;
                    //List<short> analysisTypeIdList = new List<short>();
                    var committeeTypeId = 0;//*
                    int countriesNo = countriesReq.Count;
                    int counConNo = 0;
                    List<string> countries = new List<string>();//*

                    farmCountryConstrainReturnDTO data = new farmCountryConstrainReturnDTO();


                    foreach (var cn in countriesReq)
                    {
                        var farmConstrain = uow.Repository<Farm_Constrain>().GetData().Where(k => k.Item_ID == itemId && k.Country_Id == cn.Country_ID && k.IsActive == true).ToList();
                        //tolist
                        if (farmConstrain.Count > 0)
                        {
                            counConNo++;
                        }
                        else
                        {
                            countries.Add(cn.Country.Ar_Name);
                        }
                        foreach (var con in farmConstrain)
                        {

                            if (con.Is_Preview == true)
                            {
                                ispreview = true;
                            }
                            if (con.AnalysisType_ID != null)
                            {
                                isAnalysis = true;
                            }

                        }

                    }
                    if (ispreview == true && isAnalysis == true)
                    {
                        committeeTypeId = 12;
                    }
                    else if (isAnalysis == true)
                    {
                        committeeTypeId = 3;
                    }
                    else if (ispreview == true)
                    {
                        committeeTypeId = 5;
                    }
                    if (countriesNo != counConNo)
                    {
                        constrain = "لا بد من ادخال اشتراطات للدول :";
                        foreach (var ccc in countries)
                        {
                            constrain += ccc;
                        }
                    }
                    data.committeeTypeId = committeeTypeId;
                    data.constrainText = constrain;
                    data.countries = countries;
               // }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //get analysis type for request
        public Dictionary<string, object> getCommitteeAnalysisTypeForrequest(long requestId, List<string> Device_Info)
        {                       
            //long? [] requestId1 = { 910004, 910008, 910010, 910011 };
            //long? [] requestId1 = {   910010 };
            try
            {
                var req = uow.Repository<Farm_Request>().GetData().Include(f => f.FarmsData).Where(r =>  r.ID== requestId).ToList();
              
                //var itemId = req.FarmsData.Item_ID;

               // var countriesReq = uow.Repository<Farm_Country>().GetData().Where(c => requestId1.Contains(c.Farm_Request_ID)).Select(d => d.Country_ID).ToList();
                var countriesReq = uow.Repository<Farm_Country>().GetData().Where(c => c.Farm_Request_ID == requestId).Select(d => d.Country_ID).ToList();
                string lang = Device_Info[2];
                List<CustomOptionLongId> analysisTypeIdList = new List<CustomOptionLongId>();
                var listIds = new List<long>();
                //en
                foreach (var item in req)
                {               
                foreach (var cn in countriesReq)
                {
                    var farmConstrain = uow.Repository<Farm_Constrain>().GetData().Include(y => y.AnalysisType).Where(k => k.Item_ID == item.FarmsData.Item_ID && k.Country_Id == cn).ToList();
                    CustomOptionLongId at;
                    foreach (var con in farmConstrain)
                    {
                        if (con.AnalysisType_ID != null)
                        {
                            at = new CustomOptionLongId();
                            at.Value = con.AnalysisType_ID;
                            at.DisplayText = lang == "1" ? con.AnalysisType.Name_Ar : con.AnalysisType.Name_En;
                            if (!listIds.Contains((long)at.Value))
                            {
                                listIds.Add((long)at.Value);
                                analysisTypeIdList.Add(at);
                            }
                        }

                    }
                }
                }
                analysisTypeIdList = analysisTypeIdList.Distinct().ToList();
                analysisTypeIdList.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, analysisTypeIdList);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> getAnalysisLabType(int analType, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<AnalysisLabType>().GetData().Include(l => l.AnalysisLab).Where(c => c.AnalysisTypeID == analType).Select(c => new CustomOptionLongId
            {
                //change display lang
                DisplayText = (lang == "1" ? c.AnalysisLab.Name_Ar : c.AnalysisLab.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }


    }
}
