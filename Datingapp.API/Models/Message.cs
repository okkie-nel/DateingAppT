using System;

namespace Datingapp.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }   
        public user Sender { get; set; }
        public int RecipientId { get; set; }
        public user Recipient { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }   
        public bool SenderDelete { get; set; }
        public bool RecipientDelete { get; set; }
    }
}