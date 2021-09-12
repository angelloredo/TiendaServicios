using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelos;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime? FechaCreacion { get; set; }

            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            readonly CarritoContexto _context;


            public Manejador(CarritoContexto carritoContexto)
            {
                _context = carritoContexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carrito = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacion
                };

                _context.CarritoSesion.Add(carrito);
                var result = await _context.SaveChangesAsync();


                if (result == 0)
                {
                    throw new Exception("Error al capturar Carrito");
                }
                int idSesion = carrito.CarritoSesionId;

                foreach (var selectedProduct in request.ProductoLista)
                {
                    var Detail = new CarritoSesionDetalle
                    {
                        CarritoSesionId = idSesion,
                        ProductoSeleccionado =selectedProduct,
                        FehcaCreacion = DateTime.Now
                    };

                    _context.CarritoSesionDetalle.Add(Detail);
                }
                var value = await _context.SaveChangesAsync();

                if (value > 0)
                    return Unit.Value;

                throw new Exception("Error agregado detalles");

            }
        }
    }
}
