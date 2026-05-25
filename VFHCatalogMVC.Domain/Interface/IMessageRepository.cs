using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IMessageRepository: IRepository<Message>
    {
        void AddEntity<T>(T entity) where T : class;
        IQueryable<PlantMessage> GetMessagesForNewUserPlant(int plantId);
        string GetPlantOwnerId(int plantId);
        int GetPlantId(int id);
        void UpdateMassageStatusIsAnswer(Message message);
        int GetMessageAnswerIdById(int id);
        IQueryable<T> GetMessage<T>(string userId) where T : BaseEntityProperty;
        MessageReceiver GetMessageReceiverByMessageId(int id);
              
    }
}
