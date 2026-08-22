using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImages.Middlewares
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _next = next;
        }
        public async Task Invoke(
            HttpContext context,
            IUserTokenValidationService userTokenValidationService)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachAccountToContext(
                    context,
                    token,
                    userTokenValidationService);
            }

            await _next(context);
        }

        private async Task attachAccountToContext(
                            HttpContext context,
                            string token,
                            IUserTokenValidationService userTokenValidationService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        // Token expires exactly at expiration
                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validatedToken
                );

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Get LoginId from JWT
                var loginId = int.Parse(
                    jwtToken.Claims
                        .First(x => x.Type == "LoginId")
                        .Value
                );

                // Get TokenVersion from JWT
                var tokenVersionClaim = jwtToken.Claims
                    .FirstOrDefault(x => x.Type == "TokenVersion");

                if (tokenVersionClaim == null)
                {
                    return;
                }

                var tokenVersion = int.Parse(tokenVersionClaim.Value);

                // Validate against database
                var login = await userTokenValidationService
                    .ValidateUserTokenAsync(loginId, tokenVersion);

                // User doesn't exist / inactive / token version mismatch
                if (login == null)
                {
                    return;
                }

                // Valid user
                context.Items["CurrentUser"] = login;
            }
            catch
            {
                // Do nothing if token validation fails
                // CurrentUser will remain null
            }
        }
    }
}
