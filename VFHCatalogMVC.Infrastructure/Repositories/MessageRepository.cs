using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private Context _context;
        public MessageRepository(Context context)
        {
            _context = context;
        }

        public void AddEntity<T>(T entity) where T : class 
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public int AddMessage(Message message)
        {
           _context.Messages.Add(message);
           _context.SaveChanges();
            return message.Id;
        }

        public IQueryable<PlantMessage> GetMessagesForNewUserPlant(int plantId)
        {
            var messagesList = _context.PlantMessages.Where(m => m.PlantId == plantId);
            return messagesList;
        }

        public Message GetMessageById(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == id);
            return message;
        }

        public string GetPlantOwnerId(int plantId)
        {
            var userInfo = _context.NewUserPlants.FirstOrDefault(e => e.PlantId == plantId);
            return userInfo.UserId;
        }

        public int GetPlantId(int id)
        {
            var message = _context.PlantMessages.FirstOrDefault(e => e.MessageId == id);
            if (message == null)
                return 0;
            else
            return message.PlantId;
        }

        public void UpdateMassageStatusIsAnswer(Message message)
        {
            _context.Attach(message);
            _context.Entry(message).Property(e=>e.isAnswer).IsModified = true;
            _context.SaveChanges();
        }
        public int GetMessageAnswerIdById(int id)
        {
            var message = _context.MessageAnswers.FirstOrDefault(e => e.MessageId == id);
            return message.MessageAnswerId;

        }

        public IQueryable<T> GetMessage<T>(string userId) where T: BaseEntityProperty
        {
            return _context.Set<T>().Where(e => e.UserId == userId);
        }
        public MessageReceiver GetMessageReceiverByMessageId(int id)
        {
            var message = _context.MessageReceivers.FirstOrDefault(e=>e.MessageId== id);
            return message;
        }
    }
}
