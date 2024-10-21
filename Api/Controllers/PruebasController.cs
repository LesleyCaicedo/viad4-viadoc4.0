using BusinessLayer.Services;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebasController : ControllerBase
    {
        private Response response = new ();

        private readonly IPruebaServicio pruebaServicio;

        public PruebasController(IPruebaServicio servicio)
        {
            pruebaServicio = servicio;
        }

        
        [HttpGet("[action]")]
        public async Task<IActionResult> BuscarDocumentosError(string tipoDocumento)
        {

            response = await pruebaServicio.BuscarDocumentosError(tipoDocumento);
            if (response.Code == ResponseType.Error) 
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TDocumentosEmpresas(int CiCompania, string fechaInicio, string fechaFin)
        {
            response = await pruebaServicio.TDocumentosEmpresas(CiCompania,fechaInicio,fechaFin);
            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
