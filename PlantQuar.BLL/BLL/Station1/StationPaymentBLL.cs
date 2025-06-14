using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.StationNew;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station
{
    public class StationPaymentBLL
    {
        private UnitOfWork uow;
        public StationPaymentBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(long Id, List<string> Device_Info)
        {
            try
            {
                var lang = Device_Info[2];
                Station_Accreditation_Committee entity = uow.Repository<Station_Accreditation_Committee>().Findobject(Id);
                //string stationCode = entity.Station_Accreditation.StationActivity.Station.StationCode;

                var totalamount = entity.Amount_Total;

                var committee = entity;
                //decimal totalPaid = 0;
                List<Station_Accreditation_PaymentDTO> payments = uow.Repository<Station_Accreditation_Payment>().GetData().Where(c => c.Station_Committee_ID == Id)
                    .Select(a => new Station_Accreditation_PaymentDTO
                    {
                        Amount = a.Amount

                    }).ToList();

                //List<FixedFeesAmountDTO> fees = uow.Repository<Station_Accreditation_Committee_FixedFeesAmount>().GetData()
                //.Where(c => c.Station_Committee_Id == Id).Select(x => new FixedFeesAmountDTO()
                //{
                //    Amount = x.FeesAmount_Fixed.Amount,
                //    feesName = (lang == "1") ? x.FeesAmount_Fixed.Name_Ar : x.FeesAmount_Fixed.Name_En
                //}).ToList();


                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("stationCode", 123);
                dic.Add("payments", payments);
               // dic.Add("feesAmount", fees);
                dic.Add("totalamount", totalamount);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert(Station_Accreditation_PaymentDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<Station_Accreditation_Payment>(entity);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Payment_seq");
                var CreatedModel = uow.Repository<Station_Accreditation_Payment>().InsertReturn(CModel);
                if (entity.totalRequire == entity.Amount)
                {
                    Station_Accreditation_Committee stat = uow.Repository<Station_Accreditation_Committee>().Findobject(entity.Station_Committee_ID);
                    stat.IsPaid = true;
                }
                uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CModel);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        
    }
}