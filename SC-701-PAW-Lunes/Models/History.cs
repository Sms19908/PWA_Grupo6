using System.ComponentModel.DataAnnotations;


namespace SC_701_PAW_Lunes.Models
{
    public class History
    {
        [Key]
        public int Id_history { get; set; }
        public int Id_User { get; set; }
        public String DetallesProductos { get; set; }
        public String DetallesCantidad { get; set; }
        public String DetallesMontos { get; set; }
        public decimal Total { get; set; }
        public DateOnly Fecha { get; set; }

    }
}
