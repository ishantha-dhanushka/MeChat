using MeChatDTO;
using MeChatRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChatRepository
{
    public class FriendRepository : IFriendRepository
    {
        private IUserRepository _userRepository;
        private AppDbContext _appDbContext;

        public FriendRepository(AppDbContext appDbContext, IUserRepository userRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all friends
        /// </summary>
        /// <returns></returns>
        public List<Friend> GetAll()
        {
            return _appDbContext.Friends.ToList();
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Friend GetById(string id)
        {
            return _appDbContext.Friends.Find(id);
        }

        /// <summary>
        /// Create Friend
        /// </summary>
        /// <param name="friend"></param>
        public async Task<Friend> Create(string friendId)
        {
            AppUser user = await _userRepository.GetLoggedUser();

            Friend friend = new Friend
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                FriendId = friendId,
                CreatedDate = DateTime.Now
            };

            _appDbContext.Friends.Add(friend);
            _appDbContext.SaveChanges();
            return friend;
        }

        /// <summary>
        /// Get My Friends
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FriendDTO>> GetMyFriends()
        {
            AppUser user = await _userRepository.GetLoggedUser();

            List<FriendDTO> my_friends = (from fr in _appDbContext.Friends
                                          join u in _appDbContext.Users on fr.FriendId equals u.Id
                                          where fr.UserId == user.Id
                                          select new FriendDTO()
                                          {
                                              ChatId = fr.Id.ToString(),
                                              FriendSince = fr.CreatedDate,
                                              Id = u.Id,
                                              Name = u.Name,
                                              userName = u.UserName
                                          }).Union(from fr in _appDbContext.Friends
                                                   join u in _appDbContext.Users on fr.UserId equals u.Id
                                                   where fr.FriendId == user.Id
                                                   select new FriendDTO()
                                                   {
                                                       ChatId = fr.Id.ToString(),
                                                       FriendSince = fr.CreatedDate,
                                                       Id = u.Id,
                                                       Name = u.Name,
                                                       userName = u.UserName
                                                   }).ToList();

            return my_friends;
        }

        /// <summary>
        /// Get not friends
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FriendDTO>> GetNotFriends()
        {
            AppUser user = await _userRepository.GetLoggedUser();

            // get my friends id list
            List<string> my_friends_id_list = (from fr in _appDbContext.Friends
                                          where fr.UserId == user.Id
                                          select fr.FriendId).Union(from fr in _appDbContext.Friends
                                                   where fr.FriendId == user.Id
                                                   select fr.UserId).ToList();

            // get users according to my friends id list
            List<FriendDTO> not_friends = (from u in _appDbContext.Users
                               where u.Id != user.Id && !my_friends_id_list.Contains(u.Id)
                               select new FriendDTO() {
                                   Id = u.Id,
                                   Name = u.Name,
                                   userName = u.UserName
                               }).ToList();

            return not_friends;
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        
    }
}
