using BlueBoard.Contract.Common.Models;

namespace BlueBoard.Contract.Identity.Models
{
    public class SlimProfileModel : SlimUserModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
