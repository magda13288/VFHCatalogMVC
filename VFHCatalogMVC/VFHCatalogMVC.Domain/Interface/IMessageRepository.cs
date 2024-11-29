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
        void AddNewUserPlantMessage(PlantMessage plantMessage);
        IQueryable<PlantMessage> GetMessagesForNewUserPlant(int plantId);
        Message GetMessageById (int id);
        string GetPlantOwnerId(int plantId);
        int GetPlantId(int id);
        void UpdateMassageStatusIsAnswer(Message message);
        void AddMessageAnswer(MessageAnswer message);
        int GetMessageAnswerIdById(int id);
        IQueryable<Message> GetSentMessages(string userId);
        IQueryable<MessageReceiver> GetReceivedMessages(string userId);
        MessageReceiver GetMessageReceiverByMessageId(int id);
              
    }
}
