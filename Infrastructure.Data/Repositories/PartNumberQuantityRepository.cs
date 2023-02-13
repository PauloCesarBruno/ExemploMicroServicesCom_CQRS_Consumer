using AutoMapper;
using Azul.Framework.Data.Configuration;
using Azul.Framework.Data.SqlDapper;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PartNumberQuantityRepository : DapperRepository<PartNumberQuantityEntity, long>, IPartNumberQuantityRepository
    {
        private static ConnectionSetting Settings = DatabaseConfiguration.Settings.ConnectionSettings.GetConnectionSetting(_connectionName);
        private static string _collection = Settings.Parameters.First(item => item.Key == "Collection").Value.ToString();
        private const string _connectionName = nameof(PartNumberQuantityRepository);
        private const string _partitionKey = null;
        private const string _serverUrl = null;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        protected override string ColumnIdKey => "Id";

        //public static string ConnectionId = "Uniformes";

        public PartNumberQuantityRepository(IMapper mapper, ILoggerFactory loggerFactory) : base("PartNumberQuantity", "Hangar", true)

        {
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<PartNumberQuantityRepository>();
        }

        public async Task InsertAsync(PartNumberQuantity model)
        {
            var entity = _mapper.Map<PartNumberQuantityEntity>(model);
            try
            {
                await AddAsync(entity);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
            _logger.LogInformation($"Inserted entity {JsonConvert.SerializeObject(entity)}");
        }

        public async Task UpdateAsync(PartNumberQuantity model, long id)
        {
            var entity = _mapper.Map<PartNumberQuantityEntity>(model);
            entity.Id = id;
            await base.UpdateAsync(entity, new
            {
                Pn = entity.Pn,
                Sn = entity.Sn,
                PnInterchangeable = entity.PnInterchangeable,
                PnDescription = entity.PnDescription,
                Category = entity.Category,
                Vendor = entity.Vendor,
                QtyAvailable = entity.QtyAvailable,
                QtyReserved = entity.QtyReserved,
                QtyInTransfer = entity.QtyInTransfer,
                QtyPendingRi = entity.QtyPendingRi,
                QtyUs = entity.QtyUs,
                QtyInRepair = entity.QtyInRepair,
                ModifiedDate = entity.ModifiedDate,
                CreatedDate = entity.CreatedDate,
            });
            _logger.LogInformation($"Update entity {JsonConvert.SerializeObject(entity)}");
        }

        public async Task<PartNumberQuantity> GetByReference(PartNumberQuantity entity)
        {
            Expression<Func<PartNumberQuantityEntity, bool>> predicate = (x => x.Pn == entity.Pn
                && x.Sn == entity.Sn && x.PnInterchangeable == entity.PnInterchangeable);
            var findRecord = (await WhereAsync(predicate)).FirstOrDefault();
            return _mapper.Map<PartNumberQuantity>(findRecord);
        }
    }
}
