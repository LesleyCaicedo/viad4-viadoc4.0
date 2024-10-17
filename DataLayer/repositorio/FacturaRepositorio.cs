using EntityLayer.Responses;
using EntityLayer.Models;
using DataLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.DTO.FacturaDTO;
using EntityLayer.Mappers.FacturaMapper;

namespace DataLayer.repositorio
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private readonly FacturacionElectronicaQaContext _context;
        private readonly FacturaMapper facturaMapper = new();
        private readonly FacturaDetalleMapper facturaDetalleMapper = new();
        private readonly FacturaDetalleAdicionalMapper facturaDetalleAdicionalMapper = new();
        private readonly FacturaDetalleImpuestoMapper facturaDetalleImpuestoMapper = new();
        private readonly FacturaInfoAdicionalMapper facturaInfoAdicionalMapper = new();
        private readonly FacturaTotalImpuestoMapper facturaTotalImpuestoMapper = new();
        private readonly FacturaDetalleFormaPago1Mappper facturaDetalleFormaPago1Mappper = new();
        private readonly FacturaReembolsoMapper facturaReembolsoMapper = new();

        public FacturaRepositorio(FacturacionElectronicaQaContext context)
        {
            _context = context;
        }



        public async Task<Response> IngresarFactura(Factura1DTO factura1DTO)
        {
            Response response = new Response();
            Utilitie util = new Utilitie(_context);

            try
            {
                try
                {
                    response = await util.ComprobarClaveacceso(factura1DTO.TxClaveAcceso, factura1DTO.CiTipoDocumento, factura1DTO.TxSecuencial, factura1DTO.TxPuntoEmision, factura1DTO.TxEstablecimiento);

                    if(factura1DTO.TxClaveAcceso == "")
                    {
                       factura1DTO.TxClaveAcceso = util.GenerarClaveAcceso(factura1DTO.CiTipoDocumento, factura1DTO.TxSecuencial, factura1DTO.TxPuntoEmision, factura1DTO.TxEstablecimiento, factura1DTO.TxFechaEmision, factura1DTO.ruc, factura1DTO.ciAmbiente);
                    }

                    if (response.Code == ResponseType.Error) 
                    {
                        return response;
                    }

                    Factura1 nuevaFactura = facturaMapper.Factura1ToFactura1DTO(factura1DTO);
                    _context.Facturas1.Add(nuevaFactura);

                }
                catch (Exception ex) 
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro factura cabecera {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    foreach (FacturaDetalle1DTO facturaDetalle1DTO in factura1DTO.facturaDetalleModelo)
                    {
                        FacturaDetalle1 facturaDetalle1 = facturaDetalleMapper.FacturaDetalle1ToFamiliaDetalle1DTO(facturaDetalle1DTO);
                        _context.FacturaDetalles1.Add(facturaDetalle1);
                        try
                        {
                            for (int i = 0; i < facturaDetalle1DTO.facturaDetalleAdicionalModelo.Count; i++)
                            {
                                if(facturaDetalle1DTO.facturaDetalleAdicionalModelo[i].TxCodigoPrincipal != null)
                                {
                                    if(facturaDetalle1DTO.facturaDetalleAdicionalModelo[i].TxCodigoPrincipal != "")
                                    {
                                        try
                                        {
                                            FacturaDetalleAdicional facturaDetalleAdicional = facturaDetalleAdicionalMapper.FacturaDetalleAdicionalToFacturaDetalleAdicionalDTO(facturaDetalle1DTO.facturaDetalleAdicionalModelo[i]);
                                            _context.FacturaDetalleAdicionals.Add(facturaDetalleAdicional);
                                        }
                                        catch (Exception ex)
                                        {
                                            response.Code = ResponseType.Error;
                                            response.Message = $"Error registro detalle adicional en el numero {i} {ex.Message}";
                                            response.Data = ex.Data;
                                            return response;
                                        }
                                    }
                                    
                                }
                                
                            }
                            
                            for (int i = 0; i < facturaDetalle1DTO.facturaDetalleImpuestoModelo.Count; i++)
                            {
                                try
                                {
                                    FacturaDetalleImpuesto1 facturaDetalleImpuesto1 = facturaDetalleImpuestoMapper.FacturaDetalleImpuesto1ToFacturaDetalleImpuesto1DTO(facturaDetalle1DTO.facturaDetalleImpuestoModelo[i]);
                                    _context.FacturaDetalleImpuestos1.Add(facturaDetalleImpuesto1);

                                }
                                catch (Exception ex)
                                {
                                    response.Code = ResponseType.Error;
                                    response.Message = $"Error registro detalle impuesto en el numero {i} {ex.Message}";
                                    response.Data = ex.Data;
                                    return response;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            response.Code = ResponseType.Error;
                            response.Message = $"Error {ex.Message}";
                            response.Data = ex.Data;

                            return response;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro factura detalle {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    foreach (FacturaTotalImpuestoDTO facturaTotalImpuestoDTO in factura1DTO.facturaTotalImpuestoModelo)
                    {
                        FacturaTotalImpuesto facturaTotalImpuesto = facturaTotalImpuestoMapper.FacturaTotalImpuestoToFacturaTotalImpuestoDTO(facturaTotalImpuestoDTO);
                        _context.FacturaTotalImpuestos.Add(facturaTotalImpuesto);
                    }
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro factura total impuesto adicional {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    foreach(FacturaDetalleFormaPago1DTO facturaDetalleFormaPago1DTO in factura1DTO.facturaDetalleFormaPagoModelo)
                    {
                        FacturaDetalleFormaPago1 facturaDetalleFormaPago1 = facturaDetalleFormaPago1Mappper.FacturaFP1ToFacturaFP1DTO(facturaDetalleFormaPago1DTO);
                        _context.FacturaDetalleFormaPagos1.Add(facturaDetalleFormaPago1);
                    }

                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro forma de pago {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    foreach (FacturaInfoAdicionalDTO facturaInfoAdicionalDTO in factura1DTO.facturaInfoAdicionalModelo)
                    {
                        FacturaInfoAdicional facturaInfoAdicional = facturaInfoAdicionalMapper.FacturaInfoAdicionalToFacturaInfoAdicionalDTO(facturaInfoAdicionalDTO);
                        _context.FacturaInfoAdicionals.Add(facturaInfoAdicional);
                    }
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro factura informacion adicional {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                try
                {
                    if(factura1DTO.facturaReembolsoModelo != null){
                        for (int i = 0; i < factura1DTO.facturaReembolsoModelo.Count; i++)
                        {
                            try
                            {
                                FacturaReembolsoGasto facturaReembolsoGasto = facturaReembolsoMapper.FacturaReembolsoToFacturaReembolsoDTO(factura1DTO.facturaReembolsoModelo[i]);
                                _context.FacturaReembolsoGastos.Add(facturaReembolsoGasto);

                            }
                            catch (Exception ex)
                            {
                                response.Code = ResponseType.Error;
                                response.Message = $"Error registro factura reembolso {i} {ex.Message}";
                                response.Data = ex.Data;
                                return response;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = $"Error registro factura reembolso {ex.Message}";
                    response.Data = ex.Data;
                    return response;
                }

                await _context.SaveChangesAsync();
                response.Code = ResponseType.Success;
                response.Message = "Casa ingresado correctamente";
                response.Data = factura1DTO;
                return response;

            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = $"Error registro factura {ex.Message}";
                response.Data = ex.Data;
            }
            return response;

        }
    }
}
