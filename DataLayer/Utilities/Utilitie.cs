using EntityLayer.DTO.FacturaDTO;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore;
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

            if (tipoDocu != null) 
            {
                switch (tipoDocu) {

                    case "01":

                        List<string> claveacceso = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").Select(f => f.TxClaveAcceso).ToListAsync();

                        var error = await _context.Facturas1.Where(f => f.CiEstadoRecepcionAutorizacion == "EFI").Select(f => f.TxSecuencial , f.TxPuntoEmision).ToListAsync();


                        break;

                
                }            
            }
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
    
        
    }
}
