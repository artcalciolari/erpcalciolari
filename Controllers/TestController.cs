using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErpCalciolari.Infra;

namespace ErpCalciolari.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TestController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet("test-connection")]
        public async Task<IActionResult> TestDbConnection()
        {
            try
            {
                // Test the connection to the database
                await _context.Database.OpenConnectionAsync();
                await _context.Database.CloseConnectionAsync();
                return Ok("Connection to the database was successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while trying to connect to the database: {ex.Message}");
            }
        }
    }
}