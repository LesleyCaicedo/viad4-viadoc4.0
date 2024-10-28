using EntityLayer.DTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IPruebaServicio
    {
        public Task<Response> BuscarDocumentosError(string tipoDocumento);
        public Task<Response> TDocumentosEmpresas(int CiCompania, string fechaInicio, string fechaFin);
        public Task<Response> BusquedaDocFiltros(FiltroDocDTO filtroDocDTO);
        public Task<Response> FiltroBusqueda(string claveAcceso);
    }
}
