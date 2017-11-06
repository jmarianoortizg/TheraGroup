using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.Envelope.Contracts
{
    /// <summary>
    /// Defines an envelope
    /// </summary>
    /// <typeparam name="T">The type of the envelope</typeparam>
    public interface IEnvelope<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        T Data
        {
            get; set;
        }

        #endregion Properties
    }
}
