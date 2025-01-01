using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class Message
    {
        public string MessageContent { get; set; }
        public DateTime AddedDate { get; set; }
        public bool isAnswer { get; set; }
        public virtual ICollection<MessageAnswer> MessageAnswers { get; set; }
        public virtual ICollection<PlantMessage> PlantMessages { get; set; }
    }
}
