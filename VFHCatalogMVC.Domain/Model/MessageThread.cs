using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class MessageThread
    {
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public int ReceiverMessageId { get; set; }
        public virtual MessageReceiver MessageReceiver { get; set;}

    }
}
