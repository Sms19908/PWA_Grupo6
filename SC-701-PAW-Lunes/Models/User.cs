using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SC_701_PAW_Lunes.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id_user { get; set; }
        public String NombreCompleto { get; set; } //User en Bd

        public String Email {  get; set; }
        public String PasswordHash {  get; set; }
        public String Direccion { get; set; }

    }
}
