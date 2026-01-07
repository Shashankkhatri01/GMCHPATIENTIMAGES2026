using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Repositories.Interfaces
{
    public interface IRepositoryBase<TRequestParam, KResponseParam> where TRequestParam : class where KResponseParam : class
    {
        Task<List<KResponseParam>> GetAllAsync(TRequestParam requestParam);

        Task<List<TRes>> GetAllAsync<TReq,TRes>(TReq requestParam) where TRes : new();
        Task<KResponseParam> GetByIdAsync(TRequestParam requestParam);
        Task<KResponseParam> GetByIdAsync(long id);
        Task<List<KResponseParam>> GetAllByIdAsync(long id);
        Task<long> InsertAsync(TRequestParam requestParam);
        Task<long> UpdateAsync(TRequestParam requestParam);

        Task<long> UpdateAsync<TReq>(TReq requestParam) where TReq : new();
        Task<long> SetStatusAsync(TRequestParam requestParam);
        Task<long> DeleteAsync(TRequestParam requestParam);

        Task<bool> DeleteAsync(long id);

    }
}
