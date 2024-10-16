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
    public partial class NotaDebitoImpuestoMapper
    {
        public partial NotaDebitoImpuesto NDImpuestoToNDImpuestoDTO(NotaDebitoImpuestoDTO notaDebitoImpuestoDTO);
        public partial NotaDebitoImpuestoDTO NDImpuestoToNDImpuestoDTO(NotaDebitoImpuesto notaDebitoImpuesto);
    }
}
