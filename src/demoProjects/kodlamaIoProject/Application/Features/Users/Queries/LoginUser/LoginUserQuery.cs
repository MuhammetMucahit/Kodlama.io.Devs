using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.LoginUser
{
    public class LoginUserQuery : UserForLoginDto, IRequest<AccessToken>
    {
        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<AccessToken> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(
                    x => x.Email.ToLower() == request.Email.ToLower(),
                    include: y => y.Include(c => c.UserOperationClaims).ThenInclude(z => z.OperationClaim));

                List<OperationClaim> operationClaims = new List<OperationClaim>();

                foreach (var userOperationClaim in user.UserOperationClaims)
                {
                    operationClaims.Add(userOperationClaim.OperationClaim);
                }

                _userBusinessRules.UserShouldExist(user);

                _userBusinessRules.UserCredentialsVerify(request.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken token = _tokenHelper.CreateToken(user, operationClaims);
                return token;
            }
        }
    }
}
