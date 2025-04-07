using System.ComponentModel.DataAnnotations;

namespace SC_701_PAW_Lunes.Models
{
    public class Favorites
    {
        [Key]
        public int Id_Favorites { get; set; }
        public int Id_User { get; set; }
        public int Id_Inv {  get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioProducto { get; set; }
    }
}
