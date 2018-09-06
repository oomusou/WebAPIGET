using System.Collections.Generic;
using System.Linq;
using Exists.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistsController : ControllerBase
    {
        private readonly List<Member> _members
            = new List<Member> {new Member {Username = "Sam", Password = "1234"}};

        // GET api/exists/:username
        [HttpGet("{username}")]
        public ActionResult<Response> Get(string username)
        {
            return _members
                .Where(IsMember)
                .DefaultIfEmpty(DefaultMember())
                .Select(Result)
                .FirstOrDefault();

            bool IsMember(Member x)
                => x.Username == username;

            Member DefaultMember()
                => new Member {Username = string.Empty, Password = string.Empty};

            Response Result(Member x)
                => new Response {Exists = !string.IsNullOrEmpty(x.Username)};
        }
    }
}