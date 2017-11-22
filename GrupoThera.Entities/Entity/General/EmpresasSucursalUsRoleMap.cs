﻿using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.General
{
    [Table("CAT_EMPSUCSERROLE_MAP")]
    public partial class EmpresasSucursalUsRoleMap : IEntity
    {
        [Key]
        [Column("CESUR_ID")]
        public long empresaSucursalUsId { get; set; }

        [Column("CESUR_CESM_IDX")]
        public long sucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        [Column("CESUR_CROL_ID")]
        public long rolId { get; set; }
        public virtual Rol Rol { get; set; }

    }
}
