using EntityLayer.Responses;
using DataLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Models;
using EntityLayer.DTO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.VisualBasic;

namespace DataLayer.repositorio
{
    public class PruebasRepositorio : IPruebasRepositorio
    {
        private readonly FacturacionElectronicaQaContext _context;
        
        Response response = new();
 
        public PruebasRepositorio(FacturacionElectronicaQaContext context)
        {
            _context = context;
        }

        public async Task<Response> BuscarDocumentosError(string tipoDocumento)
        {
            Response response = new();
            Utilitie util = new Utilitie(_context);

            response = await util.DocumentosNoAutorizados(tipoDocumento);

            return response;
        }

        public async Task<Response> TDocumentosEmpresas(int CiCompania, string fechaInicio, string fechaFin)
        {
            List<TipoDocumentoDTO> tipoDocumentoDTO = new List<TipoDocumentoDTO>();

            CultureInfo cultures = new CultureInfo("fr-FR");
            string dateformat = "dd/MM/yyyy";
            TipoDocumentoDTO factura = new TipoDocumentoDTO()
            {
                TxDescripcion = "Factura",
                CiTipoDocumento = "01",
                estado = new EstadosDTO()
            };

            try
            {
                List<Factura1> fechas = new();

                fechas = await _context.Facturas1.Where(f => f.CiEstado == "A" && f.CiCompania == CiCompania).ToListAsync();

                factura.estado.A = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();

                //factura.estado.A = _context.Facturas1
                //    .Where(f => f.CiEstado == "A" && f.CiCompania == CiCompania )
                //    .AsEnumerable()
                //    .Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures))
                //    .Count();
                factura.estado.FI = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "FI" && f.CiCompania == CiCompania).CountAsync();
                factura.estado.EFI = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI" && f.CiCompania == CiCompania).CountAsync();
                factura.estado.ERE = await _context.Facturas1.Where(f => (f.CiEstadoRecepcionAutorizacion == "ERE" || !f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiEstadoRecepcionAutorizacion == "AU") && f.CiCompania == CiCompania).CountAsync();
                factura.estado.RE = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "RE" && f.CiCompania == CiCompania).CountAsync();
                factura.estado.EAU = await _context.Facturas1.Where(f => (f.CiEstadoRecepcionAutorizacion == "EAU" || f.CiEstadoRecepcionAutorizacion == "NAU" || f.CiEstadoRecepcionAutorizacion == "RAU") && f.CiCompania == CiCompania).CountAsync();
                factura.estado.AU = await _context.Facturas1.Where(f => f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiCompania == CiCompania).CountAsync();
                factura.estado.EEV = await _context.Facturas1.Where(f => ((f.CiEstadoRecepcionAutorizacion == "EEV" || f.CiEstadoRecepcionAutorizacion == "ENP") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")) && f.CiCompania == CiCompania).CountAsync();
                factura.estado.ECP = await _context.Facturas1.Where(f => ((f.CiEstadoRecepcionAutorizacion == "ECP" || f.CiEstadoRecepcionAutorizacion == "ENV") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")) && f.CiCompania == CiCompania).CountAsync();
                factura.estado.Total = factura.estado.A + factura.estado.FI + factura.estado.EFI + factura.estado.ERE + factura.estado.RE + factura.estado.EAU + factura.estado.AU + factura.estado.EEV + factura.estado.ECP;

            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                
                return response;

            }
            
            TipoDocumentoDTO NotaCredito = new TipoDocumentoDTO() 
            { 
                TxDescripcion = "Nota De Credito",
                CiTipoDocumento = "04",
                estado =  new EstadosDTO()
            };



            NotaCredito.estado.A = await _context.NotaCreditos.Where(f => f.CiEstado == "A").CountAsync();
            NotaCredito.estado.FI = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "FI").CountAsync();
            NotaCredito.estado.EFI = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").CountAsync();
            NotaCredito.estado.ERE = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "ERE" || !f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiEstadoRecepcionAutorizacion == "AU").CountAsync();
            NotaCredito.estado.RE = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "RE").CountAsync();
            NotaCredito.estado.EAU = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "EAU" || f.CiEstadoRecepcionAutorizacion == "NAU" || f.CiEstadoRecepcionAutorizacion == "RAU").CountAsync();
            NotaCredito.estado.AU = await _context.NotaCreditos.Where(f => f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).CountAsync();
            NotaCredito.estado.EEV = await _context.NotaCreditos.Where(f => (f.CiEstadoRecepcionAutorizacion == "EEV" || f.CiEstadoRecepcionAutorizacion == "ENP") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).CountAsync();
            NotaCredito.estado.ECP = await _context.NotaCreditos.Where(f => (f.CiEstadoRecepcionAutorizacion == "ECP" || f.CiEstadoRecepcionAutorizacion == "ENV") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).CountAsync();
            NotaCredito.estado.Total = NotaCredito.estado.A + NotaCredito.estado.FI + NotaCredito.estado.EFI + NotaCredito.estado.ERE + NotaCredito.estado.RE + NotaCredito.estado.EAU + NotaCredito.estado.AU + NotaCredito.estado.EEV + NotaCredito.estado.ECP;

            tipoDocumentoDTO.Add(factura);
            tipoDocumentoDTO.Add(NotaCredito);
            
            response.Code = ResponseType.Success;
            response.Message = "";
            response.Data = tipoDocumentoDTO;
            return response;
        }
    }
}
