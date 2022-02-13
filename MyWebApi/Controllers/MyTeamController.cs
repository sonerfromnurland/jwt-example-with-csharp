using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Repository.IRepository;

namespace MyWebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MyTeamController : ControllerBase
{
    private ITeamRepository _iteamRepo;

    public MyTeamController(ITeamRepository iteam) => _iteamRepo = iteam;
    
    [HttpGet]
    [Authorize]
    public IActionResult GetTeamMembers()
    {
        var teamMembers = _iteamRepo.GetTeamMembers();
        return Ok(teamMembers);
    }
    
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(string userName, string password)
    {
        var user = _iteamRepo.Authenticate(userName, password);
        if (user == null)
        {
            return BadRequest(new { message = "Username or password is incorrect" });
        }
        return Ok(user);
    }
}
