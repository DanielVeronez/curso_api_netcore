using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDto
    {
        [Required(ErrorMessage = ("O campo nome é obrigatório"))]
        [StringLength(60, ErrorMessage = ("O nome deve ter no máximo {1} caracteres"))]
        public string Name { get; set; }
        
        [Required(ErrorMessage = ("O campo e-mail é obrigatório"))]
        [EmailAddress(ErrorMessage = ("O e-mail digitado não é válido"))]
        [StringLength(100, ErrorMessage = ("O campo e-mail deve conter no máximo {1} caracteres"))]
        public string Email { get; set; }
    }
}
