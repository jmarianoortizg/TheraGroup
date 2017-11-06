using GrupoThera.Entities.Entity.Mock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Dto
{
    public class productDto
    {
        [JsonProperty("products")]
        public List<Product> listProducts { get; set; }
    }
}
