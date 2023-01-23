using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.Message
{
    public class MessageReceiverVm:IMapFrom<VFHCatalogMVC.Domain.Model.MessageReceiver>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MessageId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MessageReceiverVm, VFHCatalogMVC.Domain.Model.MessageReceiver>().ReverseMap();
        }
    }
}
