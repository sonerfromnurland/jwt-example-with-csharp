using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Repository.IRepository;

namespace MyWebApi.Repository;
public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _db;
    private readonly AppSettings _appSettings;

    public TeamRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
    {
        _db = db;
        _appSettings = appSettings.Value;
    }
    
    public ICollection<Team> GetTeamMembers()
    {
        return _db.Teams.ToList();
    }
    
    
    public User Authenticate(string username, string password)
    {
        var user = _db.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);

        //user not found
        if (user == null)
        {
            return null;
        }

        //if user was found generate JWT Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);
        user.Password = "";
        return user;
    }
}