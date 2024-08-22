using Microsoft.AspNetCore.Mvc;
using MyCar_Creation.Context;
using MyCar_Creation.Dtos;
using MyCar_Creation.Entities;

namespace MyCar_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllVehicle")]
        public ActionResult GetAllVehicle()
        {
            List<Vehicle> vehicles = _context.Vehicles.ToList();
            if (vehicles is not null)
            {
                return Ok(vehicles);
            }
            return NotFound();
        }

        [HttpGet("GetVehicleById/{vehicleId}")]
        public ActionResult GetVehicleById([FromRoute] Guid vehicleId)
        {
            Vehicle vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                return Ok(vehicle);
            }
            return NotFound();
        }

        //[HttpGet("GetVehicleByCategoryId/{categoryId}")]
        //public ActionResult GetVehicleByCategoryId([FromRoute] Guid categoryId)
        //{
        //    List<Vehicle> vehicles = _context.Vehicles.Where(x => x.CategoryId == categoryId).ToList();
        //    if (vehicles is not null)
        //    {
        //        return Ok(vehicles);
        //    }
        //    return NotFound();
        //}

        [HttpPost("CreateVehicle")]
        public ActionResult CreateVehicle(VehicleDTO model)
        {
            Vehicle vehicle = new()
            {
                Brand = model.Brand,
                Model = model.Model,
                ModelYear = model.ModelYear,
                Price = model.Price,
                Description = model.Description,
                CategoryId = model.CategoryId,
            };

            _context.Vehicles.Add(vehicle);
            if (_context.SaveChanges() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("UpdateVehicle/{vehicleId}")]
        public ActionResult UpdateVehicle([FromRoute] Guid vehicleId, VehicleDTO model)
        {
            Vehicle vehicle = _context.Vehicles.Find(vehicleId);

            if (vehicle != null)
            {
                vehicle.Price = model.Price;
                vehicle.Description = model.Description;
                vehicle.CategoryId = model.CategoryId;
                vehicle.Brand = model.Brand;
                vehicle.Model = model.Model;
                vehicle.ModelYear = model.ModelYear;

                if (_context.SaveChanges() > 0)
                {
                    return Ok(vehicle);
                }
            }
            return NotFound();
        }

        [HttpDelete("DeleteVehicle/{vehicleId}")]
        public ActionResult DeleteVehicle(Guid vehicleId)
        {
            Vehicle vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                if (_context.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}