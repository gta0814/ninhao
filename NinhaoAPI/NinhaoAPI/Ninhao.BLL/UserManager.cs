using Ninhao.DTO;
using Ninhao.IBLL;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ninhao.BLL
{
    public class UserManager : IUserManager
    {

        public async Task ChangePassword(string email, string oldPwd, string newPwd)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(m => m.Email == email && m.Password == oldPwd))
                {
                    var user = await userSvc.GetAllAsync().FirstAsync(m => m.Email == email);
                    user.Password = newPwd;
                    await userSvc.EditAsync(user);
                }
            }
        }

        public async Task ChangeUserInformation(string email, string imagePath, string contact, int phone)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                var user = await userSvc.GetAllAsync().FirstAsync(m => m.Email == email);
                user.ImagePath = imagePath;
                user.Contact = contact;
                user.Phone = phone;
                await userSvc.EditAsync(user);
            };
        }

        public async Task<UserInformationDTO> GetUserByEmail(string email)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(m => m.Email == email))
                {
                    var resault = await userSvc.GetAllAsync().Where(m => m.Email == email).Select(m => new DTO.UserInformationDTO()
                    {
                        Id = m.Id,
                        Email = m.Email,
                        ImagePath = m.ImagePath,
                        Contact = m.Contact,
                        Phone = m.Phone
                    }).FirstAsync();
                    return resault;
                }
                else
                {
                    throw new ArgumentException(message: "Email not exist");
                }
            }
        }

        public async Task<bool> Login(string email, string password)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                return await userSvc.GetAllAsync().AnyAsync(m => m.Email == email && m.Password == password);
            }
        }

        public async Task Register(string email, string password, string contact, int phone)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                await userSvc.CreateAsync(new User()
                {
                    Email = email,
                    Password = password,
                    Contact = contact,
                    Phone = phone
                });
            }
        }
    }
}