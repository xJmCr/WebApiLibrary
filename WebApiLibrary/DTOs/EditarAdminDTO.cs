using System.ComponentModel.DataAnnotations;

namespace WebApiLibrary.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
