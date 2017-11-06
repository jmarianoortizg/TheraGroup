using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.Envelope
{
    public interface Poco2JsonConverter<T> 
    { 
        JObject convertToJson(T element);
        JArray convertToJson(ICollection<T> elements); 
    }
}
