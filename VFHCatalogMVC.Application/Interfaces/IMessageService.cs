using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Message;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IMessageService
    {
        MessageVm FillMessageProperties(int plantId, string user);
        void SendMessageToAdmin(MessageVm message);
        MessageForListVm GetMessagesForPlant(int plantId, int pageSize, int? pageNo);
       
    }
}
