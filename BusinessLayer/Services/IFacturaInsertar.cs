using EntityLayer.DTO.FacturaDTO;
using EntityLayer.DTO.NotaCreditoDTO;
using EntityLayer.DTO.NotaDebitoDTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IFacturaInsertar
    {
        public Task<Response> IngresarFactura(Factura1DTO factura1DTO);
        public Task<Response> IngresarNotaCredito(NotaCreditoDTO notaCreditoDTO);
        public Task<Response> IngresarNotaDebito(NotaDebitoDTO notaDebitoDTO);
    }
}
