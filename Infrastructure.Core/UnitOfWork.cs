using Domain.Core;

using Infrastructure.Transversal.Core.Accessor;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
	public class UnitOfWork : DbContext, IUnitOfWork
	{
		private readonly IContextAccessor _contextAccessor;

		public UnitOfWork(DbContextOptions options, IContextAccessor contextAccessor) : base(options)
		{
			_contextAccessor = contextAccessor;
		}

		public void Commit()
		{
			base.SaveChanges();
		}

		public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}

		public void Modify<TEntity>(TEntity item) where TEntity : class
		{
			base.Entry<TEntity>(item).State = EntityState.Modified;
		}

		public async Task CommitStrategyAsync()
		{
			var strategy = base.Database.CreateExecutionStrategy();
			await strategy.ExecuteAsync(async () =>
			{
				await base.SaveChangesAsync();
			});
		}

		public async Task CommitAsync()
		{
			//SetAudit();
			await base.SaveChangesAsync();
		}

		public async Task<TEntity> CommitEntityAsync<TEntity>(TEntity item) where TEntity : class
		{
			//SetAudit();
			await base.SaveChangesAsync();
			return item;
		}

		public void RollbackChanges()
		{
			base.ChangeTracker.Entries()
							.ToList()
							.ForEach(entry => entry.State = EntityState.Unchanged);
		}

		//private void SetAudit()
		//{
		//	ChangeTracker.Entries().Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted) && x.Metadata.GetProperties().Any(p => p.Name == "DateOwner")).ToList().ForEach(
		//	delegate (EntityEntry item)
		//	{
		//		if (item.State != EntityState.Deleted)
		//		{
		//			if (item.State == EntityState.Added)
		//			{
		//				if (item.Entity.GetType().GetProperties().Any(z => z.Name == "DateOwner"))
		//				{
		//					item.CurrentValues["DateOwner"] = DateTime.Now;
		//				}
		//				if (item.Entity.GetType().GetProperties().Any(z => z.Name == "Owner"))
		//				{
		//					item.CurrentValues["Owner"] = _contextAccessor.userName;
		//				}
		//			}
		//			if (item.Entity.GetType().GetProperties().Any(z => z.Name == "LastDate"))
		//			{
		//				item.CurrentValues["LastDate"] = DateTime.Now;
		//			}
		//			if (item.Entity.GetType().GetProperties().Any(z => z.Name == "LastModify"))
		//			{
		//				item.CurrentValues["LastModify"] = _contextAccessor.userName;
		//			}
		//		}
		//	});
		//}
	}
}