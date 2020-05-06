using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Common;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;

namespace BlueBoard.Module.Identity.Queries
{
    public class SearchUsersQueryHandler: QueryRequestHandlerBase<SearchUsersQuery, IEnumerable<SlimUserModel>>
    {
        private readonly IUserRepository userRepository;
        private readonly ICurrentUserProvider currentUserProvider;

        public SearchUsersQueryHandler(
            IMapper mapper,
            IConnectionFactory connectionFactory,
            IUserRepository userRepository,
            ICurrentUserProvider currentUserProvider)
            : base(mapper, connectionFactory)
        {
            this.userRepository = userRepository;
            this.currentUserProvider = currentUserProvider;
        }

        protected override async Task<IEnumerable<SlimUserModel>> HandleAsync(IDbConnection connection, SearchUsersQuery request)
        {
            var entities = await this.userRepository.SearchByQueryAsync(connection, request.Query);

            entities = entities.Where(i => i.Id != this.currentUserProvider.UserId);
            return this.Mapper.Map<IEnumerable<SlimUserModel>>(entities);
        }
    }
}
