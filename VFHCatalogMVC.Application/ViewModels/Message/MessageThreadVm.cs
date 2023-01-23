using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.Message
{
    public class MessageThreadVm:IMapFrom<VFHCatalogMVC.Domain.Model.MessageThread>
    {
        public int MessageId { get; set; }
        public int ReceiverMessageId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MessageThreadVm, VFHCatalogMVC.Domain.Model.MessageThread>().ReverseMap();
        }
    }
}
