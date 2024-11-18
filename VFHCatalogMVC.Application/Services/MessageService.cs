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
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
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
        private readonly IPlantHelperService _helperService;

        public MessageService(UserManager<ApplicationUser> userManager, IMessageRepository messageRepository,IMapper mapper, IPlantHelperService helperService)
        {
            _userManager = userManager;
            _messageRepo = messageRepository;
            _mapper = mapper;
            _helperService = helperService;
        }

        public MessageVm FillMessageProperties(int id, string user, IndexPlantType index, string ownerId)
        {
            var plantIdForMessage = _messageRepo.GetPlantId(id);
            var userInfo = _userManager.FindByNameAsync(user);
            var message = new MessageVm();

            if (plantIdForMessage != 0)
            {
                message.UserId = userInfo.Result.Id;
                message.AddedDate = DateTime.Now;
                message.PlantId = plantIdForMessage;
                message.MessageIdisAnswer = id;
                message.isSeed = index.seeds;
                message.isSeedling = index.seedlings;
                message.isNewPlant = index.newPlant;

            }
            else
            {
                message.UserId = userInfo.Result.Id;
                message.AddedDate = DateTime.Now;
                message.PlantId = id;
                message.isSeed = index.seeds;
                message.isSeedling = index.seedlings;
                message.isNewPlant = index.newPlant;
                message.OwnerId = ownerId;
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

        public MessageForListVm GetMessages(int pageSize, int? pageNo, MessageDisplay messageDisplay, string userName)
        {
            var userInfo = _userManager.FindByNameAsync(userName);
          
            var messages = new List<MessageReceiverVm>();
            var sentMessages = new List<MessageVm>();
            var receivedMessages = new List<MessageVm>();
            var messagesToShow = new List<MessageVm>();
            int messagesCount = 0;

            if (messageDisplay.Sent == true)
            {
                sentMessages = _messageRepo.GetSentMessages(userInfo.Result.Id).ProjectTo<MessageVm>(_mapper.ConfigurationProvider).ToList();

                foreach (var item in sentMessages)
                {
                    var user = _userManager.FindByIdAsync(item.UserId);
                    item.UserName = user.Result.UserName;
                    item.AccountName = user.Result.AccountName;

                    var messageReceiverVm = GetMessageReceiverInfo(item.Id);

                    item.MessageReceiver = messageReceiverVm;
                }
                messagesToShow = sentMessages.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                messagesCount = sentMessages.Count;
            }
            else
            {
                if(messageDisplay.Received == true) 
                {
                    messages = _messageRepo.GetReceivedMessages(userInfo.Result.Id).ProjectTo<MessageReceiverVm>(_mapper.ConfigurationProvider).ToList();

                    foreach (var item in messages)
                    {
                       var messageInfo = _messageRepo.GetMessageById(item.MessageId);
                       var messageVm = _mapper.Map<MessageVm>(messageInfo);
                       var user = _userManager.FindByIdAsync(messageVm.UserId);
                       messageVm.UserName = user.Result.UserName;
                       messageVm.AccountName = user.Result.AccountName;
                       
                       receivedMessages.Add(messageVm);
                       
                    }

                    messagesCount = receivedMessages.Count;
                    messagesToShow = receivedMessages.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                }
            }

            var messagesNewPlantsList = new MessageForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                Message = messagesToShow,
                Count = messagesCount

            };

            return messagesNewPlantsList;

        }

        public MessageForListVm GetMessagesForPlant(int plantId, int pageSize, int? pageNo, MessageDisplay messageDisplay, IndexPlantType index, string userName)
        {

            var messagesList = _messageRepo.GetMessagesForNewUserPlant(plantId).ProjectTo<PlantMessageVm>(_mapper.ConfigurationProvider).ToList();

            if (index.seeds == true)
            {
                messagesList = messagesList.Where(x => x.isSeed == true).ToList();
            }
            else
            {
                if (index.seedlings == true)
                {
                    messagesList = messagesList.Where(x => x.isSeedling == true).ToList();
                }
                else
                {
                    if (index.newPlant)
                    {
                        messagesList = messagesList.Where(x => x.isNewPlant == true).ToList();
                    }
                }
            }

            var messagesDetails = new List<MessageVm>();
            var receivedMessages = new List<MessageVm>();
            var sentMessages = new List<MessageVm>();   
            var messagesToShow = new List<MessageVm>();
            var allMessages = new List<MessageVm>();

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
                                var messageReceiverVm = GetMessageReceiverInfo(item.Id);

                                item.MessageReceiver = messageReceiverVm;
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
                            foreach (var item in messagesDetails)
                            {
                                if (item.UserId == userInfo.Result.Id)
                                {
                                    var messageReceiverVm = GetMessageReceiverInfo(item.Id);

                                    item.MessageReceiver = messageReceiverVm;
                                    allMessages.Add(item);
                                }
                                else
                                {
                                    allMessages.Add(item);
                                }
                            }

                            allMessages.OrderByDescending(x => x.AddedDate);
                            messagesToShow = allMessages.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
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

        public MessageReceiverVm GetMessageReceiverInfo(int messageId)
        {
            var messageReveiver = _messageRepo.GetMessageReceiverByMessageId(messageId);
            var messageReceiverVm = _mapper.Map<MessageReceiverVm>(messageReveiver);
            var receiverUserInfo = _userManager.FindByIdAsync(messageReceiverVm.UserId);
            messageReceiverVm.UserName = receiverUserInfo.Result.UserName;
            messageReceiverVm.AccountName = receiverUserInfo.Result.AccountName;

            return messageReceiverVm;
        }
        public int GetPlantIdForMessage(int id)
        {
            var plantId = _messageRepo.GetPlantId(id);
            return plantId;
        }

        public void SendMessage(MessageVm message)
        {
            var sendMessage = _mapper.Map<Message>(message);
            var messageId = _messageRepo.AddMessage(sendMessage);
            var indexPlant = new IndexPlantType() { seeds = message.isSeed, seedlings = message.isSeedling, newPlant = message.isNewPlant };

            if (message.MessageIdisAnswer != 0)
            {
                var messageInfo = _messageRepo.GetMessageById(message.MessageIdisAnswer);
                messageInfo.isAnswer = true;
                _messageRepo.UpdateMassageStatusIsAnswer(messageInfo);           

                SendPlantMessage(messageId, message.PlantId, messageInfo.UserId, indexPlant);

                var messageAnswerVm = new MessageAnswerVm() { MessageId = message.MessageIdisAnswer, MessageAnswerId = messageId };
                var messageAnswer = _mapper.Map<MessageAnswer>(messageAnswerVm);
                _messageRepo.AddMessageAnswer(messageAnswer);
            }
            
            if(message.isSeed == false && message.isSeedling == false)
            {
                if (messageId != 0 && message.MessageIdisAnswer == 0)
                {
                    var user = _userManager.FindByIdAsync(message.UserId);
                    var userRole = _userManager.IsInRoleAsync(user.Result, "Admin");
                    //warunek dla private user 
                    if (userRole.Result == false)
                    {
                        var admin = _userManager.GetUsersInRoleAsync("Admin");
                        var adminUser = admin.Result.FirstOrDefault(e => e.UserName.StartsWith("admin"));

                        SendPlantMessage(messageId, message.PlantId, adminUser.Id, indexPlant);

                    }
                    else
                    {
                        var userReceiver = _messageRepo.GetPlantOwnerId(message.PlantId);

                        SendPlantMessage(messageId, message.PlantId, userReceiver, indexPlant);

                    }
                }           
            }
            else
            {
                SendPlantMessage(messageId, message.PlantId, message.OwnerId, indexPlant);
            }

        }
        public void SendPlantMessage(int messageId, int plantId, string userId, IndexPlantType index)
        {
            var messageReceiver = new MessageReceiverVm() { MessageId = messageId, UserId = userId };

            _messageRepo.AddMessageReceiver(_mapper.Map<MessageReceiver>(messageReceiver));

            var plantMessage = new PlantMessageVm() { PlantId = plantId, MessageId = messageId ,isSeed = index.seeds, isSeedling = index.seedlings, isNewPlant = index.newPlant };

            _messageRepo.AddNewUserPlantMessage(_mapper.Map<PlantMessage>(plantMessage));
        }
    }
}
