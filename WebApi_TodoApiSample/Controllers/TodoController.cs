using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TodoApiSample.Entities;

namespace WebApi_TodoApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private DatabaseContext _db;

        // Dependecy injection
        public TodoController(DatabaseContext context)
        {
            _db= context;
        }
    }
}
