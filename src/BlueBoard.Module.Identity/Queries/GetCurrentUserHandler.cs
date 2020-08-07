using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Common;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Identity.Queries
{
    internal class GetCurrentUserHandler : IRequestHandler<GetCurrentUser, UserModel>
    {
        public GetCurrentUserHandler(
            IMapper mapper,
            ICurrentUserProvider currentUserProvider,
            IConnectionFactory connectionFactory,
            IUserRepository userRepository)
        {
            this.Mapper = mapper;
            this.CurrentUserProvider = currentUserProvider;
            this.ConnectionFactory = connectionFactory;
            this.UserRepository = userRepository;
        }

        private IMapper Mapper { get; }
        private ICurrentUserProvider CurrentUserProvider { get; }
        private IConnectionFactory ConnectionFactory { get; }
        private IUserRepository UserRepository { get; }

        public async Task<UserModel> Handle(GetCurrentUser request, CancellationToken cancellationToken)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                var user = await this.UserRepository.FindById(connection, this.CurrentUserProvider.UserId);

                return this.Mapper.Map<UserModel>(user);
            }
        }
    }
}
