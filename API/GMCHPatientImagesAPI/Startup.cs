using DinkToPdf;
using DinkToPdf.Contracts;
using GMCHPatientImages.Middlewares;
using GMCHPatientImagesFramework.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Rekognition;

namespace GMCHPatientImages
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GMCHPatientPortal.APIs", Version = "v1" });
            });

            // configure strongly typed settings object
            services.Configure<GMCHPatientImagesDtos.DTOs.AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddTransactionFramework(Configuration);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // =====================================================
            // FACE AUTHENTICATION
            // =====================================================

            var allowFaceAuthentication =
                Configuration.GetValue<bool>("AppSettings:AllowFaceAuthentication");

            if (allowFaceAuthentication)
            {
                // =====================================================
                // AWS REKOGNITION
                // =====================================================

                var awsOptions = Configuration.GetAWSOptions();

                services.AddDefaultAWSOptions(awsOptions);
                services.AddAWSService<IAmazonRekognition>();

                // =====================================================
                // AWS Face Verification Service
                // =====================================================

                services.AddScoped<
                    GMCHPatientImagesFramework.Services.FaceVerification.IFaceVerificationService,
                    GMCHPatientImagesFramework.Services.FaceVerification.AwsFaceVerificationService>();
            }
            else
            {
                // =====================================================
                // Face Authentication Disabled
                // =====================================================

                services.AddScoped<
                    GMCHPatientImagesFramework.Services.FaceVerification.IFaceVerificationService,
                    GMCHPatientImagesFramework.Services.FaceVerification.NoOpFaceVerificationService>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "API v1"));
            }

            //app.UseSwagger();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GMCHPatientImages.APIs v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
