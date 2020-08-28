using System;
using System.Security.Principal;

namespace Ninao.WebAPI.Filter
{
    internal class ApplicationUser : IPrincipal
    {
        private string loginName;
        private Guid userId;

        public ApplicationUser(string loginName, Guid userId)
        {
            this.loginName = loginName;
            this.userId = userId;
        }

        public IIdentity Identity => throw new NotImplementedException();

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}