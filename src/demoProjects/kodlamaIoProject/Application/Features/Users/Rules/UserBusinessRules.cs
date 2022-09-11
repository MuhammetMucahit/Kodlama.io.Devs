using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task UserEmailCannotBeDuplicated(string email)
        {
            var user = await userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("This email name exists");
        }

        public void UserCredentialsVerify(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result) throw new BusinessException("Identity information is incorrect");
        }
        public void UserShouldExist(User user)
        {
            if (user == null) throw new BusinessException("User does not exist");
        }
    }
}
