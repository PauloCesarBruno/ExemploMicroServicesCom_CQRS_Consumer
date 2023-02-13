using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPartNumberQuantityRepository
    {
        Task InsertAsync(PartNumberQuantity entity);
        Task UpdateAsync(PartNumberQuantity entity, long id);
        Task<PartNumberQuantity> GetByReference(PartNumberQuantity entity);
    }
}
