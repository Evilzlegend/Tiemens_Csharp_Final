#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMVLaw.Data;
using DMVLaw.Models;
using Microsoft.AspNetCore.Authorization;

namespace DMVLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly DmvlawContext _context;

        public DriversController(DmvlawContext context)
        {
            _context = context;
        }



        [Authorize(Roles = "DMV Personnel, Law Enforcement")]
        // GET: api/Drivers
        [HttpGet]
        public IQueryable<Object> GetDrivers()
        {
            return _context.Drivers
                .Include(d => d.Vehicles)
                .Include(d => d.DriverInfractions)
                .Select(d => new
                {
                    DriverID = d.DriverId,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    SSN = d.Ssn,
                    Address = d.Address,
                    Phone = d.Phone,
                    vehicle = d.Vehicles.Select(v => v.VehicleMake + " " + v.VehicleModel + " " + v.VehicleColor).Single(),
                    VehiclePlate = d.Vehicles.Select(v => v.VehiclePlate).Single(),
                    DriverInfractions = d.DriverInfractions.Select(i => i.Infraction.InfractionType)
                });
        }

        [Authorize(Roles = "DMV Personnel, Law Enforcement")]
        [HttpGet("DriverSearch")]
        public IQueryable<Object> GetDriver(string FirstName = null, string LastName = null, string SSN = null, string PlateNumber = null)
        {
            IQueryable<Object> driver = null;

            if(FirstName != null && LastName != null)
            {
                driver = _context.Drivers
                    .Include(d => d.Vehicles)
                    .Select(d => new
                    {
                        DriverID = d.DriverId,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        SSN = d.Ssn,
                        vehicle = d.Vehicles.Select(v => v.VehicleMake + " " + v.VehicleModel + " " + v.VehicleColor).Single(),
                        VehicesPlate = d.Vehicles.Select(v => v.VehiclePlate).Single(),
                        DriverInfractions = d.DriverInfractions.Select(i => i.Infraction.InfractionType)
                    })
                    .Where(d => (d.FirstName == FirstName) && (d.LastName == LastName));
            } else if(SSN != null)
            {
                driver = _context.Drivers
                    .Include(d => d.Vehicles)
                    .Select(d => new
                    {
                        DriverID = d.DriverId,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        SSN = d.Ssn,
                        vehicle = d.Vehicles.Select(v => v.VehicleMake + " " + v.VehicleModel + " " + v.VehicleColor).Single(),
                        VehiclePlate = d.Vehicles.Select(v => v.VehiclePlate).Single(),
                        DriverInfractions = d.DriverInfractions.Select(i => i.Infraction.InfractionType)
                    })
                    .Where(d => d.SSN == SSN);
            } else if (PlateNumber != null)
            {
                driver = _context.Drivers
                    .Include(d => d.Vehicles)
                    .Select(d => new
                    {
                        DriverID = d.DriverId,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        SSN = d.Ssn,
                        vehicles = d.Vehicles.Select(v => v.VehicleMake + " " + v.VehicleModel + " " + v.VehicleColor).Single(),
                        VehiclePlate = d.Vehicles.Select(v => v.VehiclePlate).Single(),
                        DriverInfractions = d.DriverInfractions.Select(i => i.Infraction.InfractionType)
                    })
                    .Where(d => d.VehiclePlate == PlateNumber);
            }
            return driver;
        }
        [Authorize(Roles = "DMV Peronnel")]
        // POST: api/Drivers
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = driver.DriverId }, driver);
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.DriverId == id);
        }
    }
}
