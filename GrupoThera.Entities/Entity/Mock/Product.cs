using GrupoThera.Entities.Contracts;
using Newtonsoft.Json;
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
    /// A Entity class for Product objects.
    /// </summary>
    [Table("EF_PRODUCTS")]
    public partial class Product : IEntity
    {
        /// <summary>
        ///  PK for product Element
        /// </summary>
        [Key]
        [Column("PROD_ID")]
        [JsonProperty("id")]
        public long ProdId { get; set; }

        /// <summary>
        ///  product name
        /// </summary>
        [Column("PROD_PRODUCTNAME")]
        [JsonProperty("name")]
        public string ProductName { get; set; }

        /// <summary>
        ///  FK for category Element
        /// </summary>
        [Column("PROD_CAT_ID")]
        [JsonProperty("categoryId")]
        public long CatId { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
