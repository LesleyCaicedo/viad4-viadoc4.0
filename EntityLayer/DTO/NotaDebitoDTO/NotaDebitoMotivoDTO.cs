using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTO.NotaDebitoDTO
{
    public class NotaDebitoMotivoDTO
    {
        public int CiCompania { get; set; }

        public string TxEstablecimiento { get; set; } = null!;

        public string TxPuntoEmision { get; set; } = null!;

        public string TxSecuencial { get; set; } = null!;

        public string TxRazon { get; set; } = null!;

        public decimal QnValor { get; set; }
    }
}
