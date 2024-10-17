using BusinessLayer.Services;
using EntityLayer.DTO.FacturaDTO;
using EntityLayer.DTO.NotaCreditoDTO;
using EntityLayer.DTO.NotaDebitoDTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaInsertar _facturaInsertar;
        private Response response = new();

        public FacturaController(IFacturaInsertar facturaInsertar) 
        {
            _facturaInsertar = facturaInsertar;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> IngresarFactura(Factura1DTO factura1DTO)
        {
            response = await _facturaInsertar.IngresarFactura(factura1DTO);
            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> IngresarNotaCredito(NotaCreditoDTO notaCreditoDTO)
        {
            response = await _facturaInsertar.IngresarNotaCredito(notaCreditoDTO);
            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> IngresarNotaDebito(NotaDebitoDTO notaDebitoDTO)
        {
            response = await _facturaInsertar.IngresarNotaDebito(notaDebitoDTO);
            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
