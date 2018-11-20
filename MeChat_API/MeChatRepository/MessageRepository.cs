using MeChatDTO;
using MeChatRepository.DbModels;
using MeChatRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChatRepository
{
    public class MessageRepository : IMessageRepository
    {
        private AppDbContext _appDbContext;
        public MessageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Get all messages
        /// </summary>
        /// <returns></returns>
        public List<Message> GetAll()
        {
            return _appDbContext.Messages.ToList();
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message GetById(string id)
        {
            return _appDbContext.Messages.Find(id);
        }

        /// <summary>
        /// Create message
        /// </summary>
        /// <param name="friend"></param>
        public Message Create(Message message)
        {
            _appDbContext.Messages.Add(message);
            _appDbContext.SaveChanges();
            return message;
        }

        /// <summary>
        /// Get messages by chat id
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public List<Message> GetByChatId(string chatId)
        {
            return _appDbContext.Messages.Where(m => m.ChatId == chatId).OrderBy(m => m.CreatedDate).ToList();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
