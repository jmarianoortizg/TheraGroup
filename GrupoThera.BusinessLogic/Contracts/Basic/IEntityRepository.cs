using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessLogic.Contracts.General
{
    /// <summary>
    /// Defines a dtoConverter
    /// </summary>
    /// <typeparam name="T">The type of the entity element</typeparam> 
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        #region Methods
        /// <summary>
        /// Gets one identifiable element.
        /// </summary>
        /// <param name="filter">A linq expression to search</param>
        /// <param name="eager"> If is charge all the entity</param>
        /// <param name="navigationProperty"> Specific Entities</param> 
        /// <returns>
        /// The identifiable elements
        /// </returns>
        T Get(Expression<Func<T, bool>> filter, bool eager = true, string includeProperties = "");

        /// <summary>
        /// Gets all identifiable elements.
        /// </summary>
        /// <param name="filter">A linq expression to search</param>
        /// <param name="orderBy"> Flag to order by</param>
        /// <param name="page"> Number of the page</param>
        /// <param name="pageSize"> Size of the page</param>
        /// <param name="navigationProperties"> List of properties separated by coma</param>
        /// <param name="eager"> True to load all the components</param>
        /// <returns>
        /// The identifiable elements
        /// </returns>
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null,
                                Func<IQueryable<T>,
                                IOrderedQueryable<T>> orderBy = null,
                                int? page = default(int?),
                                int? pagesize = default(int?),
                                bool eager = true,
                                string includeProperties = "");

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="entity">The element.</param>
        /// <returns>The specified element</returns>
        void Add(T entity);

        /// <summary>
        /// Updates the specified element.
        /// </summary>
        /// <param name="entity">The element.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified element.
        /// </summary>
        /// <param name="entity">The element.</param>
        void Delete(T entity);

        #endregion Methods

    }
}
