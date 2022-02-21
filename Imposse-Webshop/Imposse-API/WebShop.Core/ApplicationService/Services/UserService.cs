using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepo;
        readonly IAuthenticationHelper _authHelper;

        public UserService(IUserRepository userRepo, IAuthenticationHelper authenticationHelper)
        {
            _userRepo = userRepo;
            _authHelper = authenticationHelper;
        }

       

        public User DeleteUser(int id)
        {
            return _userRepo.UserRemove(id);
        }

        public User FindUserByID(int id)
        {
            var user = _userRepo.UserGet(id);
            return user;
        }

        public List<Review> FindOwnedReviews(int id)
        {
            return null;
        }

        public User NewUser(User user)
        {
            return _userRepo.UserAdd(user);
        }

        public List<User> ReadAllUsers()
        {
            return _userRepo.GetAll().ToList();
        }

        public User UpdateUser(User user)
        {
            return _userRepo.UserEdit(user);
        }

        public User GetByUsername(string username)
        {
            return _userRepo.GetAll().FirstOrDefault(u => u.Username == username);
        }

        public void SetPasswordInfo(User user, LoginInputModel login)
        {
            byte[] passHash, passSalt;
            _authHelper.CreatePasswordHash(login.Password, out passHash, out passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
        }

        public List<string> GetUsernames()
        {
            return _userRepo.GetAll().Select(u => u.Username).ToList();
        }
    }
}