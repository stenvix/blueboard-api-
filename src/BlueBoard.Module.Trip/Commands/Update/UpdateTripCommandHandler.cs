using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Module.Common;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Trip.Commands.Update
{
    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripModel>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly ITripRepository tripRepository;

        public UpdateTripCommandHandler(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory,
            ICurrentUserProvider currentUserProvider, ITripRepository tripRepository)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.currentUserProvider = currentUserProvider;
            this.tripRepository = tripRepository;
        }

        public async Task<TripModel> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var trip = await this.tripRepository.GetAsync(unitOfWork.Connection, request.Trip.Id);
                if (trip == null)
                {
                    throw new BlueBoardValidationException(ErrorCodes.InvalidId);
                }

                this.mapper.Map(request.Trip, trip);
                trip.UpdatedBy = this.currentUserProvider.UserId;
                trip = await this.tripRepository.UpdateTripAsync(unitOfWork.Connection, trip);
                unitOfWork.Commit();

                return this.mapper.Map<TripModel>(trip);
            }
        }
    }
}
