using GrupoThera.Core.Envelope.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.Envelope.Implementations
{
    /// <summary>
    /// Represents an envelope class
    /// </summary>
    /// <typeparam name="T">The type of the envelope</typeparam>
    /// <seealso cref="Implementations.IEnvelope{T}" />
    [DataContract]
    public class EnvelopeClientHelper<T> : IEnvelope<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember]
        public T Data
        {
            get; set;
        }

        #endregion Properties
    }
}
