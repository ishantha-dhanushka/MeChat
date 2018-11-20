using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeChatRepository.DbModels
{
    public class Message
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string ChatId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string MessageText { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
