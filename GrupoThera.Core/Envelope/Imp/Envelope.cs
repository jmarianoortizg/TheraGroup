using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.Envelope
{
    public class Envelope<T> where T : class, IEntity, new()
    {
    
        private Envelope()
        {
            // empty
        }

        /**
         * Instantiate a new DataEnvelope<T>
         * @param data the data to enwrap
         * @param <T> the type
         * @return DataEnvelope<T>
         */
        public static DefaultDataEnvelope<T> createDataEnvelope(T data)
        {
            DefaultDataEnvelope<T> envelope = new DefaultDataEnvelope<T>();
            envelope.setData(data);
            return envelope;
        }

        /**
         * Instantiate a new DataEnvelope<T>
         * @param data the data to enwrap
         * @param <T> the type
         * @return DataEnvelope<T>
         */
        public static DefaultDataEnvelope<T> createDataEnvelope(IList<T> data)
        {
            DefaultDataEnvelope<T> envelope = new DefaultDataEnvelope<T>();
            envelope.setDataList(data);
            return envelope;
        } 
 
    }
}
