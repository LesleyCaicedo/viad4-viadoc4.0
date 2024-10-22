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
using System.Security.Cryptography;

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
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "FI" && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.FI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI" && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.EFI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => (f.CiEstadoRecepcionAutorizacion == "ERE" || !f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiEstadoRecepcionAutorizacion == "AU") && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.ERE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "RE" && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.RE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => (f.CiEstadoRecepcionAutorizacion == "EAU" || f.CiEstadoRecepcionAutorizacion == "NAU" || f.CiEstadoRecepcionAutorizacion == "RAU") && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.EAU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.AU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => ((f.CiEstadoRecepcionAutorizacion == "EEV" || f.CiEstadoRecepcionAutorizacion == "ENP") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")) && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.EEV = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Facturas1.Where(f => ((f.CiEstadoRecepcionAutorizacion == "ECP" || f.CiEstadoRecepcionAutorizacion == "ENV") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")) && f.CiCompania == CiCompania).ToListAsync();
                factura.estado.ECP = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

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

            try
            {
                List<NotaCredito> fechas = new();

                fechas = await _context.NotaCreditos.Where(f => f.CiEstado == "A" && f.CiCompania == CiCompania).ToListAsync();                
                NotaCredito.estado.A = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "FI").ToListAsync();
                NotaCredito.estado.FI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").ToListAsync();
                NotaCredito.estado.EFI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "ERE" || !f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiEstadoRecepcionAutorizacion == "AU").ToListAsync();
                NotaCredito.estado.ERE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "RE").ToListAsync();
                NotaCredito.estado.RE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => f.CiEstadoRecepcionAutorizacion == "EAU" || f.CiEstadoRecepcionAutorizacion == "NAU" || f.CiEstadoRecepcionAutorizacion == "RAU").ToListAsync();
                NotaCredito.estado.EAU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                NotaCredito.estado.AU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => (f.CiEstadoRecepcionAutorizacion == "EEV" || f.CiEstadoRecepcionAutorizacion == "ENP") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                NotaCredito.estado.EEV = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


                fechas = await _context.NotaCreditos.Where(f => (f.CiEstadoRecepcionAutorizacion == "ECP" || f.CiEstadoRecepcionAutorizacion == "ENV") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                NotaCredito.estado.ECP = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                NotaCredito.estado.Total = NotaCredito.estado.A + NotaCredito.estado.FI + NotaCredito.estado.EFI + NotaCredito.estado.ERE + NotaCredito.estado.RE + NotaCredito.estado.EAU + NotaCredito.estado.AU + NotaCredito.estado.EEV + NotaCredito.estado.ECP;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;

                return response;
            }

            TipoDocumentoDTO notaDebito = new TipoDocumentoDTO()
            {
                TxDescripcion = "Nota de Debito",
                CiTipoDocumento = "05",
                estado = new EstadosDTO()
            };

            try
            {
                List<NotaDebito> fechas = new();

                fechas = await _context.NotaDebitos.Where(f => f.CiEstado == "A" && f.CiCompania == CiCompania).ToListAsync();
                notaDebito.estado.A = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => f.CiEstadoRecepcionAutorizacion == "FI").ToListAsync();
                notaDebito.estado.FI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").ToListAsync();
                notaDebito.estado.EFI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => f.CiEstadoRecepcionAutorizacion == "ERE" || !f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiEstadoRecepcionAutorizacion == "AU").ToListAsync();
                notaDebito.estado.ERE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => f.CiEstadoRecepcionAutorizacion == "RE").ToListAsync();
                notaDebito.estado.RE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => f.CiEstadoRecepcionAutorizacion == "EAU" || f.CiEstadoRecepcionAutorizacion == "NAU" || f.CiEstadoRecepcionAutorizacion == "RAU").ToListAsync();
                notaDebito.estado.EAU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                notaDebito.estado.AU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => (f.CiEstadoRecepcionAutorizacion == "EEV" || f.CiEstadoRecepcionAutorizacion == "ENP") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                notaDebito.estado.EEV = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.NotaDebitos.Where(f => (f.CiEstadoRecepcionAutorizacion == "ECP" || f.CiEstadoRecepcionAutorizacion == "ENV") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                notaDebito.estado.ECP = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;

                return response;
            }

            TipoDocumentoDTO CompRetencion = new TipoDocumentoDTO()
            {
                TxDescripcion = "Comprobante de Retencion",
                CiTipoDocumento = "07",
                estado = new EstadosDTO(),
            };

            try
            {
                List<CompRetencion> fechas = new();

                fechas = await _context.CompRetencions.Where(f => f.CiEstado == "A" && f.CiCompania == CiCompania).ToListAsync();
                CompRetencion.estado.A = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => f.CiEstadoRecepcionAutorizacion == "FI").ToListAsync();
                CompRetencion.estado.FI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").ToListAsync();
                CompRetencion.estado.EFI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => f.CiEstadoRecepcionAutorizacion == "ERE" || !f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiEstadoRecepcionAutorizacion == "AU").ToListAsync();
                CompRetencion.estado.ERE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => f.CiEstadoRecepcionAutorizacion == "RE").ToListAsync();
                CompRetencion.estado.RE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => f.CiEstadoRecepcionAutorizacion == "EAU" || f.CiEstadoRecepcionAutorizacion == "NAU" || f.CiEstadoRecepcionAutorizacion == "RAU").ToListAsync();
                CompRetencion.estado.EAU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                CompRetencion.estado.AU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => (f.CiEstadoRecepcionAutorizacion == "EEV" || f.CiEstadoRecepcionAutorizacion == "ENP") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                CompRetencion.estado.EEV = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.CompRetencions.Where(f => (f.CiEstadoRecepcionAutorizacion == "ECP" || f.CiEstadoRecepcionAutorizacion == "ENV") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                CompRetencion.estado.ECP = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();


            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;

                return response;
            }

            TipoDocumentoDTO liquidacion = new TipoDocumentoDTO()
            {
                TxDescripcion = "Liquidacion",
                CiTipoDocumento = "03",
                estado = new EstadosDTO()
            };

            try
            {
                List<Liquidacion> fechas = new();
                fechas = await _context.Liquidacions.Where(f => f.CiEstado == "A" && f.CiCompania == CiCompania).ToListAsync();
                liquidacion.estado.A = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => f.CiEstadoRecepcionAutorizacion == "FI").ToListAsync();
                liquidacion.estado.FI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").ToListAsync();
                liquidacion.estado.EFI = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => f.CiEstadoRecepcionAutorizacion == "ERE" || !f.XmlDocumentoAutorizado.Contains(">AUTORIZADO") && f.CiEstadoRecepcionAutorizacion == "AU").ToListAsync();
                liquidacion.estado.ERE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => f.CiEstadoRecepcionAutorizacion == "RE").ToListAsync();
                liquidacion.estado.RE = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => f.CiEstadoRecepcionAutorizacion == "EAU" || f.CiEstadoRecepcionAutorizacion == "NAU" || f.CiEstadoRecepcionAutorizacion == "RAU").ToListAsync();
                liquidacion.estado.EAU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                liquidacion.estado.AU = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => (f.CiEstadoRecepcionAutorizacion == "EEV" || f.CiEstadoRecepcionAutorizacion == "ENP") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                liquidacion.estado.EEV = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

                fechas = await _context.Liquidacions.Where(f => (f.CiEstadoRecepcionAutorizacion == "ECP" || f.CiEstadoRecepcionAutorizacion == "ENV") && f.XmlDocumentoAutorizado.Contains(">AUTORIZADO")).ToListAsync();
                liquidacion.estado.ECP = fechas.Where(f => DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) >= DateTime.ParseExact(fechaInicio, dateformat, cultures) && DateTime.ParseExact(f.TxFechaEmision, dateformat, cultures) <= DateTime.ParseExact(fechaFin, dateformat, cultures)).Count();
                fechas.Clear();

            } 
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;

                return response;
            }


            tipoDocumentoDTO.Add(factura);
            tipoDocumentoDTO.Add(NotaCredito);
            tipoDocumentoDTO.Add(notaDebito);
            tipoDocumentoDTO.Add(CompRetencion);
            tipoDocumentoDTO.Add(liquidacion);
            
            response.Code = ResponseType.Success;
            response.Message = "";
            response.Data = tipoDocumentoDTO;
            return response;
        }
    }
}
