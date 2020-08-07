using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Module.Identity.Helpers;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;
using Dawn;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BlueBoard.Module.Identity.Commands
{
    internal class VerifyAccessHandler : IRequestHandler<VerifyAccess, AccessTokenModel>
    {
        public VerifyAccessHandler(
            IMemoryCache memoryCache,
            IAccessHandler accessHandler,
            IConnectionFactory connectionFactory,
            IUserRepository userRepository)
        {
            this.MemoryCache = memoryCache;
            this.AccessHandler = accessHandler;
            this.ConnectionFactory = connectionFactory;
            this.UserRepository = userRepository;
        }

        private IMemoryCache MemoryCache { get; }
        private IAccessHandler AccessHandler { get; }
        private IConnectionFactory ConnectionFactory { get; }
        private IUserRepository UserRepository { get; }

        public async Task<AccessTokenModel> Handle(VerifyAccess request, CancellationToken cancellationToken)
        {
            var cachedPassword = this.MemoryCache.Get<string>(PasswordHelper.GetCacheKey(request.Email));
            if (cachedPassword != request.Password)
            {
                throw new BlueBoardValidationException(ErrorCodes.InvalidCredentials);
            }

            using (var connection = this.ConnectionFactory.Create())
            {
                var user = await this.UserRepository.FindByEmailAsync(connection, request.Email);

                Guard.Argument(user).NotNull();

                return this.AccessHandler.CreateAccessToken(user.Id, user.Email);
            }
        }
    }
}
