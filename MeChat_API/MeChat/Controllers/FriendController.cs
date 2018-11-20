using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeChatDTO;
using MeChatHub;
using MeChatRepository;
using MeChatRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MeChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private AppDbContext _dbcontext;
        private readonly IHubContext<ChatHub> _hubContext;
        private IFriendRepository _friendRepository;

        public FriendController(AppDbContext dbcontext, IHubContext<ChatHub> hubcontext, IFriendRepository friendRepository)
        {
            this._dbcontext = dbcontext;
            this._hubContext = hubcontext;
            this._friendRepository = friendRepository;
        }

        /// <summary>
        /// Get all frieends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        public List<Friend> GetAll()
        {
            return _friendRepository.GetAll();
        }

        /// <summary>
        /// Get my friends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<FriendDTO>> Get()
        {
            return _friendRepository.GetMyFriends();
        }

        /// <summary>
        /// Get members who are not friends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("notfriends")]
        public Task<IEnumerable<FriendDTO>> GetNotFriends()
        {
            return _friendRepository.GetNotFriends();
        }

        /// <summary>
        /// Make friend
        /// </summary>
        /// <param name="friend"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<Friend> CreateAsync(MakeFriendDTO friend)
        {
            return await _friendRepository.Create(friend.FriendId);
        }
    }
}