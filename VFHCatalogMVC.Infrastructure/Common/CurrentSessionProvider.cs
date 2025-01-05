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
        public CurrentSessionProvider(IHttpContextAccessor accessor)
        {
            var userId = accessor.HttpContext?.User.FindFirstValue("UserId");
            if (userId is null)
            {
                return;
            }

            _currentUserId = userId;
        }
        public string? GetUserId() => _currentUserId;
    }
}