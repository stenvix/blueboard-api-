using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Identity.Queries
{
    internal class GetUserHandler : IRequestHandler<GetUser, UserModel>
    {
        public GetUserHandler(
            IMapper mapper,
            IConnectionFactory connectionFactory,
            IUserRepository userRepository)
        {
            this.Mapper = mapper;
            this.ConnectionFactory = connectionFactory;
            this.UserRepository = userRepository;
        }

        private IMapper Mapper { get; }
        private IConnectionFactory ConnectionFactory { get; }
        private IUserRepository UserRepository { get; }

        public async Task<UserModel> Handle(GetUser request, CancellationToken cancellationToken)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                var user = await this.UserRepository.FindById(connection, request.UserId);

                return this.Mapper.Map<UserModel>(user);
            }
        }
    }
}
