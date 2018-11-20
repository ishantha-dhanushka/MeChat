using MeChatDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeChatRepository.Interfaces
{

    public interface IFriendRepository : IDisposable
    {
        List<Friend> GetAll();

        Friend GetById(string id);

        Task<Friend> Create(string friendId);
        Task<IEnumerable<FriendDTO>> GetMyFriends();
        Task<IEnumerable<FriendDTO>> GetNotFriends();
    }
}
