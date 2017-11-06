using GrupoThera.Business.Interfaces;
using GrupoThera.BusinessLogic.Contracts;
using GrupoThera.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GrupoThera.BusinessLogic.Contracts.Mock;
using GrupoThera.Entities.Entity.Mock;

namespace GrupoThera.Business.Managers
{
    /// <summary>
    /// Defines a UserInfo Manager for Product Servicer
    /// </summary> 
    public class ProductManager : IProductService
    {
        #region Fields

        private IProduct _productDAO;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ProductManager class.
        /// </summary>
        /// <param name="productDAO"> The contract of all methods for the ProductService.</param>
        public ProductManager(IProduct productDAO)
        {
            //Dependency Injection
            _productDAO = productDAO; 
        }
        #endregion Constructor

        #region Methods 

        public List<Product> getAllProducts()
        {
            return _productDAO.GetListCustom(includeProperties: "Category").ToList();
        } 

        public Product getProductById(int id)
        {
            return _productDAO.GetSingle(p => p.ProdId == id);
        }

        public void createProduct(Product newProduct) {
            _productDAO.Add(newProduct);
        }

        public void updateProduct(Product updateProduct)
        {
            _productDAO.Update(updateProduct);
        }

        public void deleteProduct(int idProduct)
        {            
            _productDAO.Delete(_productDAO.GetSingle(p => p.ProdId == idProduct));
        }

        #endregion Methods 
    }
}
