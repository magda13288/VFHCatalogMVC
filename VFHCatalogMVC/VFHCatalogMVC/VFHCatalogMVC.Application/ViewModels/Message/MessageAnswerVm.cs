using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.Message
{
    public class MessageAnswerVm : IMapFrom<VFHCatalogMVC.Domain.Model.MessageAnswer>
    {
        public int MessageId { get; set; }
        public int MessageAnswerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MessageAnswerVm, VFHCatalogMVC.Domain.Model.MessageAnswer>().ReverseMap();
        }
    }
}
