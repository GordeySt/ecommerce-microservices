using Identity.Domain.Entities;
using MediatR;
using Services.Common.ResultWrappers;
using System;

namespace Identity.Application.ApplicationUsers.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ServiceResult<ApplicationUser>>;

    public class GetUserByIdQueryHandler { }
}
