using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeChatHub;
using MeChatRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MeChat.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private AppDbContext _dbcontext;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubcontext, AppDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
            this._hubContext = hubcontext;
        }


        // GET: api/Chat
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return _dbcontext.Users.Select(u => u.UserName).ToArray();
        }

        // GET: api/Chat/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Chat
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Chat/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
