using DataLayer.repositorio;
using EntityLayer.Models;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PruebasServicio : IPruebaServicio
    {
        private readonly IPruebasRepositorio pruebasRepositorio;

        Response response = new Response();

        public PruebasServicio(IPruebasRepositorio pruebasRepositorio)
        {
            this.pruebasRepositorio = pruebasRepositorio;
        }

        public async Task<Response> BuscarDocumentosError(string tipoDocumento)
        {
            response = await pruebasRepositorio.BuscarDocumentosError(tipoDocumento);
            return response;
        }

        public async Task<Response> TDocumentosEmpresas(string idCompañia, string fechaInicio, string fachaFinal)
        {
            List<TipoDocumento>

        }


    }
}
