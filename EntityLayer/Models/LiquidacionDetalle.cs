﻿using System;
using System.Collections.Generic;

namespace EntityLayer.Models;

public partial class LiquidacionDetalle
{
    public int CiCompania { get; set; }

    public string TxEstablecimiento { get; set; } = null!;

    public string TxPuntoEmision { get; set; } = null!;

    public string TxSecuencial { get; set; } = null!;

    public string TxCodigoPrincipal { get; set; } = null!;

    public string? TxCodigoAuxiliar { get; set; }

    public string TxDescripcion { get; set; } = null!;

    public int QnCantidad { get; set; }

    public decimal? QnPrecioUnitario { get; set; }

    public decimal QnDescuento { get; set; }

    public decimal QnPrecioTotalSinImpuesto { get; set; }

    public virtual ICollection<LiquidacionDetalleAdicional> LiquidacionDetalleAdicionals { get; set; } = new List<LiquidacionDetalleAdicional>();

    public virtual ICollection<LiquidacionDetalleImpuesto> LiquidacionDetalleImpuestos { get; set; } = new List<LiquidacionDetalleImpuesto>();
}
