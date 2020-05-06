using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class UpdateCurrentProfileCommand : IRequest<UserModel>
    {
        public UpdateCurrentProfileCommand(UserModel profile)
        {
            this.Profile = profile;
        }

        public UserModel Profile { get; }
    }
}
