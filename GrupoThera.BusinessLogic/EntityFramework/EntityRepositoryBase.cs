using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GrupoThera.BusinessLogic.EntityFramework
{
    /// <summary>
    /// Defines a dtoConverter
    /// </summary>
    /// <typeparam name="T">The type of the entity element</typeparam> 
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
                                                           where TEntity : class, IEntity, new()
                                                           where TContext : DbContext, new()
    {
        #region Fields

        protected TContext Contexto = new TContext();
        protected DbSet<TEntity> DbSet;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Repository Base.
        /// </summary>
        public EntityRepositoryBase()
        {
            DbSet = Contexto.Set<TEntity>();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="entity">The element.</param>
        /// <returns>The specified element</returns>
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified element.
        /// </summary>
        /// <param name="entity">The element.</param>
        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets one identifiable element.
        /// </summary>
        /// <param name="filter">A linq expression to search</param>
        /// <param name="eager"> If is charge all the entity</param>
        /// <param name="navigationProperty"> Specific Entities</param> 
        /// <returns>
        /// The identifiable elements
        /// </returns>
        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter, bool eager = true, string includeProperties = "")
        {
            using (var context = new TContext())
            {
                var DbSet = context.Set<TEntity>();

                IQueryable<TEntity> query = DbSet;

                query = query.Where(filter);

                if (eager)
                {
                    var type = typeof(TEntity);
                    var properties = type.GetProperties();
                    foreach (var property in properties)
                    {
                        var isVirtual = property.GetGetMethod().IsVirtual;
                        if (isVirtual)
                        {
                            query = query.Include(property.Name);
                        }
                    }
                }
                else
                {
                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query.FirstOrDefault();
            }
        }

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
        public virtual IEnumerable<TEntity> GetListCustom(
                                                     Expression<Func<TEntity, bool>> filter = null,                                                        Func<IQueryable<TEntity>,
                                                     IOrderedQueryable<TEntity>> orderBy = null,
                                                     int? page = default(int?),
                                                     int? pagesize = default(int?),
                                                     bool eager = true,
                                                      string includeProperties = ""
                                                    )
        {
            using (var context = new TContext())
            {
                var DbSet = context.Set<TEntity>();

                IQueryable<TEntity> query = DbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                if (page != null && pagesize != null)
                {
                    query = query.Skip((page.Value - 1) * pagesize.Value).Take(pagesize.Value);
                }
                if (eager)
                {
                    var type = typeof(TEntity);
                    var properties = type.GetProperties();
                    foreach (var property in properties)
                    {
                        var isVirtual = property.GetGetMethod().IsVirtual;
                        if (isVirtual)
                        {
                            query = query.Include(property.Name);
                        }
                    }
                }
                else
                {
                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query.ToList();
            }
        }


        /// <summary>
        /// Updates the specified element.
        /// </summary>
        /// <param name="entity">The element.</param>
        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        #endregion Methods
    }
}
