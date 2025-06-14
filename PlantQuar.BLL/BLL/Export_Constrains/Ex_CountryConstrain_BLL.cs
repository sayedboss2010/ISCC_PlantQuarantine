using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Countries;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Export_Constrains
{

    public class Ex_CountryConstrain_BLL : IGenericBLL<Ex_CountryConstrainDTO>
    {
        private UnitOfWork uow;
        public Ex_CountryConstrain_BLL()
        {

            uow = new UnitOfWork();
        }

        #region Fill_List

        public Dictionary<string, object> CountryFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Ar_Name : c.Ar_Name),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = 0 });
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> AnalysisTypeFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<AnalysisType>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_Ar),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }

        public Dictionary<string, object> UnionFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Union>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Ar_Name : c.Ar_Name),
                Value = c.ID
            }).OrderBy(f => f.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = 0 });
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.ToList());

        }

        public Dictionary<string, object> GetCountriesUnion_Name(int CountriesUnion_Id, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            if (CountriesUnion_Id > 0)
            {
                var data = uow.Repository<Union_Country>().GetData()
               .Where(c => c.IsActive == true && c.User_Deletion_Id == null && c.Union_ID == CountriesUnion_Id
               && c.Country.IsActive == true && c.Country.User_Deletion_Id == null)
               .Select(c => new CustomOption
               {
                   DisplayText = (lang == "1" ? c.Country.Ar_Name : c.Country.En_Name),
                   Value = c.Country.ID
               }).OrderBy(a => a.DisplayText).ToList();
                //            data.Insert()
                data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = 0 });
                data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "---------" : "---------"), Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            else
            {
                var data = uow.Repository<Country>().GetData()
               .Where(c => c.IsActive == true && c.User_Deletion_Id == null)
               .Select(c => new CustomOption
               {
                   DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                   Value = c.ID
               }).OrderBy(a => a.DisplayText).ToList();
                //            data.Insert()
                data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "---- -----" : "-- ----"), Value =-1 });
                data.Insert(1, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = 0 });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);

        }

        #endregion

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

        public Dictionary<string, object> Union_NameFillDrop_Edit(int Union_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Union>().GetData().Where(a => a.ID == Union_ID && a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {

                DisplayText = (lang == "1" ? c.Ar_Name : c.Ar_Name)

            }).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());


        }

        public Dictionary<string, object> Update(Ex_CountryConstrainDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        //****************
        // insert yes only 

        private void updateAny(short? constrainOwner_ID, short? transportCountry_ID
            , long User_Updation_Id
            , long Item_ShortName_id, long? ItemCategories_ID)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            var p = db.Ex_CountryConstrain.Where(x => x.TransportCountry_ID == transportCountry_ID
            && x.Import_Country_ID == constrainOwner_ID 
            && x.IsActive == true
            && x.Item_ShortName_id== Item_ShortName_id
            && x.ItemCategories_ID == ItemCategories_ID
            ).FirstOrDefault();
            if (p != null)
            {
                p.IsActive = false;
                p.User_Updation_Id = User_Updation_Id;
                p.User_Updation_Date = DateTime.Now;
                db.SaveChanges();
            }
            // Ex_CountryConstrain dtoConstrain = uow.Repository<Ex_CountryConstrain>().Findobject(constrains.CountryConstrainsDTO.ID);
        }


        public Dictionary<string, object> InsertCustomConstrainPro(Ex_CountryConstrainDTO constrains, List<string> Device_Info)
        {
            try
            {
               
                    if (constrains.Import_Country_ID == null)
                    constrains.Import_Country_ID = 0;

                List<Union_CountryDTO> List_Union_Country = new List<Union_CountryDTO>();
                // دول الاتحاد
                ////Hadeer
                if (constrains.Union_Id > 0 && constrains.Import_Country_ID == 0)
                {
                    List_Union_Country = uow.Repository<Union_Country>().GetData()
                                   .Where(c => c.IsActive == true && c.User_Deletion_Id == null && c.Union_ID == constrains.Union_Id
                                   && c.Country.IsActive == true && c.Country.User_Deletion_Id == null)
                                   .Select(c => new Union_CountryDTO
                                   {
                                       Union_ID = c.Union_ID,
                                       Country_ID = c.Country_ID,
                                   }).ToList();
                }
                else
                {
                    Union_CountryDTO testdata = new Union_CountryDTO();

                    testdata.ID = 0;//short.Parse(constrains.Import_Country_ID.ToString());
                    testdata.Country_ID = (short)constrains.Import_Country_ID;//short.Parse(constrains.Import_Country_ID.ToString());
                    testdata.Union_ID = 0;
                    testdata.IsActive = true;
                    testdata.User_Creation_Id = 0;
                    testdata.User_Creation_Date = DateTime.Now;

                    List_Union_Country.Add(testdata);
                }

                foreach (var item in List_Union_Country)
                {
                    if (constrains.Union_Id == 0 ||( constrains.Union_Id > 0 && constrains.Import_Country_ID> 0))
                    {
                        updateAny(item.Country_ID, constrains.TransportCountry_ID, constrains.User_Creation_Id
                            , constrains.Item_ShortName_id, constrains.ItemCategories_ID);
                    }
                    else
                    {
                        PlantQuarantineEntities db = new PlantQuarantineEntities();

                        var IDold = db.Ex_CountryConstrain.Where(x => x.TransportCountry_ID == constrains.TransportCountry_ID
                                   && x.Import_Country_ID == item.Country_ID
                                   && x.IsActive == true
                                   && x.Item_ShortName_id == constrains.Item_ShortName_id
                                   && x.ItemCategories_ID == constrains.ItemCategories_ID
                                   ).FirstOrDefault();
                        if( IDold != null )
                        item.IDold = IDold.ID;
                        else item.IDold = 0;
                    }
                }
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {

                        //////// End Hadeer
                        int x = 0;
                        foreach (var item in List_Union_Country)
                        {
                            //var p = context.Ex_CountryConstrain.Where(a => a.TransportCountry_ID == constrains.TransportCountry_ID
                            //                        && a.Import_Country_ID == item.Country_ID
                            //                        && a.IsActive == true
                            //                        && a.Item_ShortName_id == constrains.Item_ShortName_id
                            //                        && a.ItemCategories_ID == constrains.ItemCategories_ID
                            //                        ).FirstOrDefault();
                            //if (p != null)
                            //{

                            //    p.IsActive = false;
                            //    p.User_Updation_Id = constrains.User_Creation_Id;
                            //    p.User_Updation_Date = DateTime.Now;
                            //    constrains.SaveChanges();
                            //}

                            //updateAny(item.Country_ID
                            //    , constrains.TransportCountry_ID
                            //    , constrains.User_Creation_Id
                            //    , constrains.Item_ShortName_id
                            //    , constrains.ItemCategories_ID);
                         
                            if (constrains.CountryConstrain_TextDTO != null)
                            {
                                x = 1;
                            }
                            if (constrains.AnalysisLabType != null)
                            {
                                x = 1;
                            }
                            if (constrains.ConstraintAirPortInternational != null)
                            {
                                x = 1;
                            }
                            if (constrains.Constraint_Treatment != null)
                            {
                                x = 1;
                            }
                            if (x == 1)
                            {
                                long Ex_CountryConstrain = item.IDold;
                                if (item.IDold == 0)
                                {
                                   
                                    var newConstrain = Mapper.Map<Ex_CountryConstrain>(constrains);
                                    Ex_CountryConstrain= uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
                                    newConstrain.ID = Ex_CountryConstrain;
                                    newConstrain.IsActive = true;
                                    newConstrain.Import_Country_ID = item.Country_ID;
                                    if (constrains.TransportCountry_ID != null || constrains.TransportCountry_ID != 0)
                                    {
                                        newConstrain.TransportCountry_ID = constrains.TransportCountry_ID;
                                    }
                                    newConstrain = uow.Repository<Ex_CountryConstrain>().InsertReturn(newConstrain);
                                    // insert to db
                                    context.Ex_CountryConstrain.Add(newConstrain);
                                    context.SaveChanges();
                                }


                                //uow.SaveChanges();
                                //text
                                if (constrains.CountryConstrain_TextDTO != null)
                                {
                                    for (int i = 0; i < constrains.CountryConstrain_TextDTO.Count; i++)
                                    {
                                      
                                        var newConstrainText = Mapper.Map<Ex_CountryConstrain_Text>
                                            (constrains.CountryConstrain_TextDTO[i]);
                                        newConstrainText.Parent_ID = constrains.CountryConstrain_TextDTO[i].Parent_ID;

                                        newConstrainText.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Text_SEQ");
                                        newConstrainText.IsActive = true;
                                        newConstrainText.User_Creation_Date = constrains.User_Creation_Date;
                                        newConstrainText.User_Creation_Id = constrains.User_Creation_Id;
                                        newConstrainText.CountryConstrain_ID = Ex_CountryConstrain;

                                        newConstrainText = uow.Repository<Ex_CountryConstrain_Text>().InsertReturn(newConstrainText);

                                        context.Ex_CountryConstrain_Text.Add(newConstrainText);
                                        context.SaveChanges();
                                        //uow.SaveChanges();
                                    }
                                }
                                // labType
                                if (constrains.AnalysisLabType != null)
                                {
                                    for (int i = 0; i < constrains.AnalysisLabType.Count; i++)
                                    {
                                        var newConstrainAnalysisLabType = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>
                                            (constrains.AnalysisLabType[i]);
                                        newConstrainAnalysisLabType.Parent_ID = constrains.AnalysisLabType[i].Parent_ID;
                                        newConstrainAnalysisLabType.ID = uow.Repository<Object>()
                                       .GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_SEQ");

                                        newConstrainAnalysisLabType.User_Creation_Date = constrains.User_Creation_Date;
                                        newConstrainAnalysisLabType.User_Creation_Id = constrains.User_Creation_Id;
                                        newConstrainAnalysisLabType.CountryConstrain_ID = Ex_CountryConstrain;
                                        newConstrainAnalysisLabType.IsAcive = true;
                                        newConstrainAnalysisLabType = uow.Repository<Ex_CountryConstrain_AnalysisLabType>()
                                         .InsertReturn(newConstrainAnalysisLabType);

                                        context.Ex_CountryConstrain_AnalysisLabType.Add(newConstrainAnalysisLabType);
                                        context.SaveChanges();
                                        // uow.SaveChanges();

                                    }
                                }
                                //AirPort
                                if (constrains.ConstraintAirPortInternational != null)
                                {
                                    for (int i = 0; i < constrains.ConstraintAirPortInternational.Count; i++)
                                    {
                                        var newConstrainAirPort = Mapper.Map<Ex_CountryConstrain_ArrivalPort>
                                            (constrains.ConstraintAirPortInternational[i]);
                                        newConstrainAirPort.Parent_ID = constrains.ConstraintAirPortInternational[i].Parent_ID;

                                        newConstrainAirPort.Id = uow.Repository<Object>()
                                       .GetNextSequenceValue_Long("Ex_CountryConstrain_ArrivalPort_SEQ");
                                        newConstrainAirPort.User_Creation_Date = constrains.User_Creation_Date;
                                        newConstrainAirPort.User_Creation_Id = constrains.User_Creation_Id;
                                        newConstrainAirPort.Ex_CountryConstrain_Id = Ex_CountryConstrain;
                                        newConstrainAirPort.IsActive = true;
                                        newConstrainAirPort = uow.Repository<Ex_CountryConstrain_ArrivalPort>()
                                         .InsertReturn(newConstrainAirPort);
                                        context.Ex_CountryConstrain_ArrivalPort.Add(newConstrainAirPort);
                                        context.SaveChanges();

                                        // uow.SaveChanges();
                                    }
                                }

                                //Constraint_Treatment
                                if (constrains.Constraint_Treatment != null)
                                {
                                    for (int i = 0; i < constrains.Constraint_Treatment.Count; i++)
                                    {
                                        var newConstrain_Treatment = Mapper.Map<Ex_CountryConstrain_Treatment>(constrains.Constraint_Treatment[i]);
                                        newConstrain_Treatment.Parent_ID = constrains.Constraint_Treatment[i].Parent_ID;

                                        newConstrain_Treatment.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_SEQ");
                                        newConstrain_Treatment.User_Creation_Date = constrains.User_Creation_Date;
                                        newConstrain_Treatment.User_Creation_Id = constrains.User_Creation_Id;
                                        newConstrain_Treatment.CountryConstrain_ID = Ex_CountryConstrain;
                                        newConstrain_Treatment.IsAcive = true;

                                        newConstrain_Treatment = uow.Repository<Ex_CountryConstrain_Treatment>()
                                         .InsertReturn(newConstrain_Treatment);
                                        context.Ex_CountryConstrain_Treatment.Add(newConstrain_Treatment);
                                        context.SaveChanges();

                                        // uow.SaveChanges();
                                    }
                                }
                                
                            }
                        }
                      if(  x == 1)
                        {
                            trans.Commit();
                        }

                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrains.ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }




        //public Dictionary<string, object> InsertCustomConstrainPro(Ex_CountryConstrainDTO constrains, List<string> Device_Info)
        //{
        //    try
        //    {

        //        if (constrains.Import_Country_ID == null)
        //            constrains.Import_Country_ID = 0;               

        //        using (PlantQuarantineEntities context = new PlantQuarantineEntities())
        //        {
        //            using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
        //            {
        //                List<Union_CountryDTO> List_Union_Country = new List<Union_CountryDTO>();
        //                // دول الاتحاد
        //                ////Hadeer
        //                if (constrains.Union_Id > 0 && constrains.Import_Country_ID == 0)
        //                {
        //                    List_Union_Country = uow.Repository<Union_Country>().GetData()
        //                                   .Where(c => c.IsActive == true && c.User_Deletion_Id == null && c.Union_ID == constrains.Union_Id
        //                                   && c.Country.IsActive == true && c.Country.User_Deletion_Id == null)
        //                                   .Select(c => new Union_CountryDTO
        //                                   {
        //                                       Union_ID = c.Union_ID,
        //                                       Country_ID = c.Country_ID,
        //                                   }).ToList();
        //                }
        //                else
        //                {
        //                    Union_CountryDTO testdata= new Union_CountryDTO();

        //                    testdata.ID = 0;//short.Parse(constrains.Import_Country_ID.ToString());
        //                    testdata.Country_ID = (short)constrains.Import_Country_ID ;//short.Parse(constrains.Import_Country_ID.ToString());
        //                    testdata.Union_ID = 0;
        //                    testdata.IsActive = true;    
        //                    testdata.User_Creation_Id = 0;
        //                    testdata.User_Creation_Date = DateTime.Now;

        //                    List_Union_Country.Add(testdata);
        //                }
        //                //////// End Hadeer

        //                foreach (var item in List_Union_Country)
        //                {

        //                    updateAny(item.Country_ID
        //                        , constrains.TransportCountry_ID
        //                        , constrains.User_Creation_Id
        //                        , constrains.Item_ShortName_id
        //                        , constrains.ItemCategories_ID);

        //                    var newConstrain = Mapper.Map<Ex_CountryConstrain>(constrains);
        //                    newConstrain.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_seq");
        //                    newConstrain.IsActive = true;
        //                    newConstrain.Import_Country_ID = item.Country_ID;
        //                    if (constrains.TransportCountry_ID != null || constrains.TransportCountry_ID != 0)
        //                    {
        //                        newConstrain.TransportCountry_ID = constrains.TransportCountry_ID;
        //                    }
        //                    newConstrain = uow.Repository<Ex_CountryConstrain>().InsertReturn(newConstrain);
        //                    // insert to db
        //                    context.Ex_CountryConstrain.Add(newConstrain);
        //                    context.SaveChanges();
        //                    //uow.SaveChanges();
        //                    //text
        //                    if (constrains.CountryConstrain_TextDTO != null)
        //                    {
        //                        for (int i = 0; i < constrains.CountryConstrain_TextDTO.Count; i++)
        //                        {
        //                            var newConstrainText = Mapper.Map<Ex_CountryConstrain_Text>
        //                                (constrains.CountryConstrain_TextDTO[i]);
        //                            newConstrainText.Parent_ID = constrains.CountryConstrain_TextDTO[i].Parent_ID;

        //                            newConstrainText.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Text_SEQ");
        //                            newConstrainText.IsActive = true;
        //                            newConstrainText.User_Creation_Date = newConstrain.User_Creation_Date;
        //                            newConstrainText.User_Creation_Id = newConstrain.User_Creation_Id;
        //                            newConstrainText.CountryConstrain_ID = newConstrain.ID;

        //                            newConstrainText = uow.Repository<Ex_CountryConstrain_Text>().InsertReturn(newConstrainText);

        //                            context.Ex_CountryConstrain_Text.Add(newConstrainText);
        //                            context.SaveChanges();
        //                            //uow.SaveChanges();
        //                        }
        //                    }
        //                    // labType
        //                    if (constrains.AnalysisLabType != null)
        //                    {
        //                        for (int i = 0; i < constrains.AnalysisLabType.Count; i++)
        //                        {
        //                            var newConstrainAnalysisLabType = Mapper.Map<Ex_CountryConstrain_AnalysisLabType>
        //                                (constrains.AnalysisLabType[i]);
        //                            newConstrainAnalysisLabType.Parent_ID = constrains.AnalysisLabType[i].Parent_ID;
        //                            newConstrainAnalysisLabType.ID = uow.Repository<Object>()
        //                           .GetNextSequenceValue_Long("Ex_CountryConstrain_AnalysisLabType_SEQ");

        //                            newConstrainAnalysisLabType.User_Creation_Date = newConstrain.User_Creation_Date;
        //                            newConstrainAnalysisLabType.User_Creation_Id = newConstrain.User_Creation_Id;
        //                            newConstrainAnalysisLabType.CountryConstrain_ID = newConstrain.ID;
        //                            newConstrainAnalysisLabType.IsAcive = true;
        //                            newConstrainAnalysisLabType = uow.Repository<Ex_CountryConstrain_AnalysisLabType>()
        //                             .InsertReturn(newConstrainAnalysisLabType);

        //                            context.Ex_CountryConstrain_AnalysisLabType.Add(newConstrainAnalysisLabType);
        //                            context.SaveChanges();
        //                            // uow.SaveChanges();

        //                        }
        //                    }
        //                    //AirPort
        //                    if (constrains.ConstraintAirPortInternational != null)
        //                    {
        //                        for (int i = 0; i < constrains.ConstraintAirPortInternational.Count; i++)
        //                        {
        //                            var newConstrainAirPort = Mapper.Map<Ex_CountryConstrain_ArrivalPort>
        //                                (constrains.ConstraintAirPortInternational[i]);
        //                            newConstrainAirPort.Parent_ID = constrains.ConstraintAirPortInternational[i].Parent_ID;

        //                            newConstrainAirPort.Id = uow.Repository<Object>()
        //                           .GetNextSequenceValue_Long("Ex_CountryConstrain_ArrivalPort_SEQ");
        //                            newConstrainAirPort.User_Creation_Date = newConstrain.User_Creation_Date;
        //                            newConstrainAirPort.User_Creation_Id = newConstrain.User_Creation_Id;
        //                            newConstrainAirPort.Ex_CountryConstrain_Id = newConstrain.ID;
        //                            newConstrainAirPort.IsActive = true;
        //                            newConstrainAirPort = uow.Repository<Ex_CountryConstrain_ArrivalPort>()
        //                             .InsertReturn(newConstrainAirPort);
        //                            context.Ex_CountryConstrain_ArrivalPort.Add(newConstrainAirPort);
        //                            context.SaveChanges();

        //                            // uow.SaveChanges();
        //                        }
        //                    }

        //                    //Constraint_Treatment
        //                    if (constrains.Constraint_Treatment != null)
        //                    {
        //                        for (int i = 0; i < constrains.Constraint_Treatment.Count; i++)
        //                        {
        //                            var newConstrain_Treatment = Mapper.Map<Ex_CountryConstrain_Treatment>(constrains.Constraint_Treatment[i]);
        //                            newConstrain_Treatment.Parent_ID = constrains.Constraint_Treatment[i].Parent_ID;

        //                            newConstrain_Treatment.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_Treatment_SEQ");
        //                            newConstrain_Treatment.User_Creation_Date = newConstrain.User_Creation_Date;
        //                            newConstrain_Treatment.User_Creation_Id = newConstrain.User_Creation_Id;
        //                            newConstrain_Treatment.CountryConstrain_ID = newConstrain.ID;
        //                            newConstrain_Treatment.IsAcive = true;

        //                            newConstrain_Treatment = uow.Repository<Ex_CountryConstrain_Treatment>()
        //                             .InsertReturn(newConstrain_Treatment);
        //                            context.Ex_CountryConstrain_Treatment.Add(newConstrain_Treatment);
        //                            context.SaveChanges();

        //                            // uow.SaveChanges();
        //                        }
        //                    } trans.Commit();
        //                }

        //            }
        //        }
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrains.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public Dictionary<string, object> GetCustomConstrain(int Import_Country_ID, short? TransportCountry_ID,
         long item_ShortName_id, long? itemCategories_ID, bool isStationAccreditation,
        bool isFarmAccreditation, bool isCompanyAccreditation, List<string> lists)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            try
            {
                #region getperv
                var data = (from Ecc in db.Ex_CountryConstrain
                            where Ecc.User_Deletion_Id == null
                            && Ecc.IsActive == true
                            && Ecc.User_Deletion_Date == null
                            && Ecc.Import_Country_ID == Import_Country_ID
                            && Ecc.Item_ShortName_id == item_ShortName_id

                            && Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)

                            && Ecc.User_Updation_Date == null
                            && Ecc.TransportCountry_ID == (TransportCountry_ID > 0 ? TransportCountry_ID : null)  
                            select new Ex_CountryConstrainDTO
                            {
                                ID = Ecc.ID,
                                IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                                IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                                IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                                ItemCategories_ID = Ecc.ItemCategories_ID
                            }).FirstOrDefault();

                if(data == null && TransportCountry_ID == 0)
                {
                     data = (from Ecc in db.Ex_CountryConstrain
                                where Ecc.User_Deletion_Id == null
                                && Ecc.IsActive == true
                                && Ecc.User_Deletion_Date == null
                                && Ecc.Import_Country_ID == 0
                                && Ecc.Item_ShortName_id == item_ShortName_id

                                //&& Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)

                                && Ecc.User_Updation_Date == null
                                //&& Ecc.TransportCountry_ID == (TransportCountry_ID > 0 ? TransportCountry_ID : null)
                                select new Ex_CountryConstrainDTO
                                {
                                    ID = Ecc.ID,
                                    IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                                    IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                                    IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                                    ItemCategories_ID = Ecc.ItemCategories_ID
                                }).FirstOrDefault();
                }
                //if (itemCategories_ID > 0)
                //{
                //    data.wh;
                //}
                //else

                //{
                //    //data = data.FirstOrDefault();
                //}
                  

                if (data != null)
                {
                    var CountryConstrain_ID = data.ID;
                    var Constrain_Text = (from CCt in db.Ex_CountryConstrain_Text
                                          join Txt in db.EX_Constrain_Text on CCt.EX_Constrain_Text_ID equals Txt.ID
                                          join Typ in db.EX_Constrain_Country_Item on Txt.EX_Constrain_Country_Item_ID equals Typ.ID
                                          where CCt.User_Deletion_Id == null
                                           && CCt.User_Deletion_Date == null
                                        && CCt.CountryConstrain_ID == CountryConstrain_ID
                                          select new Ex_CountryConstrain_TextDTO
                                          {
                                              EX_Constrain_Text_ID = Txt.ID,
                                              Ar_Name_Constrain_Type = Typ.Ar_Name,
                                              En_Name_Constrain_Type = Typ.En_Name,
                                              ConstrainText_Ar = Txt.ConstrainText_Ar,
                                              ConstrainText_En = Txt.ConstrainText_En,
                                              IsCertificate_Addtion = (bool)Txt.IsCertificate_Addtion,
                                              InSide_Certificate_Ar = Txt.InSide_Certificate_Ar,
                                              InSide_Certificate_En = Txt.InSide_Certificate_En,
                                          }).ToList();

                    var Constrain_Analysis = (
                             from CCA in db.Ex_CountryConstrain_AnalysisLabType
                             join At in db.AnalysisTypes on CCA.AnalysisTypeID equals At.ID
                             where CCA.User_Deletion_Id == null
                             //  && CCA.IsActive == true
                             && CCA.User_Deletion_Date == null
                             && CCA.CountryConstrain_ID == CountryConstrain_ID
                           && At.User_Deletion_Id == null
                           && At.User_Deletion_Date == null

                             select new Ex_CountryConstrain_AnalysisLabTypeDTO
                             {
                                 AnalysisTypeID = CCA.AnalysisTypeID,
                                 TypeName_Ar = At.Name_Ar,
                                 TypeName_En = At.Name_En,
                                 ExConstrainsLabsAndTypID = CCA.ID
                             }).ToList();

                    var Constrain_ArrivalPort = (from CCA in db.Ex_CountryConstrain_ArrivalPort
                                                 join pil in db.Port_International on CCA.Port_International_Id equals pil.ID
                                                 join Ci in db.Countries on pil.Country_ID equals Ci.ID
                                                 join v in db.Port_Type on pil.PortTypeID equals v.ID
                                                 where CCA.User_Deletion_Id == null
                                                 && CCA.User_Deletion_Date == null
                                                 && CCA.Ex_CountryConstrain_Id == CountryConstrain_ID
                                               && pil.User_Deletion_Date == null
                                               && pil.User_Deletion_Id == null
                                               && Ci.User_Deletion_Id == null
                                               && Ci.User_Deletion_Date == null
                                               && v.User_Deletion_Id == null
                                               && v.User_Deletion_Date == null
                                                 select new Ex_CountryConstrain_ArrivalPortDTO
                                                 {
                                                     AirPortName_Ar = v.Name_Ar,
                                                     AirPortName_En = v.Name_En,
                                                     CountryName_Ar = pil.Name_Ar,
                                                     CountryLabName_En = pil.Name_En,
                                                     ExConstrainsAirPortAndCountryID = pil.ID
                                                 }).ToList();


                    var Constrain_Treatment = (from ect in db.Ex_CountryConstrain_Treatment
                                               where ect.User_Deletion_Id == null
                                               && ect.User_Deletion_Date == null
                                               && ect.CountryConstrain_ID == CountryConstrain_ID
                                             && ect.User_Deletion_Date == null
                                             && ect.User_Deletion_Id == null
                                               select new Ex_CountryConstrain_TreatmentDTO
                                               {
                                                   TreatmentMethod_Ar_Name = ect.TreatmentMethod.Ar_Name,
                                                   TreatmentMethod_En_Name = ect.TreatmentMethod.En_Name,

                                                   TreatmentType_Ar_Name = ect.TreatmentMethod.TreatmentType.Ar_Name,
                                                   TreatmentType_En_Name = ect.TreatmentMethod.TreatmentType.En_Name,

                                                   TreatmentMainType_Ar_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.Ar_Name,
                                                   TreatmentMainType_En_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.En_Name,
                                                   IS_Optional = ect.IS_Optional,
                                                   Parent_ID = ect.ID,
                                                   TreatmentMethods_ID=ect.TreatmentMethods_ID
                                               }).ToList();
                    //int c = 10;
                    data.AnalysisLabType = Constrain_Analysis;
                    data.CountryConstrain_TextDTO = Constrain_Text;
                    data.ConstraintAirPortInternational = Constrain_ArrivalPort;
                    data.Constraint_Treatment = Constrain_Treatment;

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

    }
}
