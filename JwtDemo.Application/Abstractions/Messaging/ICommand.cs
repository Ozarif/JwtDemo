using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Domain.Abstractions;
using MediatR;

namespace JwtDemo.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
    {
    }
}