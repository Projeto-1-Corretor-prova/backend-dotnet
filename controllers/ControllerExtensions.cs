using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers;

public static class ControllerExtensions
{
    public static bool TryGetUserIdFromToken(this ControllerBase controller, out int userId)
    {
        userId = 0;
        
        var userIdClaim = controller.User.FindFirst(ClaimTypes.NameIdentifier) 
                          ?? controller.User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId))
        {
            return false;
        }

        return true;
    }
}
