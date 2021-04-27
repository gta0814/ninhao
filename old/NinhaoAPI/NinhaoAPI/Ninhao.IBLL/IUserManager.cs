using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.IBLL
{
    public interface IUserManager
    {
        Task Register(string email, string password, string contact, int phone);

        Task<bool> Login(string email, string password);

        Task ChangePassword(string email, string oldPwd, string newPwd);

        Task ChangeUserInformation(string email, string imagePath, string contact, int phone);

        Task<DTO.UserInformationDTO> GetUserByEmail(string email);
    }
}