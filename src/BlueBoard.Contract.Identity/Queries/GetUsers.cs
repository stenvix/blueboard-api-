using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Queries
{
    public class GetUsers : IRequest<UserModel[]>
    {
        public GetUsers(long[] userIds)
        {
            this.UserIds = userIds;
        }

        public long[] UserIds { get;  }
    }
}
