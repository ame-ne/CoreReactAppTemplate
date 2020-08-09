using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoreReactApp.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _settings;
        private readonly IGenericRepository<User> _repository;

        public AuthService(IGenericRepository<User> repository, IOptions<AppSettings> settings)
        {
            _repository = repository;
            _settings = settings.Value;
        }

        public UserResponse Authenticate(string login, string password)
        {
            User user = _repository.GetFirstOrDefault(x => x.Login == login && x.PasswordHash == GetHashedPassword(password));
            if (user == null)
            {
                return null;
            }

            var identity = GetIdentity(user);
            var encodedJwt = GenerateToken(identity);

            return new UserResponse()
            {
                Login = user.Login,
                Role = user.Role,
                Token = encodedJwt
            };
        }

        #region private methods

        private string GetHashedPassword(string password)
        {
            //var sha1 = new SHA1CryptoServiceProvider();
            //var passwordData = Encoding.ASCII.GetBytes(password);
            //var sha1PasswordData = sha1.ComputeHash(passwordData);
            //return Encoding.ASCII.GetString(sha1PasswordData);
            return password;
        }

        private string GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_settings.Secret), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        #endregion
    }
}
