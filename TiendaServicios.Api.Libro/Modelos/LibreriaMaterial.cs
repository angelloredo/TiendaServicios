﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Modelos
{
    public class LibreriaMaterial
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }  
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
