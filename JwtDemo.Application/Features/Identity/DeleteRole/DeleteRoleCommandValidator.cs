using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JwtDemo.Application.Features.Identity.DeleteRole
{
    internal class DeleteRoleCommandValidator  : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(r => r.RoleId).NotEmpty();
        }
    }
}