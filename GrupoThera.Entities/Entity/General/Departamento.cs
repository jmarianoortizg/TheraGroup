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
    [Table("CAT_DEPARTAMENTOS")]
    public partial class Departamento : IEntity
    {
        [Key]
        [Column("CDEP_ID")]
        public long departamentoId { get; set; }

        [Column("CDEP_DEPARTAMENTO")]
        public string departamento { get; set; }

        [Column("CDEP_COMENTARIOS")]
        public string comentarios { get; set; }
    }
}
