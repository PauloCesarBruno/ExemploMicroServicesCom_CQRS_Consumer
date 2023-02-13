using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class PartNumberQuantityCommandHandler : IRequestHandler<PartNumberQuantityCommand, Unit>
    {
        private readonly IPartNumberQuantityRepository _PartNumberQuantityRepository;
        private readonly IMapper _mapper;

        public PartNumberQuantityCommandHandler(IMapper mapper,
                                        IPartNumberQuantityRepository PartNumberQuantityRepository)
        {
            _PartNumberQuantityRepository = PartNumberQuantityRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PartNumberQuantityCommand request, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<PartNumberQuantity>(request);

            var findDatabase = await _PartNumberQuantityRepository.GetByReference(record);

            await UpSertEntityAsync(record, findDatabase);

            return Unit.Value;
        }

        private async Task UpSertEntityAsync(PartNumberQuantity recordFromTopic, PartNumberQuantity findDatabase)
        {
            if (findDatabase is null)
                await _PartNumberQuantityRepository.InsertAsync(recordFromTopic);
            else
                await _PartNumberQuantityRepository.UpdateAsync(recordFromTopic, findDatabase.Id);
        }
    }
}
