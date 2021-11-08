using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
	public interface IRepository<TEntity> : IDisposable where TEntity : class
	{
		Task Insert(TEntity item);
		Task InsertStrategy(TEntity item);
		Task<TEntity> InsertEntity(TEntity item);
		Task<bool> InsertWhitoutCommit(TEntity item);
		Task InsertMassive(List<TEntity> lstItem);
		Task InsertBulk(List<TEntity> lstItem);
		Task Update(TEntity item);
		Task UpdateStrategy(TEntity item);
		Task<bool> UpdateWhitoutCommit(TEntity item);
		Task UpdateMassive(List<TEntity> lstItem);
		Task UpdateBulk(List<TEntity> lstItem);
		Task<TEntity> Get(object[] id);
		Task<IReadOnlyList<TEntity>> GetAllAsync();
		IQueryable<TEntity> GetAll();
		/// <summary>
		/// Validate an entity with predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		bool ValidateEntity(Func<TEntity, bool> predicate);
		/// <summary>
		/// Get max with predicate
		/// </summary>
		/// <param name="predicateWhere"></param>
		/// <param name="predicateMax"></param>
		/// <returns></returns>
		object Max(Func<TEntity, bool> predicateWhere, Func<TEntity, object> predicateMax);
		/// <summary>
		/// Delete an register 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task Delete(object[] id);
		/// <summary>
		/// Delete an register 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task Delete(TEntity entity);
		Task DeleteMassive(List<object[]> lstId);
		Task DeleteMassive(List<TEntity> lstItem);
		Task CommitTrack();
	}
}
