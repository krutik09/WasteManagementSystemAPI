using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Auth;
public class LoginService : ILoginService
{
    public readonly byte[] secretKey = Encoding.ASCII.GetBytes("....very...very..secret.key..unknown..");
    private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
    private static readonly int SaltSize = 16;
    private static readonly int HashSize = 20;
    private static readonly int Iterations = 10000;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserType> _userTypeRepository;
    private readonly IMapper _mapper;
    public LoginService(IRepository<User> userRepository, IRepository<UserType> userTypeRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userTypeRepository = userTypeRepository;
    }
    public async Task<string?> Auth(LoginDto login)
    {
        var user = _mapper.Map<User>(new UserDto
        {
            Email = login.Email,
        });
        var actual_user = await _userRepository.GetEntity(user,"Email");
        if (actual_user == null) { 
            return null;
        }
        if (actual_user.Password!= login.Password)
        {
            return null;
        }
        return this.CreateJWT(actual_user);
    }
    public string HashPassword(string? password)
    {
        byte[] salt;
        salt = new byte[SaltSize];
        rng.GetBytes(salt);
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        var key = new Rfc2898DeriveBytes(password, salt, Iterations);
        var hash = key.GetBytes(HashSize);
        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
        var base64Hash = Convert.ToBase64String(hashBytes);
        return base64Hash;
    }

    public bool VerifyPassword(string? password, string base64Hash)
    {
        var hashBytes = Convert.FromBase64String(base64Hash);
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        var key = new Rfc2898DeriveBytes(password, salt, Iterations);
        byte[] hash = key.GetBytes(HashSize);
        for (var i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
                return false;
        }
        return true;
    }
    public string CreateJWT(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity
            (
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role,user.UserTypeId.ToString())

                }
            ),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.secretKey), SecurityAlgorithms.HmacSha256Signature)
           
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public async Task<bool> isEmailExist(string email) {
        return true;
    }
}

