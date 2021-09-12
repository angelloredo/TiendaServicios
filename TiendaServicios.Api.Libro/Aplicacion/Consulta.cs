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
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibreriaMaterialDTO>>
        {
            public Ejecuta()
            {

            }
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDTO>>
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contextoLibreria, IMapper mapper)
            {
                _context = contextoLibreria;
                _mapper = mapper;
            }
            public async Task<List<LibreriaMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _context.LibreriaMaterial.ToListAsync();
                return _mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDTO>>(libros);
            }
        }
    }
}
