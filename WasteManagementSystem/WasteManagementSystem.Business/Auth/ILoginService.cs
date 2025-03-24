using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Data.Models;

namespace WasteManagementSystem.Business.Auth;
    public interface ILoginService
    {
        Task<string?> Auth(LoginDto login);
        string CreateJWT(User user);
        bool VerifyPassword(string? password, string base64Hash);
        string HashPassword(string? password);

    }

