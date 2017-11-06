using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.Envelope
{
    /// <summary>
    /// Defines a envelope pattern fot the data
    /// 
    /// </summary> 
    public interface DataEnvelope<T> where T : class, IEntity, new()
    {
        string DATA { get; }

        /// <returns>
        /// The data
        /// </returns>
        T getData();
        /// <param>
        /// Data the data to set
        /// </param>
        void setData(T data);
        /// <returns>
        /// The dataList
        /// </returns>
        IList<T> getDataList();
        /// <param>
        /// DataList the dataList to set
        /// </param>
        void setDataList(IList<T> dataList);
    }
}
