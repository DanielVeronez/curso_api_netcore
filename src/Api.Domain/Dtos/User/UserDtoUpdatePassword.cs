using System;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdatePassword
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
