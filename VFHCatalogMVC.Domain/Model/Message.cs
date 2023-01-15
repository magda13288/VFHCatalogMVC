using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string MessageContent { get; set; }
        public DateTime AddedDate { get; set; }
        public virtual ICollection<MessageThread> MessageThreads { get; set; }
        public virtual ICollection<NewUserPlantMessage> NewUserPlantMessages { get; set; }
    }
}
