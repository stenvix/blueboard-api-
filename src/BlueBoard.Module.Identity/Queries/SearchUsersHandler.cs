using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Common;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;

namespace BlueBoard.Module.Identity.Queries
{
    internal class SearchUsersHandler: QueryRequestHandlerBase<SearchUsersQuery, SlimUserModel[]>
    {
        public SearchUsersHandler(
            IMapper mapper,
            IConnectionFactory connectionFactory,
            IUserRepository userRepository,
            ICurrentUserProvider currentUserProvider)
            : base(mapper, connectionFactory)
        {
            this.UserRepository = userRepository;
            this.CurrentUserProvider = currentUserProvider;
        }

        private IUserRepository UserRepository { get; }
        private ICurrentUserProvider CurrentUserProvider { get; }

        protected override async Task<SlimUserModel[]> HandleAsync(IDbConnection connection, SearchUsersQuery request)
        {
            var entities = await this.UserRepository.SearchByQueryAsync(connection, request.Query);

            entities = entities.Where(i => i.Id != this.CurrentUserProvider.UserId);
            return this.Mapper.Map<SlimUserModel[]>(entities);
        }
    }
}
