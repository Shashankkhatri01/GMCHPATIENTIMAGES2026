
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMCHPatientImagesDtos.DTOs;

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

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await attachAccountToContext(context, token);

            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var loginId = int.Parse(jwtToken.Claims.First(x => x.Type == "LoginId").Value);
                //var clientId = int.Parse(jwtToken.Claims.First(x => x.Type == "ClientId").Value);
                //var roleName = (jwtToken.Claims.First(x => x.Type == "RoleName").Value);

                //attach account to context on successful jwt validation
                LoginDTO login = new LoginDTO();
                login.LoginId = loginId;
                //login.ClientId = clientId;
                //login.RoleName = roleName;
                context.Items["CurrentUser"] = login;
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}
