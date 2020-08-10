using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Module.Common;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Module.Trip.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Trip.Commands
{
    internal class UpdateTripListHandler : IRequestHandler<UpdateTripList, TripListInfo>
    {
        public UpdateTripListHandler(
            IMapper mapper,
            IMediator mediator,
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

        public async Task<TripListInfo> Handle(UpdateTripList request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.UnitOfWorkFactory.Create())
            {
                var entity = await this.TripListRepository.GetAsync(unitOfWork.Connection, request.TripList.Id);
                if (entity == null)
                {
                    throw new BlueBoardValidationException(ErrorCodes.InvalidId);
                }

                this.Mapper.Map(request.TripList, entity);
                entity.UpdatedBy = this.CurrentUserProvider.UserId;
                entity = await this.TripListRepository.UpdateAsync(unitOfWork.Connection, entity);
                unitOfWork.Commit();

                return this.Mapper.Map<TripListInfo>(entity);
            }
        }
    }
}
