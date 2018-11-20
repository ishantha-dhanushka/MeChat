using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeChatDTO;
using MeChatRepository;
using MeChatRepository.DbModels;
using MeChatRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private AppDbContext _dbcontext;
        private readonly IMessageRepository _messageRepository;

        public MessageController(AppDbContext dbcontext, IMessageRepository messageRepository)
        {
            this._dbcontext = dbcontext;
            this._messageRepository = messageRepository;
        }

        [HttpGet]
        [Route("ByChatId/{chatId}")]
        public List<Message> GetByChatId(string chatId)
        {
            return _messageRepository.GetByChatId(chatId);
        }
    }
}