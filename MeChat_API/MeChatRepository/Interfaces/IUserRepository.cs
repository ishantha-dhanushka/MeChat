using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeChatRepository.Interfaces
{
    public  interface IUserRepository : IDisposable
    {
        List<AppUser> GetAll();

        Task<AppUser> GetById(string id);

        Task<IdentityResult> Create(AppUser user, string password);

        Task<AppUser> GetLoggedUser();
    }
}
