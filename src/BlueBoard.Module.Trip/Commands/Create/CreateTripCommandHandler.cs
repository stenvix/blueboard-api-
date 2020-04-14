using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Module.Common;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Entities;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Trip.Commands.Create
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripModel>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly ITripRepository tripRepository;

        public CreateTripCommandHandler(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory,
            ICurrentUserProvider currentUserProvider, ITripRepository tripRepository)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.currentUserProvider = currentUserProvider;
            this.tripRepository = tripRepository;
        }

        public async Task<TripModel> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var entity = this.mapper.Map<TripEntity>(request.Trip);
                entity.CreatedBy = this.currentUserProvider.Email;
                entity.Status = TripStatus.Initialized;
                entity = await this.tripRepository.CreateTripAsync(unitOfWork.Connection, entity);
                unitOfWork.Commit();

                return this.mapper.Map<TripModel>(entity);
            }
        }
    }
}
