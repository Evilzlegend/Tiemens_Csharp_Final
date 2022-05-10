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
    public class VehiclesController : ControllerBase
    {
        private readonly DmvlawContext _context;

        public VehiclesController(DmvlawContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [Authorize(Roles = "DMV Personnel")]
        [HttpGet]
        public IQueryable<Object> GetVehicles()
        {
            return _context.Vehicles
                .Include(v => v.Driver)
                .Select(v => new
                {
                    VehicleMake = v.VehicleMake,
                    VehicleModel = v.VehicleModel,
                    VehicleColor = v.VehicleColor,
                    VehiclePlate = v.VehiclePlate,
                    Driver = v.Driver.FirstName + " " + v.Driver.LastName
                });
        }

        // GET: api/Vehicles/GetByPlate
        [Authorize(Roles = "DMV Personnel")]
        [HttpGet("GetVehicleByPlate")]
        public IQueryable<Object> GetDriver(string PlateNumber = null)
        {
            IQueryable<Object> vehicle = null;

            if(PlateNumber != null)
            {
                vehicle = _context.Vehicles
                    .Include(v => v.Driver)
                    .Select(v => new
                    {
                        VehicleMake = v.VehicleMake,
                        VehicleModel = v.VehicleModel,
                        VehicleColor = v.VehicleColor,
                        VehiclePlate = v.VehiclePlate,
                        Driver = v.Driver.FirstName + " " + v.Driver.LastName
                    })
                    .Where(v => v.VehiclePlate == PlateNumber);
            }
            return vehicle;
        }

        // POST: api/Vehicles
        [Authorize(Roles = "DMV Personnel")]
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicle);
        }
        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
