using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IMessageRepository
    {
        void AddEntity<T>(T entity) where T : class;
        int AddMessage(Message message);
        //void AddMessageReceiver(MessageReceiver receiver);
        //void AddNewUserPlantMessage(PlantMessage plantMessage);
        IQueryable<PlantMessage> GetMessagesForNewUserPlant(int plantId);
        Message GetMessageById (int id);
        string GetPlantOwnerId(int plantId);
        int GetPlantId(int id);
        void UpdateMassageStatusIsAnswer(Message message);
        //void AddMessageAnswer(MessageAnswer message);
        int GetMessageAnswerIdById(int id);
        IQueryable<T> GetMessage<T>(string userId) where T : BaseEntityProperty;
        //IQueryable<Message> GetSentMessages(string userId);
        //IQueryable<MessageReceiver> GetReceivedMessages(string userId);
        MessageReceiver GetMessageReceiverByMessageId(int id);
              
    }
}
