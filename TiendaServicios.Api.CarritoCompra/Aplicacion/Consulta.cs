using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.DTOs;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDTO>
        {
            public int CarritoSesionId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly CarritoContexto _context;
            private readonly ILibrosService _librosService;
            public Manejador(CarritoContexto carritoContexto, ILibrosService librosService)
            {
                _context = carritoContexto;
                _librosService = librosService;
            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carrito = await _context.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var detalle = await _context.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                List<CarritoDetalleDTO> carritoDetalleDTOs = new List<CarritoDetalleDTO>();

                if (carrito == null)
                    throw new Exception("Error al encontrar el Carrito");

                foreach (var libro in detalle)
                {
                    var response = await _librosService.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        var objetoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDTO
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };
                        carritoDetalleDTOs.Add(carritoDetalle);
                    }
                }

                var CarritoSesionDTO = new CarritoDTO
                {
                    CarritoId = carrito.CarritoSesionId,
                    FechaCreacionSesion = carrito.FechaCreacion,
                    CarritoDetalleDTOs = carritoDetalleDTOs
                };

                return CarritoSesionDTO;
            }
        }
    }
}
