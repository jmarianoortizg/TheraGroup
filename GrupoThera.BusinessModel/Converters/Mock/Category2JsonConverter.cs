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
    public class Category2JsonConverter : Poco2JsonConverter<Category>
    {
        public JObject convertToJson(Category element)
        {
            JObject jsonObjectBuilder = new JObject();
            jsonObjectBuilder.Add("id", element.CatId);
            jsonObjectBuilder.Add("name", element.CategoryName);            
            jsonObjectBuilder.Add("description", element.Description);
            return jsonObjectBuilder;
        } 

        public JArray convertToJson(ICollection<Category> elements)
        {
            JArray jsonArrayBuilder = new JArray(); 
            foreach (var Category in elements)
            {
                jsonArrayBuilder.Add(new Category2JsonConverter().convertToJson(Category));
            }
            return jsonArrayBuilder;
        }
    }
}
