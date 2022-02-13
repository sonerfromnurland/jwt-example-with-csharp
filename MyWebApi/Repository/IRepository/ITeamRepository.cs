using MyWebApi.Models;

namespace MyWebApi.Repository.IRepository;

public interface ITeamRepository
{
    ICollection<Team> GetTeamMembers();
    User Authenticate(string username, string password);
}