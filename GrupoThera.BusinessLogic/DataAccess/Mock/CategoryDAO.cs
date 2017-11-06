using GrupoThera.BusinessLogic.Contracts;
using GrupoThera.BusinessLogic.EntityFramework;
using GrupoThera.BusinessLogic.EntityFramework.Context;
using GrupoThera.Entities.Entity;
using GrupoThera.Entities.Entity.Mock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoThera.BusinessLogic.DataAccess
{
    public class CategoryDAO : EntityRepositoryBase<Category, GrupoTheraContext>, ICategory
    {
        private GrupoTheraContext _context;        

        public CategoryDAO(GrupoTheraContext context) {
            _context = context;
        }

        #region Methods 
         

        #endregion Methods

    }
}
