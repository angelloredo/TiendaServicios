using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.DTOs;
using TiendaServicios.Api.Libro.Modelos;

namespace TiendaServicios.Api.Test
{
    public class MapptingTest : Profile
    {
        public MapptingTest()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDTO>();
        }
    }
}
