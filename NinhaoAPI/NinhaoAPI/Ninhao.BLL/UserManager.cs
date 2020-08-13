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
    public class UserManager
    {

        public async Task ChangePassword(string email, string oldPwd, string newPwd)
        {
            using (var userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAll().AnyAsync(m => m.Email == email && m.Password == oldPwd))
                {
                    var user = await userSvc.GetAll().FirstAsync(m => m.Email == email);
                    user.Password = newPwd;
                    await userSvc.EditAsync(user);
                }
            }
        }

        public async Task ChangeUserInformation(string email, string imagePath, string contact, int phone)
        {
            using (var userSvc = new DAL.UserService())
            {
                var user = await userSvc.GetAll().FirstAsync(m => m.Email == email);
                user.ImagePath = imagePath;
                user.Contact = contact;
                user.Phone = phone;
                await userSvc.EditAsync(user);
            };
        }

        public async Task<UserInformationDTO> GetUserByEmail(string email)
        {
            using (var userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAll().AnyAsync(m => m.Email == email))
                {
                    var resault = await userSvc.GetAll().Where(m => m.Email == email).Select(m => new DTO.UserInformationDTO()
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

        public static bool Login(string email, string password)
        {
            using (var userSvc = new DAL.UserService())
            {
                var user = userSvc.GetAll(m => m.Email == email && m.Password == password).FirstOrDefaultAsync();
                user.Wait();
                if (user.Result == null)
                {
                    
                    return false;
                }
                else
                {
                    
                    return true;
                }
            }
        }

        public static async Task Register(string email, string password, string contact, int phone, Guid carid)
        {
            using (var userSvc = new DAL.UserService())
            {
                await userSvc.CreateAsync(new User()
                {
                    Email = email,
                    Password = password,
                    Contact = contact,
                    Phone = phone,
                    CarId = carid
                });
            }
        }
    }
}