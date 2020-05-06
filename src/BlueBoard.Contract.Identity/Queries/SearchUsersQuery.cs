using System.Collections.Generic;
using BlueBoard.Contract.Common.Models;
using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Queries
{
    public class SearchUsersQuery: IRequest<IEnumerable<SlimUserModel>>
    {
        public SearchUsersQuery(string query)
        {
            this.Query = query;
        }

        public string Query { get; }
    }
}
