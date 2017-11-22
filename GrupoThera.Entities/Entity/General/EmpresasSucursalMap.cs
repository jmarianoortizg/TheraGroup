using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.General
{
    [Table("CAT_EMPSUC_MAP")]
    public partial class EmpresasSucursalMap : IEntity
    {
        [Key]
        [Column("CESM_ID")]
        public long empresaSucursalId { get; set; }

        [Column("CUSU_CDEP_ID")]
        public long sucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        [Column("CUSU_CDEP_ID")]
        public long empresaId { get; set; }
        public virtual Empresa Departamento { get; set; }

    }
}
