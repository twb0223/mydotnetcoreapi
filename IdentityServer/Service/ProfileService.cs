using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using System.Security.Claims;

namespace IdentityServer.Service
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string subject = context.Subject.Claims.ToList().Find(s => s.Type == "sub").Value;
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}
