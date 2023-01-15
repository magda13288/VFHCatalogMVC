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

        public void AddNewUserPlantMessage(NewUserPlantMessage plantMessage)
        {
            _context.NewUserPlantMessages.Add(plantMessage);
            _context.SaveChanges();
        }

        public int AddMessage(Message message)
        {
           _context.Messages.Add(message);
           _context.SaveChanges();
            return message.Id;
        }

        public IQueryable<NewUserPlantMessage> GetMessagesForNewUserPlant(int plantId)
        {
            var messagesList = _context.NewUserPlantMessages.Where(m => m.PlantId == plantId);
            return messagesList;
        }

        public Message GetMessageById(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == id);
            return message;
        }
    }
}
