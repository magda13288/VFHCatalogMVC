using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class MessageReceiver:AuditableEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        //public virtual ICollection<MessageAnswer> MessageAnswers { get; set; }
    }
}
