
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Repositories
{
    public abstract class DbClass
    {
        private readonly string _connectionString;

        protected IConfiguration _configuration;

        public DbClass(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }


        public async Task<bool> ExecuteStoredProcedureAsync<T>(string storedProc, T paramModel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand dbCommand = new SqlCommand();
                SqlTransaction tran;
                dbCommand.Connection = con;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = storedProc;
                dbCommand.AddBulkParameter(paramModel);

                await con.OpenAsync();
                tran = con.BeginTransaction();
                dbCommand.Transaction = tran;

                try
                {
                    int rowsAffected = await dbCommand.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        tran.Commit();
                        return true;
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }


        }

        public async Task<long> ExecuteStoredProcedureAffectedRowsAsync<T>(string storedProc, T paramModel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand dbCommand = new SqlCommand();
                SqlTransaction tran;
                dbCommand.Connection = con;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = storedProc;
                dbCommand.AddBulkParameter(paramModel);
                await con.OpenAsync();
                tran = con.BeginTransaction();
                dbCommand.Transaction = tran;

                try
                {
                    int rowsAffected = await dbCommand.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        tran.Commit();
                        return rowsAffected;
                    }
                    else
                    {
                        tran.Rollback();
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        public async Task<long> ExecuteStoredProcedureReturnAsync<T>(string storedProc, T paramModel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand dbCommand = new SqlCommand();
                    SqlTransaction tran;
                    dbCommand.Connection = con;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = storedProc;
                    dbCommand.AddBulkParameter(paramModel);

                    SqlParameter param = new SqlParameter("ret_val", SqlDbType.Int);
                    param.Direction = ParameterDirection.ReturnValue;
                    dbCommand.Parameters.Add(param);
                    await con.OpenAsync();
                    tran = con.BeginTransaction();
                    dbCommand.Transaction = tran;

                    try
                    {
                        int rowsAffected = await dbCommand.ExecuteNonQueryAsync();
                        int ret_val = (int)param.Value;
                        if (ret_val > 0)
                        {
                            tran.Commit();
                            return ret_val;
                        }
                        else
                        {
                            tran.Rollback();
                            return ret_val;
                        }
                    }
                    catch (Exception objE)
                    {
                        throw;
                    }
                }
                catch (Exception objE)
                {
                    throw;
                }
            }

        }

        public async Task<List<TResponseParam>> GetDataListFromStoredProcedureAsync<TRequestParam, TResponseParam>(string StoredProcName, TRequestParam paramModel) where TResponseParam : new()
        {
            DataSet ds = new DataSet();
            List<TResponseParam> result = new List<TResponseParam>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    SqlCommand command = new SqlCommand(StoredProcName, con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.AddBulkParameter(paramModel);
                    //foreach (var param in parameter)
                    //{
                    //    SqlParameter sqlParam = command.Parameters.AddWithValue(param.Key.ToString(), (object)param.Value);
                    //}


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt;
                        dt = ds.Tables[0];
                        result = dt.ToList<TResponseParam>();
                        return result;
                    }
                    else
                        return result;

                }
                catch (Exception objE)
                {
                    throw;
                }
            }

        }

        public async Task<long> ExecuteStoredProcedureBulkReturnAsync<T>(
            string storedProc,
            T paramModel,
            DataTable imageTable,
            string tvpParameterName,
            string tvpTypeName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand dbCommand = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = storedProc
                };

                // Existing scalar parameters
                dbCommand.AddBulkParameter(paramModel);

                // Remove the Images property because AddBulkParameter
                // will already have added it incorrectly.
                if (dbCommand.Parameters.Contains(tvpParameterName))
                    dbCommand.Parameters.RemoveAt(tvpParameterName);

                // Add TVP parameter
                SqlParameter tvp = new SqlParameter(tvpParameterName, SqlDbType.Structured)
                {
                    TypeName = tvpTypeName,
                    Value = imageTable
                };

                dbCommand.Parameters.Add(tvp);

                SqlParameter ret = new SqlParameter("ret_val", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };

                dbCommand.Parameters.Add(ret);

                await con.OpenAsync();

                SqlTransaction tran = con.BeginTransaction();

                dbCommand.Transaction = tran;

                try
                {
                    await dbCommand.ExecuteNonQueryAsync();

                    int retVal = Convert.ToInt32(ret.Value);

                    if (retVal > 0)
                    {
                        tran.Commit();
                        return retVal;
                    }

                    tran.Rollback();
                    return retVal;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        public async Task<TResponseParam> GetDataFromStoredProcedureAsync<TRequestParam, TResponseParam>(string StoredProcName, TRequestParam paramModel) where TResponseParam : new()
        {
            TResponseParam result;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    SqlCommand command = new SqlCommand(StoredProcName, con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.AddBulkParameter(paramModel);
                    //foreach (var param in parameter)
                    //{
                    //    SqlParameter sqlParam = command.Parameters.AddWithValue(param.Key.ToString(), (object)param.Value);
                    //}


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt;
                        dt = ds.Tables[0];
                        result = dt.ToList<TResponseParam>()[0];
                        return result;
                    }
                    else
                        return default(TResponseParam);
                }
                catch (Exception objE)
                {
                    throw;
                }
            }

        }
        public async Task<DataSet> GetDataFromStoredProcedureAsync<TRequestParam>(string StoredProcName, TRequestParam paramModel)
        {

            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    SqlCommand command = new SqlCommand(StoredProcName, con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.AddBulkParameter(paramModel);
                    //foreach (var param in parameter)
                    //{
                    //    SqlParameter sqlParam = command.Parameters.AddWithValue(param.Key.ToString(), (object)param.Value);
                    //}


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    return ds;
                }
                catch (Exception objE)
                {
                    throw;
                }
            }

        }

        

        public async Task<bool> BulkInsert(DataTable dt, string tableName)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    SqlBulkCopy objBulk = new SqlBulkCopy(con);
                    objBulk.DestinationTableName = tableName;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        objBulk.ColumnMappings.Add(dt.Columns[i].ToString(), dt.Columns[i].ToString());
                    }
                    objBulk.WriteToServer(dt);
                    return true;
                }
                catch (Exception objE)
                {
                    throw;
                }
            }

        }


    }

    public static class SqlParameterExtension
    {
        public static void AddBulkParameter<T>(this SqlCommand sqlCommand, T paramModel)
        {
            System.Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //.Where(prop => Attribute.GetCustomAttributes(prop).Any(x=> x.TypeId.GetType().Name == "IgnoreParamAttribute")).ToArray();
            foreach (PropertyInfo info in properties)
            {
                var attributes = info.GetCustomAttributes();
                if (attributes.Any(x => x.GetType().Name == "IgnoreParamAttribute")) { }
                else
                {
                    var val = info.GetValue(paramModel);
                    if (val != null)
                    {
                        //if (info.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(info.PropertyType))
                        //{
                        //    sqlCommand.Parameters.AddWithValue($"@{info.Name}", Newtonsoft.Json.JsonConvert.SerializeObject(val));
                        //}
                        //else
                        //{
                            sqlCommand.Parameters.AddWithValue($"@{info.Name}", val);
                       // }
                    }
                }
            }

        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<T> data = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            System.Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    try
                    {
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        else
                            continue;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"For field : {pro.Name} => {ex.Message}");
                    }
                    
                }
            }
            return obj;
        }
    }
}
