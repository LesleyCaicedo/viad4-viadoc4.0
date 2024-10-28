using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IFiltroServicio
    {
        public Task<Response> FiltroBusqueda(string claveAcceso);
    }
}
