using GrupoThera.Entities.Entity.General;
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
                Text = a.name,
                Value = a.rolId.ToString()
            }).ToList();
            return new SelectList(itemsList, "Value", "Text", "Selected");
        }

    }
}