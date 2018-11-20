using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeChatRepository
{
    public class Friend
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FriendId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}