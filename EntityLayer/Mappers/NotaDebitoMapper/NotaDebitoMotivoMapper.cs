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
    public partial class NotaDebitoMotivoMapper
    {
        public partial NotaDebitoMotivo NotaDebitoMotivoToNotaDebitoMotivoDTO(NotaDebitoMotivoDTO notaDebitoMotivoDTO);
        public partial NotaDebitoMotivoDTO NotaDebitoMotivoToNotaDebitoMotivoDTO(NotaDebitoMotivo notaDebitoMotivo);
    }
}
