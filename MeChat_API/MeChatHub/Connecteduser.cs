using System;
using System.Collections.Generic;
using System.Text;

namespace MeChatHub
{
    public class Connecteduser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}
