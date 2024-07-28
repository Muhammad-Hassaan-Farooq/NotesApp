using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using NotesApp.Data;

namespace NotesApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TestController : Controller
    {
        private readonly MongoDBContext _context;

        public TestController(MongoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> TestDB()
        {
            try
            {
                var result = _context.Database.RunCommand<BsonDocument>(
                    new BsonDocument("ping", 1)
                );

                return Ok(
                    "Pinged your deployment. You successfully connected to MongoDB!" + result
                );
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
