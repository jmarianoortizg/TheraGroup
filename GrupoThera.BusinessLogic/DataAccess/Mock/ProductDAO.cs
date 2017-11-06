using GrupoThera.BusinessLogic.Contracts.Mock;
using GrupoThera.BusinessLogic.EntityFramework;
using GrupoThera.BusinessLogic.EntityFramework.Context; 
using GrupoThera.Entities.Entity.Mock; 
using System.Collections.Generic;
using System.Linq;

namespace GrupoThera.BusinessLogic.DataAccess
{
    public class ProductDAO : EntityRepositoryBase<Product, GrupoTheraContext>, IProduct
    {
        private GrupoTheraContext _context;        

        public ProductDAO(GrupoTheraContext context) {
            _context = context;
        }

        #region Methods

        public List<Product> GetAll() {              
            return _context.Products.ToList();                
        } 

        #endregion Methods

    }
}
