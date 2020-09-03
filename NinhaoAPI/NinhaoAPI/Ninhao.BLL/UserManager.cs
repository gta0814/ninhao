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

        public static async Task ChangePassword(string email, string oldPwd, string newPwd)
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

        public static async Task ChangeUserInformation(UserInformationDTO userinfo)
        {
            using (var userSvc = new DAL.UserService())
            {
                var user = await userSvc.GetAll().FirstAsync(m => m.Email == userinfo.Email);
                user.FirstName = userinfo.FirstName;
                user.LastName = userinfo.LastName;
                user.NickName = userinfo.NickName;
                user.age = userinfo.Age;
                user.Gender = userinfo.Gender;
                user.ImagePath = userinfo.ImagePath;
                user.SocialMediaAccount = userinfo.Contact;
                user.Phone = userinfo.Phone;
                user.Address = userinfo.Address;
                user.CarPlate = userinfo.CarPlate;
                await userSvc.EditAsync(user);
            };
        }

        public static UserInformationDTO GetUserByEmail(string email)
        {
            using (var userSvc = new UserService())
            {
                if (userSvc.GetAll().Any(m => m.Email == email))
                {
                    var resault = userSvc.GetAll()
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
                        CarPlate = m.CarPlate,
                        Make = m.Car.Make,
                        CarModel = m.Car.CarModel,
                        CarType = m.Car.Type,
                        Color = m.Car.Color
                    }).First();
                    return resault;
                }
                else
                {
                    throw new ArgumentException(message: "Email not exist");
                }
            }
        }

        //TODO: Get driver car to store into Cookies in controller login method
        //public static DriverCarDTO GetDriverCar(Guid id)
        //{

        //}
        public static bool Login(string email, string password, out Guid userid)
        {
            using (var userSvc = new DAL.UserService())
            {
                var user = userSvc.GetAll(m => m.Email == email && m.Password == password).FirstOrDefaultAsync();
                user.Wait();
                if (user.Result == null)
                {
                    userid = new Guid();
                    return false;
                }
                else
                {
                    userid = user.Result.Id;
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
        public static async Task CreateAnonymousUser(UserInformationDTO user)
        {
            Guid carid = Guid.NewGuid();
            if (user.Make != null)
            {
                using (var carSvc = new CarService())
                {
                    await carSvc.CreateAsync(new Car()
                    {
                        Id = carid,
                        Make = user.Make,
                        CarModel = user.CarModel,
                        Type = user.CarType,
                        Color = user.Color
                    });
                }
            }
            using (var userSvc = new DAL.UserService())
            {
                await userSvc.CreateAsync(new User()
                {
                    Id = user.Id,
                    Email = "default@default.com",
                    Password = "123456",
                    FirstName = "",
                    LastName = "",
                    NickName = user.NickName,
                    age = null,
                    Gender = user.Gender,
                    ImagePath = "",
                    SocialMediaAccount = user.Contact,
                    Phone = user.Phone,
                    Address = "",
                    CarPlate = user.CarPlate,
                    CarId = user.Make == null ? null : (Guid?)carid
                });
            }
        }
    }
}