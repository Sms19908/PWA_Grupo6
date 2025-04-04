﻿using System.ComponentModel.DataAnnotations;

namespace SC_701_PAW_Lunes.Models
{
    public class Inventory
    {
        [Key]
        public int Id_Inv { get; set; } //Pk de inventario

        public int Id_Cat { get; set; }

        public string Nombre { get; set; }

        public string Marca { get; set; }

        public decimal Precio { get; set; }
        public string Descripcion { get; set; }

        public int Cantidad { get; set; }

    }
}
