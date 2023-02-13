using AutoMapper;
using Azul.Framework.Data.SqlDapper;
using Dapper;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PartNumberRepository : DapperRepository<PartNumberEntity, long>, IPartNumberRepository
    {
        // TODO : Determinar _partitionKey - filter *
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        protected override string ColumnIdKey => "Id";

        //public static string ConnectionId = "Uniformes";

        public PartNumberRepository(IMapper mapper, ILoggerFactory loggerFactory) : base("PartNumber", "Hangar", true)

        {
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<PartNumberRepository>();
        }

        public async Task InsertAsync(PartNumber model)
        {
            var entity = _mapper.Map<PartNumberEntity>(model);

            using var connection = Connection;
            connection.Open();
            try
            {
                await AddAsync(entity);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
            

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            _logger.LogInformation($"Inserted entity {JsonConvert.SerializeObject(entity)}");
        }

        public async Task UpdateAsync(PartNumber model, long id)
        {
            var entity = _mapper.Map<PartNumberEntity>(model);
            entity.Id = id;
            try
            {
                await UpdateAsync(entity, new
                {
                    PnInterchangeable = entity.PnInterchangeable,
                    Pn = entity.Pn,
                    PnDescription = entity.PnDescription,
                    Category = entity.Category,
                    StockUom = entity.StockUom,
                    HazardousMaterial = entity.HazardousMaterial,
                    Status = entity.Status,
                    ShelfLifeFlag = entity.ShelfLifeFlag,
                    ShelfLifeDays = entity.ShelfLifeDays,
                    Chapter = entity.Chapter,
                    Section = entity.Section,
                    AvarangeCost = entity.AvarangeCost,
                    StandardCost = entity.StandardCost,
                    SecondaryCost = entity.SecondaryCost,
                    ToolCalibrationFlag = entity.ToolCalibrationFlag,
                    ToolLifeDays = entity.ToolLifeDays,
                    HazardousMateriaNo = entity.HazardousMateriaNo,
                    ToolControlItem = entity.ToolControlItem,
                    InventoryType = entity.InventoryType,
                    CreatedDate = entity.CreatedDate,
                    ModifiedDate = entity.ModifiedDate,
                });
            }
            catch (Exception ex)
            {
                _ = ex;
            }

            _logger.LogInformation($"Update entity {JsonConvert.SerializeObject(entity)}");
        }

        public async Task<PartNumber> GetByReference(PartNumber entity)
        {
            Expression<Func<PartNumberEntity, bool>> predicate = (x => x.Pn == entity.Pn && x.CreatedDate == entity.CreatedDate && x.PnInterchangeable == entity.PnInterchangeable);
            var findRecord = (await WhereAsync(predicate)).FirstOrDefault();
            return _mapper.Map<PartNumber>(findRecord);
        }
    }
}