using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public IQueryable<PlantMessage> GetMessagesForNewUserPlant(int plantId)
        {
            var messagesList = _context.PlantMessages.AsNoTracking().Where(m => m.PlantId == plantId);
            return messagesList;
        }

        public string GetPlantOwnerId(int plantId)
        {
            var userInfo = _context.NewUserPlants.AsNoTracking().FirstOrDefault(e => e.PlantId == plantId);
            return userInfo.UserId;
        }

        public int GetPlantId(int id)
        {
            var message = _context.PlantMessages.AsNoTracking().FirstOrDefault(e => e.MessageId == id);
            if (message == null)
                return 0;
            else
                return message.PlantId;
        }

        public void UpdateMassageStatusIsAnswer(Message message)
        {
            _context.Attach(message);
            _context.Entry(message).Property(e => e.isAnswer).IsModified = true;
            _context.SaveChanges();
        }
        public int GetMessageAnswerIdById(int id)
        {
            var message = _context.MessageAnswers.AsNoTracking().FirstOrDefault(e => e.MessageId == id);
            return message.MessageAnswerId;

        }

        public IQueryable<T> GetMessage<T>(string userId) where T : BaseEntityProperty
        {
            return _context.Set<T>().AsNoTracking().Where(e => e.UserId == userId);
        }
        public MessageReceiver GetMessageReceiverByMessageId(int id)
        {
            var message = _context.MessageReceivers.AsNoTracking().FirstOrDefault(e => e.MessageId == id);
            return message;
        }

		public IQueryable<Message> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public IQueryable<Message> GetAll(int pageNumber, int rowCount)
		{
			throw new System.NotImplementedException();
		}

		public Message GetById(int id)
		{
			var message = _context.Messages.AsNoTracking().FirstOrDefault(m => m.Id == id);
			return message;
		}

		public int Add(Message entity)
		{
			_context.Messages.Add(entity);
			_context.SaveChanges();
			return entity.Id;
		}

		public void Delete(Message entity)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteById(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Message entity)
		{
			throw new System.NotImplementedException();
		}
	}
}
