using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;

using PlantQuar.DTO.DTO.ExportRequest;

using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class CheckReq_FeesBLL : IGenericBLL<Ex_CheckRequest_FeesDTO>
    {
        private UnitOfWork uow;
        public CheckReq_FeesBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetExportFees(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                PlantQuarantineEntities entity = new PlantQuarantineEntities();
                var data = (from fixedfees in entity.FeesAmount_Fixed
                                // join feesTypes in entity.FeesTypes on fixedfees.FeesType_ID equals feesTypes.ID
                            where fixedfees.IsActive == true && fixedfees.IsExport == true && fixedfees.IsMandatory == true && fixedfees.IsActive == true && fixedfees.User_Deletion_Id == null && fixedfees.User_Deletion_Id == null
                            select new Custom_CheckRequest_Fees
                            {
                                FixedFeesAmount_ID = fixedfees.ID,
                                FeesTypeName =(lang=="1"? fixedfees.Name_Ar: fixedfees.Name_En),
                                FeeValue = fixedfees.Amount
                            }).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.FeesTypeName).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //********************************************************//
        public Dictionary<string, object> GetPlantFees(List<Custom_ExPlants> plants, int importercountry, int transitcountry, List<string> Device_Info)
        {
            try
            {
                List<Custom_CheckRequest_Fees> fees = new List<Custom_CheckRequest_Fees>();
                foreach (var plant in plants)
                {
                    var treatment = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                           ("GetPlantConstrain_TreatmentFees @Plant_ID, @PlantPart_ID, @ProductStatus_ID, @Purpose_ID, @ImportCountry, @TransitCountry",
                             new SqlParameter("Plant_ID", SqlDbType.BigInt) { Value = (Int64)plant.Plant_ID },
                             new SqlParameter("PlantPart_ID", SqlDbType.TinyInt) { Value = (byte)plant.PlantPartType_ID },
                             new SqlParameter("ProductStatus_ID", SqlDbType.TinyInt) { Value = (byte)plant.ProductStatus_ID },
                             new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = (byte)plant.Purpose_ID },
                             new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                             new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }                      
                           ).ToList();

                    var analysis = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                           ("GetPlantConstrain_AnalysisFees @Plant_ID, @PlantPart_ID, @ProductStatus_ID, @Purpose_ID, @ImportCountry, @TransitCountry",
                             new SqlParameter("Plant_ID", SqlDbType.BigInt) { Value = (Int64)plant.Plant_ID },
                             new SqlParameter("PlantPart_ID", SqlDbType.TinyInt) { Value = (byte)plant.PlantPartType_ID },
                             new SqlParameter("ProductStatus_ID", SqlDbType.TinyInt) { Value = (byte)plant.ProductStatus_ID },
                             new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = (byte)plant.Purpose_ID },
                             new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                             new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }
                           ).ToList();

                    if (treatment.Count > 0)
                        fees.AddRange(treatment);
                    if (analysis.Count > 0)
                        fees.AddRange(analysis);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, fees.OrderBy(a => a.FeesTypeName).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }       
        public Dictionary<string, object> GetProductFees(List<Custom_ExProducts> products, int importercountry, int transitcountry, List<string> Device_Info)
        {
            try
            {              
                List<Custom_CheckRequest_Fees> fees = new List<Custom_CheckRequest_Fees>();

                foreach (var product in products)
                {
                    var treatment = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                            ("GetProductConstrain_TreatmentFees @Product_ID, @ProductStatus_ID, @Purpose_ID, @ImportCountry, @TransitCountry",
                              new SqlParameter("Product_ID", SqlDbType.BigInt) { Value = (Int64)product.Plant_ID },
                              new SqlParameter("ProductStatus_ID", SqlDbType.TinyInt) { Value = (byte)product.ProductStatus_ID },
                              new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = (byte)product.Purpose_ID },
                              new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                              new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }
                            ).ToList();

                    var analysis = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                           ("GetProductConstrain_AnalysisFees @Product_ID, @ProductStatus_ID, @Purpose_ID, @ImportCountry, @TransitCountry",
                             new SqlParameter("Product_ID", SqlDbType.BigInt) { Value = (Int64)product.Plant_ID },
                             new SqlParameter("ProductStatus_ID", SqlDbType.TinyInt) { Value = (byte)product.ProductStatus_ID },
                             new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = (byte)product.Purpose_ID },
                             new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                             new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }
                           ).ToList();

                    if (treatment.Count > 0)
                        fees.AddRange(treatment);
                    if (analysis.Count > 0)
                        fees.AddRange(analysis);
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, fees.OrderBy(a => a.FeesTypeName).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAliveFees(List<Custom_ExAliveLiableItems> alives, int importercountry, int transitcountry, List<string> Device_Info)
        {
            try
            {
                List<Custom_CheckRequest_Fees> fees = new List<Custom_CheckRequest_Fees>();
                foreach (var alive in alives)
                {
                    var treatment = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                           ("GetLiableItems_AliveConstrain_TreatmentFees @AliveID, @BiologicalPhase_ID, @Purpose_ID, @LiableStatus_ID, @ImportCountry, @TransitCountry",
                             new SqlParameter("AliveID", SqlDbType.BigInt) { Value = (Int64)alive.alive_ID },
                             new SqlParameter("BiologicalPhase_ID", SqlDbType.Int) { Value = alive.BiologicalPhase },
                             new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = alive.Purpose_ID },
                             new SqlParameter("LiableStatus_ID", SqlDbType.Int) { Value = alive.Status_ID },
                             new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                             new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }
                           ).ToList();

                    var analysis = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                            ("GetLiableItems_AliveConstrain_AnalysisFees @AliveID, @BiologicalPhase_ID, @Purpose_ID, @LiableStatus_ID, @ImportCountry, @TransitCountry",
                             new SqlParameter("AliveID", SqlDbType.BigInt) { Value = (Int64)alive.alive_ID },
                             new SqlParameter("BiologicalPhase_ID", SqlDbType.Int) { Value = alive.BiologicalPhase },
                             new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = alive.Purpose_ID },
                             new SqlParameter("LiableStatus_ID", SqlDbType.Int) { Value = alive.Status_ID },
                             new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                             new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }
                           ).ToList();

                    if (treatment.Count > 0)
                        fees.AddRange(treatment);
                    if (analysis.Count > 0)
                        fees.AddRange(analysis);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, fees.OrderBy(a => a.FeesTypeName).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetNotAliveFees(List<Custom_ExNotAliveLiableItems> notAlives, int importercountry, int transitcountry, List<string> Device_Info)
        {
            try
            {                
                List<Custom_CheckRequest_Fees> fees = new List<Custom_CheckRequest_Fees>();

                foreach (var not_alive in notAlives)
                {
                    var treatment = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                           ("GetLiableItems_NotAliveConstrain_TreatmentFees @NotAliveID, @Purpose_ID, @LiableStatus_ID, @ImportCountry, @TransitCountry",
                             new SqlParameter("NotAliveID", SqlDbType.BigInt) { Value = (Int64)not_alive.notAlive_ID },
                             new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = not_alive.Purpose_ID },
                             new SqlParameter("LiableStatus_ID", SqlDbType.Int) { Value = not_alive.Status_ID },
                             new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                             new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }
                           ).ToList();

                    var analysis = uow.Repository<Custom_CheckRequest_Fees>().SQLQuery
                            ("GetLiableItems_NotAliveConstrain_AnalysisFees @NotAliveID, @Purpose_ID, @LiableStatus_ID, @ImportCountry, @TransitCountry",
                             new SqlParameter("NotAliveID", SqlDbType.BigInt) { Value = (Int64)not_alive.notAlive_ID },
                             new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = not_alive.Purpose_ID },
                             new SqlParameter("LiableStatus_ID", SqlDbType.Int) { Value = not_alive.Status_ID },
                             new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importercountry },
                             new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitcountry }
                           ).ToList();

                    if (treatment.Count > 0)
                        fees.AddRange(treatment);
                    if (analysis.Count > 0)
                        fees.AddRange(analysis);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, fees.OrderBy(a => a.FeesTypeName).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //********************************************************//
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Ex_CheckRequest_FeesDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(Ex_CheckRequest_FeesDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> InsertList(List<Ex_CheckRequest_FeesDTO> fees, List<string> Device_Info)
        {
            try
            {
                foreach (var entity in fees)
                {
                    var CModel = Mapper.Map<Ex_CheckRequest_Fees>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_Fees_Seq");
                    uow.Repository<Ex_CheckRequest_Fees>().InsertRecord(CModel);
                    uow.SaveChanges();
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, fees.FirstOrDefault());
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        public Dictionary<string, object> Update(Ex_CheckRequest_FeesDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        #region TreatmentAnalysis Fees Linq
        /*
        public Dictionary<string, object> GetPlantFees(List<Custom_ExPlants> plants, int importercountry, int transitcountry, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entity = new PlantQuarantineEntities();

                List<short> countries = new List<short>();
                countries.Add((short)importercountry);
                countries.Add((short)transitcountry);

                List<short> unions = (from union in entity.Union_Country where countries.Contains(union.Country_ID) select union.Union_ID).ToList();

                List<Custom_CheckRequest_Fees> fees = new List<Custom_CheckRequest_Fees>();

                foreach (var plant in plants)
                {
                    var treatment = (from constrain in entity.Ex_CountryConstrain
                                     join plantTreatment in entity.Ex_CountryConstrain_Treatment on constrain.ID equals plantTreatment.CountryConstrain_ID
                                     join plantConstrain in entity.Con_Ex_Im_Plants on constrain.ID equals plantConstrain.Con_Ex_Im_ID                                    
                                     join feesAmount in entity.FixedFeesAmounts on plantTreatment.TreatmentType_ID equals feesAmount.TreatmentType_ID
                                     join feeType in entity.FeesTypes on feesAmount.FeesType_ID equals feeType.ID
                                     join treatmentType in entity.TreatmentTypes on plantTreatment.TreatmentType_ID equals treatmentType.ID

                                     where plantConstrain.ItemType_ID == 36 
                                     && constrain.IsTreatment == true && constrain.ProdPlant_ID == plant.Plant_ID
                                     && constrain.IsPlant == 4 
                                     && plantConstrain.PlantPartType_ID == plant.PlantPartType_ID
                                     && plantConstrain.ProductStatus_ID == plant.ProductStatus_ID 
                                     && plantConstrain.Purpose_ID == plant.Purpose_ID
                                     && feesAmount.IsExport == true && feeType.IsTreatment == true
                                     && feesAmount.IsActive == true && feesAmount.IsMandatory == false
                                     && feesAmount.User_Deletion_Id == null 
                                     && feeType.User_Deletion_Id == null
                                     && constrain.User_Deletion_Id == null 
                                     && plantTreatment.User_Deletion_Id == null
                                     && 
                                     ((constrain.CountryConstrain_Type == 1 && constrain.ConstrainOwner_ID == 0)
                                       || (constrain.CountryConstrain_Type == 2 && countries.Contains((short)constrain.ConstrainOwner_ID))
                                       || (constrain.CountryConstrain_Type == 3 && unions.Contains((short)constrain.ConstrainOwner_ID))
                                     )

                                     select new Custom_CheckRequest_Fees
                                     {
                                         FixedFeesAmount_ID = feesAmount.ID,
                                         FeesTypeName = feeType.Name_Ar + " " + treatmentType.Ar_Name,
                                         FeeValue = feesAmount.Amount

                                     }).ToList();

                    var analysis = (from feesAmount in entity.FixedFeesAmounts
                                    join feeType in entity.FeesTypes on feesAmount.FeesType_ID equals feeType.ID

                                    from constrain in entity.Ex_CountryConstrain
                                    join plantAnalysis in entity.Ex_CountryConstrain_AnalysisLabType on constrain.ID equals plantAnalysis.CountryConstrain_ID
                                    join plantConstrain in entity.Con_Ex_Im_Plants on constrain.ID equals plantConstrain.Con_Ex_Im_ID

                                    join analysisLabType in entity.AnalysisLabTypes on plantAnalysis.AnalysisLabTypeID equals analysisLabType.ID
                                    join analysisT in entity.AnalysisTypes on analysisLabType.AnalysisTypeID equals analysisT.ID

                                    where plantConstrain.ItemType_ID == 36 
                                    && constrain.IsAnalysis == true 
                                    && constrain.IsPlant == 4
                                    && constrain.IsActive == true
                                    && constrain.User_Deletion_Id == null
                                    && constrain.ProdPlant_ID == plant.Plant_ID 
                                    && plantConstrain.PlantPartType_ID == plant.PlantPartType_ID
                                    && plantConstrain.ProductStatus_ID == plant.ProductStatus_ID 
                                    && plantConstrain.Purpose_ID == plant.Purpose_ID
                                    && feesAmount.IsExport == true
                                     && feesAmount.IsMandatory == false
                                    && feesAmount.IsActive == true 
                                    && feesAmount.User_Deletion_Id == null 
                                    && feeType.User_Deletion_Id == null
                                    && feeType.IsTreatment == false
                                    && analysisLabType.User_Deletion_Id == null
                                     &&
                                     ((constrain.CountryConstrain_Type == 1 && constrain.ConstrainOwner_ID == 0)
                                       || (constrain.CountryConstrain_Type == 2 && countries.Contains((short)constrain.ConstrainOwner_ID))
                                       || (constrain.CountryConstrain_Type == 3 && unions.Contains((short)constrain.ConstrainOwner_ID))
                                     )

                                    select new Custom_CheckRequest_Fees
                                    {
                                        FixedFeesAmount_ID = feesAmount.ID,
                                        FeesTypeName = feeType.Name_Ar + " " +analysisT.Name_Ar,
                                        FeeValue = feesAmount.Amount

                                    }).ToList();

                    fees.AddRange(treatment);
                    fees.AddRange(analysis);
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, fees.OrderBy(a => a.FixedFeesAmount_ID).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }           
        }*/
        #endregion
    }
}