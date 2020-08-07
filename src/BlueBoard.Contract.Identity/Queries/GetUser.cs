using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Queries
{
    public class GetUser: IRequest<UserModel>
    {
        public GetUser(long userId)
        {
            this.UserId = userId;
        }

        public long UserId { get;  }
    }
}
