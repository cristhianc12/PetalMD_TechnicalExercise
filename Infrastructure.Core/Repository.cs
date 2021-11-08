using Domain.Core;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private UnitOfWork _unitOfWork;

		public Repository(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public UnitOfWork UnitOfWork
		{
			get { return _unitOfWork; }
		}

		public async Task Delete(object[] id)
		{
			var item = await Get(id);
			await Delete(item);
		}

		/// <summary>
		/// Delete an register 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task Delete(TEntity entity)
		{
			GetSet().Remove(entity);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteMassive(List<object[]> lstId)
		{
			foreach (var id in lstId)
			{
				var item = await Get(id);
				GetSet().Remove(item);
			}
			await _unitOfWork.CommitAsync();
		}

		/// <summary>
		/// Delete massive a list Entity
		/// </summary>
		/// <param name="lstItem"></param>
		/// <returns></returns>
		public async Task DeleteMassive(List<TEntity> lstItem)
		{
			lstItem.ForEach(delegate (TEntity item)
			{
				_unitOfWork.Entry(item).State = EntityState.Deleted;
				GetSet().Remove(item);
			});
			await _unitOfWork.CommitAsync();
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}

		public async Task<TEntity> Get(object[] id)
		{
			return await GetSet().FindAsync(id);
		}

		public IQueryable<TEntity> GetAll()
		{
			return GetSet();
		}

		public async Task<IReadOnlyList<TEntity>> GetAllAsync()
		{
			return await GetSet().ToListAsync();
		}

		public DbSet<TEntity> GetSet()
		{
			return _unitOfWork.Set<TEntity>();
		}
		/// <summary>
		/// Validate an entity with predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public bool ValidateEntity(Func<TEntity, bool> predicate)
		{
			var list = _unitOfWork.Set<TEntity>().Where(predicate).ToList();
			if (list.Count > 0)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Get max with predicate
		/// </summary>
		/// <param name="predicateWhere"></param>
		/// <param name="predicateMax"></param>
		/// <returns></returns>
		public object Max(Func<TEntity, bool> predicateWhere, Func<TEntity, object> predicateMax)
		{
            if (GetSet().Any(predicateWhere))
            {
				return GetSet().Where(predicateWhere).Max(predicateMax);
			}
            else
            {
				return 0;
            }
		}

		public async Task Insert(TEntity item)
		{
			GetSet().Add(item);
			await _unitOfWork.CommitAsync();
		}

		public async Task InsertStrategy(TEntity item)
		{
			GetSet().Add(item);
			await _unitOfWork.CommitStrategyAsync();
		}

		public async Task<bool> InsertWhitoutCommit(TEntity item)
		{
			GetSet().Add(item);
			return await Task.FromResult(true);
		}

		public async Task CommitTrack()
		{
			await _unitOfWork.CommitAsync();
		}

		public async Task<TEntity> InsertEntity(TEntity item)
		{
			GetSet().Add(item);
			return await _unitOfWork.CommitEntityAsync(item);
		}

		public async Task InsertMassive(List<TEntity> lstItem)
		{
			lstItem.ForEach(delegate (TEntity item)
			{
				GetSet().Add(item);
			});
			await _unitOfWork.CommitAsync();
		}

		public async Task Update(TEntity item)
		{
			_unitOfWork.Entry(item).State = EntityState.Modified;
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateStrategy(TEntity item)
		{
			_unitOfWork.Entry(item).State = EntityState.Modified;
			await _unitOfWork.CommitStrategyAsync();
		}

		public async Task<bool> UpdateWhitoutCommit(TEntity item)
		{
			_unitOfWork.Entry(item).State = EntityState.Modified;
			return await Task.FromResult(true);
		}

		public async Task UpdateMassive(List<TEntity> lstItem)
		{
			lstItem.ForEach(delegate (TEntity item)
			{
				_unitOfWork.Entry(item).State = EntityState.Modified;
			});
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateBulk(List<TEntity> lstItem)
		{
			using (var transaction = _unitOfWork.Database.BeginTransaction())
			{
				var bulkConfig = new BulkConfig() { UseTempDB = true };
				await _unitOfWork.BulkUpdateAsync(lstItem, bulkConfig);
				transaction.Commit();
			}
		}

		public async Task InsertBulk(List<TEntity> lstItem)
		{
			using (var transaction = _unitOfWork.Database.BeginTransaction())
			{
				var bulkConfig = new BulkConfig() { UseTempDB = true };
				await _unitOfWork.BulkInsertAsync(lstItem, bulkConfig);
				transaction.Commit();
			}
		}
	}
}
