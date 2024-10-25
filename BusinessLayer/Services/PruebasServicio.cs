﻿using DataLayer.repositorio;
using EntityLayer.DTO;
using EntityLayer.Models;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PruebasServicio : IPruebaServicio
    {
        private readonly IPruebasRepositorio pruebasRepositorio;

        Response response = new Response();

        public PruebasServicio(IPruebasRepositorio pruebas)
        {
            pruebasRepositorio = pruebas;
        }

        public async Task<Response> BuscarDocumentosError(string tipoDocumento)
        {
            response = await pruebasRepositorio.BuscarDocumentosError(tipoDocumento);
            return response;
        }

        public async Task<Response> BusquedaDocFiltros(int CiCompania, string CiTipoDocumento, string NumDocumentos, string ClaveAcceso, string Identificacion, string NombreRS, string FechaInicio, string FechaFin, string Autorizacion)
        {
            response = await pruebasRepositorio.BusquedaDocFiltros(CiCompania, CiTipoDocumento, NumDocumentos, ClaveAcceso, Identificacion, NombreRS, FechaInicio, FechaFin, Autorizacion);
            return response;
        }

        public async Task<Response> TDocumentosEmpresas(int CiCompania, string fechaInicio, string fechaFin)
        {
            response = await pruebasRepositorio.TDocumentosEmpresas(CiCompania,fechaInicio,fechaFin);
            return response;
        }


    }
}
