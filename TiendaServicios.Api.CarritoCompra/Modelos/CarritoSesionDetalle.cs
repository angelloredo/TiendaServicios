﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Modelos
{
    public class CarritoSesionDetalle
    {
        public int CarritoSesionDetalleId { get; set; } 
        public DateTime? FehcaCreacion { get; set; }    
        public string ProductoSeleccionado { get; set; }
        public int CarritoSesionId { get; set; }
        public CarritoSesion CarritoSesion { get; set; }

    }
}
