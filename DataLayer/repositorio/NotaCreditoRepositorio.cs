using DataLayer.Utilities;
using EntityLayer.DTO.NotaCreditoDTO;
using EntityLayer.Mappers.NotaCreditoMapper;
using EntityLayer.Models;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositorio
{
    public class NotaCreditoRepositorio : INotaCreditoRepositorio
    {
        private readonly FacturacionElectronicaQaContext _context;
        private readonly NotaCreditoMapper notaCreditoMapper = new();
        private readonly NotaCreditoDetalleMapper notaCreditoDetalleMapper = new();
        private readonly NCDetalleAdicionalMapper nCDetalleAdicionalMapper = new();
        private readonly NCDetalleImpuestoMapper nCDetalleImpuestoMapper = new();
        private readonly NCInfoAdicionalMapper ncInfoAdicionalMapper = new();
        private readonly NCTotalImpuestoMapper NCTotalImpuestoMapper = new();

        public NotaCreditoRepositorio(FacturacionElectronicaQaContext context)
        {
            _context = context;
        }

        public async Task<Response> IngresarNotaCredito(NotaCreditoDTO notaCreditoDTO)
        {
            Response response = new Response();
            Utilitie util = new Utilitie(_context);

            try
            {
                try
                {
                    response = await util.ComprobarClaveacceso(notaCreditoDTO.TxClaveAcceso, notaCreditoDTO.CiTipoDocumento, notaCreditoDTO.TxSecuencial, notaCreditoDTO.TxPuntoEmision, notaCreditoDTO.TxEstablecimiento);

                    if (notaCreditoDTO.TxClaveAcceso == "")
                    {
                        notaCreditoDTO.TxClaveAcceso = util.GenerarClaveAcceso(notaCreditoDTO.CiTipoDocumento, notaCreditoDTO.TxSecuencial, notaCreditoDTO.TxPuntoEmision, notaCreditoDTO.TxEstablecimiento, notaCreditoDTO.TxFechaEmision, notaCreditoDTO.ruc, notaCreditoDTO.ciAmbiente);
                    }
                    
                    if (response.Code == ResponseType.Error)
                    {
                        return response;
                    }
                    
                    NotaCredito nuevaNC = notaCreditoMapper.NotaCreditoToNotaCreditoDTO(notaCreditoDTO);
                    _context.NotaCreditos.Add(nuevaNC);

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro nota credito cabecera {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    foreach (NotaCreditoDetalleDTO notaCreditoDetalleDTO in notaCreditoDTO.NotaCreditoDetalleModelo)
                    {
                        NotaCreditoDetalle notaCreditoDetalle = notaCreditoDetalleMapper.NCDetalleToNCDetalleDTO(notaCreditoDetalleDTO);
                        _context.NotaCreditoDetalles.Add(notaCreditoDetalle);

                        try
                        {
                            for (int i = 0; i < notaCreditoDetalleDTO.notaCreditoDetalleAdicionalModelo.Count; i++)
                            {
                                try
                                {
                                    NotaCreditoDetalleAdicional notaCreditoDetalleAdicional = nCDetalleAdicionalMapper.NCDetalleAdicionalToNCDetalleAdicionalDTO(notaCreditoDetalleDTO.notaCreditoDetalleAdicionalModelo[i]);
                                    _context.NotaCreditoDetalleAdicionals.Add(notaCreditoDetalleAdicional);

                                    await _context.SaveChangesAsync();
                                }
                                catch (Exception ex)
                                {
                                    response.Code = ResponseType.Error;
                                    response.Message = $"Error registro detalle adicional numero {i} {ex.Message}";
                                    response.Data = ex.Data;
                                    return response;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            response.Code = ResponseType.Error;
                            response.Message = $"Error registro detalle adicional nota credito {ex.Message}";
                            response.Data = ex.Data;
                            return response;
                        }

                        try
                        {
                            for (int i = 0; i < notaCreditoDetalleDTO.notaCreditoDetalleImpuestoModelo.Count; i++)
                            {
                                try
                                {
                                    NotaCreditoDetalleImpuesto notaCreditoDetalleImpuesto = nCDetalleImpuestoMapper.NCDetalleImpuestoToNCDetalleImpuestoDTO(notaCreditoDetalleDTO.notaCreditoDetalleImpuestoModelo[i]);
                                    _context.NotaCreditoDetalleImpuestos.Add(notaCreditoDetalleImpuesto);

                                    await _context.SaveChangesAsync();
                                }
                                catch (Exception ex)
                                {
                                    response.Code = ResponseType.Error;
                                    response.Message = $"Error registro detalle impuesto numero {i} {ex.Message}";
                                    response.Data = ex.Data;
                                    return response;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            response.Code = ResponseType.Error;
                            response.Message = $"Error registro detalle impuesto nota credito {ex.Message}";
                            response.Data = ex.Data;
                            return response;
                        }

                    }

                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro detalle nota credito {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    foreach (NotaCreditoTotalImpuestoDTO notaCreditoTotalImpuestoDTO in notaCreditoDTO.notaCreditoTotalImpuestoModelo)
                    {
                        NotaCreditoTotalImpuesto notaCreditoTotalImpuesto = NCTotalImpuestoMapper.NCTotalImpuestoToNCTotalImpuestoDTO(notaCreditoTotalImpuestoDTO);
                        _context.NotaCreditoTotalImpuestos.Add(notaCreditoTotalImpuesto);

                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro nota credito cabecera {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    foreach (NotaCreditoInfoAdicionalDTO notaCreditoInfoAdicionalDTO in notaCreditoDTO.NotaCreditoInfoAdicionalModelo)
                    {
                        NotaCreditoInfoAdicional notaCreditoInfoAdicional = ncInfoAdicionalMapper.NCInfoAdicionalToNCInfoAdicionalDTO(notaCreditoInfoAdicionalDTO);
                        _context.NotaCreditoInfoAdicionals.Add(notaCreditoInfoAdicional);

                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro nota credito cabecera {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = $"Nota de credito ingresada correctamente";
                response.Data = null;

            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = $"Error registro de nota credito {ex.Message}";
                response.Data = ex.Data;
                return response;
            }
            return response;
        }


    }
}
