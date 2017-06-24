using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace World.Controllers
{
  [Route("api/[controller]")]
  [Authorize]
  public class IdentityController : Controller
  {
    // GET api/identity
    [HttpGet]
    public IActionResult Get()
    {
      return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }
  }
}
