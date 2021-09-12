using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Persistencia;
using TiendaServicios.Api.Autor.Modelos;
using FluentValidation;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaDeNacimiento { get; set; }
        }

        public class EjecutaValidation : AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();

            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _context;
            public Manejador(ContextoAutor context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var AutorLibro = new AutorLibro
                {

                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaDeNacimiento = request.FechaDeNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString()
                };


                _context.AutorLibro.Add(AutorLibro);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                    return Unit.Value;


                throw new Exception("Error al insertar Autor del libro.");

            }
        }
    }
}
