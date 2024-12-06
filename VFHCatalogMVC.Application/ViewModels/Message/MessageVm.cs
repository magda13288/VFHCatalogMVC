using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.Message
{
    public class MessageVm:IMapFrom<VFHCatalogMVC.Domain.Model.Message>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MessageContent { get; set; }
        public DateTime AddedDate { get; set; }
        public bool isAnswer { get; set; }
        [NotMapped]
        public int MessageIdisAnswer { get; set; }
        [NotMapped]
        public int PlantId { get; set; }
        [NotMapped]
        public MessageReceiverVm MessageReceiver { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [NotMapped]
        public bool isSeed { get; set; }
        [NotMapped]
        public bool isSeedling { get; set; }
        [NotMapped]
        public bool isNewPlant { get; set; }
        [NotMapped]
        public string OwnerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Message, MessageVm>().ReverseMap();
        }

        public class MessageValidation : AbstractValidator<MessageVm>
        {
            public MessageValidation()
            {
                RuleFor(e => e.MessageContent).NotEmpty().WithMessage("Wiadomość nie może być pusta");
            }
        }
    }
}
