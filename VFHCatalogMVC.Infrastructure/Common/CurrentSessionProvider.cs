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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentSessionProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var userId = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            if (userId is null)
            {
                return;
            }

            _currentUserId = userId;
        }
        public string? GetUserId() => _currentUserId;
    }
}