using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Message;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMessageRepository _messageRepo;
        private readonly IMapper _mapper;

        public MessageService(UserManager<ApplicationUser> userManager, IMessageRepository messageRepository,IMapper mapper)
        {
            _userManager = userManager;
            _messageRepo = messageRepository;
            _mapper= mapper;
        }

        public MessageVm FillMessageProperties(int plantId, string user)
        {
            var userInfo = _userManager.FindByNameAsync(user);
            var message = new MessageVm() {UserId = userInfo.Result.Id, AddedDate = DateTime.Now, PlantId = plantId};

            return message;
        }

        public MessageForListVm GetMessagesForPlant(int plantId, int pageSize, int? pageNo)
        {
            var messagesList = _messageRepo.GetMessagesForNewUserPlant(plantId).ProjectTo<NewUserPlantMessageVm>(_mapper.ConfigurationProvider).ToList();
            var messagesDetails = new List<MessageVm>();
            var messagesToShow = new List<MessageVm>();

            if (messagesList != null)
            {
                foreach (var item in messagesList)
                {
                    var message = _messageRepo.GetMessageById(item.MessageId);
                    var messageVm = _mapper.Map<MessageVm>(message);

                    var userInfo = _userManager.FindByIdAsync(messageVm.UserId);
                    messageVm.UserName = userInfo.Result.UserName;
                    messageVm.AccountName = userInfo.Result.AccountName;

                    messagesDetails.Add(messageVm);

                    //var messageReceiver = new MessageReceiverVm() { };
                }

                messagesToShow = messagesDetails.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
            }

            var messagesNewPlantsList = new MessageForListVm()
            {
                PageSize= pageSize,
                CurrentPage= pageNo,
                Message = messagesToShow,
                Count = messagesToShow.Count,

            };    
            return messagesNewPlantsList;
        }


        public void SendMessageToAdmin(MessageVm message)
        {
            var sendMessage = _mapper.Map<Message>(message);
            var messageId = _messageRepo.AddMessage(sendMessage);

            if (messageId != 0)
            {
                var admin = _userManager.GetUsersInRoleAsync("Admin");
                var adminUser = admin.Result.FirstOrDefault(e => e.UserName.StartsWith("admin"));

                var messageReceiver = new MessageReceiverVm() { MessageId = messageId, UserId = adminUser.Id };

                _messageRepo.AddMessageReceiver(_mapper.Map<MessageReceiver>(messageReceiver));

                var newUserPlantMessage = new NewUserPlantMessageVm() { PlantId = message.PlantId, MessageId = messageId };

                _messageRepo.AddNewUserPlantMessage(_mapper.Map<NewUserPlantMessage>(newUserPlantMessage));
            }

        }
    }
}
