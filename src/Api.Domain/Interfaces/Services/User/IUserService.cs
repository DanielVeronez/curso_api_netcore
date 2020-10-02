using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        //Os nomes são usados para os mesmos que o HTTP
        Task<UserDto> Get(Guid id); //Busca um usuário
        Task<IEnumerable<UserDto>> GetAll(); //Busca todos os usuários
        Task<UserDtoCreateResult> Post(UserDto user); //Insere um novo usuário
        Task<UserDtoUpdateResult> Put(UserDto user); //Atualiza um usuário
        Task<bool> Delete(Guid id); //Deleta um usuário
    }
}
