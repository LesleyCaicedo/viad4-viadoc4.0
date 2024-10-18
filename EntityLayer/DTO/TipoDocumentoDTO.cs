using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTO
{
    public class TipoDocumentoDTO
    {
        public string CiTipoDocumento { get; set; } = null!;

        public string TxDescripcion { get; set; } = null!;

        public string CiEstado { get; set; } = null!;
    }
}
