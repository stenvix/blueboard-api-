using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Queries
{
    public class GetCurrentUser : IRequest<UserModel>
    {
    }
}
