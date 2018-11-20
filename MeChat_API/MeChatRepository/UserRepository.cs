using MeChatRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeChatRepository
{
    public class UserRepository : IUserRepository
    {

        private UserManager<AppUser> _userManager;
        private AppDbContext _appDbContext;
        private IHttpContextAccessor _httpContextAccessor;

        public UserRepository(UserManager<AppUser> userManager, AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor) {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<AppUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Create(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        /// <summary>
        /// Get logged user
        /// </summary>
        /// <returns></returns>
        public async Task<AppUser> GetLoggedUser() {
            string username = this._httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return await _userManager.FindByNameAsync(username);
        }

        public void Dispose()
        {
            _userManager.Dispose();
            _appDbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
