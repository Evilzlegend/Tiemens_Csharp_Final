#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMVLaw.Data;
using DMVLaw.Models;

namespace DMVLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverInfractionsController : ControllerBase
    {
        private readonly DmvlawContext _context;

        public DriverInfractionsController(DmvlawContext context)
        {
            _context = context;
        }

        // POST: api/DriverInfractions
        [Authorize(Roles = "Law Enforcement")]
        [HttpPost]
        public async Task<ActionResult<DriverInfraction>> PostDriverInfraction([FromBody] DriverInfraction driverInfraction)
        {
            _context.DriverInfractions.Add(driverInfraction);
            await _context.SaveChangesAsync();

            return Ok(driverInfraction);
        }

        private bool DriverInfractionExists(int id)
        {
            return _context.DriverInfractions.Any(e => e.DriverInfractionId == id);
        }
    }
}