using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTO.NotaDebitoDTO
{
    public class NotaDebitoInfoAdicionalDTO
    {
        public int CiNotaDebitoInfoAdicional { get; set; }

        public int CiCompania { get; set; }

        public string TxEstablecimiento { get; set; } = null!;

        public string TxPuntoEmision { get; set; } = null!;

        public string TxSecuencial { get; set; } = null!;

        public string? TxNombre { get; set; }

        public string? TxValor { get; set; }
    }
}
