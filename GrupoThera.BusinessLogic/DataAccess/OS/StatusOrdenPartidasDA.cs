using GrupoThera.BusinessLogic.Contracts.OS;
using GrupoThera.BusinessLogic.EntityFramework;
using GrupoThera.BusinessLogic.EntityFramework.Context;
using GrupoThera.Entities.Entity.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessLogic.DataAccess.OS
{
    public class StatusOrdenPartidasDA : EntityRepositoryBase<StatusOrdenPartidas, GrupoTheraContext>, IStatusOrdenPartidas
    {
    }
}
