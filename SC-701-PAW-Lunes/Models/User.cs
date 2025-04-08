using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SC_701_PAW_Lunes.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)] 
        public string NombreCompleto { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone] 
        public string Telefono { get; set; }

        [Required]
        public string Rol { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now; 
        
    }
}
