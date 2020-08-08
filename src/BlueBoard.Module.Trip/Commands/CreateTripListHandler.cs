using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Module.Common;
using BlueBoard.Module.Trip.Repositories;
using BlueBoard.Module.Trip.Repositories.Entities;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Trip.Commands
{
    public class CreateTripListHandler : IRequestHandler<CreateTripList, TripListInfo>
    {
        public CreateTripListHandler(
            IMapper mapper,
            IUnitOfWorkFactory unitOfWorkFactory,
            ITripListRepository tripListRepository,
            ICurrentUserProvider currentUserProvider)
        {
            this.Mapper = mapper;
            this.UnitOfWorkFactory = unitOfWorkFactory;
            this.TripListRepository = tripListRepository;
            this.CurrentUserProvider = currentUserProvider;
        }

        private IMapper Mapper { get; }
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }
        private ITripListRepository TripListRepository { get; }
        private ICurrentUserProvider CurrentUserProvider { get; }

        public async Task<TripListInfo> Handle(CreateTripList request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.UnitOfWorkFactory.Create())
            {
                var entity = this.Mapper.Map<TripListEntity>(request.TripList);
                entity.CreatedBy = this.CurrentUserProvider.UserId;
                entity = await this.TripListRepository.CreateAsync(unitOfWork.Connection, entity);
                unitOfWork.Commit();

                return this.Mapper.Map<TripListInfo>(entity);
            }
        }
    }
}
