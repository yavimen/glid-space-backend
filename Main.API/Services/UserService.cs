using AutoMapper;
using Main.API.DtoModels;
using Main.API.Persistance;
using Main.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Main.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<string> AddUser(UserRegistrationDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            
            await _userManager.CreateAsync(user, newUser.Password);


            await _userManager.AddToRoleAsync(user, "Visitor");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task ConfirmEmail(string token, string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<bool> IsUserEmailRegistratedExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user == null ? false : true;
        }

        public async Task DeleteUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);
        }
    }
}
