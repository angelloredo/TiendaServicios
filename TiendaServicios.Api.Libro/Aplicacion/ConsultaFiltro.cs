using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.DTOs;
using TiendaServicios.Api.Libro.Modelos;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {

        public class LibroUnico : IRequest<LibreriaMaterialDTO>
        {
            public Guid? LibroId { get; set;    }

        }

        public class Manejador : IRequestHandler<LibroUnico, LibreriaMaterialDTO>
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async  Task<LibreriaMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _context.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();

                if (libro == null)
                    throw new Exception("No se encontro el libro");

                return _mapper.Map<LibreriaMaterial, LibreriaMaterialDTO>(libro);
            }
        }
    }
}
