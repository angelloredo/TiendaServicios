using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.DTOs;
using TiendaServicios.Api.Libro.Modelos;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Test
{
    public class LibrosServiceTest
    {

        private List<LibreriaMaterial> ObtenerDataDePrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);


            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }


        private Mock<ContextoLibreria> CrearContexto()
        {

            var dataPRueba = ObtenerDataDePrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPRueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPRueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPRueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPRueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPRueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>
                ().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPRueba.Provider));


            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;
        }

        [Fact]
        public async void GetLibros()
        {
            System.Diagnostics.Debugger.Launch();
            //1. Emular a la instancia de netityFramework Core
            var mockContexto = CrearContexto();

            var mappCongif = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapptingTest());

            });

            var mapper = mappCongif.CreateMapper();

            // 3. Instanciar a la clase manejador y pasarle como parametros los mocks creados

            var manejador = new Consulta.Manejador(mockContexto.Object, mapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async void GetLibroById()
        {
            var mockContexto = CrearContexto();

            var mappCongif = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapptingTest());

            });

            var mapper = mappCongif.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var librp = await manejador.Handle(request, new System.Threading.CancellationToken());


            Assert.NotNull(librp);

            Assert.True(librp.LibreriaMaterialId == Guid.Empty);


        }


        [Fact]
        public async void GuardarLibro()
        {
            System.Diagnostics.Debugger.Launch();
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta();

            request.Titulo = "lIBRO DE mICROSEVIES";

            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());


            Assert.True(libro != null);

        }

    }
}
