using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class UpdateCurrentProfile : IRequest<UserModel>
    {
        public UpdateCurrentProfile(UserModel profile)
        {
            this.Profile = profile;
        }

        public UserModel Profile { get; }
    }
}
