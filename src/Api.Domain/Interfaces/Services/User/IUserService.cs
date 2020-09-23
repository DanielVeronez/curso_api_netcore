using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        //Os nomes são usados para os mesmos que o HTTP
        Task<UserEntity> Get(Guid id); //Busca um usuário
        Task<IEnumerable<UserEntity>> GetAll(); //Busca todos os usuários
        Task<UserEntity> Post(UserEntity user); //Insere um novo usuário
        Task<UserEntity> Put(UserEntity user); //Atualiza um usuário
        Task<bool> Delete(Guid id); //Deleta um usuário
    }
}
