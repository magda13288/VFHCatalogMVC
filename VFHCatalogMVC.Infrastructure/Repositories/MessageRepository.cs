using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void AddMessageReceiver(MessageReceiver receiver)
        {
            _context.MessageReceivers.Add(receiver);
            _context.SaveChanges();
        }

        public void AddNewUserPlantMessage(PlantMessage plantMessage)
        {
            _context.PlantMessages.Add(plantMessage);
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
            _context.Entry(message).Property("isAnswer").IsModified = true;
            _context.SaveChanges();
        }

        public void AddMessageAnswer(MessageAnswer message)
        {
            _context.MessageAnswers.Add(message);
            _context.SaveChanges();
        }
        public int GetMessageAnswerIdById(int id)
        {
            var message = _context.MessageAnswers.FirstOrDefault(e => e.MessageId == id);
            return message.MessageAnswerId;

        }

        public IQueryable<Message> GetSentMessages(string userId)
        {
           var messages = _context.Messages.Where(e => e.UserId == userId);
            return messages;
        }

        public IQueryable<MessageReceiver> GetReceivedMessages(string userId)
        {
           var messages = _context.MessageReceivers.Where(e => e.UserId == userId);
           return messages;
        }

        public MessageReceiver GetMessageReceiverByMessageId(int id)
        {
            var message = _context.MessageReceivers.FirstOrDefault(e=>e.MessageId== id);
            return message;
        }
    }
}
