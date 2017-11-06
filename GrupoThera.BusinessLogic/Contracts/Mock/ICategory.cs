using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.Entities.Entity;
using GrupoThera.Entities.Entity.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessLogic.Contracts
{
    public interface ICategory : IEntityRepository<Category>
    {
        
    }
}
