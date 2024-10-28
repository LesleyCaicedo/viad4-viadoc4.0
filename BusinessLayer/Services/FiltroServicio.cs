using DataLayer.repositorio;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class FiltroServicio : IFiltroServicio
    {
        private readonly IFiltroRepositorio _filtroRepositorio;
        Response response = new Response();

        public FiltroServicio(IFiltroRepositorio filtroRepositorio)
        {
            _filtroRepositorio = filtroRepositorio;
        }

        public async Task<Response> FiltroBusqueda(string claveAcceso)
        {
            response = await _filtroRepositorio.FiltroBusqueda(claveAcceso);
            return response;
        }
    }
}
