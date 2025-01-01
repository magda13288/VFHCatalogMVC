using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class MessageReceiver: BaseEntityProperty
    {
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }

        //public virtual ICollection<MessageAnswer> MessageAnswers { get; set; }
    }
}
