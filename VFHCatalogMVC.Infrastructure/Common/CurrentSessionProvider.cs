using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Infrastructure.Common
{
    public class CurrentSessionProvider : ICurrentSessionProvider
    {
        private readonly string? _currentUserId;
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentSessionProvider(IHttpContextAccessor accessor)
        {
            _contextAccessor = accessor;
            var userId = accessor.HttpContext?.User.FindFirstValue("UserName");
            if (userId is null)
            {
                return;
            }

            _currentUserId = userId;
        }
        public string? GetUserId() => _currentUserId;
    }
}