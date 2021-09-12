using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Modelos
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; } 

        public DateTime? FechaCreacion { get; set; }
        public virtual ICollection<CarritoSesionDetalle> ListDetalles { get; set; }
    }
}
