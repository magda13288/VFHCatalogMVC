using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.ViewModels.Message;
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

        public MessageService(
            UserManager<ApplicationUser> userManager, 
            IMessageRepository messageRepository,
            IMapper mapper, 
            IPlantHelperService helperService)
        {
            _userManager = userManager;
            _messageRepo = messageRepository;
            _mapper = mapper;
            _helperService = helperService;
        }

        public MessageVm FillMessageProperties(
            int id,
            string user, 
            IndexPlantType index, 
            string ownerId)
        {
            var plantIdForMessage = _messageRepo.GetPlantId(id);
            var userInfo = _userManager.FindByNameAsync(user);
                      
            var message = new MessageVm
            {
                UserId = userInfo.Result.Id,
                AddedDate = DateTime.Now,
                PlantId = plantIdForMessage != 0 ? plantIdForMessage : id,
                isSeed = index.seeds,
                isSeedling = index.seedlings,
                isNewPlant = index.newPlant,
                MessageIdisAnswer = plantIdForMessage != 0 ? id : 0,
                OwnerId = plantIdForMessage == 0 ? ownerId : null 
            };
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

        private List<MessageVm> GetSentMessages(string userId)
        {
            var sentMessages = _messageRepo
                .GetSentMessages(userId)
                .ProjectTo<MessageVm>(_mapper.ConfigurationProvider)
                .ToList();

            foreach (var message in sentMessages)
            {
                var user = _userManager.FindByIdAsync(message.UserId).Result;
                message.UserName = user?.UserName;
                message.AccountName = user?.AccountName;
                message.MessageReceiver = GetMessageReceiverInfo(message.Id);
            }

            return sentMessages;
        }

        private List<MessageVm> GetReceivedMessages(string userId)
        {
            var messages = _messageRepo
                .GetReceivedMessages(userId)
                .ProjectTo<MessageReceiverVm>(_mapper.ConfigurationProvider)
                .ToList();

            var receivedMessages = new List<MessageVm>();

            foreach (var item in messages)
            {
                var messageInfo = _messageRepo.GetMessageById(item.MessageId);
                var messageVm = _mapper.Map<MessageVm>(messageInfo);
                var user = _userManager.FindByIdAsync(messageVm.UserId).Result;
                messageVm.UserName = user?.UserName;
                messageVm.AccountName = user?.AccountName;
                receivedMessages.Add(messageVm);
            }

            return receivedMessages;
        }
        public MessageForListVm GetMessages(
            int pageSize,
            int? pageNo, 
            MessageDisplay messageDisplay, 
            string userName)
        {
            var userInfo = _userManager.FindByNameAsync(userName).Result;

            if (userInfo == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var messagesToShow = new List<MessageVm>();
            int messagesCount = 0;

            if (messageDisplay.Sent == true)
            {
                var sentMessages = GetSentMessages(userInfo.Id);

                messagesCount = sentMessages.Count;
                messagesToShow = Paginate(sentMessages, pageSize, pageNo);
            }
            else
            {
                if(messageDisplay.Received == true) 
                {
                    var receivedMessages = GetReceivedMessages(userInfo.Id);
                    messagesCount = receivedMessages.Count;
                    messagesToShow = Paginate(receivedMessages, pageSize, pageNo);
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

        private List<PlantMessageVm> FilterMessagesByPlantType(List<PlantMessageVm> messagesList, IndexPlantType index)
        {
            if (index.seeds)
                return messagesList.Where(x => x.isSeed).ToList();

            if (index.seedlings)
                return messagesList.Where(x => x.isSeedling).ToList();

            if (index.newPlant)
                return messagesList.Where(x => x.isNewPlant).ToList();

            return messagesList;
        }

        private List<MessageVm> GetDetailedMessages(List<PlantMessageVm> messagesList, int plantId)
        {
            var messagesDetails = new List<MessageVm>();

            foreach (var item in messagesList)
            {
                var message = _messageRepo.GetMessageById(item.MessageId);
                var messageVm = _mapper.Map<MessageVm>(message);

                var user = _userManager.FindByIdAsync(messageVm.UserId).Result;
                messageVm.UserName = user?.UserName;
                messageVm.AccountName = user?.AccountName;
                messageVm.PlantId = plantId;

                messagesDetails.Add(messageVm);
            }

            return messagesDetails;
        }

        private List<MessageVm> FilterMessagesByDisplay(
            List<MessageVm> messagesDetails,
            string currentUserId,
            MessageDisplay messageDisplay,
            int pageSize,
            int? pageNo)
        {
            var filteredMessages = new List<MessageVm>();

            if (messageDisplay.Received == true)
            {

                filteredMessages  = messagesDetails
                    .Where(m=>m.UserId == currentUserId)
                    .OrderByDescending(m=>m.AddedDate)
                    .ToList();

            }
            else
            {
                if (messageDisplay.Sent == true)
                {
                    filteredMessages = messagesDetails
                         .Where(m => m.UserId == currentUserId)
                         .Select(m =>
                         {
                             m.MessageReceiver = GetMessageReceiverInfo(m.Id);
                             return m;
                         })
                         .OrderByDescending(m => m.AddedDate)
                         .ToList();
                }
                else
                {
                    if (messageDisplay.ViewAll == true)
                    {
                        filteredMessages = messagesDetails
                            .Select(m =>
                            {
                                if (m.UserId == currentUserId)
                                {
                                    m.MessageReceiver = GetMessageReceiverInfo(m.Id);
                                }
                                return m;
                            })
                            .OrderByDescending(m => m.AddedDate)
                            .ToList();
                    }
                }
            }
            return Paginate(filteredMessages, pageSize, pageNo);
        }

        public MessageForListVm GetMessagesForPlant(
            int plantId, 
            int pageSize,
            int? pageNo, 
            MessageDisplay messageDisplay, 
            IndexPlantType index, 
            string userName)
        {

            var messagesList = _messageRepo.GetMessagesForNewUserPlant(plantId).ProjectTo<PlantMessageVm>(_mapper.ConfigurationProvider).ToList();

            messagesList = FilterMessagesByPlantType(messagesList, index);
            var messagesToShow = new List<MessageVm>();

            if (messagesList != null)
            {
                var messagesDetails = GetDetailedMessages(messagesList, plantId);

                var userInfo = _userManager.FindByNameAsync(userName);

                if (userInfo == null)
                {
                    throw new InvalidOperationException("User not found.");
                }

                 messagesToShow = FilterMessagesByDisplay(messagesDetails, userInfo.Result.Id, messageDisplay, pageSize, pageNo);
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

        private MessageReceiverVm GetMessageReceiverInfo(int messageId)
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
        private void HandleAnswerMessage(MessageVm message, int messageId, IndexPlantType indexPlant)
        {
            var messageInfo = _messageRepo.GetMessageById(message.MessageIdisAnswer);
            messageInfo.isAnswer = true;
            _messageRepo.UpdateMassageStatusIsAnswer(messageInfo);

            SendPlantMessage(messageId, message.PlantId, messageInfo.UserId, indexPlant);

            var messageAnswerVm = new MessageAnswerVm
            {
                MessageId = message.MessageIdisAnswer,
                MessageAnswerId = messageId
            };

            var messageAnswer = _mapper.Map<MessageAnswer>(messageAnswerVm);
            _messageRepo.AddMessageAnswer(messageAnswer);
        }
        private void HandleNonSeedlingMessage(MessageVm message, int messageId, IndexPlantType indexPlant)
        {
            if (messageId == 0 && message.MessageIdisAnswer !=0) return;

            var user = _userManager.FindByIdAsync(message.UserId).Result;
            var userRole = _userManager.IsInRoleAsync(user, "Admin").Result;

            if (!userRole)
            {
                var adminUser = _userManager.GetUsersInRoleAsync("Admin")
                    .Result.FirstOrDefault(e => e.UserName.StartsWith("admin"));

                if (adminUser != null)
                {
                    SendPlantMessage(messageId, message.PlantId, adminUser.Id, indexPlant);
                }
            }
            else
            {
                var userReceiver = _messageRepo.GetPlantOwnerId(message.PlantId);
                SendPlantMessage(messageId, message.PlantId, userReceiver, indexPlant);
            }
        }
        public void SendMessage(MessageVm message)
        {
            var sendMessage = _mapper.Map<Message>(message);
            var messageId = _messageRepo.AddMessage(sendMessage);

            var indexPlant = new IndexPlantType() 
            {   seeds = message.isSeed, 
                seedlings = message.isSeedling, 
                newPlant = message.isNewPlant 
            };

            if (message.MessageIdisAnswer != 0)
            {
                HandleAnswerMessage(message, messageId, indexPlant);
                return;
            }
            
            if(!message.isSeed && !message.isSeedling)
            {
                HandleNonSeedlingMessage(message, messageId, indexPlant);
                return;
            }
            else
            {
                SendPlantMessage(messageId, message.PlantId, message.OwnerId, indexPlant);
            }

        }
        public void SendPlantMessage(int messageId, int plantId, string userId, IndexPlantType index)
        {
            var messageReceiver = new MessageReceiverVm() 
            {   MessageId = messageId,
                UserId = userId 
            };

            _messageRepo.AddMessageReceiver(_mapper.Map<MessageReceiver>(messageReceiver));

            var plantMessage = new PlantMessageVm() 
            {   PlantId = plantId, 
                MessageId = messageId,
                isSeed = index.seeds, 
                isSeedling = index.seedlings, 
                isNewPlant = index.newPlant 
            };

            _messageRepo.AddNewUserPlantMessage(_mapper.Map<PlantMessage>(plantMessage));
        }

        private List<T> Paginate<T>(IEnumerable<T> items, int pageSize, int? pageNo)
        {
            if (!pageNo.HasValue || pageNo <= 0)
            {
                pageNo = 1; // default first page
            }

            return items.Skip(pageSize * (pageNo.Value - 1)).Take(pageSize).ToList();
        }
    }
}
