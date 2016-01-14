using System;
using System.Web.Http;

namespace WebApiExtAuth.Api.Controllers
{
  public class SamplesController : ApiController
  {
    public IHttpActionResult Get()
    {
      return Ok(Guid.NewGuid());
    }
  }
}