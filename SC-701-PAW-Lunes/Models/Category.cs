﻿using System.ComponentModel.DataAnnotations;

namespace SC_701_PAW_Lunes.Models
{
    public class Category
    {
        [Key]
        public int Id_Cat { get; set; } //Fk de categorias

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Tallas { get; set; }
    }
}
