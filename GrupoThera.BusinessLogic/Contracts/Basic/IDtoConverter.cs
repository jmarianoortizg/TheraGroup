using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessLogic.Contracts.General
{
    /// <summary>
    /// Defines a dtoConverter
    /// </summary>
    /// <typeparam name="T">The type of the entity element</typeparam>
    /// <typeparam name="D">The type of the dto element</typeparam>
    public interface IDtoConverter<T, D> where T : IEntity where D : IDto
    {
        #region Methods

        /// <summary>
        /// Convert a dto element to the entity element.
        /// </summary>
        /// <param name="dto">The dto element.</param>
        /// <returns>The specified element</returns>
        T ConvertToEntity(D dto);

        /// <summary>
        /// Convert a entity element to the dto element.
        /// </summary>
        /// <param name="entity">The entity element.</param>
        /// <returns>The specified element</returns>
        D ConvertToDto(T entity);

        /// <summary>
        /// Convert a list of dto elements to the list of entities elements.
        /// </summary>
        /// <param name="dtos">The list of dtos elements.</param>
        /// <returns>The specified list of elements</returns>
        IList<T> ConvertToEntity(IList<D> dtos);

        /// <summary>
        /// Convert a list of entities elements to the list of dtos elements.
        /// </summary>
        /// <param name="entities">The list of entities elements.</param>
        /// <returns>The specified list of elements</returns>
        IList<D> ConvertToDto(IList<T> entities);

        #endregion Methods
    }
}
