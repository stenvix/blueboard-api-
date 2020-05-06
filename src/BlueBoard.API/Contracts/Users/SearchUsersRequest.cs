using System.ComponentModel.DataAnnotations;
using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Users
{
    public class SearchUsersRequest : ApiRequest
    {
        [Required]
        public string Query { get; set; }
    }
}
