using EntityLayer.DTO.NotaDebitoDTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositorio
{
    public interface INotaDebitoRepositorio
    {
        public Task<Response> IngresarNotaDebito(NotaDebitoDTO notaDebitoDTO);
    }
}
