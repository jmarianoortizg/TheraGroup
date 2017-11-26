using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.Catalogs
{
    [Table("CONFIGURACION")]
    public partial class Configuracion : IEntity
    {
        [Key]
        [Column("CONF_ID")]
        public long configuracionId { get; set; }

        [Column("CONF_PARAMETRO")]
        public string parametro { get; set; }

        [Column("CONF_VALOR1")]
        public string valor1 { get; set; }

        [Column("CONF_VALOR2")]
        public string valor2 { get; set; }

        [Column("CONF_VALOR3")]
        public string valor3 { get; set; }

        [Column("CONF_DESCRIPCION")]
        public string descripcion { get; set; }

    }
}
