using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Module.Identity.SignIn
{
    public class SignInCommandHandler : AsyncRequestHandler<SignInCommand>
    {
        protected override Task Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
