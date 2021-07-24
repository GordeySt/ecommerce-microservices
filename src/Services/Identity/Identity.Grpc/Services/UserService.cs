using AutoMapper;
using Grpc.Core;
using Identity.Application.ApplicationUsers.Queries.GetUserById;
using Identity.Grpc.Protos;
using MediatR;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.Grpc.Services
{
    public class UserService : UserProtoService.UserProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<ApplicationUserModel> GetUserById(GetUserByIdRequest request,
            ServerCallContext context)
        {
            var getUserByIdResult = await _mediator.Send(new GetUserByIdQuery(new Guid(request.Id)));

            if (getUserByIdResult.Result is not ServiceResultType.Success)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    getUserByIdResult.Message));
            }

            return _mapper.Map<ApplicationUserModel>(getUserByIdResult.Data);
        }
    }
}
