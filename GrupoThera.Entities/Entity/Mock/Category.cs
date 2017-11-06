using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.Mock
{
    /// <summary>
    /// A Entity class for category objects.
    /// </summary>
    [Table("EF_CATEGORIES")]
    public partial class Category : IEntity
    {
        public Category()
        {
            this.Product = new HashSet<Product>();
        }

        /// <summary>
        ///  PK for category Element
        /// </summary>
        [Key]
        [Column("CAT_ID")]
        public long CatId { get; set; }

        /// <summary>
        ///  Category name
        /// </summary>
        [Column("CAT_CATEGORYNAME")]
        public string CategoryName { get; set; }

        /// <summary>
        ///  Description of the category
        /// </summary>
        [Column("CAT_DESCRIPTION")]
        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
