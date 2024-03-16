using System;
using Microsoft.AspNetCore.Http.Extensions;

namespace Application.Services.Core
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApplicationContext(
            IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Uri GetRawUrl() => new Uri(this.httpContextAccessor.HttpContext.Request.GetEncodedUrl());
    }
}

