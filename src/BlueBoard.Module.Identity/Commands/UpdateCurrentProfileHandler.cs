using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Module.Common;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Identity.Commands
{
    internal class UpdateCurrentProfileHandler : IRequestHandler<UpdateCurrentProfile, UserModel>
    {
        private readonly IMapper mapper;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IUserRepository userRepository;

        public UpdateCurrentProfileHandler(
            IMapper mapper,
            ICurrentUserProvider currentUserProvider,
            IUnitOfWorkFactory unitOfWorkFactory,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.currentUserProvider = currentUserProvider;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.userRepository = userRepository;
        }

        public async Task<UserModel> Handle(UpdateCurrentProfile request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var user = await this.userRepository.FindById(unitOfWork.Connection, this.currentUserProvider.UserId);
                this.mapper.Map(request.Profile, user);
                user = await this.userRepository.Update(unitOfWork.Connection, user, user.Id);
                unitOfWork.Commit();

                return this.mapper.Map<UserModel>(user);
            }
        }
    }
}
