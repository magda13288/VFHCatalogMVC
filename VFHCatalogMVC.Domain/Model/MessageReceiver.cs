using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class MessageReceiver
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public bool isRead { get; set; }
        public bool isAnsweared { get; set; }
        public virtual ICollection<MessageThread> MessageThreads { get; set; }
    }
}
