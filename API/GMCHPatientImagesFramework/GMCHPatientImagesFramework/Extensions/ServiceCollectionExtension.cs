 
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMCHPatientImagesFramework.Repositories;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTransactionFramework(this IServiceCollection services, IConfiguration GMCHPatientImages)
        {
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));

            services.AddTransient(typeof(IDropdownsRepository), typeof(DropdownsRepository));
            services.AddTransient(typeof(IDropdownsService), typeof(DropdownsService));

            services.AddTransient(typeof(IUserMenuService), typeof(UserMenuService));
            services.AddTransient(typeof(IUserMenuRepository), typeof(UserMenuRepository));
            
            
      services.AddTransient(typeof(ILoginService), typeof(LoginService));
      services.AddTransient(typeof(ILoginRepository), typeof(LoginRepository));

      services.AddTransient(typeof(IPatientImagesService), typeof(PatientImagesService));
      services.AddTransient(typeof(IPatientImagesRepository), typeof(PatientImagesRepository));




      return services;
        }
    }
}
