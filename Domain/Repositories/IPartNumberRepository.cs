using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPartNumberRepository
    {
        Task InsertAsync(PartNumber entity);
        Task UpdateAsync(PartNumber entity, long id);
        Task<PartNumber> GetByReference(PartNumber entity);
    }
}
