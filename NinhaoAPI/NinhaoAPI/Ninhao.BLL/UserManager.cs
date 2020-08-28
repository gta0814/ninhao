using Ninhao.DAL;
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
                user.SocialMediaAccount = contact;
                user.Phone = phone;
                await userSvc.EditAsync(user);
            };
        }

        public async Task<UserInformationDTO> GetUserByEmail(string email)
        {
            using (var userSvc = new UserService())
            {
                if (await userSvc.GetAll().AnyAsync(m => m.Email == email))
                {
                    var resault = await userSvc.GetAll()
                        .Where(m => m.Email == email)
                        .Include(m=>m.Car)
                        .Select(m => new DTO.UserInformationDTO()
                    {
                        Id = m.Id,
                        Email = m.Email,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        NickName = m.NickName,
                        Age = m.age,
                        Gender = m.Gender,
                        ImagePath = m.ImagePath,
                        Contact = m.SocialMediaAccount,
                        Phone = m.Phone,
                        Address = m.Address,
                        Make = m.Car.Make,
                        CarModel = m.Car.CarModel,
                        CarType = m.Car.Type,
                        Color = m.Car.Color
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

        public static async Task Register(string email, string password, string firstname, string lastname, string nickname, int? age, string gender, string imagepath, string contact, long phone, string address, string plate, string make, string carModel, string type, string color)
        {
            Guid carid = Guid.NewGuid();
            if (make != null)
            {
                using (var carSvc = new CarService())
                {
                    await carSvc.CreateAsync(new Car()
                    {
                        Id = carid,
                        Make = make,
                        CarModel = carModel,
                        Type = type,
                        Color = color
                    });
                }
            }

            using (var userSvc = new DAL.UserService())
            {
                await userSvc.CreateAsync(new User()
                {
                    Email = email,
                    Password = password,
                    FirstName = firstname,
                    LastName = lastname,
                    NickName = nickname,
                    age = age,
                    Gender = gender,
                    ImagePath = imagepath,
                    SocialMediaAccount = contact,
                    Phone = phone,
                    Address = address,
                    CarPlate = plate,
                    CarId = make == null ? null : (Guid?)carid
                });
            }
        }
    }
}