using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Contract.Trip.Queries;
using BlueBoard.Module.Trip.Repositories;
using BlueBoard.Module.Trip.Repositories.Entities;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Trip.Commands
{
    internal class CreateTripHandler : IRequestHandler<CreateTrip, TripModel>
    {
        public CreateTripHandler(
            IMapper mapper,
            IMediator mediator,
            IUnitOfWorkFactory unitOfWorkFactory,
            ITripRepository tripRepository)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.UnitOfWorkFactory = unitOfWorkFactory;
            this.TripRepository = tripRepository;
        }

        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }
        private ITripRepository TripRepository { get; }

        public async Task<TripModel> Handle(CreateTrip request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.UnitOfWorkFactory.Create())
            {
                var currentUser = await this.Mediator.Send(new GetCurrentUser(), cancellationToken);
                var entity = this.Mapper.Map<TripEntity>(request.Trip);
                entity.CreatedBy = currentUser.Id;
                entity.Status = TripStatus.Initialized;
                entity = await this.TripRepository.CreateTripAsync(unitOfWork.Connection, entity);
                unitOfWork.Commit();

                return await this.Mediator.Send(new GetTrip(entity.Id), cancellationToken);
            }
        }
    }
}
