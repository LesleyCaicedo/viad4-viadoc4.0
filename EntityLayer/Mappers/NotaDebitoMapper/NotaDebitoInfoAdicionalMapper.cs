using EntityLayer.DTO.NotaDebitoDTO;
using EntityLayer.Models;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Mappers.NotaDebitoMapper
{
    [Mapper]
    public partial class NotaDebitoInfoAdicionalMapper
    {
        public partial NotaDebitoInfoAdicional NDInfoAdicionalToNDIndoAdicionalDTO(NotaDebitoInfoAdicionalDTO notaDebitoInfoAdicionalDTO);
        public partial NotaDebitoInfoAdicionalDTO NDInfoAdicionalToNDIndoAdicionalDTO(NotaDebitoInfoAdicional notaDebitoInfoAdicional);
    }
}
