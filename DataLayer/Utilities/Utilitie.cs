
using EntityLayer.DTO;
using EntityLayer.DTO.FacturaDTO;
using EntityLayer.Models;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities
{
    public class Utilitie
    {
        private readonly FacturacionElectronicaQaContext _context;

        public Utilitie(FacturacionElectronicaQaContext context)
        {
            _context = context;
        }

        public async Task<Response> DocumentosNoAutorizados(string tipoDocu)
        {
            Response response = new Response();
            List<string> claveacceso = new List<string>();

            if (tipoDocu != null) 
            {
                switch (tipoDocu) {

                    case "01":

                        claveacceso = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI" ||
                                                                          f.CiEstadoRecepcionAutorizacion == "ERE" ||
                                                                          f.CiEstadoRecepcionAutorizacion == "EAU" || 
                                                                          f.CiEstadoRecepcionAutorizacion == "ECO" || 
                                                                          f.CiEstadoRecepcionAutorizacion == "NAU" || 
                                                                          f.CiEstadoRecepcionAutorizacion == "RAU" || 
                                                                          f.CiEstadoRecepcionAutorizacion == "RER").Select(f => f.TxClaveAcceso).ToListAsync();
                        
                        response.Code = ResponseType.Success;
                        response.Message = "Lista de facturas error";
                        response.Data = claveacceso;
                        //var error = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").Select(f => new Factura1DTO { TxSecuencial = f.TxSecuencial, TxPuntoEmision = f.} ).ToListAsync();

                        break;

                    case "03":

                        claveacceso = await _context.Liquidacions.Where(l => l.CiEstadoRecepcionAutorizacion == "EFI" ||
                                                                             l.CiEstadoRecepcionAutorizacion == "ERE" ||
                                                                             l.CiEstadoRecepcionAutorizacion == "EAU" ||
                                                                             l.CiEstadoRecepcionAutorizacion == "ECO" ||
                                                                             l.CiEstadoRecepcionAutorizacion == "NAU" ||
                                                                             l.CiEstadoRecepcionAutorizacion == "RAU" ||
                                                                             l.CiEstadoRecepcionAutorizacion == "RER").Select(l => l.TxClaveAcceso).ToListAsync();

                        response.Code = ResponseType.Success;
                        response.Message = "Lista de liquidaciones error";
                        response.Data = claveacceso;

                        break;

                    case "04":

                        claveacceso = await _context.NotaCreditos.Where(c => c.CiEstadoRecepcionAutorizacion == "EFI" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "ERE" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "EAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "ECO" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "NAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "RAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "RER").Select(c => c.TxClaveAcceso).ToListAsync();

                        response.Code = ResponseType.Success;
                        response.Message = "Lista de Notas Credito error";
                        response.Data = claveacceso;

                        break;

                    case "05":

                        claveacceso = await _context.NotaDebitos.Where(d => d.CiEstadoRecepcionAutorizacion == "EFI" ||
                                                                            d.CiEstadoRecepcionAutorizacion == "ERE" ||
                                                                            d.CiEstadoRecepcionAutorizacion == "EAU" ||
                                                                            d.CiEstadoRecepcionAutorizacion == "ECO" ||
                                                                            d.CiEstadoRecepcionAutorizacion == "NAU" ||
                                                                            d.CiEstadoRecepcionAutorizacion == "RAU" ||
                                                                            d.CiEstadoRecepcionAutorizacion == "RER").Select(d => d.TxClaveAcceso).ToListAsync();

                        response.Code = ResponseType.Success;
                        response.Message = "Lista de Notas Debito error";
                        response.Data = claveacceso;

                        break;

                    case "06":

                        claveacceso = await _context.GuiaRemisions.Where(c => c.CiEstadoRecepcionAutorizacion == "EFI" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "ERE" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "EAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "ECO" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "NAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "RAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "RER").Select(c => c.TxClaveAcceso).ToListAsync();

                        response.Code = ResponseType.Success;
                        response.Message = "Lista de Remisiones error";
                        response.Data = claveacceso;

                        break;

                    case "07":

                        claveacceso = await _context.CompRetencions.Where(c => c.CiEstadoRecepcionAutorizacion == "EFI" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "ERE" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "EAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "ECO" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "NAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "RAU" ||
                                                                             c.CiEstadoRecepcionAutorizacion == "RER").Select(c => c.TxClaveAcceso).ToListAsync();

                        response.Code = ResponseType.Success;
                        response.Message = "Lista de error";
                        response.Data = claveacceso;


                        break;

                    default:
                        response.Code = ResponseType.Error;
                        response.Message = "No ingreso un tipo de documento valido";
                        break;

                }            
            }

            return response;
        }

        public async Task<Response> ComprobarClaveacceso(string claveacceso, string tipoDocu, string txSecuencial, 
                                                         string txPuntoEmision, string txEstablecimiento)
        {
            Response response = new Response();

            if (tipoDocu != null || tipoDocu != string.Empty)
            {
                switch (tipoDocu)
                {
                    case "01":
                        var factura = await _context.Facturas1.FirstOrDefaultAsync(f => f.TxClaveAcceso == claveacceso && 
                                                                                        f.TxEstablecimiento == txEstablecimiento && 
                                                                                        f.TxSecuencial == txSecuencial && 
                                                                                        f.TxPuntoEmision == txPuntoEmision);
                        if (factura == null)
                        {
                            response.Code = ResponseType.Success;
                            response.Message = string.Empty;
                        }
                        else
                        {
                            response.Code = ResponseType.Error;
                            response.Message = "Existe esa Clave Acceso";
                        }
                        break;

                    case "03":
                        var liquidacion = await _context.Liquidacions.FirstOrDefaultAsync(f => f.TxClaveAcceso == claveacceso &&
                                                                                          f.TxEstablecimiento == txEstablecimiento &&
                                                                                          f.TxSecuencial == txSecuencial &&
                                                                                          f.TxPuntoEmision == txPuntoEmision);
                        if (liquidacion == null)
                        {
                            response.Code = ResponseType.Success;
                            response.Message = string.Empty;
                        }
                        else
                        {
                            response.Code = ResponseType.Error;
                            response.Message = "Existe esa Clave Acceso";
                        }
                        break;

                    case "04":
                        var notacredito = await _context.NotaCreditos.FirstOrDefaultAsync(f => f.TxClaveAcceso == claveacceso &&
                                                                                          f.TxEstablecimiento == txEstablecimiento &&
                                                                                          f.TxSecuencial == txSecuencial &&
                                                                                          f.TxPuntoEmision == txPuntoEmision);
                        if (notacredito == null)
                        {
                            response.Code = ResponseType.Success;
                            response.Message = string.Empty;
                        }
                        else
                        {
                            response.Code = ResponseType.Error;
                            response.Message = "Existe esa Clave Acceso";
                        }
                        break;

                    case "05":
                        var notadebito = await _context.NotaDebitos.FirstOrDefaultAsync(f => f.TxClaveAcceso == claveacceso &&
                                                                                        f.TxEstablecimiento == txEstablecimiento &&
                                                                                        f.TxSecuencial == txSecuencial &&
                                                                                        f.TxPuntoEmision == txPuntoEmision);
                        if (notadebito == null)
                        {
                            response.Code = ResponseType.Success;
                            response.Message = string.Empty;
                        }
                        else
                        {
                            response.Code = ResponseType.Error;
                            response.Message = "Existe esa Clave Acceso";
                        }
                        break;

                    case "06":
                        var guiaremision = await _context.GuiaRemisions.FirstOrDefaultAsync(f => f.TxClaveAcceso == claveacceso &&
                                                                                            f.TxEstablecimiento == txEstablecimiento &&
                                                                                            f.TxSecuencial == txSecuencial &&
                                                                                            f.TxPuntoEmision == txPuntoEmision);
                        if (guiaremision == null)
                        {
                            response.Code = ResponseType.Success;
                            response.Message = string.Empty;
                        }
                        else
                        {
                            response.Code = ResponseType.Error;
                            response.Message = "Existe esa Clave Acceso";
                        }
                        break;

                    case "07":
                        var comprobanteRetencion = await _context.CompRetencions.FirstOrDefaultAsync(f => f.TxClaveAcceso == claveacceso &&
                                                                                                     f.TxEstablecimiento == txEstablecimiento &&
                                                                                                     f.TxSecuencial == txSecuencial &&
                                                                                                     f.TxPuntoEmision == txPuntoEmision);
                        if (comprobanteRetencion == null)
                        {
                            response.Code = ResponseType.Success;
                            response.Message = string.Empty;
                        }
                        else
                        {
                            response.Code = ResponseType.Error;
                            response.Message = "Existe esa Clave Acceso";
                        }
                        break;


                    default:
                        response.Code = ResponseType.Error;
                        response.Message = "No ingreso un tipo de documento valido";
                        break;
                }

                return response;
            }
            else
            {
                response.Code = ResponseType.Error;
                response.Message = "No envio documento valido";
                response.Data = claveacceso;
                return response;
            }
        }
    
        public string GenerarClaveAcceso(string ciTipoDoc,string txSecuencial, string txPuntoEmision, string txEstablecimiento, 
                                                 string txFechaEmision, string ruc, string ciAmbiente )
        {
            string claveAcceso = string.Empty;


            claveAcceso += txFechaEmision.Replace("/", "").Replace("-", "");
            claveAcceso += ciTipoDoc;
            claveAcceso += ruc;
            claveAcceso += ciAmbiente;
            claveAcceso += txEstablecimiento;
            claveAcceso += txPuntoEmision;
            claveAcceso += txSecuencial;
            claveAcceso += NumeroA();
            claveAcceso += "1";
            claveAcceso += CalculaDigitoVerificador(claveAcceso);

            return claveAcceso;
        }

        public string NumeroA()
        {
            Random rnd = new Random();

            string numRandom = rnd.Next(1, 99999999).ToString();
            numRandom = numRandom.PadLeft(8, '0');

            return numRandom;
        }

        public string CalculaDigitoVerificador(string txClaveAcceso)
        {
            Response response = new Response();

            int digitoVerificador = -1;
            try
            {
                char[] arrDigitosRuc = txClaveAcceso.ToCharArray();
                int[] arrResultado = new int[0];
                int cont = 2;
                decimal resultado = 0;

                for (int i = (txClaveAcceso.Length - 1); i >= 0; i--)
                {

                    int[] temp = new int[(txClaveAcceso.Length) - i];
                    if (arrResultado != null)
                        Array.Copy(arrResultado, temp, Math.Min(arrResultado.Length, temp.Length));
                    arrResultado = temp;

                    arrResultado[arrResultado.Length - 1] = Convert.ToInt32(arrDigitosRuc[i].ToString()) * cont;
                    cont += 1;
                    if (cont == 8)
                    {
                        cont = 2;
                    }
                }
                resultado = arrResultado.Sum() % 11;
                resultado = 11 - resultado;

                switch (Convert.ToInt32(resultado))
                {
                    case 10:
                        digitoVerificador = 1;
                        break;
                    case 11:
                        digitoVerificador = 0;
                        break;
                    default:
                        digitoVerificador = Convert.ToInt32(resultado);
                        break;
                }
            }
            catch (Exception ex)
            {
                
            }
            return digitoVerificador.ToString();
        }

        public async Task<Response> FiltroCA(int compania,string claveAcceso)
        {
            Response response = new Response();

            var empresa = await _context.Compania.FirstOrDefaultAsync(c => c.CiCompania == compania);

            if(empresa == null)
            {
                response.Code = ResponseType.Error;
                response.Message = "Empresa no existe";
            }
            else
            {
                if(claveAcceso != null)
                {
                    List<Factura1> datos = new List<Factura1>();

                    datos = await _context.Facturas1.Where(x => x.CiCompania == compania && x.TxClaveAcceso == claveAcceso).ToListAsync();

                    response.Code = ResponseType.Success;
                    response.Message = "_____________________";
                    response.Data = datos;

                    return response;
                }
            }

            return response;
        }
        
        //public async Task<Response> DocumentoFiltro(int CiCompania, string CiTipoDocumento, string NumDocumentos, string ClaveAcceso, string Identificacion, string NombreRS, string FechaInicio, string FechaFin, string Autorizacion)
        //{
        //    Response response = new Response();

        //    string tipoDocu = CiTipoDocumento;

        //    if (tipoDocu != null)
        //    {
        //        switch (tipoDocu)
        //        {
        //            case "01":
        //                List<Factura1> cabecera = new();        

        //                var compania = await _context.Compania.FirstOrDefaultAsync(c => c.CiCompania == CiCompania);

        //                if (compania == null)
        //                {
        //                    response.Code = ResponseType.Error;
        //                    response.Message = "Esa compañia no existe";
                            
        //                }else
        //                {
        //                    if (ClaveAcceso != null)
        //                    {
        //                        var cabecera1 = await _context.Facturas1.Where(r => r.CiCompania == CiCompania && r.TxClaveAcceso == ClaveAcceso).FirstOrDefaultAsync();
        //                        cabecera.Add(cabecera1);
        //                    }

        //                    //if (NombreRS != null)
        //                    //{
        //                    //    cabecera = await _context.Facturas1.Where(r => r.TxRazonSocialComprador == NombreRS).ToListAsync();
        //                    //}

        //                    response.Code = ResponseType.Success;
        //                    response.Message = "_____________________";
        //                    response.Data = cabecera;
        //                }        

        //                break;

        //            case "03":

                        
        //                break;

        //            case "04":

                        
        //                break;

        //            case "05":

                        
        //                break;

        //            case "06":

                        
        //                break;

        //            case "07":

                            

        //                break;

        //            default:
        //                response.Code = ResponseType.Error;
        //                response.Message = "No ingreso un tipo de documento valido";
        //                break;
        //        }

        //        return response;
        //    }

        //    return response;
        //}



    }
}
