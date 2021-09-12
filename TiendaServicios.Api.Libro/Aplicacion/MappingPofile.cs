using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.DTOs;
using TiendaServicios.Api.Libro.Modelos;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class MappingPofile : Profile
    {
        public MappingPofile()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDTO>();
        }
    }
}
