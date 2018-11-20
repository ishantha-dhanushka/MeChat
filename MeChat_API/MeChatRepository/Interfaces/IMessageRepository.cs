using MeChatRepository.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeChatRepository.Interfaces
{
    public interface IMessageRepository : IDisposable
    {
        List<Message> GetAll();
        Message GetById(string id);
        Message Create(Message message);
        List<Message> GetByChatId(string chatId);
    }
}
