using GrupoThera.Business.Interfaces;
using GrupoThera.BusinessLogic.Contracts;
 using GrupoThera.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
 

namespace GrupoThera.Business.Managers
{
    /// <summary>
    /// Defines a UserInfo Manager for Product Servicer
    /// </summary> 
    public class CategoryManager : ICategoryService
    {
        #region Fields

        private ICategory _categoryDAO;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CategoryManager class.
        /// </summary>
        /// <param name="userInfoDAO"> The contract of all methods for the CategoryService.</param>
        public CategoryManager(ICategory categoryDAO)
        {
            //Dependency Injection
            _categoryDAO = categoryDAO; 
        }


        #endregion Constructor

        #region Methods 

        #endregion Methods 
    }
}
