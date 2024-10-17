using EntityLayer.DTO.NotaDebitoDTO;
using EntityLayer.Mappers.NotaDebitoMapper;
using EntityLayer.Models;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositorio 
{
    public class NotaDebitoRepositorio : INotaDebitoRepositorio
    {
        private readonly FacturacionElectronicaQaContext _context;

        private readonly NotaDebitoMapper notaDebitoMapper = new();
        private readonly NotaDebitoImpuestoMapper notaDebitoImpuestoMapper = new();
        private readonly NotaDebitoInfoAdicionalMapper notaDebitoInfoAdicionalMapper = new();
        private readonly NotaDebitoMotivoMapper notaDebitoMotivoMapper = new();

        public NotaDebitoRepositorio(FacturacionElectronicaQaContext context)
        {
            _context = context;
        }

        public async Task<Response> IngresarNotaDebito(NotaDebitoDTO notaDebitoDTO)
        {
            Response response = new Response();

            try
            {
                try
                {
                    NotaDebito nuevoDebito = notaDebitoMapper.NotaDebitoToNotaDebitoDTO(notaDebitoDTO);
                    _context.NotaDebitos.Add(nuevoDebito);
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro nota debito cabecera {ex.Message}";
                    response.Data = ex.Data;

                    return response;
                }

                try
                {
                    foreach (NotaDebitoImpuestoDTO notaDebitoImpuestoDTO in notaDebitoDTO.notaDebitoImpuestoModelo)
                    {
                        NotaDebitoImpuesto notaDebitoImpuesto = notaDebitoImpuestoMapper.NDImpuestoToNDImpuestoDTO(notaDebitoImpuestoDTO);
                        _context.NotaDebitoImpuestos.Add(notaDebitoImpuesto);
                    }

                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro nota debito impuesto{ex.Message}";
                    response.Data = ex.Data;

                    return response;
                }

                try
                {
                    foreach (NotaDebitoInfoAdicionalDTO notaDebitoInfoAdicionalDTO in notaDebitoDTO.notaDebitoInfoAdicionalModelo)
                    {
                        NotaDebitoInfoAdicional notaDebitoInfoAdicional = notaDebitoInfoAdicionalMapper.NDInfoAdicionalToNDIndoAdicionalDTO(notaDebitoInfoAdicionalDTO);
                        _context.NotaDebitoInfoAdicionals.Add(notaDebitoInfoAdicional);
                    }
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro nota debito informacion adicional{ex.Message}";
                    response.Data = ex.Data;

                    return response;
                }

                /* NotaDebitoModelo 
                try
                {
                    foreach (NotaDebitoMotivoDTO notaDebitoMotivoDTO in notaDebitoDTO.notaDebitoMotivoModelo)
                    {
                        NotaDebitoMotivo notaDebitoMotivo = notaDebitoMotivoMapper.NotaDebitoMotivoToNotaDebitoMotivoDTO(notaDebitoMotivoDTO);
                        _context.NotaDebitoMotivos.Add(notaDebitoMotivo);
                    }

                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro nota debito motivol{ex.Message}";
                    response.Data = ex.Data;

                    return response;
                }
                */
                
                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = $"Nota de credito ingresada correctamente";
                response.Data = null;

            }
            catch (Exception ex) 
            {
                response.Code = ResponseType.Error;
                response.Message = $"Error no se pudo registrar nota debito{ex.Message}";
                response.Data = ex.Data;

                return response;
            }
            return response;
        }

    }
}
