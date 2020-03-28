using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Module.Identity.Helpers;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using Dawn;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BlueBoard.Module.Identity.VerifyAccess
{
    public class VerifyAccessCommandHandler : IRequestHandler<VerifyAccessCommand, AccessTokenModel>
    {
        private readonly IMemoryCache memoryCache;
        private readonly IAccessHandler accessHandler;
        private readonly IConnectionFactory connectionFactory;
        private readonly IUserRepository userRepository;

        public VerifyAccessCommandHandler(IMemoryCache memoryCache, IAccessHandler accessHandler,
            IConnectionFactory connectionFactory, IUserRepository userRepository)
        {
            this.memoryCache = memoryCache;
            this.accessHandler = accessHandler;
            this.connectionFactory = connectionFactory;
            this.userRepository = userRepository;
        }

        public async Task<AccessTokenModel> Handle(VerifyAccessCommand request, CancellationToken cancellationToken)
        {
            var cachedPassword = this.memoryCache.Get<string>(PasswordHelper.GetCacheKey(request.Email));
            if (cachedPassword != request.Password)
            {
                throw new BlueBoardValidationException(ErrorCodes.InvalidCredentials);
            }

            using (var connection = this.connectionFactory.Create())
            {
                var user = await this.userRepository.FindByEmailAsync(connection, request.Email);

                Guard.Argument(user).NotNull();

                return this.accessHandler.CreateAccessToken(user.Id, user.Email);
            }
        }
    }
}
