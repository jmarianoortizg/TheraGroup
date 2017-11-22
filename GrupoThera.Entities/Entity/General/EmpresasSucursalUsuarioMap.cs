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
    [Table("CAT_EMPSUCMAPUSER_MAP")]
    public partial class EmpresaSucursalUsuarioMap : IEntity
    {
        [Key]
        [Column("CESM_IDX")]
        public long empresaSucursalUsId { get; set; }

        [Column("CESM_CUSU_ID")]
        public long usuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Column("CESM_ID")]
        public long empresaSucursalId { get; set; }
        public virtual EmpresasSucursalMap EmpresasSucursalMap { get; set; }
    }
}
