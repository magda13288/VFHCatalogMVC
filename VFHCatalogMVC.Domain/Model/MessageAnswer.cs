using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class MessageAnswer
    {
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public int MessageAnswerId { get; set; }
        public virtual Message AnswerMessage { get; set; }
      
        //public virtual MessageReceiver MessageReceiver { get; set; }
    }
}
