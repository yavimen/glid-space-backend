using Main.API.DtoModels;
using Main.API.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        //Task<User> GetUserById(int id);

        Task<string> AddUser(UserRegistrationDto user);

        Task DeleteUserByEmail(string email);

        //Task UpdateUser(string id);

        Task<bool> IsUserEmailRegistratedExist(string email);

        Task ConfirmEmail(string token, string email);
    }
}
