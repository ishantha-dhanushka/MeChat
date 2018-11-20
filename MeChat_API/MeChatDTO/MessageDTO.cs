using System;
using System.Collections.Generic;
using System.Text;

namespace MeChatDTO
{
    public class MessageDTO
    {
        public string ChatId {get;set;}
        public string UserId { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
