using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WasteManagementSystem.API.Controllers;

public class BaseController : ControllerBase
{
    private int? _loggedInUserId { get; set; }
    private string? _loggedInUserRole { get; set; }
    protected int LoggedInUserId
    {
        get
        {
            if (_loggedInUserId != null)
            {
                return _loggedInUserId.Value;
            }
            var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _loggedInUserId = loggedInUserId;
            return loggedInUserId;
        }   
    }
    protected string LoggedInUserRole
    {
        get
        {
            if( _loggedInUserRole != null)
            {
                return _loggedInUserRole;
            }
            var loggedInUserRole = User.FindFirstValue(ClaimTypes.Role);
            _loggedInUserRole = loggedInUserRole;
            return loggedInUserRole;
        }
    }
}

