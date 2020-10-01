using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = ("O e-mail é um campo obrigatório"))]
        [EmailAddress(ErrorMessage = ("Formato de e-mail invalido"))]
        [StringLength(100, ErrorMessage = "O e-mail não pode ser maior que {1} caracteres")]
        public string Email { get; set; }

    }
}
