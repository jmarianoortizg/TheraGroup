using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using GrupoThera.Entities.Entity.OS;
using GrupoThera.Entities.Entity.OTPre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Utils
{
    public static class DropListHelper
    {
        public static SelectList GetDepartamentos(IList<Departamento> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.departamento,
                Value = a.departamentoId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetSucursales(IList<Sucursal> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.nombre,
                Value = a.sucursalId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetEmpresas(IList<Empresa> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.nombre,
                Value = a.empresaId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetRoles(IList<Rol> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.code + " | " + a.descripcion,
                Value = a.rolId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetAreaServicios(IList<AreaServicio> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.areaServicioId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetCiudad(IList<Ciudad> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.nombre,
                Value = a.ciudadId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetClasificacionServicio(IList<ClasificacionServicio> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.clasificacionServicioId.ToString(),
                Selected = false
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetClasificacionServicioValue0(IList<ClasificacionServicio> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.clasificacionServicioId.ToString()
            }).ToList();
            itemsList.Add(new SelectListItem() { Text = "No Clasificacion Servicio Seleccionado", Value = "0" });
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetCliente(IList<Cliente> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.rfc + "-" + a.razonSocial,
                Value = a.clienteId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetClienteValue0(IList<Cliente> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.rfc + "-" + a.razonSocial,
                Value = a.clienteId.ToString()
            }).ToList();
            itemsList.Add(new SelectListItem() { Text = "No Cliente Seleccionado", Value = "0" });
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetConfiguracion(IList<Configuracion> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.configuracionId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetEstado(IList<Estado> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.nombre,
                Value = a.estadoId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetFormaPago(IList<FormaPago> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.pago,
                Value = a.formaPagoId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetFrecuenciaServicio(IList<FrecuenciaServicio> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.frecuencia,
                Value = a.frecuenciaServicioId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetGiro(IList<Giro> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.giroId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetMetodoCotizacion(IList<MetodoCotizacion> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.nombre,
                Value = a.metodoCotizacionId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetMoneda(IList<Moneda> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.simbolo + " " + a.moneda + "-" + a.tipoCambio,
                Value = a.monedaId.ToString(),
                Selected = false
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetProvedor(IList<Provedor> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.razonSocial,
                Value = a.provedorId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetServicio(IList<Servicio> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.clave + "-" + a.descripcion,
                Value = a.servicioId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetTiempoEntrega(IList<TiempoEntrega> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.tiempoEntrega,
                Value = a.tiempoEntregaId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetStatusCotizacion(IList<StatusCotizacion> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.statusCotizacionId.ToString()
            }).ToList();
            itemsList.Add(new SelectListItem() { Text = "No Estatus Seleccionado", Value = "0" });
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetStatusOTPreliminar(IList<StatusOTPreliminar> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.statusOTPreliminarId.ToString()
            }).ToList();
            itemsList.Add(new SelectListItem() { Text = "No Estatus Seleccionado", Value = "0" });
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetStatusOrdenPartidasAreaTecnicaInicial(IList<StatusOrdenPartidas> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.statusOrdenPartidasId.ToString()
            }).ToList();

            itemsList.Remove(itemsList.Where(c => c.Text.Equals("Cancelada")).Single());

            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetStatusOrdenPartidasAreaTecnicaProcess(IList<StatusOrdenPartidas> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.statusOrdenPartidasId.ToString()
            }).ToList();

            itemsList.Remove(itemsList.Where(c => c.Text.Equals("Abierta")).Single());
            itemsList.Remove(itemsList.Where(c => c.Text.Equals("Cancelada")).Single());

            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetStatusOrdenPartidasVentas(IList<StatusOrdenPartidas> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.descripcion,
                Value = a.statusOrdenPartidasId.ToString()
            }).ToList();
            itemsList.Remove(itemsList.Where(c => c.Text.Equals("Realizada")).Single());
            itemsList.Remove(itemsList.Where(c => c.Text.Equals("En Proceso")).Single());
            itemsList.Remove(itemsList.Where(c => c.Text.Equals("Programada")).Single());
            itemsList.Remove(itemsList.Where(c => c.Text.Equals("Por Cancelar")).Single());

            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetUsuario(IList<Usuario> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.nombre + " " + a.apellidos,
                Value = a.usuarioId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

        public static SelectList GetEmpresaSucursalUsuarioMap(IList<EmpresaSucursalUsuarioMap> list)
        {
            List<SelectListItem> itemsList = list.Select(a => new SelectListItem()
            {
                Text = a.Usuario.nombre + a.Usuario.apellidos,
                Value = a.usuarioId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }
    }
}