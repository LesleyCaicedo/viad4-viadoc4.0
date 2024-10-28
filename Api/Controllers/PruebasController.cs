using BusinessLayer.Services;
using EntityLayer.DTO;
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
        public async Task<IActionResult> BusquedaDocFiltros([FromQuery] FiltroDocDTO filtroDocDTO)
        //(int CiCompania, string CiTipoDocumento, string NumDocumentos, string ClaveAcceso, string Identificacion, string NombreRS, string FechaInicio, string FechaFin, string Autorizacion)
        {
            response = await pruebaServicio.BusquedaDocFiltros(filtroDocDTO);
                //(CiCompania, CiTipoDocumento, NumDocumentos, ClaveAcceso, Identificacion, NombreRS, FechaInicio, FechaFin, Autorizacion);

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

        [HttpGet("[action]")]
        public async Task<IActionResult> FiltroBusqueda(string claveAcceso)
        {
            response = await pruebaServicio.FiltroBusqueda(claveAcceso);
            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
