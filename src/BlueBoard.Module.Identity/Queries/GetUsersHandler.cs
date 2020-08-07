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
    internal class GetUsersHandler : IRequestHandler<GetUsers, UserModel[]>
    {
        public GetUsersHandler(
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

        public async Task<UserModel[]> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                var entities = await this.UserRepository.GetAllAsync(connection, request.UserIds);

                return this.Mapper.Map<UserModel[]>(entities);
            }
        }
    }
}
