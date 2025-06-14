using PlantQuar.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.UOW.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        protected PlantQuarantineEntities AsDataBase = null;
        protected DbSet<T> AsTable;
        private readonly bool _lazyLoadingEnabled = true;

        public Repository(PlantQuarantineEntities _AsDataBase)
        {
            AsDataBase = _AsDataBase;
            AsTable = AsDataBase.Set<T>();
            AsDataBase.Configuration.LazyLoadingEnabled = _lazyLoadingEnabled;
        }

        public IQueryable<T> GetData()
        {
            AsDataBase.Configuration.ProxyCreationEnabled = false;
            return AsTable;
        }

        public void Save_Error(string fullName, string message, string name, object device_Info)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetData(params object[] obj)
        {
            AsDataBase.Configuration.ProxyCreationEnabled = false;
            return AsTable;
        }

        public IQueryable<T> GetData(int pageSize, int index, Expression<Func<T, bool>> x)
        {
            AsDataBase.Configuration.ProxyCreationEnabled = false;
            return AsTable.OrderBy(x).Skip(index).Take(pageSize);
        }
        //GetCount
        public long GetCount()
        {
            return AsTable.Count();
        }
        // select by id
        public T Findobject(object Id)
        {
            return AsTable.Find(Id);
        }

        // Add
        public void InsertRecord(T objRecord)
        {
            AsTable.Add(objRecord);
        }

        public T InsertReturn(T objRecord)
        {
            AsTable.Add(objRecord);
            return objRecord;
        }
        //  Update      
        public void Update(T objRecord)
        {
            AsTable.Attach(objRecord);
            AsDataBase.Entry(objRecord).State = EntityState.Modified;
        }
        //Eslam_12-10-2023
        //delete 
        public void Delete_Eslam(T objRecord)
        {
            AsTable.Attach(objRecord);
            AsDataBase.Entry(objRecord).State = EntityState.Deleted;
        }
        public T UpdateReturn(T objRecord)
        {
            AsTable.Attach(objRecord);
            AsDataBase.Entry(objRecord).State = EntityState.Modified;
            return objRecord;
        }

        // Get List By any parameter
        public IEnumerable<T> GetbyMany(Expression<Func<T, bool>> x)
        {
            return AsDataBase.Set<T>().Where(x).ToList();
        }

        // Check If existe
        public bool GetAny(Expression<Func<T, bool>> x)
        {
            return AsDataBase.Set<T>().Any(x);
        }

        public IEnumerable<T> GetWithInclude<MultiModels>(Expression<Func<T, bool>> x, params string[] include)
        {
            IQueryable<T> query = this.AsDataBase.Set<T>();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(x).ToList();
        }

        //Execute Stored Procedure
        public DbRawSqlQuery<T> SQLQuery(string sql, params object[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    return AsDataBase.Database.SqlQuery<T>(sql, parameters);
                }
                else
                {
                    return AsDataBase.Database.SqlQuery<T>(sql);

                }
            }
            catch
            {
                return null;
            }
        }

        //public void Save_Error(string ErrorMessage, string FunctionName, List<string> Device_Info)
        //{
        //    SQLQuery(
        //        "Sp_plant_Error_Insert  @PageName, @ErrorMessage, @FunctionName,@User_Ip,@IsWeb",
        //        new SqlParameter("PageName", SqlDbType.VarChar) { Value = "Request.Url.AbsoluteUri.ToString()" },
        //        new SqlParameter("ErrorMessage", SqlDbType.VarChar) { Value = ErrorMessage },
        //        new SqlParameter("FunctionName", SqlDbType.VarChar) { Value = FunctionName },
        //        new SqlParameter("User_Ip", SqlDbType.VarChar) { Value = Device_Info[0] },
        //        new SqlParameter("IsWeb", SqlDbType.Bit) { Value = bool.Parse(Device_Info[1]) }
        //   ).ToList();
        //}
        /// <summary>
        /// call stored from DB
        /// </summary>
        /// <param name="StoredName">Stored Name</param>
        /// <param name="paramters_Type">Dictionary<string, SqlDbType> string -> paramter name in sotred  / SqlDbType paramter data type</param>
        /// <param name="Device_Info"></param>
        public DbRawSqlQuery<T> CallStored(string StoredName, Dictionary<string, SqlDbType> paramters_Type = null,
            Dictionary<string, string> paramters_Data = null, List<string> Device_Info = null)
        {
            try
            {
                SqlParameter[] param_Data;
                dynamic data;
                StringBuilder sql = new StringBuilder(StoredName);
                if (paramters_Type != null)
                {
                    param_Data = new SqlParameter[paramters_Type.Count];
                    int x = 0;
                    foreach (string item in paramters_Type.Keys)
                    {
                        param_Data[x] = new SqlParameter(item, SqlDbType.NVarChar) { Value = paramters_Data[item] };
                        sql.Append(" @" + item + ",");
                        x++;
                    }
                    data = SQLQuery(sql.ToString().TrimEnd(','), param_Data);
                }
                else
                {
                    data = SQLQuery(sql.ToString().TrimEnd(','), null);
                }
                return data;
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, object> DataReturn(int state_Code, object dataDTO)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("state_Code", state_Code);
            dic.Add("obj", dataDTO);

            return dic;
        }

        public void Save_Error(string PageName, string ErrorMessage, string FunctionName, List<string> Device_Info)
        {
            try
            {
                SQLQuery(
                        "Sp_plant_Error_Insert  @PageName, @ErrorMessage, @FunctionName,@User_Ip,@IsWeb",
                        new SqlParameter("PageName", SqlDbType.VarChar) { Value = "Request.Url.AbsoluteUri.ToString()" },
                        new SqlParameter("ErrorMessage", SqlDbType.VarChar) { Value = ErrorMessage },
                        new SqlParameter("FunctionName", SqlDbType.VarChar) { Value = FunctionName },
                        new SqlParameter("User_Ip", SqlDbType.VarChar) { Value = Device_Info[0] },
                        new SqlParameter("IsWeb", SqlDbType.Bit) { Value = bool.Parse(Device_Info[1]) }
                   ).ToList();
            }
            catch (Exception ex)
            {
            }
          
        }

        public byte GetNextSequenceValue_Byte(string seqName)
        {
            byte seq = AsDataBase.Database.SqlQuery<byte>("SELECT NEXT VALUE FOR dbo." + seqName).Single();
            return (seq != 0) ? seq : (byte)1;
        }

        public short GetNextSequenceValue_Short(string seqName)
        {
            short seq = AsDataBase.Database.SqlQuery<short>("SELECT NEXT VALUE FOR dbo." + seqName).Single();
            return (seq != 0) ? seq : (short)1;
        }

        public int GetNextSequenceValue_Int(string seqName)
        {
            int seq = AsDataBase.Database.SqlQuery<int>("SELECT NEXT VALUE FOR dbo." + seqName).Single();
            return (seq != 0) ? seq : 1;
        }

        public long GetNextSequenceValue_Long(string seqName)
        {
            long seq = AsDataBase.Database.SqlQuery<long>("SELECT NEXT VALUE FOR dbo." + seqName).Single();
            return (seq != 0) ? seq : 1;
        }
    }
}