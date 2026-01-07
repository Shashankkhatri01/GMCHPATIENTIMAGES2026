using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class UserMenuService : IUserMenuService
    {
        private readonly AppSettings _appSettings;
        private IUserMenuRepository _repository;

        public UserMenuService(IUserMenuRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }

        //public async Task<ReturnObject<List<UserMenuDTO>>> GetAll(UserMenuDTO userMenuDTO)
        //{
        //    var response = await _repository.GetAllAsync(userMenuDTO);


        //    if (response.Count <= 0)
        //        throw new AppException($"{StringConstants.RecordNotFound}");

        //    return new ReturnObject<List<UserMenuDTO>>
        //    {
        //        ReturnValue = response,
        //        Status = true,
        //        Success = true
        //    };
        //}

        public async Task<ReturnObject<List<UserMenuDTO>>> GetAll(UserMenuDTO userMenuDTO)
        {
            var response = await _repository.GetAllAsync(userMenuDTO);

            var groupedList = response.GroupBy(x => new { x.MenuId,x.Name }).ToList();
            //List of New model
            List<UserMenuDTO> result = new List<UserMenuDTO>();
            UserMenuDTO userMenuDTO1;

            foreach (var item in groupedList)
            {
                userMenuDTO1 = new UserMenuDTO
                {
                    MenuId = item.Key.MenuId,
                    Name = item.Key.Name,
                    children = item.Select(x => new UserSubMenuDTO
                    {
                        SubMenuId = x.SubMenuId,
                        Name = x.SubMenuName,
                        Url=x.Url,


                    }).ToList()
                };
                result.Add(userMenuDTO1);
            }

            if (response == null)
                throw new AppException($"{StringConstants.RecordNotFound}");

            return new ReturnObject<List<UserMenuDTO>>
            {
                ReturnValue = result,
                Status = true,
                Success = true
            };
        }


        public async Task<ReturnObject<UserMenuDTO>> GetById(long id)
        {
            var response = await _repository.GetByIdAsync(id);

            if (response == null)
                throw new AppException($"{StringConstants.RecordNotFound}");

            return new ReturnObject<UserMenuDTO>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }


       

    }
}
