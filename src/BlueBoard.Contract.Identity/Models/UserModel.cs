using BlueBoard.Common.Enums;

namespace BlueBoard.Contract.Identity.Models
{
    public class UserModel: SlimUserModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
