using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Queries
{
    public class GetCurrentUserQuery : IRequest<UserModel>
    {
    }
}
