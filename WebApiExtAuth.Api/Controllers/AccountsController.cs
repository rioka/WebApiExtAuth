using System;
using System.Web.Http;
using WebApiExtAuth.Api.Data;

namespace WebApiExtAuth.Api.Controllers
{
  [Authorize]
  public class AccountsController : ApiController
  {
    private readonly InMemoryAuthRepository _repo;
    
    public AccountsController()
    {
      _repo = new InMemoryAuthRepository();
    }

    [AllowAnonymous]
    public IHttpActionResult Get()
    {
      return Ok("aaa");
    }

    // POST api/Account/Register
    [AllowAnonymous]
    public IHttpActionResult Post([FromBody] User user)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      _repo.Add(user.UserName, user.Password);

      return Ok();
    }
  }
}