using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Entities;

namespace BusinessLayer.EntitiesServices.AuthServices
{
    public interface IAuthService
    {
        Task<(bool Success, string Token, string ErrorMessage)> LoginAsync(string username, string password);
        Task<IdentityResult> RegisterAsync(string username, string password);
    }
}
