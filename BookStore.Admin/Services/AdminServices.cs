using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Admin.Services;

public class AdminServices : IAdminServices
{
    private readonly AdminContext _db;
    private readonly IConfiguration _config;

    public AdminServices(AdminContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }
    public AdminEntity Register(AdminEntity entity)
    {
        entity.AdminEmail = entity.AdminEmail.ToLower();
        _db.AdminTable.Add(entity);
        _db.SaveChanges();
        return entity;
    }
    public string Login(string email, string password)
    {
        string token = string.Empty;
        AdminEntity admin = _db.AdminTable.FirstOrDefault(x => x.AdminEmail == email && x.AdminPassword == password);

        if (admin != null)
        {
            token = GenerateToken(admin.AdminId, admin.AdminEmail);
        }
        return token;
    }

    private string GenerateToken(int userId, string email)
    {
        byte[] key = Encoding.ASCII.GetBytes(_config["JWT-Key"]);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Sid,userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


}
