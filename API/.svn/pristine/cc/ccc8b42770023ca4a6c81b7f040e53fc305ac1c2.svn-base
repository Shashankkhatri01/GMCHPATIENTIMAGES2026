using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GMCHPatientImagesDtos.DTOs;

namespace GMCHPatientImagesFramework.Utils
{
    public class TokenManager
    {
        private readonly AppSettings _appSettings;
        public TokenManager(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public string generateJwtToken(LoginDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("LoginId", user.LoginId.ToString()),
                //new Claim("ClientId", user.ClientId.ToString()) ,
                //  new Claim("RoleName", user.RoleName.ToString()) ,
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(long.Parse(_appSettings.JwtTokenDuration)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshTokenDTO generateRefreshToken(string ipAddress)
        {
            return new RefreshTokenDTO
            {
                Token = randomTokenString(),
                ExpiryDate = DateTime.UtcNow.AddDays(long.Parse(_appSettings.RefreshTokenDuration)),
                CrDate = DateTime.UtcNow,
                IpAddress = ipAddress
            };
        }

        public string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
