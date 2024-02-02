using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public int UnreadReceived{ get; set; }
        public int UnreadSent { get; set; }

    }
}
