using EntityLayer.Responses;
using DataLayer.repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.DTO.FacturaDTO;
using EntityLayer.DTO.NotaCreditoDTO;
using EntityLayer.DTO.NotaDebitoDTO;

namespace BusinessLayer.Services
{
    public class FacturaInsertar : IFacturaInsertar
    {
        public readonly IFacturaRepositorio _facturaRepositorio;
        public readonly INotaCreditoRepositorio _notaCreditoRepositorio;
        public readonly INotaDebitoRepositorio _debitoRepositorio;

        private Response response = new();

        public FacturaInsertar(IFacturaRepositorio facturaRepositorio, INotaCreditoRepositorio notaCreditoRepositorio, INotaDebitoRepositorio debitoRepositorio)
        {
            _facturaRepositorio = facturaRepositorio;

            _notaCreditoRepositorio = notaCreditoRepositorio;
            _debitoRepositorio = debitoRepositorio;
        }

        public async Task<Response> IngresarFactura(Factura1DTO factura1DTO)
        {
            response = await _facturaRepositorio.IngresarFactura(factura1DTO);
            return response;
        }

        public async Task<Response> IngresarNotaCredito(NotaCreditoDTO notaCreditoDTO)
        {
            response = await _notaCreditoRepositorio.IngresarNotaCredito(notaCreditoDTO);
            return response;
        }

        public async Task<Response> IngresarNotaDebito(NotaDebitoDTO notaDebitoDTO)
        {
            response = await _debitoRepositorio.IngresarNotaDebito(notaDebitoDTO);
            return response;
        }

    }
}
