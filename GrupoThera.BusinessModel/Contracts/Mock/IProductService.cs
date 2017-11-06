using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.Entities.Entity;
using GrupoThera.Entities.Entity.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Business.Interfaces
{
    /// <summary>
    /// Defines a userInfo service class 
    /// Contains all the methods used for the manager user info
    /// </summary> 
    public interface IProductService
    {
        #region Methods 

        List<Product> getAllProducts();
        Product getProductById(int id);
        void createProduct(Product newProduct);
        void updateProduct(Product updateProduct);
        void deleteProduct(int idProduct);

        #endregion Methods
    }
}
