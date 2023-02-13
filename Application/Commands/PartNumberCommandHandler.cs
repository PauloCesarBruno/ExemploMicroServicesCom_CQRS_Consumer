using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class PartNumberCommandHandler : IRequestHandler<PartNumberCommand, Unit>
    {
        private readonly IPartNumberRepository _partNumberRepository;
        private readonly IMapper _mapper;

        public PartNumberCommandHandler(IMapper mapper,
                                        IPartNumberRepository partNumberRepository)
        {
            _partNumberRepository = partNumberRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PartNumberCommand request, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<PartNumber>(request);

            var findDatabase = await _partNumberRepository.GetByReference(record);

            await UpSertEntityAsync(record, findDatabase);

            return Unit.Value;
        }

        private async Task UpSertEntityAsync(PartNumber recordFromTopic, PartNumber findDatabase)
        {
            if (findDatabase is null)
                await _partNumberRepository.InsertAsync(recordFromTopic);
            else
                await _partNumberRepository.UpdateAsync(recordFromTopic, findDatabase.Id);
        }
    }
}
