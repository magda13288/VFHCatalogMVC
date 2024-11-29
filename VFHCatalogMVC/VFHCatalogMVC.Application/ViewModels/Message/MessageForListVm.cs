using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Message
{
    public class MessageForListVm
    {
        public List<MessageVm> Message { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}
