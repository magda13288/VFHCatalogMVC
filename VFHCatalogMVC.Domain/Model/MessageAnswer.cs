using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class MessageAnswer:AuditableEntity
    {
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public int MessageAnswerId { get; set; }
        public virtual Message AnswerMessage { get; set; }
        //public virtual MessageReceiver MessageReceiver { get; set; }
    }
}
