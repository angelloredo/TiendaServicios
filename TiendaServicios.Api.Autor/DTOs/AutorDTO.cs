﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Autor.DTOs
{
    public class AutorDTO
    {
        public string AutorLibroGuid { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }

    }
}
