using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTO
{
    public class FiltroDocDTO
    {
        public int CiCompania { get; set; }
        public string CiTipoDocumento { get; set; }
        public string NumDocumentos { get; set; }
        public string ClaveAcceso { get; set; }
        public string Identificacion { get; set; }
        public string NombreRS { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Autorizacion { get; set; }
    }
}


//int CiCompania, string CiTipoDocumento, string NumDocumentos, string ClaveAcceso, string Identificacion, string NombreRS, string FechaInicio, string FechaFin, string Autorizacion
//CiCompania,CiTipoDocumento,NumDocumentos,ClaveAcceso,Identificacion,NombreRS,FechaInicio,FechaFin,Autorizacion