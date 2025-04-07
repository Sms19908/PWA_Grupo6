using System.ComponentModel.DataAnnotations;

namespace SC_701_PAW_Lunes.Models
{
    public class Cart
    {
        [Key]
        public int Id_Cart { get; set; }
        public int Id_User { get; set; }
        public int Id_Inv { get; set; }
        public String NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }

    }
}
