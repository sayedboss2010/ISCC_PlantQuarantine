using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Export_Certificate
{
    public class CertificateListBLL
    {
        private PlantQuarantineEntities entities = new PlantQuarantineEntities();
        private UnitOfWork uow;

        public CertificateListBLL()
        {

            uow = new UnitOfWork();
        }

        //public Dictionary<string, object> GetAllCertificates(string fromDate, string endDate, byte ISAccepted,string requestNumber, List<string> Device_Info)
        //{

        //    try
        //    {

        //        Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
        //        paramters_Type.Add("ISAccepted", SqlDbType.TinyInt);
        //        paramters_Type.Add("fromDate", SqlDbType.VarChar);
        //        paramters_Type.Add("toDate", SqlDbType.VarChar);
        //        paramters_Type.Add("requestNumber", SqlDbType.VarChar);

        //        Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
        //        paramters_Data.Add("ISAccepted", ISAccepted.ToString());
        //        paramters_Data.Add("fromDate", (fromDate!= null?fromDate:""));  //"2018-12-26"
        //        paramters_Data.Add("toDate", (endDate !=null ?endDate:""));  //"2018-12-26"
        //        paramters_Data.Add("requestNumber", (requestNumber != null ? requestNumber : ""));

        //        var data = uow.Repository<Certificate_Get_Data_ResultDTO>().CallStored("Certificate_Get_Data", paramters_Type,
        //        paramters_Data, Device_Info).ToList();



        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public Dictionary<string, object> GetAllCertificates(byte ISAccepted, string requestNumber, short? Country_Id, long? Company_Id, short? companyTypes, List<string> Device_Info)
        {
            //string fromDate, string endDate,

            try
            {
                string lang = Device_Info[2];

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("ISAccepted", SqlDbType.VarChar);
                //paramters_Type.Add("fromDate", SqlDbType.VarChar);
                //paramters_Type.Add("toDate", SqlDbType.VarChar);
                paramters_Type.Add("requestNumber", SqlDbType.VarChar);
                paramters_Type.Add("Country_Id", SqlDbType.VarChar);
                paramters_Type.Add("Company_Id", SqlDbType.VarChar);
                //paramters_Type.Add("lang", SqlDbType.VarChar);
                //paramters_Type.Add("lang", SqlDbType.VarChar);
                if (requestNumber == null)
                {
                    requestNumber = "";
                }
                if (Country_Id == null)
                {
                    Country_Id = -1;
                }
                if (Company_Id == null)
                {
                    Company_Id = -1;
                }
                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("ISAccepted", ISAccepted.ToString());
                //paramters_Data.Add("fromDate", (fromDate!= null?fromDate:""));  //"2018-12-26"
                //paramters_Data.Add("toDate", (endDate !=null ?endDate:""));  //"2018-12-26"
                paramters_Data.Add("requestNumber", (requestNumber != null ? requestNumber : ""));
                //paramters_Data.Add("requestNumber", requestNumber);
                paramters_Data.Add("Country_Id", Country_Id.ToString());
                paramters_Data.Add("Company_Id", Company_Id.ToString());

                var data = uow.Repository<Certificate_Get_Data_ResultDTO>().CallStored("Certificate_Get_Data_N", paramters_Type,
                paramters_Data, Device_Info).ToList();
                //var s=data.Where(a=>a.)


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> AcceptOrNotAcceptCertificates(AcceptCertificate accept, List<string> Device_Info)
        {

            try
            {

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("certificateId", SqlDbType.BigInt);
                paramters_Type.Add("ISAccepted", SqlDbType.Bit);
                paramters_Type.Add("User_Updation_Date", SqlDbType.SmallDateTime);
                paramters_Type.Add("User_Updation_Id", SqlDbType.BigInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("certificateId", accept.certificateId.ToString());
                paramters_Data.Add("ISAccepted", accept.IsAccepted.ToString());
                paramters_Data.Add("User_Updation_Date", accept.User_Updation_Date.ToString("yyyy-MM-dd"));
                paramters_Data.Add("User_Updation_Id", accept.User_Updation_Id.ToString());

                var data = uow.Repository<object>().CallStored("Certificate_Accept", paramters_Type,
                paramters_Data, Device_Info).ToList();



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> printCertificates(long certificateId, short User_Updation_Id, bool IsAccepted, int ISPrint, List<string> Device_Info)
        {
            try
            {
                var User_Updation_Date = DateTime.Now;
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("certificateId", SqlDbType.BigInt);
                paramters_Type.Add("User_Updation_Date", SqlDbType.SmallDateTime);
                paramters_Type.Add("User_Updation_Id", SqlDbType.BigInt);
                paramters_Type.Add("ISPrint", SqlDbType.Int);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("certificateId", certificateId.ToString());
                paramters_Data.Add("User_Updation_Date", User_Updation_Date.ToString("yyyy-MM-dd"));
                paramters_Data.Add("User_Updation_Id", User_Updation_Id.ToString());
                paramters_Data.Add("ISPrint", ISPrint.ToString());

                var data = uow.Repository<object>().CallStored("Certificate_Printed", paramters_Type,
                paramters_Data, Device_Info).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Company Type
        public Dictionary<string, object> GetAllCompany(long Company, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from cn in entities.Company_National
                   
                       // where sc.Company_Type_Id == 6
                        select new CustomOptionLongId
                        { //change display lang
                            DisplayText = (lang == "1" ? cn.Name_Ar : cn.Name_En),
                            Value = cn.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }
        public Dictionary<string, object> GetAllOrgniztion(long Orgniztion, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from cn in entities.Public_Organization
                        //join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
                        //where sc.Company_Type_Id == 7
                        select new CustomOptionLongId
                        { //change displaylang
                            DisplayText = (lang == "1" ? cn.Name_Ar : cn.Name_En),
                            Value = cn.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }
        public Dictionary<string, object> GetAllPerson(long Person, List<string> Device_Info)
        {
            try
            {
     
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from cn in entities.People
                       // join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
                        //where sc.Company_Type_Id == 8
                        select new CustomOptionLongId
                        { //change display lang
                            DisplayText = (lang == "1" ? cn.Name : cn.Name_EN),
                            Value = cn.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }

    }
}
