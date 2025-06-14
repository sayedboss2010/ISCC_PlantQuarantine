using AutoMapper;

using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Export_Constrains
{
    public class Export_ConstrainsBLL : IGenericBLL<Ex_CountryConstrainDTO>
    {
        private UnitOfWork uow;
        List<ExConstrainsLabsAndTyp> Analysis = new List<ExConstrainsLabsAndTyp>();
        List<ExConstrainsText> Text = new List<ExConstrainsText>();
        PlantQuarantineEntities db = new PlantQuarantineEntities();
        List<ExConstrainsAirPortAndCountry> Portss = new List<ExConstrainsAirPortAndCountry>();
        public Export_ConstrainsBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Ex_CountryConstrainDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(Ex_CountryConstrainDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(Ex_CountryConstrainDTO entity, List<string> Device_Info)
        {
            try
            {
                Ex_CountryConstrain CModel = uow.Repository<Ex_CountryConstrain>().Findobject(entity.ID);

                CModel.User_Updation_Date = entity.User_Updation_Date;
                CModel.User_Updation_Id = entity.User_Updation_Id;
                CModel.IsActive = entity.IsActive;

                //28-6-2020 constrain updates
                //CModel.IsCertificate_Addtion = entity.IsCertificate_Addtion;
                CModel.IsFarmAccreditation = entity.IsFarmAccreditation;
                CModel.IsCompanyAccreditation = entity.IsCompanyAccreditation;
                CModel.IsStationAccreditation = entity.IsStationAccreditation;

                //CModel.IsAnalysis = entity.IsAnalysis;
                //CModel.IsTreatment = entity.IsTreatment;

                #region Arrival port
                //11-9-2019 update centers
                if (entity.ArrivalPortList.Count > 0)
                {
                    UpdateArrivalPortList(entity.ArrivalPortList, CModel.ID,
                     CModel.User_Updation_Id, entity.User_Updation_Date, Device_Info);
                }
                #endregion

                uow.Repository<Ex_CountryConstrain>().Update(CModel);
                uow.SaveChanges();

                // if (CModel.IsAnalysis == true)
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();

                    var analysis = (from t in entities.Ex_CountryConstrain_AnalysisLabType
                                    where t.CountryConstrain_ID == entity.ID
                                    select t).FirstOrDefault();

                    analysis.User_Updation_Date = entity.User_Updation_Date;
                    analysis.User_Updation_Id = entity.User_Updation_Id;

                    uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Update(analysis);
                    uow.SaveChanges();
                }
                // if (CModel.IsTreatment == true)
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();

                    var treatment = (from t in entities.Ex_CountryConstrain_Treatment
                                     where t.CountryConstrain_ID == entity.ID
                                     select t).FirstOrDefault();

                    treatment.User_Updation_Date = entity.User_Updation_Date;
                    treatment.User_Updation_Id = entity.User_Updation_Id;
                    treatment.IsAcive = entity.IsActive;

                    uow.Repository<Ex_CountryConstrain_Treatment>().Update(treatment);
                    uow.SaveChanges();
                }

                var empDTO = Mapper.Map<Ex_CountryConstrain, Ex_CountryConstrainDTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetCustomConstrain(int constrainOwner_ID,
            int countryConstrain_Type, long item_ShortName_id, long itemCategories_ID, bool isStationAccreditation,
            bool isFarmAccreditation, bool isCompanyAccreditation, List<string> lists)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            //         select distinct   *from Ex_CountryConstrain Ecc
            //join Ex_CountryConstrain_Text Ecx on Ecc.ID = Ecx.CountryConstrain_ID
            try
            {
                #region getperv
                var data = (
                        from Ecc in db.Ex_CountryConstrain


                        where
                        Ecc.User_Deletion_Id == null
                        && Ecc.IsActive == true
                        && Ecc.User_Deletion_Date == null
                        && Ecc.ConstrainOwner_ID == constrainOwner_ID
                        && Ecc.CountryConstrain_Type == countryConstrain_Type
                        && Ecc.Item_ShortName_id == item_ShortName_id
                        && Ecc.ItemCategories_ID == itemCategories_ID
                        && Ecc.User_Updation_Date == null
                        && Ecc.TransportCountry_ID == null




                        select new ExCountryConstainDisplay
                        {

                            CountryConstrain_ID = Ecc.ID,


                            IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                            IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                            IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation

                        }

                        ).SingleOrDefault();


                if (data != null)
                {
                    var CountryConstrain_ID = data.CountryConstrain_ID;

                    var data3 = (
                    from CCt in db.Ex_CountryConstrain_Text



                    where
                    CCt.User_Deletion_Id == null
                     && CCt.User_Deletion_Date == null
                  && CCt.CountryConstrain_ID == CountryConstrain_ID



                    select new ExConstrainsText
                    {
                        ConstrainText_Ar = CCt.ConstrainText_Ar,
                        ConstrainText_En = CCt.ConstrainText_En,
                        IsCertificate_Addtion = (bool)CCt.IsCertificate_Addtion,
                        InSide_Certificate_Ar = CCt.InSide_Certificate_Ar,
                        InSide_Certificate_En = CCt.InSide_Certificate_En,



                    }

                    ).ToList();



                    var data1 = (
                             from CCA in db.Ex_CountryConstrain_AnalysisLabType
                             join Al in db.AnalysisLabTypes
                             on CCA.AnalysisLabTypeID equals Al.ID

                             join At in db.AnalysisTypes on
                            Al.AnalysisTypeID equals At.ID

                             join v in db.AnalysisLabs on
                     Al.AnalysisLabID equals v.ID


                             where
                             CCA.User_Deletion_Id == null
                             //  && CCA.IsActive == true
                             && CCA.User_Deletion_Date == null
                             && CCA.CountryConstrain_ID == CountryConstrain_ID
                           && Al.User_Deletion_Date == null
                           && Al.User_Deletion_Id == null
                           && At.User_Deletion_Id == null
                           && At.User_Deletion_Date == null
                           && v.User_Deletion_Id == null
                           && v.User_Deletion_Date == null




                             select new ExConstrainsLabsAndTyp
                             {
                                 LabName_Ar = v.Name_Ar,
                                 LabName_En = v.Name_En,
                                 TypeName_Ar = At.Name_Ar,
                                 TypeName_En = At.Name_En,
                                 ExConstrainsLabsAndTypID = Al.ID



                             }

                             ).ToList();

                    //            --join Ex_CountryConstrain_ArrivalPort EcP on Ecc.ID = Ecp.Ex_CountryConstrain_Id
                    //--join Port_International Pil on Ecp.Port_International_Id = Pil.ID
                    //--join country c on pil.Country_ID = c.ID
                    //--join Port_Type pt on Pil.PortTypeID = pt.ID


                    var data2 = (
                             from CCA in db.Ex_CountryConstrain_ArrivalPort
                             join pil in db.Port_International
                             on CCA.Port_International_Id equals pil.ID

                             join Ci in db.Countries on
                            pil.Country_ID equals Ci.ID

                             join v in db.Port_Type on
                     pil.PortTypeID equals v.ID


                             where
                             CCA.User_Deletion_Id == null
                             && CCA.User_Deletion_Date == null
                             && CCA.Ex_CountryConstrain_Id == CountryConstrain_ID
                           && pil.User_Deletion_Date == null
                           && pil.User_Deletion_Id == null
                           && Ci.User_Deletion_Id == null
                           && Ci.User_Deletion_Date == null
                           && v.User_Deletion_Id == null
                           && v.User_Deletion_Date == null




                             select new ExConstrainsAirPortAndCountry
                             {
                                 AirPortName_Ar = v.Name_Ar,
                                 AirPortName_En = v.Name_En,
                                 CountryName_Ar = pil.Name_Ar,
                                 CountryLabName_En = pil.Name_En,
                                 ExConstrainsAirPortAndCountryID = pil.ID



                             }

                             ).ToList();


                    //int c = 10;

                    data.Analysis = data1;
                    data.Text = data3;
                    data.Ports = data2;
                }


                #endregion


                return uow.Repository<Object>()
                        .DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, lists);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }




        }




        public Dictionary<string, object> Update_Activation(Ex_CountryConstrainDTO entity, List<string> Device_Info)
        {
            try
            {
                Ex_CountryConstrain CModel = uow.Repository<Ex_CountryConstrain>().Findobject(entity.ID);
                var constrainsList = uow.Repository<Ex_CountryConstrain>().GetData()
                    .Where(c => c.CountryConstrain_Type == CModel.CountryConstrain_Type &&
                c.ConstrainOwner_ID == CModel.ConstrainOwner_ID &&
                c.Item_ShortName_id == CModel.Item_ShortName_id
                // c.IsPlant == CModel.IsPlant &&
                //c.ProdPlant_ID == CModel.ProdPlant_ID
                && c.User_Deletion_Id == null).ToList();
                foreach (var con in constrainsList)
                {
                    Ex_CountryConstrain CModel2 = uow.Repository<Ex_CountryConstrain>().Findobject(con.ID);
                    CModel2.User_Updation_Date = entity.User_Updation_Date;
                    CModel2.User_Updation_Id = entity.User_Updation_Id;
                    CModel2.IsActive = entity.IsActive;

                    //28-6-2020 constrain updates
                    //CModel2.IsCertificate_Addtion = entity.IsCertificate_Addtion;
                    CModel2.IsFarmAccreditation = entity.IsFarmAccreditation;
                    CModel2.IsCompanyAccreditation = entity.IsCompanyAccreditation;
                    CModel2.IsStationAccreditation = entity.IsStationAccreditation;
                }



                #region Arrival port
                //11-9-2019 update centers
                if (entity.ArrivalPortList.Count > 0)
                {
                    foreach (var con in constrainsList)
                    {
                        UpdateArrivalPortList(entity.ArrivalPortList, con.ID,
                     CModel.User_Updation_Id, entity.User_Updation_Date, Device_Info);
                    }
                    //   UpdateArrivalPortList(entity.ArrivalPortList, CModel.ID,
                    //CModel.User_Updation_Id, entity.User_Updation_Date, Device_Info);
                }
                #endregion

                uow.Repository<Ex_CountryConstrain>().Update(CModel);
                uow.SaveChanges();



                var empDTO = Mapper.Map<Ex_CountryConstrain, Ex_CountryConstrainDTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        private void UpdateArrivalPortList(List<int> arrivalPortList,
            long Ex_CountryConstrain_Id, long? user_id, DateTime? Date_Now, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData().Where(x => x.Ex_CountryConstrain_Id == Ex_CountryConstrain_Id
                && x.User_Deletion_Id == null).ToList();
                var addlst = arrivalPortList.Except(data.Select(x => x.Port_International_Id)).ToList();
                var deletelst = data.Where(x => arrivalPortList.IndexOf(x.Port_International_Id) == -1).ToList();
                InsertArrivalPortList((long)user_id, Date_Now, Ex_CountryConstrain_Id, addlst, Device_Info);
                foreach (var item in deletelst)
                {
                    item.User_Deletion_Date = Date_Now;
                    item.User_Deletion_Id = user_id;
                    uow.Repository<Ex_CountryConstrain_ArrivalPort>().Update(item);
                    uow.SaveChanges();
                    //DeleteArrivalPortList(item, Device_Info);
                }
                //  return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, arrivalPortList);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                //  return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }
        }

        private void DeleteArrivalPortList(Ex_CountryConstrain_ArrivalPort item, List<string> device_Info)
        {
            throw new NotImplementedException();
        }

        private void InsertArrivalPortList(long user_id, DateTime? Date_Now, long Ex_CountryConstrain_Id,
            List<int> Ex_CountryConstrain_ArrivalPortLst, List<string> device_Info)
        {
            try
            {
                Ex_CountryConstrain_ArrivalPort dto;
                foreach (var item in Ex_CountryConstrain_ArrivalPortLst)
                {
                    dto = new Ex_CountryConstrain_ArrivalPort();
                    dto.Port_International_Id = item;
                    dto.Ex_CountryConstrain_Id = Ex_CountryConstrain_Id;
                    dto.User_Creation_Date = (DateTime)Date_Now;
                    dto.User_Creation_Id = user_id;
                    dto.Id = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_ArrivalPort_seq");
                    uow.Repository<Ex_CountryConstrain_ArrivalPort>().InsertRecord(dto);
                    uow.SaveChanges();
                }
                //  return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Ex_CountryConstrain_ArrivalPortLst);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, device_Info);
                //   return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update_ActivationProcedure_Data(short countriesLstTransit, List<ProcedureDataToSendDTO> dataToSend, int IsPlant, List<string> Device_Info)
        {
            /*
             for each row insert a new  row with new activation
             item details
             save analysis, treatment
             */
            Ex_CountryConstrain x;//= new Ex_CountryConstrain();
            Ex_CountryConstrain_AnalysisLabType _AnalysisLabType;//= new Ex_CountryConstrain_AnalysisLabType();
            Ex_CountryConstrain_Treatment constrain_Treatment;//= new Ex_CountryConstrain_Treatment();
            //Con_Ex_Im_Plants con_Ex_Im_Plants;//= new Con_Ex_Im_Plants();
            //Con_Ex_Im_Products con_Ex_Im_Product = new Con_Ex_Im_Products();
            //Con_Ex_Im_LiableItems_Alive con_Ex_Im_alive = new Con_Ex_Im_LiableItems_Alive();
            //Con_Ex_Im_LiableItems_NotAlive con_Ex_Im_aliveNot = new Con_Ex_Im_LiableItems_NotAlive();

            PlantQuarantineEntities db = new PlantQuarantineEntities();

            foreach (var item in dataToSend)
            {
                var ex_CountryConstrain = db.Ex_CountryConstrain.AsNoTracking()
                    .Where(a => a.ID == item.id).FirstOrDefault();
                //insert new row
                //var ex_CountryConstrain = new Ex_CountryConstrain();
                //ex_CountryConstrain = Mapper.Map(x,ex_CountryConstrain);
                ex_CountryConstrain.IsActive = item.activenew;
                ex_CountryConstrain.TransportCountry_ID = countriesLstTransit;
                ex_CountryConstrain.User_Creation_Date = DateTime.Now;
                ex_CountryConstrain.User_Creation_Id = 1;

                ex_CountryConstrain.User_Updation_Date = null;
                ex_CountryConstrain.User_Updation_Id = null;
                ex_CountryConstrain.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
                //uow.Repository<Ex_CountryConstrain>().InsertRecord(ex_CountryConstrain);
                //uow.SaveChanges();
                db.Ex_CountryConstrain.Add(ex_CountryConstrain);
                db.SaveChanges();
                //if (ex_CountryConstrain.IsAnalysis)
                {
                    //save analysis  insert new row

                    _AnalysisLabType = db.Ex_CountryConstrain_AnalysisLabType.AsNoTracking().Where(a => a.CountryConstrain_ID == item.id).FirstOrDefault();

                    _AnalysisLabType.CountryConstrain_ID = ex_CountryConstrain.ID;
                    _AnalysisLabType.User_Creation_Date = DateTime.Now;
                    _AnalysisLabType.User_Creation_Id = 1;

                    _AnalysisLabType.User_Updation_Date = null;
                    _AnalysisLabType.User_Updation_Id = null;
                    _AnalysisLabType.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_seq");
                    uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(_AnalysisLabType);
                    uow.SaveChanges();
                }
                //if (ex_CountryConstrain.IsTreatment)
                {
                    //save analysis  insert new row

                    constrain_Treatment = db.Ex_CountryConstrain_Treatment.AsNoTracking().Where(a => a.CountryConstrain_ID == item.id).FirstOrDefault();

                    constrain_Treatment.CountryConstrain_ID = ex_CountryConstrain.ID;
                    constrain_Treatment.User_Creation_Date = DateTime.Now;
                    constrain_Treatment.User_Creation_Id = 1;

                    constrain_Treatment.User_Updation_Date = null;
                    constrain_Treatment.User_Updation_Id = null;
                    constrain_Treatment.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_seq");
                    uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(constrain_Treatment);
                    uow.SaveChanges();
                }

                //switch (IsPlant)
                //{
                //    case 4:
                //        {
                //            //plant
                //            con_Ex_Im_Plants = db.Con_Ex_Im_Plants.AsNoTracking().Where(a => a.Con_Ex_Im_ID == item.id).FirstOrDefault();
                //            con_Ex_Im_Plants.Con_Ex_Im_ID = ex_CountryConstrain.ID;
                //            con_Ex_Im_Plants.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Con_Ex_Im_Plants_seq");
                //            uow.Repository<Con_Ex_Im_Plants>().InsertRecord(con_Ex_Im_Plants);
                //            uow.SaveChanges();
                //            break;
                //        }

                //}
            }
            throw new NotImplementedException();
        }

        ///***************************sayed************************/
        public Dictionary<string, object> GetCustomConstrain_Plant
            (long Item_ShortName_id, long catId, int constrainType, int owner, List<string> Device_Info)
        {
            try
            {
                long? categoryId = null;
                if (catId > 0) categoryId = catId;

                //START HERE Get Constrains
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var constrain = new CustomCountryConstrain();
                constrain = (from cc in entities.Ex_CountryConstrain
                                 //join plant in entities.Con_Ex_Im_Plants on cc.ID equals plant.Con_Ex_Im_ID
                             where cc.CountryConstrain_Type == constrainType //1
                                                                             //&& cc.TransportCountry_ID == null
                                && cc.ConstrainOwner_ID == owner //75
                                && cc.IsExport == true
                                && cc.Item_ShortName_id == Item_ShortName_id //6
                                && cc.ItemCategories_ID == null// categoryId
                                && cc.IsActive == true && cc.User_Deletion_Id == null
                             select new CustomCountryConstrain
                             {
                                 ID = cc.ID,
                                 ownerTypeId = cc.CountryConstrain_Type,
                                 countryId = (short)((cc.CountryConstrain_Type == 2) ? owner : 0),
                                 unionId = (short)((cc.CountryConstrain_Type == 3) ? owner : 0),
                                 //IsAnalysis = cc.IsAnalysis,
                                 //IsTreatment = cc.IsTreatment,
                                 //28-6-2020 constrain updates

                                 //IsCertificate_Addtion_plant = (bool)cc.IsCertificate_Addtion,
                                 IsCompanyAccreditation_plant = (bool)cc.IsCompanyAccreditation,
                                 IsStationAccreditation_plant = (bool)cc.IsStationAccreditation,
                                 IsFarmAccreditation_plant = (bool)cc.IsFarmAccreditation,
                                 //emanarrival
                                 ArrivalPortList_plant = cc.Ex_CountryConstrain_ArrivalPort.
                           Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
                           .Select(z => z.Port_International.ID).ToList(),
                             }
                         ).FirstOrDefault();

                //eman
                long constrainID = (constrain != null ? constrain.ID : 0);
                if (constrainID != 0)
                {
                    var plants = (from cc in entities.Ex_CountryConstrain
                                  join isn in entities.Item_ShortName on cc.Item_ShortName_id equals isn.ID
                                  //join plant in entities.Con_Ex_Im_Plants on cc.ID equals plant.Con_Ex_Im_ID
                                  where cc.ID == constrainID
                                  select new plants
                                  {
                                      Item_ShortName_id = isn.ID,  //cc.ProdPlant_ID,
                                      ItemCategories_ID = cc.ItemCategories_ID,// plant.PlantCat_ID,
                                      IsCertificate_Addtion = (bool)cc.IsCertificate_Addtion,// PlantPartType_ID,


                                  }).FirstOrDefault();
                    constrain.plants = plants;
                }









                if (constrainID != 0)
                {
                    List<Ex_CountryConstrain_Text> plantTexts = uow.Repository<Ex_CountryConstrain_Text>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
                    constrain.plants.PlantConstrain_Rows = plantTexts.Select(x => new CustomPlantConstrain_Rows
                    {
                        countryConstraintext_Id = constrainID,
                        Id = x.ID,
                        ConstrainText_Ar = x.ConstrainText_Ar,
                        ConstrainText_En = x.ConstrainText_En,
                        InSide_Certificate_Ar = x.InSide_Certificate_Ar,
                        InSide_Certificate_En = x.InSide_Certificate_En,
                        IsCertificate_Addtion = (bool)x.IsCertificate_Addtion
                    }).ToList();
                    List<Ex_CountryConstrain_Treatment> plantTreatments = uow.Repository<Ex_CountryConstrain_Treatment>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
                    constrain.plants.PlantConstrain_Treatments = plantTreatments.Select(x => new CustomPlantConstrain_Treatments
                    {
                        TreatmentConstrain_ID = constrainID,
                        Id = x.ID,
                        TreatmentType_ID = x.TreatmentType_ID,
                        TreatmentMaterial_ID = (byte)x.TreatmentMaterial_ID,
                        TreatmentMethod = (byte)x.TreatmentMethod,
                        TheDose = (decimal)x.TheDose,
                        Exposure_Day = (int)x.Exposure_Day,
                        Exposure_Hour = (int)x.Exposure_Hour,
                        Exposure_Minute = (int)x.Exposure_Minute,
                        TreatmentMainType_ID = (byte)uow.Repository<TreatmentType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.TreatmentType_ID).Select(z => z.MainType_ID).FirstOrDefault()

                    }).ToList();
                    List<Ex_CountryConstrain_AnalysisLabType> plantAnalysis = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().GetData().Include(v => v.AnalysisLabType).Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
                    constrain.plants.PlantConstrain_Analysis = plantAnalysis.Select(x => new CustomPlantConstrain_Analysis
                    {
                        AnalysisConstrain_ID = constrainID,
                        Id = x.ID,
                        AnalysisLabTypeID = x.AnalysisLabTypeID,
                        AnalysisLab_ID = x.AnalysisLabTypeID,
                        AnalysisType_ID = (byte)uow.Repository<AnalysisType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisTypeID).Select(z => z.ID).FirstOrDefault(),
                        //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


                    }).ToList();
                    List<Ex_CountryConstrain_ArrivalPort> plantPorts = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData().Include(v => v.Port_International).Where(p => p.User_Deletion_Id == null && p.Ex_CountryConstrain_Id == constrainID).ToList();
                    constrain.plants.PlantConstrain_ArrivalPorts = plantPorts.Select(x => new CustomPlantConstrain_ArrivalPorts
                    {
                        ArrivalConstrain_ID = constrainID,
                        Id = x.Id,
                        PortInternationalID = x.Port_International_Id,

                        PortType_ID = (byte)uow.Repository<Port_Type>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.Port_International.PortTypeID).Select(z => z.ID).FirstOrDefault(),
                        //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


                    }).ToList();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrain);
                }
                uow.Repository<Object>().Save_Error(this.GetType().FullName, "noData", MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        ///***************************endsayed************************/

        //public short GetCountryByConstrainID(int Port_International_Id, List<string> Device_Info)
        //{
        //    string lang = Device_Info[2];

        //    short? Country_ID = uow.Repository<Port_International>().GetData().
        //        Where(x => x.ID == Port_International_Id).Select(x => x.Country_ID).FirstOrDefault();
        //    if (Country_ID == null)
        //        return 0;
        //    else
        //        return Country_ID.Value;
        //}

        ////GetPlantConstrain_Activation
        //public Dictionary<string, object> GetPlantConstrain_Activation
        //   (long plantId, byte purposeId, byte statusId, byte partType, long catId, int constrainType, int owner, List<string> Device_Info)
        //{
        //    try
        //    {
        //        long? categoryId = null;
        //        if (catId > 0) categoryId = catId;

        //        //START HERE Get Constrains
        //        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        //        var data = (
        //                   from cc in uow.Repository<Ex_CountryConstrain>().GetData()
        //                   join plant in uow.Repository<Con_Ex_Im_Plants>().GetData() on

        //                    cc.ID equals plant.Con_Ex_Im_ID
        //                   where cc.CountryConstrain_Type == constrainType
        //                                       && cc.ConstrainOwner_ID == owner && cc.IsExport == true
        //                                       && cc.IsPlant == 4 && cc.ProdPlant_ID == plantId
        //                                       && cc.User_Deletion_Id == null
        //                                        && cc.IsActive == true
        //                                       && plant.ItemType_ID == 36
        //                                       && plant.PlantCat_ID == categoryId
        //                                       && plant.PlantPartType_ID == partType
        //                                       && plant.ProductStatus_ID == statusId
        //                                       && plant.Purpose_ID == purposeId

        //                   select new Ex_CountryConstrainDTO
        //                   {
        //                       ID = cc.ID,
        //                       //28-6-2020 constrain updates

        //                       //ConstrainText_Ar = cc.ConstrainText_Ar,
        //                       //ConstrainText_En = cc.ConstrainText_En,
        //                       //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        //                       //InSide_Certificate_En = cc.InSide_Certificate_En,
        //                       //IsAnalysis = cc.IsAnalysis,
        //                       //IsTreatment = cc.IsTreatment,
        //                       IsActive = cc.IsActive,
        //                       //28-6-2020 constrain updates
        //                       //IsCertificate_Addtion = cc.IsCertificate_Addtion,
        //                       IsFarmAccreditation = cc.IsFarmAccreditation,
        //                       IsCompanyAccreditation = cc.IsCompanyAccreditation,
        //                       IsStationAccreditation = cc.IsStationAccreditation,

        //                       ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        //                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        //                       .Select(z => z.Port_International.ID).ToList(),

        //                       CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        //                       (c => c.Port_International.Country_ID).FirstOrDefault()

        //                   }
        //                   ).ToList();
        //        //eman
        //        var datareturn = data.OrderBy(o => o.ID).Take(1).ToList();
        //        foreach (var cc in datareturn)
        //        {
        //            //cc.ConstrainText_Ar = "1-" + cc.ConstrainText_Ar + Environment.NewLine ;
        //            //cc.ConstrainText_En = "1-" + cc.ConstrainText_En + Environment.NewLine ;
        //            var i = 1;
        //            //cc.ConstrainText_Ar = "";
        //            //cc.ConstrainText_En = "";
        //            var con_ar = "";
        //            var con_en = "";
        //            //foreach (var dd in data)
        //            //{

        //            //    con_ar += i + "-" + dd.ConstrainText_Ar + Environment.NewLine;
        //            //    con_en += i + "-" + dd.ConstrainText_En + Environment.NewLine;
        //            //    i++;
        //            //}
        //            //cc.ConstrainText_Ar = con_ar;
        //            //cc.ConstrainText_En = con_en;
        //        }

        //        // var dataDTO = new List<Ex_CountryConstrainDTO>();

        //        //dataDTO.Add(data);

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}


        /////***************************************************/
        //////GetCustomConstrain_Product
        ////public Dictionary<string, object> GetCustomConstrain_Product
        ////    (long productId, byte purposeId, byte statusId, int constrainType, int owner, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join product in entities.Con_Ex_Im_Products on cc.ID equals product.Con_Ex_Im_ID
        ////                         where cc.CountryConstrain_Type == constrainType
        ////                         &&cc.TransportCountry_ID == null
        ////                            && cc.ConstrainOwner_ID == owner && cc.IsExport == true
        ////                            && cc.IsPlant == 5 && cc.ProdPlant_ID == productId
        ////                            && cc.IsActive == true && cc.User_Deletion_Id == null
        ////                            && product.ItemType_ID == 36
        ////                            && product.ProductStatus_ID == statusId
        ////                            && product.Purpose_ID == purposeId
        ////                         select new CustomCountryConstrain
        ////                         {
        ////                             ID = cc.ID,
        ////                             ownerTypeId = cc.CountryConstrain_Type,
        ////                             countryId = (short)((cc.CountryConstrain_Type == 2) ? owner : 0),
        ////                             unionId = (short)((cc.CountryConstrain_Type == 3) ? owner : 0),
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             //28-6-2020 constrain updates

        ////                             //IsCertificate_Addtion_product = (bool)cc.IsCertificate_Addtion,
        ////                             IsCompanyAccreditation_product = (bool)cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation_product = (bool)cc.IsStationAccreditation,
        ////                             IsFarmAccreditation_product = (bool)cc.IsFarmAccreditation,

        ////                         }
        ////                   ).FirstOrDefault();
        ////        //eman
        ////        long constrainID = (constrain != null ? constrain.ID : 0);

        ////        var products = (from cc in entities.Ex_CountryConstrain
        ////                        join product in entities.Con_Ex_Im_Products on cc.ID equals product.Con_Ex_Im_ID
        ////                        where cc.ID == constrainID
        ////                        && product.ItemType_ID == 36
        ////                        && product.ProductStatus_ID == statusId
        ////                        && product.Purpose_ID == purposeId

        ////                        select new products
        ////                        {
        ////                            ProductId = cc.ProdPlant_ID,
        ////                            purposeId = (byte)product.Purpose_ID,
        ////                            statusId = (byte)product.ProductStatus_ID,

        ////                        }).FirstOrDefault();
        ////        constrain.products = products;
        ////        List<Ex_CountryConstrain_Text> productTexts = uow.Repository<Ex_CountryConstrain_Text>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.products.ProductConstrain_Rows = productTexts.Select(x => new CustomPlantConstrain_Rows
        ////        {
        ////            countryConstraintext_Id = constrainID,
        ////            Id = x.ID,
        ////            ConstrainText_Ar = x.ConstrainText_Ar,
        ////            ConstrainText_En = x.ConstrainText_En,
        ////            InSide_Certificate_Ar = x.InSide_Certificate_Ar,
        ////            InSide_Certificate_En = x.InSide_Certificate_En,
        ////            IsCertificate_Addtion = (bool)x.IsCertificate_Addtion
        ////        }).ToList();
        ////        List<Ex_CountryConstrain_Treatment> productTreatments = uow.Repository<Ex_CountryConstrain_Treatment>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.products.ProductConstrain_Treatments = productTreatments.Select(x => new CustomPlantConstrain_Treatments
        ////        {
        ////            TreatmentConstrain_ID = constrainID,
        ////            Id = x.ID,
        ////            TreatmentType_ID = x.TreatmentType_ID,
        ////            TreatmentMaterial_ID = (byte)x.TreatmentMaterial_ID,
        ////            TreatmentMethod = (byte)x.TreatmentMethod,
        ////            TheDose = (decimal)x.TheDose,
        ////            Exposure_Day = (int)x.Exposure_Day,
        ////            Exposure_Hour = (int)x.Exposure_Hour,
        ////            Exposure_Minute = (int)x.Exposure_Minute,
        ////            TreatmentMainType_ID = (byte)uow.Repository<TreatmentType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.TreatmentType_ID).Select(z => z.MainType_ID).FirstOrDefault()

        ////        }).ToList();
        ////        List<Ex_CountryConstrain_AnalysisLabType> productAnalysis = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().GetData().Include(v => v.AnalysisLabType).Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.products.ProductConstrain_Analysis = productAnalysis.Select(x => new CustomPlantConstrain_Analysis
        ////        {
        ////            AnalysisConstrain_ID = constrainID,
        ////            Id = x.ID,
        ////            AnalysisLabTypeID = x.AnalysisLabTypeID,
        ////            AnalysisLab_ID = x.AnalysisLabTypeID,
        ////            AnalysisType_ID = (byte)uow.Repository<AnalysisType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisTypeID).Select(z => z.ID).FirstOrDefault(),
        ////            //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


        ////        }).ToList();
        ////        List<Ex_CountryConstrain_ArrivalPort> productPorts = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData().Include(v => v.Port_International).Where(p => p.User_Deletion_Id == null && p.Ex_CountryConstrain_Id == constrainID).ToList();
        ////        constrain.products.ProductConstrain_ArrivalPorts = productPorts.Select(x => new CustomPlantConstrain_ArrivalPorts
        ////        {
        ////            ArrivalConstrain_ID = constrainID,
        ////            Id = x.Id,
        ////            PortInternationalID = x.Port_International_Id,

        ////            PortType_ID = (byte)uow.Repository<Port_Type>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.Port_International.PortTypeID).Select(z => z.ID).FirstOrDefault(),
        ////            //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


        ////        }).ToList();



        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrain);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}

        //////GetConstrain_Product_Activation
        ////public Dictionary<string, object> GetConstrain_Product_Activation
        ////    (long productId, byte purposeId, byte statusId, int constrainType, int owner, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join product in entities.Con_Ex_Im_Products on cc.ID equals product.Con_Ex_Im_ID
        ////                         where cc.CountryConstrain_Type == constrainType
        ////                            && cc.ConstrainOwner_ID == owner && cc.IsExport == true
        ////                            && cc.IsPlant == 5 && cc.ProdPlant_ID == productId
        ////                            && cc.User_Deletion_Id == null
        ////                           && cc.IsActive == true
        ////                           && product.ItemType_ID == 36
        ////                            && product.ProductStatus_ID == statusId
        ////                            && product.Purpose_ID == purposeId
        ////                         select new Ex_CountryConstrainDTO
        ////                         {
        ////                             ID = cc.ID,
        ////                             //28-6-2020 constrain updates

        ////                             //ConstrainText_Ar = cc.ConstrainText_Ar,
        ////                             //ConstrainText_En = cc.ConstrainText_En,
        ////                             //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                             //InSide_Certificate_En = cc.InSide_Certificate_En,
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             IsActive = cc.IsActive,
        ////                             //28-6-2020 constrain updates

        ////                             //IsCertificate_Addtion = cc.IsCertificate_Addtion,
        ////                             IsFarmAccreditation = cc.IsFarmAccreditation,
        ////                             IsCompanyAccreditation = cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation = cc.IsStationAccreditation,

        ////                             ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       .Select(z => z.Port_International.ID).ToList(),

        ////                             CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        ////                       (c => c.Port_International.Country_ID).FirstOrDefault()
        ////                         }
        ////                   ).ToList();
        ////        //eman
        ////        var datareturn = constrain.OrderBy(o => o.ID).Take(1).ToList();
        ////        foreach (var cc in datareturn)
        ////        {
        ////            //cc.ConstrainText_Ar = "1-" + cc.ConstrainText_Ar + Environment.NewLine ;
        ////            //cc.ConstrainText_En = "1-" + cc.ConstrainText_En + Environment.NewLine ;
        ////            var i = 1;
        ////            //cc.ConstrainText_Ar = "";
        ////            //cc.ConstrainText_En = "";
        ////            var con_ar = "";
        ////            var con_en = "";
        ////            //foreach (var dd in constrain)
        ////            //{

        ////            //    con_ar += i + "-" + dd.ConstrainText_Ar + Environment.NewLine;
        ////            //    con_en += i + "-" + dd.ConstrainText_En + Environment.NewLine;
        ////            //    i++;
        ////            //}
        ////            //cc.ConstrainText_Ar = con_ar;
        ////            //cc.ConstrainText_En = con_en;
        ////        }

        ////        // var dataDTO = new List<Ex_CountryConstrainDTO>();

        ////        //dataDTO.Add(data);

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}
        /////***************************************************/
        ////public Dictionary<string, object> GetCustomConstrain_LiablAlive
        ////   (long aliveLiableId, byte purposeId, int statusId, int phaseId, int constrainType, int owner, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join alive in entities.Con_Ex_Im_LiableItems_Alive on cc.ID equals alive.Con_Ex_Im_ID
        ////                         where cc.CountryConstrain_Type == constrainType
        ////                         &&cc.TransportCountry_ID ==null
        ////                            && cc.ConstrainOwner_ID == owner && cc.IsExport == true
        ////                            && cc.IsPlant == 16 && cc.ProdPlant_ID == aliveLiableId
        ////                            && cc.IsActive == true && cc.User_Deletion_Id == null
        ////                            && alive.ItemType_ID == 36
        ////                            && alive.LiableStatus_ID == statusId
        ////                            && alive.Purpose_ID == purposeId
        ////                            && alive.BiologicalPhase_ID == phaseId
        ////                         select new CustomCountryConstrain
        ////                         {
        ////                             ID = cc.ID,
        ////                             ownerTypeId = cc.CountryConstrain_Type,
        ////                             countryId = (short)((cc.CountryConstrain_Type == 2) ? owner : 0),
        ////                             unionId = (short)((cc.CountryConstrain_Type == 3) ? owner : 0),
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             //28-6-2020 constrain updates
        ////                             //IsCertificate_Addtion_live = (bool)cc.IsCertificate_Addtion,
        ////                             IsCompanyAccreditation_live = (bool)cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation_live = (bool)cc.IsStationAccreditation,
        ////                             IsFarmAccreditation_live = (bool)cc.IsFarmAccreditation,
        ////                             //emanarrival
        ////                       //      ArrivalPortList_live = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       //Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       //.Select(z => z.Port_International.ID).ToList(),
        ////                        }
        ////                   ).FirstOrDefault();


        ////        long constrainID = (constrain != null ? constrain.ID : 0);

        ////        var alives = (from cc in entities.Ex_CountryConstrain
        ////                          join alive in entities.Con_Ex_Im_LiableItems_Alive on cc.ID equals alive.Con_Ex_Im_ID
        ////                          where cc.ID == constrainID
        ////                          && alive.ItemType_ID == 36
        ////                          && alive.LiableStatus_ID == statusId
        ////                          && alive.Purpose_ID == purposeId
        ////                          && alive.BiologicalPhase_ID == phaseId

        ////                          select new Alives
        ////                          {

        ////                              aliveId = cc.ProdPlant_ID,
        ////                              purposeId = (byte)alive.Purpose_ID,
        ////                              statusId = alive.LiableStatus_ID,
        ////                              biologicalPhaseId = (int)alive.BiologicalPhase_ID,
        ////                              //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                              //InSide_Certificate_En = cc.InSide_Certificate_En
        ////                          }).FirstOrDefault();


        ////        constrain.Alives = alives;
        ////        List<Ex_CountryConstrain_Text> AlivesTexts = uow.Repository<Ex_CountryConstrain_Text>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.Alives.AliveConstrain_Rows = AlivesTexts.Select(x => new CustomPlantConstrain_Rows
        ////        {
        ////            countryConstraintext_Id = constrainID,
        ////            Id = x.ID,
        ////            ConstrainText_Ar = x.ConstrainText_Ar,
        ////            ConstrainText_En = x.ConstrainText_En,
        ////            InSide_Certificate_Ar = x.InSide_Certificate_Ar,
        ////            InSide_Certificate_En = x.InSide_Certificate_En,
        ////            IsCertificate_Addtion = (bool)x.IsCertificate_Addtion
        ////        }).ToList();
        ////        List<Ex_CountryConstrain_Treatment> AlivesTreatments = uow.Repository<Ex_CountryConstrain_Treatment>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.Alives.AliveConstrain_Treatments = AlivesTreatments.Select(x => new CustomPlantConstrain_Treatments
        ////        {
        ////            TreatmentConstrain_ID = constrainID,
        ////            Id = x.ID,
        ////            TreatmentType_ID = x.TreatmentType_ID,
        ////            TreatmentMaterial_ID = (byte)x.TreatmentMaterial_ID,
        ////            TreatmentMethod = (byte)x.TreatmentMethod,
        ////            TheDose = (decimal)x.TheDose,
        ////            Exposure_Day = (int)x.Exposure_Day,
        ////            Exposure_Hour = (int)x.Exposure_Hour,
        ////            Exposure_Minute = (int)x.Exposure_Minute,
        ////            TreatmentMainType_ID = (byte)uow.Repository<TreatmentType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.TreatmentType_ID).Select(z => z.MainType_ID).FirstOrDefault()

        ////        }).ToList();
        ////        List<Ex_CountryConstrain_AnalysisLabType> AlivesAnalysis = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().GetData().Include(v => v.AnalysisLabType).Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.Alives.AliveConstrain_Analysis = AlivesAnalysis.Select(x => new CustomPlantConstrain_Analysis
        ////        {
        ////            AnalysisConstrain_ID = constrainID,
        ////            Id = x.ID,
        ////            AnalysisLabTypeID = x.AnalysisLabTypeID,
        ////            AnalysisLab_ID = x.AnalysisLabTypeID,
        ////            AnalysisType_ID = (byte)uow.Repository<AnalysisType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisTypeID).Select(z => z.ID).FirstOrDefault(),
        ////            //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


        ////        }).ToList();
        ////        List<Ex_CountryConstrain_ArrivalPort> AlivesPorts = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData().Include(v => v.Port_International).Where(p => p.User_Deletion_Id == null && p.Ex_CountryConstrain_Id == constrainID).ToList();
        ////        constrain.Alives.AliveConstrain_ArrivalPorts = AlivesPorts.Select(x => new CustomPlantConstrain_ArrivalPorts
        ////        {
        ////            ArrivalConstrain_ID = constrainID,
        ////            Id = x.Id,
        ////            PortInternationalID = x.Port_International_Id,

        ////            PortType_ID = (byte)uow.Repository<Port_Type>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.Port_International.PortTypeID).Select(z => z.ID).FirstOrDefault(),
        ////            //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


        ////        }).ToList();


        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrain);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}

        ////public Dictionary<string, object> GetConstrain_LiablAlive_Activation(long aliveLiableId, byte purposeId, int statusId, int phaseId, int constrainType, int owner, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join alive in entities.Con_Ex_Im_LiableItems_Alive on cc.ID equals alive.Con_Ex_Im_ID
        ////                         where cc.CountryConstrain_Type == constrainType
        ////                         &&cc.TransportCountry_ID ==null
        ////                            && cc.ConstrainOwner_ID == owner && cc.IsExport == true
        ////                            && cc.IsPlant == 16 && cc.ProdPlant_ID == aliveLiableId
        ////                            && cc.User_Deletion_Id == null
        ////                              && cc.IsActive == true
        ////                            && alive.ItemType_ID == 36
        ////                            && alive.LiableStatus_ID == statusId
        ////                            && alive.Purpose_ID == purposeId
        ////                            && alive.BiologicalPhase_ID == phaseId
        ////                         select new Ex_CountryConstrainDTO
        ////                         {
        ////                             ID = cc.ID,
        ////                             //28-6-2020 constrain updates

        ////                             //ConstrainText_Ar = cc.ConstrainText_Ar,
        ////                             //ConstrainText_En = cc.ConstrainText_En,
        ////                             //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                             //InSide_Certificate_En = cc.InSide_Certificate_En,
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             IsActive = cc.IsActive,

        ////                             //28-6-2020 constrain updates

        ////                             // IsCertificate_Addtion = cc.IsCertificate_Addtion,
        ////                             IsFarmAccreditation = cc.IsFarmAccreditation,
        ////                             IsCompanyAccreditation = cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation = cc.IsStationAccreditation,

        ////                             ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       .Select(z => z.Port_International.ID).ToList(),

        ////                             CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        ////                       (c => c.Port_International.Country_ID).FirstOrDefault()
        ////                         }
        ////                   ).ToList();
        ////        //eman
        ////        var datareturn = constrain.OrderBy(o => o.ID).Take(1).ToList();
        ////        foreach (var cc in datareturn)
        ////        {
        ////            //cc.ConstrainText_Ar = "1-" + cc.ConstrainText_Ar + Environment.NewLine ;
        ////            //cc.ConstrainText_En = "1-" + cc.ConstrainText_En + Environment.NewLine ;
        ////            var i = 1;
        ////            //cc.ConstrainText_Ar = "";
        ////            //cc.ConstrainText_En = "";
        ////            var con_ar = "";
        ////            var con_en = "";
        ////            //foreach (var dd in constrain)
        ////            //{

        ////            //    con_ar += i + "-" + dd.ConstrainText_Ar + Environment.NewLine;
        ////            //    con_en += i + "-" + dd.ConstrainText_En + Environment.NewLine;
        ////            //    i++;
        ////            //}
        ////            //cc.ConstrainText_Ar = con_ar;
        ////            //cc.ConstrainText_En = con_en;
        ////        }

        ////        // var dataDTO = new List<Ex_CountryConstrainDTO>();

        ////        //dataDTO.Add(data);

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}

        /////***************************************************/
        ////public Dictionary<string, object> GetCustomConstrain_LiableNotAlive
        ////    (long notAliveLiableId, byte purposeId, int statusId, int constrainType, int owner, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join notAlive in entities.Con_Ex_Im_LiableItems_NotAlive on cc.ID equals notAlive.Con_Ex_Im_ID
        ////                         where cc.CountryConstrain_Type == constrainType
        ////                         &&cc.TransportCountry_ID == null
        ////                            && cc.ConstrainOwner_ID == owner && cc.IsExport == true
        ////                            && cc.IsPlant == 33 && cc.ProdPlant_ID == notAliveLiableId
        ////                            && cc.IsActive == true && cc.User_Deletion_Id == null
        ////                            && notAlive.ItemType_ID == 36
        ////                            && notAlive.LiableStatus_ID == statusId
        ////                            && notAlive.Purpose_ID == purposeId
        ////                         select new CustomCountryConstrain
        ////                         {
        ////                             ID = cc.ID,
        ////                             ownerTypeId = cc.CountryConstrain_Type,
        ////                             countryId = (short)((cc.CountryConstrain_Type == 2) ? owner : 0),
        ////                             unionId = (short)((cc.CountryConstrain_Type == 3) ? owner : 0),
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             //28-6-2020 constrain updates

        ////                             //IsCertificate_Addtion_notlive = (bool)cc.IsCertificate_Addtion,
        ////                             IsCompanyAccreditation_notlive = (bool)cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation_notlive = (bool)cc.IsStationAccreditation,
        ////                             IsFarmAccreditation_notlive = (bool)cc.IsFarmAccreditation,
        ////                             //emanarrival
        ////                             //      ArrivalPortList_notlive = cc.Ex_CountryConstrain_ArrivalPort.
        ////                             //Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                             //.Select(z => z.Port_International.ID).ToList(),
        ////                         }
        ////                   ).FirstOrDefault();

        ////        long constrainID = (constrain != null ? constrain.ID : 0);

        ////        var notAlives = (from cc in entities.Ex_CountryConstrain
        ////                         join notAlive in entities.Con_Ex_Im_LiableItems_NotAlive on cc.ID equals notAlive.Con_Ex_Im_ID
        ////                         where cc.ID == constrainID
        ////                         && notAlive.ItemType_ID == 36
        ////                         && notAlive.LiableStatus_ID == statusId
        ////                         && notAlive.Purpose_ID == purposeId

        ////                         select new NotAlives
        ////                         {

        ////                             notAliveId = cc.ProdPlant_ID,
        ////                             purposeId = (byte)notAlive.Purpose_ID,
        ////                             statusId = notAlive.LiableStatus_ID,
        ////                             //28-6-2020 constrain updates

        ////                             //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                             //InSide_Certificate_En = cc.InSide_Certificate_En
        ////                         }).FirstOrDefault();
        ////        constrain.NotAlives = notAlives;

        ////        List<Ex_CountryConstrain_Text> NotAlivesTexts = uow.Repository<Ex_CountryConstrain_Text>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.NotAlives.NotAliveConstrain_Rows = NotAlivesTexts.Select(x => new CustomPlantConstrain_Rows
        ////        {
        ////            countryConstraintext_Id = constrainID,
        ////            Id = x.ID,
        ////            ConstrainText_Ar = x.ConstrainText_Ar,
        ////            ConstrainText_En = x.ConstrainText_En,
        ////            InSide_Certificate_Ar = x.InSide_Certificate_Ar,
        ////            InSide_Certificate_En = x.InSide_Certificate_En,
        ////            IsCertificate_Addtion = (bool)x.IsCertificate_Addtion
        ////        }).ToList();
        ////        List<Ex_CountryConstrain_Treatment> NotAlivesTreatments = uow.Repository<Ex_CountryConstrain_Treatment>().GetData().Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.NotAlives.NotAliveConstrain_Treatments = NotAlivesTreatments.Select(x => new CustomPlantConstrain_Treatments
        ////        {
        ////            TreatmentConstrain_ID = constrainID,
        ////            Id = x.ID,
        ////            TreatmentType_ID = x.TreatmentType_ID,
        ////            TreatmentMaterial_ID = (byte)x.TreatmentMaterial_ID,
        ////            TreatmentMethod = (byte)x.TreatmentMethod,
        ////            TheDose = (decimal)x.TheDose,
        ////            Exposure_Day = (int)x.Exposure_Day,
        ////            Exposure_Hour = (int)x.Exposure_Hour,
        ////            Exposure_Minute = (int)x.Exposure_Minute,
        ////            TreatmentMainType_ID = (byte)uow.Repository<TreatmentType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.TreatmentType_ID).Select(z => z.MainType_ID).FirstOrDefault()

        ////        }).ToList();
        ////        List<Ex_CountryConstrain_AnalysisLabType> NotAlivesAnalysis = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().GetData().Include(v => v.AnalysisLabType).Where(p => p.User_Deletion_Id == null && p.CountryConstrain_ID == constrainID).ToList();
        ////        constrain.NotAlives.NotAliveConstrain_Analysis = NotAlivesAnalysis.Select(x => new CustomPlantConstrain_Analysis
        ////        {
        ////            AnalysisConstrain_ID = constrainID,
        ////            Id = x.ID,
        ////            AnalysisLabTypeID = x.AnalysisLabTypeID,
        ////            AnalysisLab_ID = x.AnalysisLabTypeID,
        ////            AnalysisType_ID = (byte)uow.Repository<AnalysisType>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisTypeID).Select(z => z.ID).FirstOrDefault(),
        ////            //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


        ////        }).ToList();
        ////        List<Ex_CountryConstrain_ArrivalPort> NotAlivesPorts = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData().Include(v => v.Port_International).Where(p => p.User_Deletion_Id == null && p.Ex_CountryConstrain_Id == constrainID).ToList();
        ////        constrain.NotAlives.NotAliveConstrain_ArrivalPorts = NotAlivesPorts.Select(x => new CustomPlantConstrain_ArrivalPorts
        ////        {
        ////            ArrivalConstrain_ID = constrainID,
        ////            Id = x.Id,
        ////            PortInternationalID = x.Port_International_Id,

        ////            PortType_ID = (byte)uow.Repository<Port_Type>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.Port_International.PortTypeID).Select(z => z.ID).FirstOrDefault(),
        ////            //AnalysisLab_ID = (byte)uow.Repository<AnalysisLab>().GetData().Where(z => z.User_Deletion_Id == null && z.ID == x.AnalysisLabType.AnalysisLabID).Select(z => z.ID).FirstOrDefault()


        ////        }).ToList();




        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrain);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}

        ////public Dictionary<string, object> GetConstrain_LiableNotAlive_Activation(long notAliveLiableId, byte purposeId, int statusId, int constrainType, int owner, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join notAlive in entities.Con_Ex_Im_LiableItems_NotAlive on cc.ID equals notAlive.Con_Ex_Im_ID
        ////                         where cc.CountryConstrain_Type == constrainType
        ////                            && cc.ConstrainOwner_ID == owner && cc.IsExport == true
        ////                            && cc.IsPlant == 33 && cc.ProdPlant_ID == notAliveLiableId
        ////                            && cc.User_Deletion_Id == null
        ////                            && cc.IsActive == true
        ////                            && notAlive.ItemType_ID == 36
        ////                            && notAlive.LiableStatus_ID == statusId
        ////                            && notAlive.Purpose_ID == purposeId
        ////                         select new Ex_CountryConstrainDTO
        ////                         {
        ////                             ID = cc.ID,
        ////                             //28-6-2020 constrain updates

        ////                             //ConstrainText_Ar = cc.ConstrainText_Ar,
        ////                             //ConstrainText_En = cc.ConstrainText_En,
        ////                             //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                             //InSide_Certificate_En = cc.InSide_Certificate_En,
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             IsActive = cc.IsActive,

        ////                             //28-6-2020 constrain updates

        ////                             //IsCertificate_Addtion = cc.IsCertificate_Addtion,
        ////                             IsFarmAccreditation = cc.IsFarmAccreditation,
        ////                             IsCompanyAccreditation = cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation = cc.IsStationAccreditation,

        ////                             ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       .Select(z => z.Port_International.ID).ToList(),

        ////                             CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        ////                       (c => c.Port_International.Country_ID).FirstOrDefault()
        ////                         }
        ////                   ).ToList();
        ////        //eman
        ////        var datareturn = constrain.OrderBy(o => o.ID).Take(1).ToList();
        ////        foreach (var cc in datareturn)
        ////        {
        ////            //cc.ConstrainText_Ar = "1-" + cc.ConstrainText_Ar + Environment.NewLine ;
        ////            //cc.ConstrainText_En = "1-" + cc.ConstrainText_En + Environment.NewLine ;
        ////            var i = 1;
        ////            //cc.ConstrainText_Ar = "";
        ////            //cc.ConstrainText_En = "";
        ////            var con_ar = "";
        ////            var con_en = "";
        ////            //foreach (var dd in constrain)
        ////            //{

        ////            //    con_ar += i + "-" + dd.ConstrainText_Ar + Environment.NewLine;
        ////            //    con_en += i + "-" + dd.ConstrainText_En + Environment.NewLine;
        ////            //    i++;
        ////            //}
        ////            //cc.ConstrainText_Ar = con_ar;
        ////            //cc.ConstrainText_En = con_en;
        ////        }

        ////        // var dataDTO = new List<Ex_CountryConstrainDTO>();

        ////        //dataDTO.Add(data);

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}
        //////*************************************************//
        ////public Dictionary<string, object> DeleteConstrains(List<DeleteParameters> deletedConstrains, List<string> list)
        ////{
        ////    try
        ////    {
        ////        foreach (var obj in deletedConstrains)
        ////        {
        ////            var Cmodel = uow.Repository<Ex_CountryConstrain>().Findobject(obj.id);
        ////            if (Cmodel != null)
        ////            {
        ////                Cmodel.User_Deletion_Date = obj._DateNow;
        ////                Cmodel.User_Deletion_Id = obj.Userid;
        ////                uow.Repository<Ex_CountryConstrain>().Update(Cmodel);
        ////                uow.SaveChanges();

        ////                //if (Cmodel.IsTreatment == true)
        ////                {
        ////                    PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////                    var treatment = (from t in entities.Ex_CountryConstrain_Treatment
        ////                                     where t.CountryConstrain_ID == obj.id
        ////                                     select t).FirstOrDefault();

        ////                    treatment.User_Deletion_Date = obj._DateNow;
        ////                    treatment.User_Deletion_Id = obj.Userid;
        ////                    uow.Repository<Ex_CountryConstrain_Treatment>().Update(treatment);
        ////                    uow.SaveChanges();
        ////                }
        ////               // if (Cmodel.IsAnalysis == true)
        ////                {
        ////                    PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////                    var analysis = (from t in entities.Ex_CountryConstrain_AnalysisLabType
        ////                                    where t.CountryConstrain_ID == obj.id
        ////                                    select t).FirstOrDefault();

        ////                    analysis.User_Deletion_Date = obj._DateNow;
        ////                    analysis.User_Deletion_Id = obj.Userid;
        ////                    uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Update(analysis);
        ////                    uow.SaveChanges();
        ////                }
        ////            }
        ////        }

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, deletedConstrains);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, list);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}

        public Dictionary<string, object> InsertCustomConstrain(

            ConstrainCountryDTO constrains,
            List<string> Device_Info)
        {
            try
            {
                #region comment
                //Ex_CountryConstrainDTO dtoConstrain = new Ex_CountryConstrainDTO();
                //dtoConstrain.CountryConstrain_Type = constrains.ownerTypeId;
                //dtoConstrain.Item_ShortName_id = constrains.Item_ShortName_id;
                //dtoConstrain.ItemCategories_ID = constrains.ItemCategories_ID;
                //dtoConstrain.IsExport = true;
                //dtoConstrain.IsActive = true;
                //dtoConstrain.User_Creation_Date = constrains.User_Creation_Date;
                //dtoConstrain.User_Creation_Id = constrains.User_Creation_Id;
                ////emank


                //if (constrains.ownerTypeId == 1)
                //{
                //    dtoConstrain.ConstrainOwner_ID = constrains.local;
                //}
                //else if (constrains.ownerTypeId == 2)
                //{
                //    dtoConstrain.ConstrainOwner_ID = constrains.countryId;
                //}
                //else if (constrains.ownerTypeId == 3)
                //{
                //    dtoConstrain.ConstrainOwner_ID = constrains.unionId;
                //}
                ////eman

                //////**************** Plants *************//
                //if (constrains.plants != null)
                //{

                //    dtoConstrain.ItemCategories_ID = constrains.ItemCategories_ID;
                //    dtoConstrain.Item_ShortName_id = constrains.Item_ShortName_id;
                //    dtoConstrain.IsFarmAccreditation = constrains.IsFarmAccreditation_plant;
                //    dtoConstrain.IsCompanyAccreditation = constrains.IsCompanyAccreditation_plant;
                //    dtoConstrain.IsStationAccreditation = constrains.IsStationAccreditation_plant;

                //    var newConstrain = Mapper.Map<Ex_CountryConstrain>(dtoConstrain);
                //    newConstrain.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
                //    newConstrain = uow.Repository<Ex_CountryConstrain>().InsertReturn(newConstrain);
                //    uow.SaveChanges();
                //    dtoConstrain.ID = newConstrain.ID;

                //    //Con_Ex_Im_PlantsDTO plantDto = new Con_Ex_Im_PlantsDTO();
                //    //plantDto.Con_Ex_Im_ID = dtoConstrain.ID;
                //    //plantDto.ItemType_ID = 36;
                //    //plantDto.Plant_ID = constrains.plants.PlantId;
                //    //plantDto.PlantCat_ID = (constrains.plants.PlantCatId > 0) ? constrains.plants.PlantCatId : null;
                //    //plantDto.PlantPartType_ID = constrains.plants.plantPartId;
                //    //plantDto.ProductStatus_ID = constrains.plants.statusId;
                //    //plantDto.Purpose_ID = constrains.plants.purposeId;

                //    //Con_Ex_Im_PlantsBll bll = new Con_Ex_Im_PlantsBll();
                //    //bll.Insert(plantDto, Device_Info);
                //    var disableDone = 0;
                //    foreach (var plant in constrains.plants.PlantConstrain_Rows)
                //    {
                //        //if (plant.IsAnalysis_IsTreatment == 1)
                //        //{
                //        //    dtoConstrain.IsAnalysis = true;
                //        //    dtoConstrain.IsTreatment = false;
                //        //}
                //        //else if (plant.IsAnalysis_IsTreatment == 2)
                //        //{
                //        //    dtoConstrain.IsTreatment = true;
                //        //    dtoConstrain.IsAnalysis = false;
                //        //}
                //        //else
                //        //{
                //        //    dtoConstrain.IsAnalysis = false;
                //        //    dtoConstrain.IsTreatment = false;
                //        //}

                //        //dtoConstrain.IsPlant = 4;
                //        //dtoConstrain.ProdPlant_ID = plant.PlantId;
                //        //emanarrival
                //        //if (plant.ArrivalPortList!=null && plant.ArrivalPortList.Count > 0)
                //        //{
                //        //    UpdateArrivalPortList(plant.ArrivalPortList, dtoConstrain.ID,
                //        //     dtoConstrain.User_Creation_Id, dtoConstrain.User_Creation_Date, Device_Info);
                //        //}

                //        //eman


                //        if (plant.countryConstraintext_Id > 0)
                //        {
                //            if (disableDone == 0)
                //            {
                //                DisableConstrain(plant.countryConstraintext_Id, constrains.User_Creation_Id, constrains.User_Creation_Date);
                //                disableDone++;
                //            }
                //            //dtoConstrain.ID = plant.countryConstrain_Id;

                //            //constrains.ID =
                //            //    UpdateCustomPlant(dtoConstrain, plant, constrains.User_Creation_Id, constrains.User_Creation_Date, Device_Info);
                //            plant.User_Creation_Date = constrains.User_Creation_Date;
                //            plant.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantText(dtoConstrain.ID, plant, Device_Info);
                //        }
                //        else
                //        {
                //            plant.User_Creation_Date = constrains.User_Creation_Date;
                //            plant.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantText(dtoConstrain.ID, plant, Device_Info);
                //        }


                //    }
                //    //InsertCustomPlantTreatment
                //    foreach (var treatment in constrains.plants.PlantConstrain_Treatments)
                //    {

                //        if (treatment.TreatmentConstrain_ID > 0)
                //        {
                //            if (disableDone == 0)
                //            {
                //                DisableConstrain(treatment.TreatmentConstrain_ID, constrains.User_Creation_Id, constrains.User_Creation_Date);
                //                disableDone++;
                //            }
                //            treatment.User_Creation_Date = constrains.User_Creation_Date;
                //            treatment.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantTreatment(dtoConstrain.ID, treatment, Device_Info);
                //        }
                //        else
                //        {
                //            treatment.User_Creation_Date = constrains.User_Creation_Date;
                //            treatment.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantTreatment(dtoConstrain.ID, treatment, Device_Info);
                //        }


                //        //treatment.User_Creation_Date = constrains.User_Creation_Date;
                //        //treatment.User_Creation_Id = constrains.User_Creation_Id;

                //        //    constrains.ID = InsertCustomPlantTreatment(dtoConstrain.ID, treatment, Device_Info);


                //    }
                //    //insert analysis
                //    foreach (var analysis in constrains.plants.PlantConstrain_Analysis)
                //    {

                //        if (analysis.AnalysisConstrain_ID > 0)
                //        {
                //            if (disableDone == 0)
                //            {
                //                DisableConstrain(analysis.AnalysisConstrain_ID, constrains.User_Creation_Id, constrains.User_Creation_Date);
                //                disableDone++;
                //            }
                //            analysis.User_Creation_Date = constrains.User_Creation_Date;
                //            analysis.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantAnalysis(dtoConstrain.ID, analysis, Device_Info);
                //        }
                //        else
                //        {
                //            analysis.User_Creation_Date = constrains.User_Creation_Date;
                //            analysis.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantAnalysis(dtoConstrain.ID, analysis, Device_Info);
                //        }




                //    }
                //    //insert Arrival
                //    foreach (var port in constrains.plants.PlantConstrain_ArrivalPorts)
                //    {

                //        if (port.ArrivalConstrain_ID > 0)
                //        {
                //            if (disableDone == 0)
                //            {
                //                DisableConstrain(port.ArrivalConstrain_ID, constrains.User_Creation_Id, constrains.User_Creation_Date);
                //                disableDone++;
                //            }
                //            port.User_Creation_Date = constrains.User_Creation_Date;
                //            port.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantPort(dtoConstrain.ID, port, Device_Info);
                //        }
                //        else
                //        {
                //            port.User_Creation_Date = constrains.User_Creation_Date;
                //            port.User_Creation_Id = constrains.User_Creation_Id;

                //            constrains.ID = InsertCustomPlantPort(dtoConstrain.ID, port, Device_Info);
                //        }




                //    }

                //}
                #endregion


                if (constrains.CountryConstrainsDTO.ID != 0)
                {
                    Ex_CountryConstrain dtoConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(constrains.CountryConstrainsDTO.ID);
                    dtoConstrain.IsActive = false;
                    dtoConstrain.User_Updation_Id = constrains.CountryConstrainsDTO.User_Creation_Id;
                    dtoConstrain.User_Updation_Date = DateTime.Now;


                    uow.Repository<Ex_CountryConstrain>().Update(dtoConstrain);
                    uow.SaveChanges();

                }
                var newConstrain = Mapper.Map<Ex_CountryConstrain>(constrains.CountryConstrainsDTO);
                newConstrain.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
                newConstrain.IsExport = true;
                newConstrain.IsActive = true;
                if (constrains.CountryConstrainsDTO.TransportCountry_ID != null || constrains.CountryConstrainsDTO.TransportCountry_ID != 0)
                {
                    newConstrain.TransportCountry_ID = constrains.CountryConstrainsDTO.TransportCountry_ID;
                }
                newConstrain = uow.Repository<Ex_CountryConstrain>()
                 .InsertReturn(newConstrain);
                // insert to db
                uow.SaveChanges();
                //text
                if (constrains.CountryConstrain_TextDTO != null)
                    for (int i = 0; i < constrains.CountryConstrain_TextDTO.Count; i++)
                    {


                        var newConstrainText = Mapper.Map<Ex_CountryConstrain_Text>
                            (constrains.CountryConstrain_TextDTO[i]);


                        newConstrainText.ID = uow.Repository<Object>()
                       .GetNextSequenceValue_Long("Ex_CountryConstrain_Text_SEQ");
                        newConstrainText.IsActive = true;
                        newConstrainText.User_Creation_Date = newConstrain.User_Creation_Date;
                        newConstrainText.User_Creation_Id = newConstrain.User_Creation_Id;
                        newConstrainText.CountryConstrain_ID = newConstrain.ID;

                        newConstrainText = uow.Repository<Ex_CountryConstrain_Text>()
                         .InsertReturn(newConstrainText);


                        uow.SaveChanges();



                    }
                if (constrains.AnalysisLabType != null)
                    // labType
                    for (int i = 0; i < constrains.AnalysisLabType.Count; i++)
                    {


                        var newConstrainAnalysisLabType = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>
                            (constrains.AnalysisLabType[i]);


                        newConstrainAnalysisLabType.ID = uow.Repository<Object>()
                       .GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_SEQ");

                        newConstrainAnalysisLabType.User_Creation_Date = newConstrain.User_Creation_Date;
                        newConstrainAnalysisLabType.User_Creation_Id = newConstrain.User_Creation_Id;
                        newConstrainAnalysisLabType.CountryConstrain_ID = newConstrain.ID;
                        //newConstrainAnalysisLabType. = true;


                        newConstrainAnalysisLabType = uow.Repository<Ex_CountryConstrain_AnalysisLabType>()
                         .InsertReturn(newConstrainAnalysisLabType);


                        uow.SaveChanges();



                    }
                if (constrains.ConstraintAirPortInternational != null)
                {
                    for (int i = 0; i < constrains.ConstraintAirPortInternational.Count; i++)
                    {


                        var newConstrainAirPort = Mapper.Map<Ex_CountryConstrain_ArrivalPort>
                            (constrains.ConstraintAirPortInternational[i]);


                        newConstrainAirPort.Id = uow.Repository<Object>()
                       .GetNextSequenceValue_Long("Ex_CountryConstrain_ArrivalPort_SEQ");

                        newConstrainAirPort.User_Creation_Date = newConstrain.User_Creation_Date;
                        newConstrainAirPort.User_Creation_Id = newConstrain.User_Creation_Id;
                        newConstrainAirPort.Ex_CountryConstrain_Id = newConstrain.ID;

                        newConstrainAirPort = uow.Repository<Ex_CountryConstrain_ArrivalPort>()
                         .InsertReturn(newConstrainAirPort);


                        uow.SaveChanges();



                    }

                }




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                    constrains.CountryConstrainsDTO.ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }




        // insert yes only 
        public Dictionary<string, object> InsertCustomConstrainPro(

           ConstrainCountryDTO constrains,
           List<string> Device_Info)
        {
            try
            {



                updateAny(constrains.CountryConstrainsDTO.ConstrainOwner_ID,
                    constrains.CountryConstrainsDTO.TransportCountry_ID, constrains.CountryConstrainsDTO.User_Creation_Id);


                var newConstrain = Mapper.Map<Ex_CountryConstrain>(constrains.CountryConstrainsDTO);
                newConstrain.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
                newConstrain.IsExport = true;
                newConstrain.IsActive = true;
                if (constrains.CountryConstrainsDTO.TransportCountry_ID != null || constrains.CountryConstrainsDTO.TransportCountry_ID != 0)
                {
                    newConstrain.TransportCountry_ID = constrains.CountryConstrainsDTO.TransportCountry_ID;
                }
                newConstrain = uow.Repository<Ex_CountryConstrain>()
                 .InsertReturn(newConstrain);
                // insert to db
                uow.SaveChanges();
                //text
                if (constrains.CountryConstrain_TextDTO != null)
                    for (int i = 0; i < constrains.CountryConstrain_TextDTO.Count; i++)
                    {


                        var newConstrainText = Mapper.Map<Ex_CountryConstrain_Text>
                            (constrains.CountryConstrain_TextDTO[i]);
                        newConstrainText.Parent_ID = constrains.CountryConstrain_TextDTO[i].Parent_ID;


                        newConstrainText.ID = uow.Repository<Object>()
                       .GetNextSequenceValue_Long("Ex_CountryConstrain_Text_SEQ");
                        newConstrainText.IsActive = true;

                        newConstrainText.User_Creation_Date = newConstrain.User_Creation_Date;
                        newConstrainText.User_Creation_Id = newConstrain.User_Creation_Id;
                        newConstrainText.CountryConstrain_ID = newConstrain.ID;

                        newConstrainText = uow.Repository<Ex_CountryConstrain_Text>()
                         .InsertReturn(newConstrainText);


                        uow.SaveChanges();



                    }
                if (constrains.AnalysisLabType != null)
                    // labType
                    for (int i = 0; i < constrains.AnalysisLabType.Count; i++)
                    {


                        var newConstrainAnalysisLabType = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>
                            (constrains.AnalysisLabType[i]);
                        newConstrainAnalysisLabType.Parent_ID = constrains.AnalysisLabType[i].Parent_ID;


                        newConstrainAnalysisLabType.ID = uow.Repository<Object>()
                       .GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_SEQ");

                        newConstrainAnalysisLabType.User_Creation_Date = newConstrain.User_Creation_Date;
                        newConstrainAnalysisLabType.User_Creation_Id = newConstrain.User_Creation_Id;
                        newConstrainAnalysisLabType.CountryConstrain_ID = newConstrain.ID;
                        //newConstrainAnalysisLabType. = true;


                        newConstrainAnalysisLabType = uow.Repository<Ex_CountryConstrain_AnalysisLabType>()
                         .InsertReturn(newConstrainAnalysisLabType);


                        uow.SaveChanges();



                    }
                if (constrains.ConstraintAirPortInternational != null)
                {
                    for (int i = 0; i < constrains.ConstraintAirPortInternational.Count; i++)
                    {


                        var newConstrainAirPort = Mapper.Map<Ex_CountryConstrain_ArrivalPort>
                            (constrains.ConstraintAirPortInternational[i]);
                        newConstrainAirPort.Parent_ID = constrains.ConstraintAirPortInternational[i].Parent_ID;


                        newConstrainAirPort.Id = uow.Repository<Object>()
                       .GetNextSequenceValue_Long("Ex_CountryConstrain_ArrivalPort_SEQ");

                        newConstrainAirPort.User_Creation_Date = newConstrain.User_Creation_Date;
                        newConstrainAirPort.User_Creation_Id = newConstrain.User_Creation_Id;
                        newConstrainAirPort.Ex_CountryConstrain_Id = newConstrain.ID;

                        newConstrainAirPort = uow.Repository<Ex_CountryConstrain_ArrivalPort>()
                         .InsertReturn(newConstrainAirPort);


                        uow.SaveChanges();



                    }

                }




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                    constrains.CountryConstrainsDTO.ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        private void updateAny(short? constrainOwner_ID, short? transportCountry_ID, long User_Updation_Id)
        {

            var p = db.Ex_CountryConstrain.Where(x => x.TransportCountry_ID == transportCountry_ID
            && x.ConstrainOwner_ID == constrainOwner_ID && x.IsActive == true).FirstOrDefault();
            if (p != null)
            {
                p.IsActive = false;
                p.User_Updation_Id = User_Updation_Id;
                p.User_Updation_Date = DateTime.Now;
                db.SaveChanges();

            }

            // Ex_CountryConstrain dtoConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(constrains.CountryConstrainsDTO.ID);



        }

        private void DisableConstrain(long countryConstrain_Id, long User_Updation_Id, DateTime User_Updation_Date)
        {
            Ex_CountryConstrain dtoConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(countryConstrain_Id);
            dtoConstrain.IsActive = false;
            dtoConstrain.User_Updation_Id = User_Updation_Id;
            dtoConstrain.User_Updation_Date = DateTime.Now;

            uow.Repository<Ex_CountryConstrain>().Update(dtoConstrain);
            uow.SaveChanges();
        }

        //////****************** PLANTS
        //////private long UpdateCustomPlant(Ex_CountryConstrainDTO dtoConstrain, CustomPlantConstrain plant, short userId, DateTime userDate, List<string> device_Info)
        //////{
        //////    Ex_CountryConstrain dbConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(dtoConstrain.ID);

        //////    bool db_isAnalysis = dbConstrain.IsAnalysis;
        //////    bool db_isTreatment = dbConstrain.IsTreatment;

        //////    dtoConstrain.User_Creation_Id = dbConstrain.User_Creation_Id;
        //////    dtoConstrain.User_Creation_Date = dbConstrain.User_Creation_Date;

        //////    dtoConstrain.User_Updation_Id = userId;
        //////    dtoConstrain.User_Updation_Date = userDate;

        //////    var Co = Mapper.Map(dtoConstrain, dbConstrain);
        //////    uow.Repository<Ex_CountryConstrain>().Update(Co);
        //////    uow.SaveChanges();
        //////    //*****************//
        //////    Con_Ex_Im_PlantsDTO plantDto = new Con_Ex_Im_PlantsDTO();
        //////    plantDto.ID = plant.Id;
        //////    plantDto.Con_Ex_Im_ID = dtoConstrain.ID;
        //////    plantDto.ItemType_ID = 36;
        //////    plantDto.Plant_ID = plant.PlantId;
        //////    plantDto.PlantCat_ID = (plant.PlantCatId > 0) ? plant.PlantCatId : null;
        //////    plantDto.PlantPartType_ID = plant.plantPartId;
        //////    plantDto.ProductStatus_ID = plant.statusId;
        //////    plantDto.Purpose_ID = plant.purposeId;

        //////    Con_Ex_Im_PlantsBll bll = new Con_Ex_Im_PlantsBll();
        //////    bll.Update(plantDto, device_Info);
        //////    //*******************//
        //////    if (plant.IsAnalysis_IsTreatment == 1)
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).SingleOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        Ex_CountryConstrain_AnalysisLabTypeDTO analysisDto = new Ex_CountryConstrain_AnalysisLabTypeDTO();
        //////        analysisDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        analysisDto.AnalysisLabTypeID = plant.AnalysisLabTypeID;
        //////        analysisDto.IsAcive = true;

        //////        if (plant.AnalysisConstrain_ID > 0)
        //////        {
        //////            analysisDto.ID = plant.AnalysisConstrain_ID;

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Findobject(analysisDto.ID);

        //////            analysisDto.User_Creation_Date = dbAnaConstrain.User_Creation_Date;
        //////            analysisDto.User_Creation_Id = dbAnaConstrain.User_Creation_Id;

        //////            analysisDto.User_Updation_Date = userDate;
        //////            analysisDto.User_Updation_Id = userId;

        //////            var analysisObj = Mapper.Map(analysisDto, dbAnaConstrain);
        //////            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Update(analysisObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            analysisDto.User_Creation_Date = userDate;
        //////            analysisDto.User_Creation_Id = userId;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>(analysisDto);
        //////            CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_seq");
        //////            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else if (plant.IsAnalysis_IsTreatment == 2)
        //////    {
        //////        if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).SingleOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        CountryConstrain_TreatmentDTO treatmentDto = new CountryConstrain_TreatmentDTO();
        //////        treatmentDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        treatmentDto.Exposure_Day = plant.Exposure_Day;
        //////        treatmentDto.Exposure_Hour = plant.Exposure_Hour;
        //////        treatmentDto.Exposure_Minute = plant.Exposure_Minute;
        //////        treatmentDto.TheDose = plant.TheDose;
        //////        treatmentDto.TreatmentMaterial_ID = (byte)plant.TreatmentMaterial_ID;
        //////        treatmentDto.TreatmentMethod = (byte)plant.TreatmentMethod;
        //////        treatmentDto.TreatmentType_ID = (byte)plant.TreatmentType_ID;
        //////        treatmentDto.IsAcive = true;

        //////        if (plant.TreatmentConstrain_ID > 0)
        //////        {
        //////            treatmentDto.ID = plant.TreatmentConstrain_ID;

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain = uow.Repository<Ex_CountryConstrain_Treatment>().Findobject(treatmentDto.ID);

        //////            treatmentDto.User_Creation_Id = dbTreatmentConstrain.User_Creation_Id;
        //////            treatmentDto.User_Creation_Date = dbTreatmentConstrain.User_Creation_Date;

        //////            treatmentDto.User_Updation_Id = userId;
        //////            treatmentDto.User_Updation_Date = userDate;

        //////            var treatmentObj = Mapper.Map(treatmentDto, dbTreatmentConstrain);
        //////            uow.Repository<Ex_CountryConstrain_Treatment>().Update(treatmentObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            treatmentDto.User_Creation_Id = userId;
        //////            treatmentDto.User_Creation_Date = userDate;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_Treatment>(treatmentDto);
        //////            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_Seq"); uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).FirstOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////        else if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).FirstOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////    }
        //////    return dtoConstrain.ID;
        //////}

        private long InsertCustomPlantText(long dtoConstrainId, CustomPlantConstrain_Rows plant, List<string> Device_Info)
        {
            Ex_CountryConstrain_TextDTO plantsText = new Ex_CountryConstrain_TextDTO();
            plantsText.ConstrainText_Ar = plant.ConstrainText_Ar;
            plantsText.ConstrainText_En = plant.ConstrainText_En;
            plantsText.CountryConstrain_ID = dtoConstrainId;
            plantsText.IsActive = true;
            plantsText.IsAcceppted = true;
            plantsText.InSide_Certificate_Ar = plant.InSide_Certificate_Ar;
            plantsText.InSide_Certificate_En = plant.InSide_Certificate_En;
            plantsText.IsCertificate_Addtion = plant.IsCertificate_Addtion;
            plantsText.User_Creation_Id = plant.User_Creation_Id;
            plantsText.User_Creation_Date = plant.User_Creation_Date;

            var newConstrainText = Mapper.Map<Ex_CountryConstrain_Text>(plantsText);
            newConstrainText.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Text_seq");
            newConstrainText = uow.Repository<Ex_CountryConstrain_Text>().InsertReturn(newConstrainText);
            uow.SaveChanges();
            //var newConstrain = Mapper.Map<Ex_CountryConstrain>(dtoConstrain);
            //newConstrain.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
            //newConstrain = uow.Repository<Ex_CountryConstrain>().InsertReturn(newConstrain);
            //uow.SaveChanges();
            //dtoConstrain.ID = newConstrain.ID;

            //Con_Ex_Im_PlantsDTO plantDto = new Con_Ex_Im_PlantsDTO();
            //plantDto.Con_Ex_Im_ID = dtoConstrain.ID;
            //plantDto.ItemType_ID = 36;
            //plantDto.Plant_ID = plant.PlantId;
            //plantDto.PlantCat_ID = (plant.PlantCatId > 0) ? plant.PlantCatId : null;
            //plantDto.PlantPartType_ID = plant.plantPartId;
            //plantDto.ProductStatus_ID = plant.statusId;
            //plantDto.Purpose_ID = plant.purposeId;

            //Con_Ex_Im_PlantsBll bll = new Con_Ex_Im_PlantsBll();
            //bll.Insert(plantDto, Device_Info);
            //insert analysis or treatment
            //if (plant.IsAnalysis_IsTreatment == 1)
            //{
            //    Ex_CountryConstrain_AnalysisLabTypeDTO analysisDto = new Ex_CountryConstrain_AnalysisLabTypeDTO();
            //    analysisDto.CountryConstrain_ID = dtoConstrain.ID;
            //    analysisDto.AnalysisLabTypeID = plant.AnalysisLabTypeID;
            //    analysisDto.IsAcive = true;
            //    analysisDto.User_Creation_Id = dtoConstrain.User_Creation_Id;
            //    analysisDto.User_Creation_Date = dtoConstrain.User_Creation_Date;

            //    var CModel = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>(analysisDto);
            //    CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_seq");

            //    uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(CModel);
            //    uow.SaveChanges();
            //}
            //else if (plant.IsAnalysis_IsTreatment == 2)
            //{
            //    CountryConstrain_TreatmentDTO treatmentDto = new CountryConstrain_TreatmentDTO();
            //    treatmentDto.CountryConstrain_ID = dtoConstrain.ID;
            //    treatmentDto.Exposure_Day = plant.Exposure_Day;
            //    treatmentDto.Exposure_Hour = plant.Exposure_Hour;
            //    treatmentDto.Exposure_Minute = plant.Exposure_Minute;
            //    treatmentDto.TheDose = plant.TheDose;
            //    treatmentDto.TreatmentMaterial_ID = (byte)plant.TreatmentMaterial_ID;
            //    treatmentDto.TreatmentMethod = (byte)plant.TreatmentMethod;
            //    treatmentDto.TreatmentType_ID = (byte)plant.TreatmentType_ID;
            //    treatmentDto.IsAcive = true;
            //    treatmentDto.User_Creation_Id = dtoConstrain.User_Creation_Id;
            //    treatmentDto.User_Creation_Date = dtoConstrain.User_Creation_Date;

            //    var CModel = Mapper.Map<Ex_CountryConstrain_Treatment>(treatmentDto);
            //    CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_seq");

            //    uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(CModel);
            //    uow.SaveChanges();
            //}


            //#region Arrival port
            //11 - 9 - 2019 update centers
            //if (arrivalPorts != null && (arrivalPorts.Count > 0))
            //{
            //    UpdateArrivalPortList(arrivalPorts, dtoConstrain.ID,
            //     dtoConstrain.User_Creation_Id, dtoConstrain.User_Creation_Date, Device_Info);
            //}
            //#endregion

            return dtoConstrainId;
        }
        private long InsertCustomPlantTreatment(long dtoConstrainId, CustomPlantConstrain_Treatments treatment, List<string> Device_Info)
        {

            CountryConstrain_TreatmentDTO treatmentDto = new CountryConstrain_TreatmentDTO();
            treatmentDto.CountryConstrain_ID = dtoConstrainId;
            treatmentDto.Exposure_Day = treatment.Exposure_Day;
            treatmentDto.Exposure_Hour = treatment.Exposure_Hour;
            treatmentDto.Exposure_Minute = treatment.Exposure_Minute;
            treatmentDto.TheDose = treatment.TheDose;
            treatmentDto.TreatmentMaterial_ID = (byte)treatment.TreatmentMaterial_ID;
            treatmentDto.TreatmentMethod = (byte)treatment.TreatmentMethod;
            treatmentDto.TreatmentType_ID = (byte)treatment.TreatmentType_ID;
            treatmentDto.IsAcive = true;
            treatmentDto.User_Creation_Id = treatment.User_Creation_Id;
            treatmentDto.User_Creation_Date = treatment.User_Creation_Date;

            var CModel = Mapper.Map<Ex_CountryConstrain_Treatment>(treatmentDto);
            CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_seq");

            uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(CModel);
            uow.SaveChanges();


            return dtoConstrainId;
        }
        private long InsertCustomPlantAnalysis(long dtoConstrainId, CustomPlantConstrain_Analysis analysis, List<string> Device_Info)
        {

            Ex_CountryConstrain_AnalysisLabTypeDTO analysisDto = new Ex_CountryConstrain_AnalysisLabTypeDTO();
            analysisDto.CountryConstrain_ID = dtoConstrainId;
            analysisDto.AnalysisLabTypeID = analysis.AnalysisLab_ID;
            analysisDto.IsAcive = true;
            analysisDto.User_Creation_Id = analysis.User_Creation_Id;
            analysisDto.User_Creation_Date = analysis.User_Creation_Date;

            var CModel = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>(analysisDto);
            CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_seq");

            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(CModel);
            uow.SaveChanges();


            return dtoConstrainId;
        }
        private long InsertCustomPlantPort(long dtoConstrainId, CustomPlantConstrain_ArrivalPorts port, List<string> Device_Info)
        {

            Ex_CountryConstrain_ArrivalPortDTO portDto = new Ex_CountryConstrain_ArrivalPortDTO();
            portDto.Ex_CountryConstrain_Id = dtoConstrainId;
            portDto.Port_International_Id = port.PortInternationalID;
            portDto.User_Creation_Id = port.User_Creation_Id;
            portDto.User_Creation_Date = port.User_Creation_Date;

            var CModel = Mapper.Map<Ex_CountryConstrain_ArrivalPort>(portDto);
            CModel.Id = uow.Repository<object>().GetNextSequenceValue_Long("Ex_CountryConstrain_ArrivalPort_seq");

            uow.Repository<Ex_CountryConstrain_ArrivalPort>().InsertRecord(CModel);
            uow.SaveChanges();


            return dtoConstrainId;
        }
        //////******************* PRODUCTS
        //////private long UpdateCustomProduct(Ex_CountryConstrainDTO dtoConstrain, CustomProductConstrain product, short userId, DateTime userDate, List<string> device_Info)
        //////{
        //////    Ex_CountryConstrain dbConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(dtoConstrain.ID);

        //////    bool db_isAnalysis = dbConstrain.IsAnalysis;
        //////    bool db_isTreatment = dbConstrain.IsTreatment;

        //////    dtoConstrain.User_Creation_Id = dbConstrain.User_Creation_Id;
        //////    dtoConstrain.User_Creation_Date = dbConstrain.User_Creation_Date;

        //////    dtoConstrain.User_Updation_Id = userId;
        //////    dtoConstrain.User_Updation_Date = userDate;

        //////    var Co = Mapper.Map(dtoConstrain, dbConstrain);
        //////    uow.Repository<Ex_CountryConstrain>().Update(Co);
        //////    uow.SaveChanges();
        //////    //*****************//
        //////    Con_Ex_Im_ProductsDTO productDto = new Con_Ex_Im_ProductsDTO();
        //////    productDto.ID = product.Id;
        //////    productDto.Con_Ex_Im_ID = dtoConstrain.ID;
        //////    productDto.ItemType_ID = 36;
        //////    productDto.Product_ID = product.ProductId;
        //////    productDto.ProductStatus_ID = product.statusId;
        //////    productDto.Purpose_ID = product.purposeId;

        //////    Con_Ex_Im_ProductsBll bll = new Con_Ex_Im_ProductsBll();
        //////    bll.Update(productDto, device_Info);
        //////    //*******************//
        //////    if (product.IsAnalysis_IsTreatment == 1)
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).SingleOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        Ex_CountryConstrain_AnalysisLabTypeDTO analysisDto = new Ex_CountryConstrain_AnalysisLabTypeDTO();
        //////        analysisDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        analysisDto.AnalysisLabTypeID = product.AnalysisLabTypeID;
        //////        analysisDto.IsAcive = true;

        //////        if (product.AnalysisConstrain_ID > 0)
        //////        {
        //////            analysisDto.ID = product.AnalysisConstrain_ID;

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Findobject(analysisDto.ID);

        //////            analysisDto.User_Creation_Date = dbAnaConstrain.User_Creation_Date;
        //////            analysisDto.User_Creation_Id = dbAnaConstrain.User_Creation_Id;

        //////            analysisDto.User_Updation_Date = userDate;
        //////            analysisDto.User_Updation_Id = userId;

        //////            var analysisObj = Mapper.Map(analysisDto, dbAnaConstrain);
        //////            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Update(analysisObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            analysisDto.User_Creation_Date = userDate;
        //////            analysisDto.User_Creation_Id = userId;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>(analysisDto);
        //////            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_Seq"); uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else if (product.IsAnalysis_IsTreatment == 2)
        //////    {
        //////        if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).SingleOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        CountryConstrain_TreatmentDTO treatmentDto = new CountryConstrain_TreatmentDTO();
        //////        treatmentDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        treatmentDto.Exposure_Day = product.Exposure_Day;
        //////        treatmentDto.Exposure_Hour = product.Exposure_Hour;
        //////        treatmentDto.Exposure_Minute = product.Exposure_Minute;
        //////        treatmentDto.TheDose = product.TheDose;
        //////        treatmentDto.TreatmentMaterial_ID = (byte)product.TreatmentMaterial_ID;
        //////        treatmentDto.TreatmentMethod = (byte)product.TreatmentMethod;
        //////        treatmentDto.TreatmentType_ID = (byte)product.TreatmentType_ID;
        //////        treatmentDto.IsAcive = true;

        //////        if (product.TreatmentConstrain_ID > 0)
        //////        {
        //////            treatmentDto.ID = product.TreatmentConstrain_ID;

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain = uow.Repository<Ex_CountryConstrain_Treatment>().Findobject(treatmentDto.ID);

        //////            treatmentDto.User_Creation_Id = dbTreatmentConstrain.User_Creation_Id;
        //////            treatmentDto.User_Creation_Date = dbTreatmentConstrain.User_Creation_Date;

        //////            treatmentDto.User_Updation_Id = userId;
        //////            treatmentDto.User_Updation_Date = userDate;

        //////            var treatmentObj = Mapper.Map(treatmentDto, dbTreatmentConstrain);
        //////            uow.Repository<Ex_CountryConstrain_Treatment>().Update(treatmentObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            treatmentDto.User_Creation_Id = userId;
        //////            treatmentDto.User_Creation_Date = userDate;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_Treatment>(treatmentDto);
        //////            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_Seq"); uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).FirstOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////        else if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).FirstOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////    }
        //////    return dtoConstrain.ID;
        //////}

        //////private long InsertCustomProduct(Ex_CountryConstrainDTO dtoConstrain, CustomProductConstrain product, List<int> arrivalPorts, List<string> Device_Info)
        //////{

        //////    var newConstrain = Mapper.Map<Ex_CountryConstrain>(dtoConstrain);
        //////    newConstrain.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
        //////    newConstrain = uow.Repository<Ex_CountryConstrain>().InsertReturn(newConstrain);
        //////    uow.SaveChanges();
        //////    dtoConstrain.ID = newConstrain.ID;

        //////    Con_Ex_Im_ProductsDTO productDto = new Con_Ex_Im_ProductsDTO();
        //////    productDto.Con_Ex_Im_ID = dtoConstrain.ID;
        //////    productDto.ItemType_ID = 36;
        //////    productDto.Product_ID = product.ProductId;
        //////    productDto.ProductStatus_ID = product.statusId;
        //////    productDto.Purpose_ID = product.purposeId;

        //////    Con_Ex_Im_ProductsBll bll = new Con_Ex_Im_ProductsBll();
        //////    bll.Insert(productDto, Device_Info);

        //////    if (product.IsAnalysis_IsTreatment == 1)
        //////    {
        //////        Ex_CountryConstrain_AnalysisLabTypeDTO analysisDto = new Ex_CountryConstrain_AnalysisLabTypeDTO();
        //////        analysisDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        analysisDto.AnalysisLabTypeID = product.AnalysisLabTypeID;
        //////        analysisDto.IsAcive = true;
        //////        analysisDto.User_Creation_Id = dtoConstrain.User_Creation_Id;
        //////        analysisDto.User_Creation_Date = dtoConstrain.User_Creation_Date;

        //////        var CModel = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>(analysisDto);
        //////        CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_Seq");
        //////        uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(CModel);
        //////        uow.SaveChanges();
        //////    }
        //////    else if (product.IsAnalysis_IsTreatment == 2)
        //////    {
        //////        CountryConstrain_TreatmentDTO treatmentDto = new CountryConstrain_TreatmentDTO();
        //////        treatmentDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        treatmentDto.Exposure_Day = product.Exposure_Day;
        //////        treatmentDto.Exposure_Hour = product.Exposure_Hour;
        //////        treatmentDto.Exposure_Minute = product.Exposure_Minute;
        //////        treatmentDto.TheDose = product.TheDose;
        //////        treatmentDto.TreatmentMaterial_ID = (byte)product.TreatmentMaterial_ID;
        //////        treatmentDto.TreatmentMethod = (byte)product.TreatmentMethod;
        //////        treatmentDto.TreatmentType_ID = (byte)product.TreatmentType_ID;
        //////        treatmentDto.IsAcive = true;
        //////        treatmentDto.User_Creation_Id = dtoConstrain.User_Creation_Id;
        //////        treatmentDto.User_Creation_Date = dtoConstrain.User_Creation_Date;

        //////        var CModel = Mapper.Map<Ex_CountryConstrain_Treatment>(treatmentDto);
        //////        CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_Seq"); uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(CModel);
        //////        uow.SaveChanges();
        //////    }
        //////    if (arrivalPorts != null && (arrivalPorts.Count > 0))
        //////    {
        //////        UpdateArrivalPortList(arrivalPorts, dtoConstrain.ID,
        //////         dtoConstrain.User_Creation_Id, dtoConstrain.User_Creation_Date, Device_Info);
        //////    }
        //////    return dtoConstrain.ID;
        //////}
        //////******************* LIABLE ITEMS ALIVE
        //////private long UpdateCustomLiableAlive(Ex_CountryConstrainDTO dtoConstrain, CustomAliveConstrain alive, short userId, DateTime userDate, List<string> device_Info)
        //////{
        //////    Ex_CountryConstrain dbConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(dtoConstrain.ID);

        //////    bool db_isAnalysis = dbConstrain.IsAnalysis;
        //////    bool db_isTreatment = dbConstrain.IsTreatment;

        //////    dtoConstrain.User_Creation_Id = dbConstrain.User_Creation_Id;
        //////    dtoConstrain.User_Creation_Date = dbConstrain.User_Creation_Date;

        //////    dtoConstrain.User_Updation_Id = userId;
        //////    dtoConstrain.User_Updation_Date = userDate;

        //////    var Co = Mapper.Map(dtoConstrain, dbConstrain);
        //////    uow.Repository<Ex_CountryConstrain>().Update(Co);
        //////    uow.SaveChanges();
        //////    //*****************//
        //////    Con_Ex_Im_AliveDTO aliveDto = new Con_Ex_Im_AliveDTO();
        //////    aliveDto.ID = alive.Id;
        //////    aliveDto.Con_Ex_Im_ID = dtoConstrain.ID;
        //////    aliveDto.ItemType_ID = 36;
        //////    aliveDto.LiableStatus_ID = alive.statusId;
        //////    aliveDto.Purpose_ID = alive.purposeId;
        //////    aliveDto.BiologicalPhase_ID = alive.biologicalPhaseId;

        //////    Con_Ex_Im_LiableItems_AliveBll bll = new Con_Ex_Im_LiableItems_AliveBll();
        //////    bll.Update(aliveDto, device_Info);
        //////    //*******************//
        //////    if (alive.IsAnalysis_IsTreatment == 1)
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).SingleOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        Ex_CountryConstrain_AnalysisLabTypeDTO analysisDto = new Ex_CountryConstrain_AnalysisLabTypeDTO();
        //////        analysisDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        analysisDto.AnalysisLabTypeID = alive.AnalysisLabTypeID;
        //////        analysisDto.IsAcive = true;

        //////        if (alive.AnalysisConstrain_ID > 0)
        //////        {
        //////            analysisDto.ID = alive.AnalysisConstrain_ID;

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Findobject(analysisDto.ID);

        //////            analysisDto.User_Creation_Date = dbAnaConstrain.User_Creation_Date;
        //////            analysisDto.User_Creation_Id = dbAnaConstrain.User_Creation_Id;

        //////            analysisDto.User_Updation_Date = userDate;
        //////            analysisDto.User_Updation_Id = userId;

        //////            var analysisObj = Mapper.Map(analysisDto, dbAnaConstrain);
        //////            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Update(analysisObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            analysisDto.User_Creation_Date = userDate;
        //////            analysisDto.User_Creation_Id = userId;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>(analysisDto);
        //////            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_Seq");

        //////            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else if (alive.IsAnalysis_IsTreatment == 2)
        //////    {
        //////        if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).SingleOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        CountryConstrain_TreatmentDTO treatmentDto = new CountryConstrain_TreatmentDTO();
        //////        treatmentDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        treatmentDto.Exposure_Day = alive.Exposure_Day;
        //////        treatmentDto.Exposure_Hour = alive.Exposure_Hour;
        //////        treatmentDto.Exposure_Minute = alive.Exposure_Minute;
        //////        treatmentDto.TheDose = alive.TheDose;
        //////        treatmentDto.TreatmentMaterial_ID = (byte)alive.TreatmentMaterial_ID;
        //////        treatmentDto.TreatmentMethod = (byte)alive.TreatmentMethod;
        //////        treatmentDto.TreatmentType_ID = (byte)alive.TreatmentType_ID;
        //////        treatmentDto.IsAcive = true;

        //////        if (alive.TreatmentConstrain_ID > 0)
        //////        {
        //////            treatmentDto.ID = alive.TreatmentConstrain_ID;

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain = uow.Repository<Ex_CountryConstrain_Treatment>().Findobject(treatmentDto.ID);

        //////            treatmentDto.User_Creation_Id = dbTreatmentConstrain.User_Creation_Id;
        //////            treatmentDto.User_Creation_Date = dbTreatmentConstrain.User_Creation_Date;

        //////            treatmentDto.User_Updation_Id = userId;
        //////            treatmentDto.User_Updation_Date = userDate;

        //////            var treatmentObj = Mapper.Map(treatmentDto, dbTreatmentConstrain);
        //////            uow.Repository<Ex_CountryConstrain_Treatment>().Update(treatmentObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            treatmentDto.User_Creation_Id = userId;
        //////            treatmentDto.User_Creation_Date = userDate;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_Treatment>(treatmentDto);
        //////            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_Seq");
        //////            uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).FirstOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////        else if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).FirstOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////    }

        //////    return dtoConstrain.ID;
        //////}


        //////******************* LIABLE ITEMS NOT ALIVE
        //////private long UpdateCustomLiableNotAlive(Ex_CountryConstrainDTO dtoConstrain, CustomNotAliveConstrain notAlive, short userId, DateTime userDate, List<string> device_Info)
        //////{
        //////    Ex_CountryConstrain dbConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(dtoConstrain.ID);

        //////    bool db_isAnalysis = dbConstrain.IsAnalysis;
        //////    bool db_isTreatment = dbConstrain.IsTreatment;

        //////    dtoConstrain.User_Creation_Id = dbConstrain.User_Creation_Id;
        //////    dtoConstrain.User_Creation_Date = dbConstrain.User_Creation_Date;

        //////    dtoConstrain.User_Updation_Id = userId;
        //////    dtoConstrain.User_Updation_Date = userDate;

        //////    var Co = Mapper.Map(dtoConstrain, dbConstrain);
        //////    uow.Repository<Ex_CountryConstrain>().Update(Co);
        //////    uow.SaveChanges();
        //////    //*****************//
        //////    Con_Ex_Im_NotAliveDTO notAliveDto = new Con_Ex_Im_NotAliveDTO();
        //////    notAliveDto.ID = notAlive.Id;
        //////    notAliveDto.Con_Ex_Im_ID = dtoConstrain.ID;
        //////    notAliveDto.ItemType_ID = 36;
        //////    notAliveDto.LiableStatus_ID = notAlive.statusId;
        //////    notAliveDto.Purpose_ID = notAlive.purposeId;

        //////    Con_Ex_Im_LiableItems_NotAliveBll bll = new Con_Ex_Im_LiableItems_NotAliveBll();
        //////    bll.Update(notAliveDto, device_Info);
        //////    //*******************//
        //////    if (notAlive.IsAnalysis_IsTreatment == 1)
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).SingleOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        Ex_CountryConstrain_AnalysisLabTypeDTO analysisDto = new Ex_CountryConstrain_AnalysisLabTypeDTO();
        //////        analysisDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        analysisDto.AnalysisLabTypeID = notAlive.AnalysisLabTypeID;
        //////        analysisDto.IsAcive = true;

        //////        if (notAlive.AnalysisConstrain_ID > 0)
        //////        {
        //////            analysisDto.ID = notAlive.AnalysisConstrain_ID;

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain = uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Findobject(analysisDto.ID);

        //////            analysisDto.User_Creation_Date = dbAnaConstrain.User_Creation_Date;
        //////            analysisDto.User_Creation_Id = dbAnaConstrain.User_Creation_Id;

        //////            analysisDto.User_Updation_Date = userDate;
        //////            analysisDto.User_Updation_Id = userId;

        //////            var analysisObj = Mapper.Map(analysisDto, dbAnaConstrain);
        //////            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().Update(analysisObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            analysisDto.User_Creation_Date = userDate;
        //////            analysisDto.User_Creation_Id = userId;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>(analysisDto);
        //////            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_Seq");
        //////            uow.Repository<Ex_CountryConstrain_AnalysisLabType>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else if (notAlive.IsAnalysis_IsTreatment == 2)
        //////    {
        //////        if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).SingleOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }

        //////        CountryConstrain_TreatmentDTO treatmentDto = new CountryConstrain_TreatmentDTO();
        //////        treatmentDto.CountryConstrain_ID = dtoConstrain.ID;
        //////        treatmentDto.Exposure_Day = notAlive.Exposure_Day;
        //////        treatmentDto.Exposure_Hour = notAlive.Exposure_Hour;
        //////        treatmentDto.Exposure_Minute = notAlive.Exposure_Minute;
        //////        treatmentDto.TheDose = notAlive.TheDose;
        //////        treatmentDto.TreatmentMaterial_ID = (byte)notAlive.TreatmentMaterial_ID;
        //////        treatmentDto.TreatmentMethod = (byte)notAlive.TreatmentMethod;
        //////        treatmentDto.TreatmentType_ID = (byte)notAlive.TreatmentType_ID;
        //////        treatmentDto.IsAcive = true;

        //////        if (notAlive.TreatmentConstrain_ID > 0)
        //////        {
        //////            treatmentDto.ID = notAlive.TreatmentConstrain_ID;

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain = uow.Repository<Ex_CountryConstrain_Treatment>().Findobject(treatmentDto.ID);

        //////            treatmentDto.User_Creation_Id = dbTreatmentConstrain.User_Creation_Id;
        //////            treatmentDto.User_Creation_Date = dbTreatmentConstrain.User_Creation_Date;

        //////            treatmentDto.User_Updation_Id = userId;
        //////            treatmentDto.User_Updation_Date = userDate;

        //////            var treatmentObj = Mapper.Map(treatmentDto, dbTreatmentConstrain);
        //////            uow.Repository<Ex_CountryConstrain_Treatment>().Update(treatmentObj);
        //////            uow.SaveChanges();
        //////        }
        //////        else
        //////        {
        //////            treatmentDto.User_Creation_Id = userId;
        //////            treatmentDto.User_Creation_Date = userDate;

        //////            var CModel = Mapper.Map<Ex_CountryConstrain_Treatment>(treatmentDto);
        //////            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_Seq");
        //////            uow.Repository<Ex_CountryConstrain_Treatment>().InsertRecord(CModel);
        //////            uow.SaveChanges();
        //////        }
        //////    }
        //////    else
        //////    {
        //////        if (db_isTreatment == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_Treatment dbTreatmentConstrain =
        //////                (from treatment in entity.Ex_CountryConstrain_Treatment
        //////                 where treatment.CountryConstrain_ID == dtoConstrain.ID
        //////                 select treatment).FirstOrDefault();

        //////            dbTreatmentConstrain.User_Deletion_Id = userId;
        //////            dbTreatmentConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////        else if (db_isAnalysis == true)
        //////        {
        //////            PlantQuarantineEntities entity = new PlantQuarantineEntities();

        //////            Ex_CountryConstrain_AnalysisLabType dbAnaConstrain =
        //////                (from analysis in entity.Ex_CountryConstrain_AnalysisLabType
        //////                 where analysis.CountryConstrain_ID == dtoConstrain.ID
        //////                 select analysis).FirstOrDefault();

        //////            dbAnaConstrain.User_Deletion_Id = userId;
        //////            dbAnaConstrain.User_Deletion_Date = userDate;

        //////            entity.SaveChanges();
        //////        }
        //////    }

        //////    return dtoConstrain.ID;
        //////}






        ////#region ActivationProcedure
        ////public Dictionary<string, object> GetPlantConstrain_ActivationProcedure
        //// (long plantId, byte purposeId, byte statusId, byte partType, long catId, int ownerImportId, int ownerTransitId, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        long? categoryId = null;
        ////        if (catId > 0) categoryId = catId;

        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        ////        //get import data + its union
        ////        //get transit data + its union

        ////        var data = (
        ////                   from cc in entities.Ex_CountryConstrain
        ////                   join plant in entities.Con_Ex_Im_Plants on

        ////                    cc.ID equals plant.Con_Ex_Im_ID
        ////                   where
        ////                 ((cc.ConstrainOwner_ID == ownerImportId ) ||
        ////                  (cc.ConstrainOwner_ID == ownerTransitId ) ||
        ////                  (cc.ConstrainOwner_ID == ownerImportId && cc.TransportCountry_ID == ownerTransitId)
        ////                  ) &&
        ////                        cc.IsExport == true
        ////                        && cc.IsPlant == 4 && cc.ProdPlant_ID == plantId
        ////                        && cc.User_Deletion_Id == null
        ////                        && cc.IsActive == true
        ////                        && plant.ItemType_ID == 36
        ////                        && plant.PlantCat_ID == categoryId
        ////                        && plant.PlantPartType_ID == partType
        ////                        && plant.ProductStatus_ID == statusId
        ////                        && plant.Purpose_ID == purposeId

        ////                   select new Ex_CountryConstrainDTO
        ////                   {
        ////                       ID = cc.ID,
        ////                       //28-6-2020 constrain updates

        ////                       //ConstrainText_Ar = cc.ConstrainText_Ar,
        ////                       //ConstrainText_En = cc.ConstrainText_En,
        ////                       //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                       //InSide_Certificate_En = cc.InSide_Certificate_En,
        ////                       //IsAnalysis = cc.IsAnalysis,
        ////                       //IsTreatment = cc.IsTreatment,
        ////                       IsActive = cc.IsActive,
        ////                       //28-6-2020 constrain updates
        ////                      // IsCertificate_Addtion = cc.IsCertificate_Addtion,
        ////                       IsFarmAccreditation = cc.IsFarmAccreditation,
        ////                       IsCompanyAccreditation = cc.IsCompanyAccreditation,
        ////                       IsStationAccreditation = cc.IsStationAccreditation,

        ////                       ConstrainOwner_ID = cc.ConstrainOwner_ID,
        ////                       ConstrainOwner_Name = entities.Countries.Where(c => c.ID == cc.ConstrainOwner_ID).FirstOrDefault().Ar_Name,

        ////                       TransportCountry_ID = cc.TransportCountry_ID,
        ////                       TransportCountry_Name = entities.Countries.Where(c => c.ID == cc.TransportCountry_ID).FirstOrDefault().Ar_Name,

        ////                       CountryConstrain_Type = cc.CountryConstrain_Type,
        ////                       CountryConstrain_TypeName = entities.A_SystemCode.Where(c => c.Id == cc.CountryConstrain_Type).FirstOrDefault().ValueName,


        ////                       ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       .Select(z => z.Port_International.ID).ToList(),

        ////                       CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        ////                       (c => c.Port_International.Country_ID).FirstOrDefault()

        ////                   }
        ////                   ).OrderBy(x => new { x.CountryConstrain_Type, x.ConstrainOwner_ID }).ToList();

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}


        ////public Dictionary<string, object> GetConstrain_Product_ActivationProcedure
        ////   (long productId, byte purposeId, byte statusId, int ownerImportId, int ownerTransitId, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join product in entities.Con_Ex_Im_Products on cc.ID equals product.Con_Ex_Im_ID
        ////                         where
        ////                ((cc.ConstrainOwner_ID == ownerImportId) ||
        ////                 (cc.ConstrainOwner_ID == ownerTransitId ) ||
        ////                 (cc.ConstrainOwner_ID == ownerImportId && cc.TransportCountry_ID == ownerTransitId)
        ////                 ) && cc.IsExport == true
        ////                            && cc.IsPlant == 5 && cc.ProdPlant_ID == productId
        ////                            && cc.User_Deletion_Id == null
        ////                           && cc.IsActive == true
        ////                           && product.ItemType_ID == 36
        ////                            && product.ProductStatus_ID == statusId
        ////                            && product.Purpose_ID == purposeId
        ////                         select new Ex_CountryConstrainDTO
        ////                         {
        ////                             ID = cc.ID,
        ////                             //28-6-2020 constrain updates
        ////                             //ConstrainText_Ar = cc.ConstrainText_Ar,
        ////                             //ConstrainText_En = cc.ConstrainText_En,
        ////                             //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                             //InSide_Certificate_En = cc.InSide_Certificate_En,
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             IsActive = cc.IsActive,

        ////                             //28-6-2020 constrain updates
        ////                             //IsCertificate_Addtion = cc.IsCertificate_Addtion,
        ////                             IsFarmAccreditation = cc.IsFarmAccreditation,
        ////                             IsCompanyAccreditation = cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation = cc.IsStationAccreditation,

        ////                             ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       .Select(z => z.Port_International.ID).ToList(),

        ////                             CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        ////                       (c => c.Port_International.Country_ID).FirstOrDefault()
        ////                         }
        ////                   ).ToList();
        ////        //eman
        ////        var datareturn = constrain.OrderBy(o => o.ID).Take(1).ToList();
        ////        foreach (var cc in datareturn)
        ////        {
        ////            //cc.ConstrainText_Ar = "1-" + cc.ConstrainText_Ar + Environment.NewLine ;
        ////            //cc.ConstrainText_En = "1-" + cc.ConstrainText_En + Environment.NewLine ;
        ////            var i = 1;
        ////            //cc.ConstrainText_Ar = "";
        ////            //cc.ConstrainText_En = "";
        ////            var con_ar = "";
        ////            var con_en = "";
        ////            //foreach (var dd in constrain)
        ////            //{

        ////            //    con_ar += i + "-" + dd.ConstrainText_Ar + Environment.NewLine;
        ////            //    con_en += i + "-" + dd.ConstrainText_En + Environment.NewLine;
        ////            //    i++;
        ////            //}
        ////            //cc.ConstrainText_Ar = con_ar;
        ////            //cc.ConstrainText_En = con_en;
        ////        }

        ////        // var dataDTO = new List<Ex_CountryConstrainDTO>();

        ////        //dataDTO.Add(data);

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}







        ////public Dictionary<string, object> GetConstrain_LiablAlive_ActivationProcedure(long aliveLiableId, byte purposeId, int statusId, int phaseId, int ownerImportId, int ownerTransitId, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join alive in entities.Con_Ex_Im_LiableItems_Alive on cc.ID equals alive.Con_Ex_Im_ID
        ////                         where
        ////                ((cc.ConstrainOwner_ID == ownerImportId ) ||
        ////                 (cc.ConstrainOwner_ID == ownerTransitId ) ||
        ////                 (cc.ConstrainOwner_ID == ownerImportId && cc.TransportCountry_ID == ownerTransitId)
        ////                 )

        ////                           && cc.IsExport == true
        ////                            && cc.IsPlant == 16 && cc.ProdPlant_ID == aliveLiableId
        ////                            && cc.User_Deletion_Id == null
        ////                              && cc.IsActive == true
        ////                            && alive.ItemType_ID == 36
        ////                            && alive.LiableStatus_ID == statusId
        ////                            && alive.Purpose_ID == purposeId
        ////                            && alive.BiologicalPhase_ID == phaseId
        ////                         select new Ex_CountryConstrainDTO
        ////                         {
        ////                             ID = cc.ID,
        ////                             //28-6-2020 constrain updates
        ////                             //ConstrainText_Ar = cc.ConstrainText_Ar,
        ////                             //ConstrainText_En = cc.ConstrainText_En,
        ////                             //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                             //InSide_Certificate_En = cc.InSide_Certificate_En,
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             IsActive = cc.IsActive,


        ////                             //28-6-2020 constrain updates
        ////                             //IsCertificate_Addtion = cc.IsCertificate_Addtion,
        ////                             IsFarmAccreditation = cc.IsFarmAccreditation,
        ////                             IsCompanyAccreditation = cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation = cc.IsStationAccreditation,

        ////                             ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       .Select(z => z.Port_International.ID).ToList(),

        ////                             CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        ////                       (c => c.Port_International.Country_ID).FirstOrDefault()
        ////                         }
        ////                   ).ToList();
        ////        //eman
        ////        var datareturn = constrain.OrderBy(o => o.ID).Take(1).ToList();
        ////        foreach (var cc in datareturn)
        ////        {
        ////            //cc.ConstrainText_Ar = "1-" + cc.ConstrainText_Ar + Environment.NewLine ;
        ////            //cc.ConstrainText_En = "1-" + cc.ConstrainText_En + Environment.NewLine ;
        ////            var i = 1;
        ////            //cc.ConstrainText_Ar = "";
        ////            //cc.ConstrainText_En = "";
        ////            var con_ar = "";
        ////            var con_en = "";
        ////            //foreach (var dd in constrain)
        ////            //{

        ////            //    con_ar += i + "-" + dd.ConstrainText_Ar + Environment.NewLine;
        ////            //    con_en += i + "-" + dd.ConstrainText_En + Environment.NewLine;
        ////            //    i++;
        ////            //}
        ////            //cc.ConstrainText_Ar = con_ar;
        ////            //cc.ConstrainText_En = con_en;
        ////        }

        ////        // var dataDTO = new List<Ex_CountryConstrainDTO>();

        ////        //dataDTO.Add(data);

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}




        ////public Dictionary<string, object> GetConstrain_LiableNotAlive_ActivationProcedure(long notAliveLiableId, byte purposeId, int statusId, int ownerImportId, int ownerTransitId, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        //START HERE Get Constrains
        ////        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        ////        var constrain = (from cc in entities.Ex_CountryConstrain
        ////                         join notAlive in entities.Con_Ex_Im_LiableItems_NotAlive on cc.ID equals notAlive.Con_Ex_Im_ID
        ////                         where
        ////                  ((cc.ConstrainOwner_ID == ownerImportId ) ||
        ////                   (cc.ConstrainOwner_ID == ownerTransitId ) ||
        ////                   (cc.ConstrainOwner_ID == ownerImportId && cc.TransportCountry_ID == ownerTransitId)
        ////                   ) &&

        ////                             cc.IsExport == true
        ////                            && cc.IsPlant == 33 && cc.ProdPlant_ID == notAliveLiableId
        ////                            && cc.User_Deletion_Id == null
        ////                            && cc.IsActive == true
        ////                            && notAlive.ItemType_ID == 36
        ////                            && notAlive.LiableStatus_ID == statusId
        ////                            && notAlive.Purpose_ID == purposeId
        ////                         select new Ex_CountryConstrainDTO
        ////                         {
        ////                             ID = cc.ID,
        ////                             //28-6-2020 constrain updates
        ////                             //ConstrainText_Ar = cc.ConstrainText_Ar,
        ////                             //ConstrainText_En = cc.ConstrainText_En,
        ////                             //InSide_Certificate_Ar = cc.InSide_Certificate_Ar,
        ////                             //InSide_Certificate_En = cc.InSide_Certificate_En,
        ////                             //IsAnalysis = cc.IsAnalysis,
        ////                             //IsTreatment = cc.IsTreatment,
        ////                             IsActive = cc.IsActive,


        ////                             //28-6-2020 constrain updates
        ////                             //IsCertificate_Addtion = cc.IsCertificate_Addtion,
        ////                             IsFarmAccreditation = cc.IsFarmAccreditation,
        ////                             IsCompanyAccreditation = cc.IsCompanyAccreditation,
        ////                             IsStationAccreditation = cc.IsStationAccreditation,

        ////                             ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
        ////                       Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
        ////                       .Select(z => z.Port_International.ID).ToList(),

        ////                             CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
        ////                       (c => c.Port_International.Country_ID).FirstOrDefault()
        ////                         }
        ////                   ).ToList();
        ////        //eman
        ////        var datareturn = constrain.OrderBy(o => o.ID).Take(1).ToList();
        ////        foreach (var cc in datareturn)
        ////        {
        ////            //cc.ConstrainText_Ar = "1-" + cc.ConstrainText_Ar + Environment.NewLine ;
        ////            //cc.ConstrainText_En = "1-" + cc.ConstrainText_En + Environment.NewLine ;
        ////            var i = 1;
        ////            //cc.ConstrainText_Ar = "";
        ////            //cc.ConstrainText_En = "";
        ////            var con_ar = "";
        ////            var con_en = "";
        ////            //foreach (var dd in constrain)
        ////            //{

        ////            //    con_ar += i + "-" + dd.ConstrainText_Ar + Environment.NewLine;
        ////            //    con_en += i + "-" + dd.ConstrainText_En + Environment.NewLine;
        ////            //    i++;
        ////            //}
        ////            //cc.ConstrainText_Ar = con_ar;
        ////            //cc.ConstrainText_En = con_en;
        ////        }

        ////        // var dataDTO = new List<Ex_CountryConstrainDTO>();

        ////        //dataDTO.Add(data);

        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}

        ////public Dictionary<string, object> Update_ActivationProcedure(Ex_CountryConstrainDTO entity, List<string> Device_Info)
        ////{
        ////    try
        ////    {
        ////        Ex_CountryConstrain CModel = uow.Repository<Ex_CountryConstrain>().Findobject(entity.ID);
        ////        var constrainsList = uow.Repository<Ex_CountryConstrain>().GetData()
        ////            .Where(c => c.CountryConstrain_Type == CModel.CountryConstrain_Type &&
        ////        c.ConstrainOwner_ID == CModel.ConstrainOwner_ID &&
        ////         c.IsPlant == CModel.IsPlant &&
        ////        c.ProdPlant_ID == CModel.ProdPlant_ID &&
        ////        c.User_Deletion_Id == null).ToList();
        ////        foreach (var con in constrainsList)
        ////        {
        ////            Ex_CountryConstrain CModel2 = uow.Repository<Ex_CountryConstrain>().Findobject(con.ID);
        ////            CModel2.User_Updation_Date = entity.User_Updation_Date;
        ////            CModel2.User_Updation_Id = entity.User_Updation_Id;



        ////            CModel2.IsActive = entity.IsActive_Action;

        ////        }



        ////        uow.Repository<Ex_CountryConstrain>().Update(CModel);
        ////        uow.SaveChanges();



        ////        var empDTO = Mapper.Map<Ex_CountryConstrain, Ex_CountryConstrainDTO>(CModel);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        ////        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        ////    }
        ////}
        ////#endregion


        ExCountryConstainDisplay commdata = new ExCountryConstainDisplay();


        public Dictionary<string, object> GetCustomConstrainProc(int constrainOwner_ID,
                  int TransportCountry_ID, long item_ShortName_id, long itemCategories_ID, bool isStationAccreditation,
                  bool isFarmAccreditation, bool isCompanyAccreditation, List<string> lists)
            {
            PlantQuarantineEntities db = new PlantQuarantineEntities();

            try
            {
                #region getperv
                commdata = (
                    from Ecc in db.Ex_CountryConstrain


                    where
                    Ecc.User_Deletion_Id == null
                    && Ecc.IsActive == true
                    && Ecc.User_Deletion_Date == null
                    && Ecc.ConstrainOwner_ID == TransportCountry_ID
                    && Ecc.TransportCountry_ID == constrainOwner_ID
                    && Ecc.Item_ShortName_id == item_ShortName_id
                    && Ecc.ItemCategories_ID == itemCategories_ID
                    && Ecc.User_Updation_Date == null



                    select new ExCountryConstainDisplay
                    {

                        CountryConstrain_ID = Ecc.ID,
                        IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                        IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                        IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,


                    }

                    ).SingleOrDefault();


                if (commdata != null)
                {
                    var CountryConstrain_ID = commdata.CountryConstrain_ID;

                    commdata.Text = (
                    from CCt in db.Ex_CountryConstrain_Text



                    where
                    CCt.User_Deletion_Id == null
                     && CCt.User_Deletion_Date == null
                  && CCt.CountryConstrain_ID == CountryConstrain_ID



                    select new ExConstrainsText
                    {
                        ID = CCt.ID,
                        ConstrainText_Ar = CCt.ConstrainText_Ar,
                        ConstrainText_En = CCt.ConstrainText_En,
                        IsCertificate_Addtion = (bool)CCt.IsCertificate_Addtion,
                        InSide_Certificate_Ar = CCt.InSide_Certificate_Ar,
                        InSide_Certificate_En = CCt.InSide_Certificate_En,
                        Parent_ID = CCt.Parent_ID



                    }

                    ).ToList();



                    commdata.Analysis = (
                             from CCA in db.Ex_CountryConstrain_AnalysisLabType
                             join Al in db.AnalysisLabTypes
                             on CCA.AnalysisLabTypeID equals Al.ID

                             join At in db.AnalysisTypes on
                            Al.AnalysisTypeID equals At.ID

                             join v in db.AnalysisLabs on
                     Al.AnalysisLabID equals v.ID


                             where
                             CCA.User_Deletion_Id == null
                             //  && CCA.IsActive == true
                             && CCA.User_Deletion_Date == null
                             && CCA.CountryConstrain_ID == CountryConstrain_ID
                           && Al.User_Deletion_Date == null
                           && Al.User_Deletion_Id == null
                           && At.User_Deletion_Id == null
                           && At.User_Deletion_Date == null
                           && v.User_Deletion_Id == null
                           && v.User_Deletion_Date == null




                             select new ExConstrainsLabsAndTyp
                             {
                                 ID = CCA.ID,
                                 LabName_Ar = v.Name_Ar,
                                 LabName_En = v.Name_En,
                                 TypeName_Ar = At.Name_Ar,
                                 TypeName_En = At.Name_En,
                                 ExConstrainsLabsAndTypID = Al.ID
                                 ,
                                 Parent_ID = CCA.Parent_ID


                             }

                             ).ToList();




                    commdata.Ports = (
                             from CCA in db.Ex_CountryConstrain_ArrivalPort
                             join pil in db.Port_International
                             on CCA.Port_International_Id equals pil.ID

                             join Ci in db.Countries on
                            pil.Country_ID equals Ci.ID

                             join v in db.Port_Type on
                     pil.PortTypeID equals v.ID


                             where
                             CCA.User_Deletion_Id == null
                             && CCA.User_Deletion_Date == null
                             && CCA.Ex_CountryConstrain_Id == CountryConstrain_ID
                           && pil.User_Deletion_Date == null
                           && pil.User_Deletion_Id == null
                           && Ci.User_Deletion_Id == null
                           && Ci.User_Deletion_Date == null
                           && v.User_Deletion_Id == null
                           && v.User_Deletion_Date == null
                             select new ExConstrainsAirPortAndCountry
                             {
                                 ID = CCA.Id,
                                 AirPortName_Ar = v.Name_Ar,
                                 AirPortName_En = v.Name_En,
                                 CountryName_Ar = pil.Name_Ar,
                                 CountryLabName_En = pil.Name_En,
                                 ExConstrainsAirPortAndCountryID = pil.ID

                                 ,
                                 Parent_ID = CCA.Parent_ID

                             }

                             ).ToList();



                    //Analysis.AddRange(commdata.Analysis);
                    //Text.AddRange(commdata.Text);
                    //Portss.AddRange(commdata.Ports);
                }


                #endregion


                getData(constrainOwner_ID, item_ShortName_id, itemCategories_ID);

                getData(TransportCountry_ID, item_ShortName_id, itemCategories_ID);
                getData(0, item_ShortName_id, itemCategories_ID);

                var UnionData = (
                                     from c in db.Countries
                                     join uc in db.Union_Country on c.ID equals uc.Country_ID
                                     join u in db.Unions on uc.Union_ID equals u.ID
                                     where
                                     c.User_Deletion_Id == null
                                     && c.IsActive == true
                                     && c.User_Deletion_Date == null
                                     && (c.ID == constrainOwner_ID)
                                     && uc.User_Deletion_Id == null
                                     && uc.IsActive == true
                                     && uc.User_Deletion_Date == null
                                     && u.User_Deletion_Id == null
                                     && u.IsActive == true
                                     && u.User_Deletion_Date == null
                                     select new CustomOption
                                     {
                                         Value = u.ID


                                     }

                                     ).ToList();

                foreach (var un in UnionData)
                {
                    getData((int)un.Value, item_ShortName_id, itemCategories_ID);
                }
                var UnionData1 = (
                                     from c in db.Countries
                                     join uc in db.Union_Country on c.ID equals uc.Country_ID
                                     join u in db.Unions on uc.Union_ID equals u.ID
                                     where
                                     c.User_Deletion_Id == null
                                     && c.IsActive == true
                                     && c.User_Deletion_Date == null
                                     && (c.ID == TransportCountry_ID)
                                     && uc.User_Deletion_Id == null
                                     && uc.IsActive == true
                                     && uc.User_Deletion_Date == null
                                     && u.User_Deletion_Id == null
                                     && u.IsActive == true
                                     && u.User_Deletion_Date == null
                                     select new CustomOption
                                     {
                                         Value = u.ID


                                     }

                                     ).ToList();

                foreach (var un in UnionData1)
                {
                    getData((int)un.Value, item_ShortName_id, itemCategories_ID);
                }


                ExCountryConstainDisplay data = new ExCountryConstainDisplay();
                data.Analysis = Analysis;
                data.Text = Text;
                data.Ports = Portss;






                //data.Text = data.Text.Except(commdata.Text).ToList();
                //data.Analysis = data.Analysis.Except(commdata.Analysis).ToList();
                //data.Ports = data.Ports.Except(commdata.Ports).ToList();




                if (commdata != null)
                {
                    data.Text = data.Text
                                    .Where(x => !commdata.Text.Any(y => y.Parent_ID == x.ID)).ToList();
                    data.Analysis = data.Analysis
                      .Where(x => !commdata.Analysis.Any(y => y.Parent_ID == x.ID)).ToList();
                    data.Ports = data.Ports
                      .Where(x => !commdata.Ports.Any(y => y.Parent_ID == x.ID)).ToList();


                }


                TakenEx_CounstrainDataDTO takenEx_CounstrainDataDTO = new TakenEx_CounstrainDataDTO();
                takenEx_CounstrainDataDTO.noExCountryConstainDisplay = data;
                takenEx_CounstrainDataDTO.yesExCountryConstainDisplay = commdata;





                return uow.Repository<Object>()
                        .DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, takenEx_CounstrainDataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, lists);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }




        }


        public ExCountryConstainDisplay getData(int constrainOwner_ID,
                   long item_ShortName_id, long itemCategories_ID)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();

            #region getperv
            var data = (
                    from Ecc in db.Ex_CountryConstrain


                    where
                    Ecc.User_Deletion_Id == null
                    && Ecc.IsActive == true
                    && Ecc.User_Deletion_Date == null
                    && Ecc.ConstrainOwner_ID == constrainOwner_ID
                     && Ecc.Item_ShortName_id == item_ShortName_id
                    && Ecc.ItemCategories_ID == itemCategories_ID
                    && Ecc.User_Updation_Date == null
                    && Ecc.TransportCountry_ID == null



                    select new ExCountryConstainDisplay
                    {

                        CountryConstrain_ID = Ecc.ID,




                    }

                    ).SingleOrDefault();


            if (data != null)
            {
                var CountryConstrain_ID = data.CountryConstrain_ID;
                #region MyRegion

                var texts = (
                from CCt in db.Ex_CountryConstrain_Text

                where
                CCt.User_Deletion_Id == null
                 && CCt.User_Deletion_Date == null
              && CCt.CountryConstrain_ID == CountryConstrain_ID

                select new ExConstrainsText
                {
                    ID = CCt.ID,
                    ConstrainText_Ar = CCt.ConstrainText_Ar,
                    ConstrainText_En = CCt.ConstrainText_En,
                    IsCertificate_Addtion = (bool)CCt.IsCertificate_Addtion,
                    InSide_Certificate_Ar = CCt.InSide_Certificate_Ar,
                    InSide_Certificate_En = CCt.InSide_Certificate_En
                    ,
                    Parent_ID = CCt.Parent_ID

                }

                ).ToList();
                #endregion



                #region MyRegion
                var labs = (
                             from CCA in db.Ex_CountryConstrain_AnalysisLabType
                             join Al in db.AnalysisLabTypes
                             on CCA.AnalysisLabTypeID equals Al.ID

                             join At in db.AnalysisTypes on
                            Al.AnalysisTypeID equals At.ID

                             join v in db.AnalysisLabs on
                     Al.AnalysisLabID equals v.ID


                             where
                             CCA.User_Deletion_Id == null
                             //  && CCA.IsActive == true
                             && CCA.User_Deletion_Date == null
                             && CCA.CountryConstrain_ID == CountryConstrain_ID
                           && Al.User_Deletion_Date == null
                           && Al.User_Deletion_Id == null
                           && At.User_Deletion_Id == null
                           && At.User_Deletion_Date == null
                           && v.User_Deletion_Id == null
                           && v.User_Deletion_Date == null




                             select new ExConstrainsLabsAndTyp
                             {
                                 ID = CCA.ID,
                                 LabName_Ar = v.Name_Ar,
                                 LabName_En = v.Name_En,
                                 TypeName_Ar = At.Name_Ar,
                                 TypeName_En = At.Name_En,
                                 ExConstrainsLabsAndTypID = Al.ID

                                 ,
                                 Parent_ID = CCA.Parent_ID

                             }

                             ).ToList();

                #endregion


                #region MyRegion

                var ports = (
                         from CCA in db.Ex_CountryConstrain_ArrivalPort
                         join pil in db.Port_International
                         on CCA.Port_International_Id equals pil.ID

                         join Ci in db.Countries on
                        pil.Country_ID equals Ci.ID

                         join v in db.Port_Type on
                 pil.PortTypeID equals v.ID


                         where
                         CCA.User_Deletion_Id == null
                         && CCA.User_Deletion_Date == null
                         && CCA.Ex_CountryConstrain_Id == CountryConstrain_ID
                       && pil.User_Deletion_Date == null
                       && pil.User_Deletion_Id == null
                       && Ci.User_Deletion_Id == null
                       && Ci.User_Deletion_Date == null
                       && v.User_Deletion_Id == null
                       && v.User_Deletion_Date == null




                         select new ExConstrainsAirPortAndCountry
                         {
                             ID = CCA.Id,
                             AirPortName_Ar = v.Name_Ar,
                             AirPortName_En = v.Name_En,
                             CountryName_Ar = pil.Name_Ar,
                             CountryLabName_En = pil.Name_En,
                             ExConstrainsAirPortAndCountryID = pil.ID

                             ,
                             Parent_ID = CCA.Parent_ID

                         }

                         ).ToList();

                #endregion

                #endregion




                Analysis.AddRange(labs);
                Text.AddRange(texts);
                Portss.AddRange(ports);

            }

            return data;

        }



    }
}