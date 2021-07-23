using AutoMapper;
using Grpc.Core;
using Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo;
using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Grpc.Protos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.Grpc.Services
{
    public class UserService : UserProtoService.UserProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _dbContext;

        public UserService(IMediator mediator, IMapper mapper, IApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public override async Task<ApplicationUserModel> GetCurrentUser(GetCurrentUserRequest request,
            ServerCallContext context)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == new Guid(request.Id));

            if (user is null)
            {
                throw new RpcException(new Status(StatusCode.Unauthenticated,
                    NotFoundExceptionMessageConstants.NotFoundUserMessage));
            }

            return _mapper.Map<ApplicationUserModel>(user);
        }
    }
}
