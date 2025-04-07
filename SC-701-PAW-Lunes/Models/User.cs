using Microsoft.AspNetCore.Identity;

namespace SC_701_PAW_Lunes.Models
{
    public class User : IdentityUser
    {
        public String NombreCompleto { get; set; }

    }
}
