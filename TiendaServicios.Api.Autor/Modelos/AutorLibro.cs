using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Autor.Modelos
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string AutorLibroGuid { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public ICollection<GradoAcademico> GradoAcademicos { get; set; }

    }
}
