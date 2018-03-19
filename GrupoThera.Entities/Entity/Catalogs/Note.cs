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
    [Table("NOTE")]
    public partial class Note : IEntity
    {
        [Key]
        [Column("NOTE_ID")]
        public long noteId { get; set; }

        [Column("NOTE_ID_DOC")]
        public long noteDocId { get; set; }

        [Column("NOTE_CONTENT")]
        public string content { get; set; }

        [Column("NOTE_DOCUMENT")]
        public string document { get; set; }

        [Column("NOTE_OWNER")]
        public string owner { get; set; }

        [Column("NOTE_DATE")]
        public DateTime creation { get; set; }
    }
}
