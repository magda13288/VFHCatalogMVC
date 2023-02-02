using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Message;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IMessageService
    {
        //id = plantId || id = messageId
        MessageVm FillMessageProperties(/*int plantId,*/int id, string user, IndexPlantType index, string ownerId);
        void SendMessage(MessageVm message);
        MessageForListVm GetMessagesForPlant(int plantId, int pageSize, int? pageNo, MessageDisplay messageDisplay, IndexPlantType index, string userName);
        void SendPlantMessage(int messageId, int plantId, string userId, IndexPlantType index);
        int GetPlantIdForMessage(int id);
        MessageVm GetMessageById(int id);
        MessageForListVm GetMessages(int pageSize, int? pageNo, MessageDisplay messageDisplay, /*IndexPlantType index,*/ string userName);

    }
}
