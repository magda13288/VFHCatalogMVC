using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Message;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IMessageService
    {
        //id = plantId || id = messageId
        MessageVm FillMessageProperties(int id, string user);
        void SendNewPlantMessage(MessageVm message);
        MessageForListVm GetMessagesForPlant(int plantId, int pageSize, int? pageNo);

        void SendNewPlantUserMessage(int messageId, int plantId, string userId);

        //int GetPlantIdForMessage(int id);
    }
}
