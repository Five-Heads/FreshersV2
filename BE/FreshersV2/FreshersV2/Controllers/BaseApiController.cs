using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected T ExtractClaim<T>(string claim)
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var userClaim = user.Claims.FirstOrDefault(c => c.Type == claim);
                if (userClaim != null)
                {
                    return (T)Convert.ChangeType(userClaim.Value, typeof(T));
                }
            }

            return default;
        }

        protected bool IsInRole(string role)
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                return user.IsInRole(role);
            }
            return false;
        }
    }
}
