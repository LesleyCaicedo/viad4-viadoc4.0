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

        public EstadosDTO estado { get; set; }
    }

    public class EstadosDTO
    {
        public int A {  get; set; } = 0;
        public int FI { get; set; } = 0;
        public int EFI { get; set; } = 0;
        public int ERE { get; set; } = 0;
        public int RE { get; set; } = 0;
        public int EAU { get; set; } = 0;
        public int AU { get; set; } = 0;
        public int EEV { get; set; } = 0;
        public int ECP { get; set; } = 0;
        public int Total { get; set; } = 0;
    
    }
}
