using AutoMapper;
using Catalog.API.BL.Interfaces;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Identity.Grpc.Protos;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<User>> AddUserAsync(ApplicationUserModel userModel)
        {
            var user = await _usersRepository.GetByIdAsync(new Guid(userModel.Id));

            if (user is not null)
            {
                return new ServiceResult<User>(ServiceResultType.BadRequest);
            }

            var userEntity = _mapper.Map<User>(userModel);

            return await _usersRepository.AddAsync(userEntity);
        }
    }
}
