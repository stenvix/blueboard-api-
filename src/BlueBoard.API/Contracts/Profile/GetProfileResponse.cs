using BlueBoard.API.Contracts.Base;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.API.Contracts.Profile
{
    public class GetProfileResponse : ApiResponse
    {
        public GetProfileResponse(ProfileModel profileModel) : base(ResponseCode.Success, null)
        {
            this.Profile = profileModel;
        }

        public ProfileModel Profile { get;  }
    }
}
