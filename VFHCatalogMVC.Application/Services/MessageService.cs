using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.HelperClasses;
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
        private readonly IHelperService _helperService;

        public MessageService(UserManager<ApplicationUser> userManager, IMessageRepository messageRepository,IMapper mapper, IHelperService helperService)
        {
            _userManager = userManager;
            _messageRepo = messageRepository;
            _mapper = mapper;
            _helperService = helperService;
        }

        public MessageVm FillMessageProperties(int id, string user)
        {
            var plantIdForMessage = _messageRepo.GetPlantId(id);
            var userInfo = _userManager.FindByNameAsync(user);
            var message = new MessageVm();

            if (plantIdForMessage != 0)
            {
                message.UserId = userInfo.Result.Id;
                message.AddedDate = DateTime.Now;
                message.PlantId = plantIdForMessage;
                message.messageIdisAnswer = id;

            }
            else
            {
                message.UserId = userInfo.Result.Id;
                message.AddedDate = DateTime.Now;
                message.PlantId = id;
            }

            return message;
        }

        public MessageVm GetMessageById(int id)
        {
            var message = _messageRepo.GetMessageById(id);
            var messageVm = _mapper.Map<MessageVm>(message);
            
            var user = _userManager.FindByIdAsync(messageVm.UserId);
            messageVm.AccountName = user.Result.AccountName;
            messageVm.UserName = user.Result.UserName;


            return messageVm;
        }

        public MessageForListVm GetMessagesForPlant(int plantId, int pageSize, int? pageNo, MessageDisplay messageDisplay, string userName)
        {
     
            var messagesList = _messageRepo.GetMessagesForNewUserPlant(plantId).ProjectTo<NewUserPlantMessageVm>(_mapper.ConfigurationProvider).ToList();
            var messagesDetails = new List<MessageVm>();
            var receivedMessages = new List<MessageVm>();
            var sentMessages = new List<MessageVm>();   
            var messagesToShow = new List<MessageVm>();

            if (messagesList != null)
            {
                foreach (var item in messagesList)
                {
                    var message = _messageRepo.GetMessageById(item.MessageId);
                    var messageVm = _mapper.Map<MessageVm>(message);

                    var user = _userManager.FindByIdAsync(messageVm.UserId);
                    messageVm.UserName = user.Result.UserName;
                    messageVm.AccountName = user.Result.AccountName;
                    messageVm.PlantId= plantId;

                    messagesDetails.Add(messageVm);
                }

                var userInfo = _userManager.FindByNameAsync(userName);

                if (messageDisplay.Received == true)
                {
                    foreach (var item in messagesDetails)
                    {
                        if (item.UserId != userInfo.Result.Id)
                        {
                            receivedMessages.Add(item);
                        }
                    }
                    receivedMessages.OrderByDescending(x => x.AddedDate);
                    messagesToShow = receivedMessages.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                }
                else
                {
                    if (messageDisplay.Sent == true)
                    {
                        foreach (var item in messagesDetails)
                        {
                            if (item.UserId == userInfo.Result.Id)
                            {
                                sentMessages.Add(item);
                            }
                        }
                        sentMessages.OrderByDescending(x => x.AddedDate);
                        messagesToShow = sentMessages.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                    }
                    else
                    {
                        if (messageDisplay.ViewAll == true)
                        {
                            messagesDetails.OrderByDescending(x => x.AddedDate);
                            messagesToShow = messagesDetails.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                    }
                }
            }

            var messagesNewPlantsList = new MessageForListVm()
            {
                PageSize= pageSize,
                CurrentPage= pageNo,
                Message = messagesToShow,
                Count = messagesList.Count,

            };    

            return messagesNewPlantsList;
        }

        public int GetPlantIdForMessage(int id)
        {
            var plantId = _messageRepo.GetPlantId(id);
            return plantId;
        }

        public void SendNewPlantMessage(MessageVm message)
        {
            var sendMessage = _mapper.Map<Message>(message);
            var messageId = _messageRepo.AddMessage(sendMessage);

            if (message.messageIdisAnswer != 0)
            {
                var messageInfo = _messageRepo.GetMessageById(message.messageIdisAnswer);
                messageInfo.isAnswer = true;
                _messageRepo.UpdateMassageStatusIsAnswer(messageInfo);

                SendNewPlantUserMessage(messageId, message.PlantId, messageInfo.UserId);

                var messageAnswerVm = new MessageAnswerVm() { MessageId = message.messageIdisAnswer, MessageAnswerId = messageId };
                var messageAnswer = _mapper.Map<MessageAnswer>(messageAnswerVm);
                _messageRepo.AddMessageAnswer(messageAnswer);
            }

            if (messageId != 0 && message.messageIdisAnswer == 0)
            {
                var user = _userManager.FindByIdAsync(message.UserId);
                var userRole = _userManager.IsInRoleAsync(user.Result, "Admin");
                //warunek dla private user 
                if (userRole.Result == false)
                {
                    var admin = _userManager.GetUsersInRoleAsync("Admin");
                    var adminUser = admin.Result.FirstOrDefault(e => e.UserName.StartsWith("admin"));

                    SendNewPlantUserMessage(messageId, message.PlantId, adminUser.Id);
                  
                }
                else
                {
                    var userReceiver = _messageRepo.GetPlantOwnerId(message.PlantId);

                    SendNewPlantUserMessage(messageId, message.PlantId, userReceiver);
                  
                }

            }

        }
        public void SendNewPlantUserMessage(int messageId, int plantId, string userId)
        {
            var messageReceiver = new MessageReceiverVm() { MessageId = messageId, UserId = userId };

            _messageRepo.AddMessageReceiver(_mapper.Map<MessageReceiver>(messageReceiver));

            var newUserPlantMessage = new NewUserPlantMessageVm() { PlantId = plantId, MessageId = messageId };

            _messageRepo.AddNewUserPlantMessage(_mapper.Map<NewUserPlantMessage>(newUserPlantMessage));
        }
    }
}
