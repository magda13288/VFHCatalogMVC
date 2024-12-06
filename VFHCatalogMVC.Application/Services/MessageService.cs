using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                      
            var message = new MessageVm
            {
                UserId = userInfo.Result.Id,
                AddedDate = DateTime.Now,
                PlantId = plantIdForMessage != 0 ? plantIdForMessage : id,
                isSeed = index.seeds,
                isSeedling = index.seedlings,
                isNewPlant = index.newPlant,
                MessageIdisAnswer = plantIdForMessage != 0 ? id : (int?)null,
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
        private List<T> Paginate<T>(IEnumerable<T> items, int pageSize, int? pageNo)
        {
            if (!pageNo.HasValue || pageNo <= 0)
            {
                pageNo = 1; // default first page
            }

            return items.Skip(pageSize * (pageNo.Value - 1)).Take(pageSize).ToList();
        }
        public MessageForListVm GetMessages(int pageSize, int? pageNo, MessageDisplay messageDisplay, string userName)
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
        public MessageForListVm GetMessagesForPlant(int plantId, int pageSize, int? pageNo, MessageDisplay messageDisplay, IndexPlantType index, string userName)
        {

            var messagesList = _messageRepo.GetMessagesForNewUserPlant(plantId).ProjectTo<PlantMessageVm>(_mapper.ConfigurationProvider).ToList();

            messagesList = FilterMessagesByPlantType(messagesList, index);

            var receivedMessages = new List<MessageVm>();
            var sentMessages = new List<MessageVm>();   
            var messagesToShow = new List<MessageVm>();
            var allMessages = new List<MessageVm>();

            if (messagesList != null)
            {
                var messagesDetails = GetDetailedMessages(messagesList, plantId);

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
