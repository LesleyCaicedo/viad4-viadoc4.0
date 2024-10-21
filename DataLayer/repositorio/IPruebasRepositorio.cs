using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositorio
{
    public interface IPruebasRepositorio
    {
        public Task<Response> BuscarDocumentosError(string tipoDocumento);

        public Task<Response> TDocumentosEmpresas(int CiCompania, string fechaInicio, string fechaFin);
    }
}
