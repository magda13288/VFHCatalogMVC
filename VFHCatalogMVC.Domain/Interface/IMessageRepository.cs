using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IMessageRepository
    {
        int AddMessage(Message message);
        void AddMessageReceiver(MessageReceiver receiver);
        void AddNewUserPlantMessage(NewUserPlantMessage plantMessage);
        IQueryable<NewUserPlantMessage> GetMessagesForNewUserPlant(int plantId);
        Message GetMessageById (int id);
    }
}
