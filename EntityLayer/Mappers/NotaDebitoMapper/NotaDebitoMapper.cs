﻿using EntityLayer.DTO.NotaDebitoDTO;
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
    public partial class NotaDebitoMapper
    {
        public partial NotaDebito NotaDebitoToNotaDebitoDTO(NotaDebitoDTO notaDebitoDTO);
        public partial NotaDebitoDTO NotaDebitoToNotaDebitoDTO(NotaDebito notaDebito);
    }
}
