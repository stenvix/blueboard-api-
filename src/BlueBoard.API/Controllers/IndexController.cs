using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    public class IndexController : ControllerBase
    {
        [HttpGet("Index")]
        public string Index() => "BlueBoard API";
    }
}