using BlueBoard.API.Contracts.Base;
using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.API.Contracts.Profile
{
    public class UpdateProfileRequest: ApiRequest
    {
        public SlimProfileModel Profile { get; set; }
    }
}
