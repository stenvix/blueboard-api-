using BlueBoard.API.Contracts.Base;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.API.Contracts.Profile
{
    public class UpdateProfileResponse : ApiResponse
    {
        public ProfileModel Profile { get; }

        public UpdateProfileResponse(ProfileModel profile) : base(ResponseCode.Success, null)
        {
            this.Profile = profile;
        }
    }
}
