using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TechMed.Application.InputModels;
using TechMed.Application.Services.Interfaces;
using TechMed.Application.ViewModels;
using TechMed.Infrastructure.Auth;

namespace TechMed.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthService _authService;

        public LoginService(IAuthService authService)
        {
            _authService = authService;
        }
        public LoginViewModel? Authenticate(LoginInputModel login)
        {
            var passHashed = _authService.ComputeSha256Hash(login.Password);
            if (login.Username == "admin" && passHashed == _authService.ComputeSha256Hash("admin"))
            {
                var token = _authService.GenerateJwtToken(login.Username, "admin");
                return new LoginViewModel
                {
                    Username = login.Username,
                    Token = token,
                };
            }
            return null;
        }
    }
}