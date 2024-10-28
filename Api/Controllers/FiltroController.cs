using BusinessLayer.Services;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltroController : ControllerBase
    {
        Response response = new Response();
        private readonly IFiltroServicio _filtroServicio;

        public FiltroController(IFiltroServicio filtroServicio)
        {
            _filtroServicio = filtroServicio;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> FiltroBusqueda(string claveAcceso)
        {
            response = await _filtroServicio.FiltroBusqueda(claveAcceso);
            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
