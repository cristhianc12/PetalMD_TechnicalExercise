using Domain.Security.Entities;
using Domain.Security.Repositories;

using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class UsersRepository : IUsersRepository
    {
        public Task CommitTrack()
        {
            throw new NotImplementedException();
        }

        public Task Delete(object[] id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Users entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMassive(List<object[]> lstId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMassive(List<Users> lstItem)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Users> Get(object[] id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Users> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Users>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Users item)
        {
            throw new NotImplementedException();
        }

        public Task InsertBulk(List<Users> lstItem)
        {
            throw new NotImplementedException();
        }

        public Task<Users> InsertEntity(Users item)
        {
            throw new NotImplementedException();
        }

        public Task InsertMassive(List<Users> lstItem)
        {
            throw new NotImplementedException();
        }

        public Task InsertStrategy(Users item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertWhitoutCommit(Users item)
        {
            throw new NotImplementedException();
        }

        public object Max(Func<Users, bool> predicateWhere, Func<Users, object> predicateMax)
        {
            throw new NotImplementedException();
        }

        public Task Update(Users item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBulk(List<Users> lstItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMassive(List<Users> lstItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStrategy(Users item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWhitoutCommit(Users item)
        {
            throw new NotImplementedException();
        }

        public bool ValidateEntity(Func<Users, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
