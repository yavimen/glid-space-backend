using FluentValidation;
using Main.API.DtoModels;
using Main.API.EmailService;
using Main.API.Extensions;
using Main.API.Persistance;
using Main.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IValidator<UserRegistrationDto> _validator;

        public AccountController(IUserService userService, IEmailSender emailSender, 
            IValidator<UserRegistrationDto> validator)
        {
            _userService = userService;
            _emailSender = emailSender;
            _validator = validator;
        }
        [HttpGet]
        public async Task<ICollection<User>> GetAllUsers()
        {
            return _userService.GetAllUsers().ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDto newUser) 
        {
            var validationResult = _validator.Validate(newUser);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToStringErrorMessages());

            if (_userService.IsUserEmailRegistratedExist(newUser.Email).Result)
                return BadRequest("Account with this email already exist");

            var token = _userService.AddUser(newUser).Result;

            var confirmationLink = Url
                .Action(nameof(ConfirmEmail), "Account", 
                new { token, email = newUser.Email }, Request.Scheme);

            var message = new Message(new string[] { newUser.Email }, 
                "Email confirmation link", confirmationLink);
            await _emailSender.SendEmailWithHtmlContentAsync(message);

            return StatusCode(StatusCodes.Status202Accepted, "Confirm your email.");
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUserByEmail(string email) 
        {
            if (!_userService.IsUserEmailRegistratedExist(email).Result)
                return BadRequest();

            await _userService.DeleteUserByEmail(email);

            return Ok();
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            if (!_userService.IsUserEmailRegistratedExist(email).Result)
                return BadRequest("User email not registrated.");

            await _userService.ConfirmEmail(token, email);

            var html = "<h1 style=\"text-align: center; font-family: system-ui;\">" +
                "Email confirmed." +
                "</h1>\r\n   " +
                "<h2 style=\"text-align: center; font-family: system-ui;\">" +
                "Welcome to Glid Family ;)" +
                "</h2>";

            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            }; ;
        }
    }
}
