using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Common;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Identity.Queries
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserModel>
    {
        private readonly IMapper mapper;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly IConnectionFactory connectionFactory;
        private readonly IUserRepository userRepository;

        public GetCurrentUserQueryHandler(IMapper mapper, ICurrentUserProvider currentUserProvider,
            IConnectionFactory connectionFactory, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.currentUserProvider = currentUserProvider;
            this.connectionFactory = connectionFactory;
            this.userRepository = userRepository;
        }

        public async Task<UserModel> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var user = await this.userRepository.FindById(connection, this.currentUserProvider.UserId);

                return this.mapper.Map<UserModel>(user);
            }
        }
    }
}
