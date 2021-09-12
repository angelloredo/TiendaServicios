using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibroService : ILibrosService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LibroService> _logger;

        public LibroService(IHttpClientFactory httpClientFactory, ILogger<LibroService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool resultado, LibroRemote Libro, string ErrorMsj)> GetLibro(Guid LibroId)
        {
            try
            {
                var Client = _httpClientFactory.CreateClient("Libros");
                var res = await Client.GetAsync($"api/LibroMaterial/{LibroId}");

                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    var result = JsonSerializer.Deserialize<LibroRemote>(content, options);
                    return (true, result, "");
                }

                return (false, null, "Error en el Servicio Libros");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, "Error al ingresar a Servicio Libros");
            }

        }
    }
}
