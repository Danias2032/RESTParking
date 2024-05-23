using Microsoft.AspNetCore.Mvc;
using Parkfinder.Models;
using Parkfinder.RESTParking.Repositories;

namespace Parkfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkeringController : ControllerBase
    {
        private readonly PFRepoDB _parkingRepository;

        public ParkeringController(PFRepoDB parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        // GET: api/Parking
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Parkeringsområde>> Get([FromQuery] DateTime? date = null, [FromQuery] string? orderBy = null)
        {
            try
            {
                IEnumerable<Parkeringsområde> measurements = _parkingRepository.GetParkingList(date, orderBy);
                if (!measurements.Any())
                {
                    return NoContent();
                }
                return Ok(measurements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }

        // GET api/Parking/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Parkeringsområde> Get(int id)
        {
            Parkeringsområde? measurement = _parkingRepository.GetID(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return Ok(measurement);
        }


        // POST api/Parking
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Parkeringsområde> Post([FromBody] Parkeringsområde measurement)
        {
            try
            {
                measurement.Validate();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            _parkingRepository.Add(measurement);
            return CreatedAtAction(nameof(Get), new { id = measurement.Id }, measurement);
        }

        // PUT api/Parking/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Parkeringsområde> Put(int id, [FromBody] Parkeringsområde measurement)
        {
            try
            {
                measurement.Validate();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            Parkeringsområde? updatedMeasurement = _parkingRepository.Update(id, measurement);
            if (updatedMeasurement == null)
            {
                return NotFound();
            }
            return Ok(updatedMeasurement);
        }

        // DELETE api/Parking/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Parkeringsområde?> Delete(int id)
        {
            Parkeringsområde? measurement = _parkingRepository.Delete(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return Ok(measurement);
        }

    }
}
