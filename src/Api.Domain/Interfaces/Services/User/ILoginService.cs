using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto user);

        Task<object> UpdatePassword(UserDtoUpdatePassword user);
    }
}
