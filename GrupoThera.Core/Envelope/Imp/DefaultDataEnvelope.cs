using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.Envelope
{
    public class DefaultDataEnvelope<T> : DataEnvelope<T> where T : class, IEntity, new()
    {
        private T m_data;
        private IList<T> m_dataList; 

        public T getData()
        {
            return this.m_data;
        }

        public IList<T> getDataList()
        {
            return this.m_dataList;
        }

        public void setData(T data)
        {
            this.m_data = data;
        }

        public void setDataList(IList<T> dataList)
        {
            this.m_dataList = dataList;
        }

        public string DATA
        {
            get
            { 
                return "data";
            } 
        } 

    }
}
