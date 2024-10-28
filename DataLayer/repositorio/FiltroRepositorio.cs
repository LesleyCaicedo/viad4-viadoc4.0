using EntityLayer.Models;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositorio
{
    public class FiltroRepositorio : IFiltroRepositorio
    {
        private readonly FacturacionElectronicaQaContext _context;
        Response response = new Response();

        public FiltroRepositorio(FacturacionElectronicaQaContext context)
        {
            _context = context;
        }

        public async Task<Response> FiltroBusqueda(string claveAcceso)
        {
            List<Factura1> lista = new List<Factura1>();

            try
            {
                if (claveAcceso == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Error";
                }
                else
                {
                    lista = await _context.Facturas1.Where(x => x.TxClaveAcceso == claveAcceso).ToListAsync();

                    response.Data = lista;
                }

                return response;
            } catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}
