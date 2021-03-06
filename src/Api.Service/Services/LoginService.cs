using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using Api.Domain.Repository;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IConfiguration _configuration { get; }
        private readonly IMapper _mapper;

        public LoginService(IUserRepository repository,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations,
                            IConfiguration configuration,
                            IMapper mapper)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();
            string pwd = String.Empty;

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);

                if (baseUser != null)
                    pwd = CreateMD5(user.Password);

                //if (baseUser == null || pwd != baseUser.Password)
                if (baseUser == null)
                {
                    return new
                    {
                        autenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(user.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds); //configurado no appsettings.json

                    var handler = new JwtSecurityTokenHandler();
                    var token = CreateToken(identity, createDate, expirationDate, handler);

                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
                return new
                {
                    autenticated = false,
                    message = "Falha ao autenticar"
                };
        }

        public async Task<object> UpdatePassword(UserDtoUpdatePassword user)
        {
            var baseUser = new UserEntity();
            string newPwd = String.Empty;
            string oldPwd = String.Empty;

            if (user != null)
            {
                baseUser = await _repository.FindById(user.Id);

                if (baseUser != null)
                {
                    newPwd = CreateMD5(user.NewPassword);
                    oldPwd = CreateMD5(user.OldPassword);
                }

                if (!oldPwd.Equals(baseUser.Password))
                {
                    return new
                    {
                        message = "A senha antiga não confere"
                    };
                }

                if (newPwd.Equals(baseUser.Password))
                {
                    return new
                    {
                        message = "A senha nova é igual a atual"
                    };
                }

                var model = _mapper.Map<UserModel>(user);
                var entity = _mapper.Map<UserEntity>(model);
                var result = await _repository.UpdateAsync(entity);

                return new { message = "Atualizado com sucesso!" };

            }
            else
                return new
                {
                    message = "Falha ao atualizar senha"
                };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createData, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createData,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário logado com sucesso"
            };
        }

        private static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
