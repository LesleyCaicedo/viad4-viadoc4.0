using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTO.FacturaDTO
{
    public class Factura1DTO
    {
        public string ruc {  get; set; }
        public string ciAmbiente {  get; set; }
        public int CiCompania { get; set; }

        public int CiTipoEmision { get; set; }

        public string TxClaveAcceso { get; set; } = null!;

        public string CiTipoDocumento { get; set; } = null!;

        public string TxEstablecimiento { get; set; } = null!;

        public string TxPuntoEmision { get; set; } = null!;

        public string TxSecuencial { get; set; } = null!;

        public string TxFechaEmision { get; set; } = null!;

        public string CiTipoIdentificacionComprador { get; set; } = null!;

        public string? TxGuiaRemision { get; set; }

        public string TxRazonSocialComprador { get; set; } = null!;

        public string TxIdentificacionComprador { get; set; } = null!;

        public decimal QnTotalSinImpuestos { get; set; }

        public decimal QnTotalDescuento { get; set; }

        public decimal QnPropina { get; set; }

        public decimal QnImporteTotal { get; set; }

        public string TxMoneda { get; set; } = null!;

        public int? CiContingenciaDet { get; set; }

        public string? TxEmail { get; set; }

        public string? TxNumeroAutorizacion { get; set; }

        public string? TxFechaHoraAutorizacion { get; set; }

        public string? XmlDocumentoAutorizado { get; set; }

        public string? CiEstado { get; set; }

        public string? CiEstadoRecepcionAutorizacion { get; set; }

        public string? TxCodError { get; set; }

        public string? TxMensajeError { get; set; }

        public int? CiNumeroIntento { get; set; }

        public List<FacturaDetalle1DTO> facturaDetalleModelo { get; set; }
        public List<FacturaTotalImpuestoDTO> facturaTotalImpuestoModelo { get; set; }
        public List<FacturaDetalleFormaPago1DTO> facturaDetalleFormaPagoModelo { get; set; }
        public List<FacturaInfoAdicionalDTO> facturaInfoAdicionalModelo { get; set; }
        public List<FacturaReembolsoDTO> facturaReembolsoModelo { get; set; }



    }
}
