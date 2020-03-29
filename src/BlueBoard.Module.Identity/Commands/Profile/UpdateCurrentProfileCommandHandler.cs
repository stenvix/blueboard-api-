using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Module.Common;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Identity.Commands.Profile
{
    public class UpdateCurrentProfileCommandHandler : IRequestHandler<UpdateCurrentProfileCommand, ProfileModel>
    {
        private readonly IMapper mapper;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IUserRepository userRepository;

        public UpdateCurrentProfileCommandHandler(IMapper mapper, ICurrentUserProvider currentUserProvider,
            IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.currentUserProvider = currentUserProvider;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.userRepository = userRepository;
        }

        public async Task<ProfileModel> Handle(UpdateCurrentProfileCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var user = await this.userRepository.FindById(unitOfWork.Connection, this.currentUserProvider.UserId);
                this.mapper.Map(request.Profile, user);
                user = await this.userRepository.Update(unitOfWork.Connection, user);
                unitOfWork.Commit();

                return this.mapper.Map<ProfileModel>(user);
            }
        }
    }
}
