using GrupoThera.Entities.Entity;
using GrupoThera.Core.Envelope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using GrupoThera.Entities.Entity.Mock;

namespace GrupoThera.BusinessModel.Converters.Mock
{
    public class Product2JsonConverter : Poco2JsonConverter<Product>
    {
        public JObject convertToJson(Product element)
        {
            JObject jsonObjectBuilder = new JObject();
            jsonObjectBuilder.Add("id", element.ProdId);
            jsonObjectBuilder.Add("name", element.ProductName);
            if (element.Category != null)
            {
                jsonObjectBuilder.Add("category", new Category2JsonConverter().convertToJson(element.Category));
            }
            return jsonObjectBuilder;
        } 

        public JArray convertToJson(ICollection<Product> elements)
        {
            JArray jsonArrayBuilder = new JArray(); 
            foreach (var product in elements)
            {
                jsonArrayBuilder.Add(new Product2JsonConverter().convertToJson(product));
            }
            return jsonArrayBuilder;
        }
    }
}
