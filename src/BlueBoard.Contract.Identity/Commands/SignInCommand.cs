using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class SignInCommand : IRequest
    {
        public string Email { get; set; }
    }
}
