
using GMCHPatientImagesFramework.Repositories;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddTransient(typeof(IPatientImagesDetailService), typeof(PatientImagesDetailService));
            services.AddTransient(typeof(IPatientImagesDetailRepository), typeof(PatientImagesDetailRepository));

            services.AddTransient(typeof(IPatientImagesDetailSaveService), typeof(PatientImagesDetailSaveService));
            services.AddTransient(typeof(IPatientImagesDetailSaveRepository), typeof(PatientImagesDetailSaveRepository));

            services.AddTransient(typeof(IPatientImagesHISService), typeof(PatientImagesHISService));
            services.AddTransient(typeof(IPatientImagesHISRepository), typeof(PatientImagesHISRepository));

            services.AddTransient(typeof(IDischargeDeskService), typeof(DischargeDeskService));
            services.AddTransient(typeof(IDischargeDeskRepository), typeof(DischargeDeskRepository));

            services.AddHttpClient<IWhatsAppAPIService, WhatsAppAPIService>();
            services.AddTransient(typeof(IWhatsAppAPIRepository), typeof(WhatsAppAPIRepository));

            services.AddTransient<IHealthCheckResponseService, HealthCheckResponseService>();

            return services;
        }
    }
}
