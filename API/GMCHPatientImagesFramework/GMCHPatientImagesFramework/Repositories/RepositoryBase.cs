
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using System.Data;

namespace GMCHPatientImagesFramework.Repositories
{

    public class SeachByIdDTO
    {
        public long Id { get; set; }
        public string Mode { get; set; }
    }

    public class RepositoryBase<TRequestParam, KResponseParam> : DbClass, IRepositoryBase<TRequestParam, KResponseParam>
      where TRequestParam : class, new() where KResponseParam : class, new()
    {
        public string ProcedureName;
        public RepositoryBase(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<long> DeleteAsync(TRequestParam requestParam)
        {
            var response = await ExecuteStoredProcedureReturnAsync<TRequestParam>(ProcedureName, requestParam);
            return response;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await ExecuteStoredProcedureAsync<SeachByIdDTO>(ProcedureName, new SeachByIdDTO { Id = id, Mode="delete" });
            return response;
        }

        public async Task<List<KResponseParam>> GetAllAsync(TRequestParam requestParam)
        {
            var response = await GetDataListFromStoredProcedureAsync<TRequestParam, KResponseParam>(ProcedureName, requestParam);
            return response;
        }

        public async Task<List<TRes>> GetAllAsync<TReq, TRes>(TReq requestParam) where TRes : new()
        {

            var response = await GetDataListFromStoredProcedureAsync<TReq, TRes>(ProcedureName, requestParam);
            return response;
        }

        public async Task<KResponseParam> GetByIdAsync(TRequestParam requestParam)
        {
            var response = await GetDataFromStoredProcedureAsync<TRequestParam, KResponseParam>(ProcedureName, requestParam);
            return response;
        }

        

        public async Task<KResponseParam> GetByIdAsync(long id)
        {
            var response = await GetDataFromStoredProcedureAsync<SeachByIdDTO, KResponseParam>(ProcedureName, new SeachByIdDTO { Id = id, Mode= "searchbyId" });
            return response;
        }

        public async Task<List<KResponseParam>> GetAllByIdAsync(long id)
        {

            var response = await GetDataListFromStoredProcedureAsync<SeachByIdDTO, KResponseParam>(ProcedureName, new SeachByIdDTO { Id = id });
            return response;
        }

        public async Task<long> InsertAsync(TRequestParam requestParam)
        {
            var response = await ExecuteStoredProcedureReturnAsync<TRequestParam>(ProcedureName, requestParam );
            return response;
        }

        public async Task<long> SetStatusAsync(TRequestParam requestParam)
        {
            var response = await ExecuteStoredProcedureReturnAsync<TRequestParam>(ProcedureName, requestParam);
            return response;
        }

        public async Task<long> UpdateAsync(TRequestParam requestParam)
        {
            var response = await ExecuteStoredProcedureReturnAsync<TRequestParam>(ProcedureName, requestParam);
           
            return response;
        }

        public async Task<long> UpdateAsync<TReq>(TReq requestParam) where TReq : new ()
        {
            var response = await ExecuteStoredProcedureReturnAsync<TReq>(ProcedureName, requestParam);
            return response;
        }
    }
}
