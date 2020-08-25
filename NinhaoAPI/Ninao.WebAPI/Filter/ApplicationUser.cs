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
    }
}