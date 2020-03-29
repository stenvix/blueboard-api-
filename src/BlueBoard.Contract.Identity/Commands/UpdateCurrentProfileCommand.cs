using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class UpdateCurrentProfileCommand : IRequest<ProfileModel>
    {
        public SlimProfileModel Profile { get; set; }
    }
}
