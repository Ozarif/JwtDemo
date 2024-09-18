using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Domain.Identity
{
    public static class DemoUserError
    {
                public static readonly Error InvalidCredentials = new(
                    "User.InvalidCredentials",
                    "Invalid credentials");

           public static readonly Error AlreadyExists = new(
                    "User.AlreadyExist",
                    "User already exists");


        public static readonly Error ServerError = new(
                    "ApplicationUser.ServerError",
                    "Failed to create user");
    }
}