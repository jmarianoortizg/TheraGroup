using GrupoThera.Entities.Contracts;
using GrupoThera.Core.Envelope;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.Envelope
{
    public class DataEnvelope2JsonConverter<T> where T : class, IEntity,  new()
    { 
        public JObject convertToJson(DataEnvelope<T> element, Poco2JsonConverter<T> dataConverter)
        { 
            JObject jsonObjectBuilder = new JObject();
            if (element.getData() == null)
            {
                jsonObjectBuilder.Add("data",dataConverter.convertToJson(element.getDataList()));
            }
            else
            {
                jsonObjectBuilder.Add("data", dataConverter.convertToJson(element.getData()));
            }
            return jsonObjectBuilder;
        }
    }
}
