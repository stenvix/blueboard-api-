using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Queries
{
    public class SearchUsersQuery: IRequest<SlimUserModel[]>
    {
        public SearchUsersQuery(string query)
        {
            this.Query = query;
        }

        public string Query { get; }
    }
}
