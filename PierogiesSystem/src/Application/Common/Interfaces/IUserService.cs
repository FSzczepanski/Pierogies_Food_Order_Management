namespace CleanArchitecture.Application.Common.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using BC = BCrypt.Net.BCrypt;
    using System.Security.Claims;
    using System.Text;
    using Domain.Entities;
    using Exceptions;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Models;

    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IApplicationDbContext _applicationDbContext;

        public UserService(IOptions<AppSettings> appSettings, IApplicationDbContext applicationDbContext)
        {
            _appSettings = appSettings.Value;
             _applicationDbContext = applicationDbContext;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
              var user = _applicationDbContext.MyUsers.SingleOrDefault(x => x.Username == request.Username);
            if (!BC.Verify(request.Password, user?.PasswordHash) || user == null)
            {
                throw new ValidationException();
            }

            
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _applicationDbContext.MyUsers;
        }

        public User GetById(Guid id)
        {
            return _applicationDbContext.MyUsers.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
