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
    [Table("CAT_USUARIOS")]
    public partial class Usuario : IEntity
    {
        [Key]
        [Column("CUSU_ID")]
        public long usuarioId { get; set; }

        [Column("CUSU_USUARIO")]
        public string usuario { get; set; }

        [Column("CUSU_PW")]
        public string password { get; set; }

        [Column("CUSU_NOMBRE")]
        public string nombre { get; set; }

        [Column("CUSU_APELLIDOS")]
        public string apellidos { get; set; }

        [Column("CUSU_EMAIL")]
        public string correo { get; set; }

        [Column("CUSU_PUESTO")]
        public string puesto { get; set; }

        [Column("CUSU_CDEP_ID")]
        public long departamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
