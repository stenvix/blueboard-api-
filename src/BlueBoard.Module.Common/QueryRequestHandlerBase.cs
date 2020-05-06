using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Common
{
    public abstract class QueryRequestHandlerBase<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IRequest<TResult>
    {
        private readonly IConnectionFactory connectionFactory;
        protected readonly IMapper Mapper;

        protected QueryRequestHandlerBase(IMapper mapper, IConnectionFactory connectionFactory)
        {
            this.Mapper = mapper;
            this.connectionFactory = connectionFactory;
        }

        public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                return await this.HandleAsync(connection, request);
            }
        }

        protected abstract Task<TResult> HandleAsync(IDbConnection connection, TQuery request);
    }
}
