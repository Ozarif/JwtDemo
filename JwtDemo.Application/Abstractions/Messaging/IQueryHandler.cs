using MediatR;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Abstractions.Messaging
{
     public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}