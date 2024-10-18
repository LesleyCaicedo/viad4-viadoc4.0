using EntityLayer.Responses;
using DataLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
