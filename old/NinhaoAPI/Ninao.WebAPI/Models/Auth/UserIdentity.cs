using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Ninao.WebAPI.Models.Auth
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity(string name, Guid id)
        {
            Name = name;
            Id = id;
        }
        public string Name { get; }
        public Guid Id { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
    }
}