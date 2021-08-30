using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MapboxApi.Data;
using MapboxApi.DTOs;

namespace MapboxApi.Controllers
{
    [ApiController]
    [Route("api/pins")]
    public class PinsController : ControllerBase
    {
        private readonly IApi _dataAccess;
        public PinsController(IApi data)
        {
            _dataAccess = data;
        }
        
        //GET api/pins
        [HttpGet]
        public ActionResult<IEnumerable<PinReadDTO>> GetAllPins()
        {
            IEnumerable<PinReadDTO> pins;
            try
            {
                pins = _dataAccess.GetPins();
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
                return NotFound();
            }

            if (pins.Count() != 0)
            {
                return Ok(pins);
            }
            else
            {
                return NotFound();
            }
        }

        //GET api/pins/{id}
        [HttpGet]
        [Route("{id}", Name = "GetPin")]
        public ActionResult<PinReadDTO> GetPin(int id)
        {
            PinReadDTO pin;
            try
            {
                pin = _dataAccess.GetPin(id);
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
                return NotFound();
            }

            if (pin != null)
            {
                return Ok(pin);
            }
            else
            {
                return NotFound();
            }
        }

        //POST api/pins
        [HttpPost]
        public ActionResult<PinReadDTO> CreatePin(PinCreateDTO createPin)
        {
            try
            {
                int id = _dataAccess.CreatePin(createPin);

                PinReadDTO pin = new PinReadDTO
                {
                    Id = id,
                    Type = createPin.Type,
                    Longitude = createPin.Longitude,
                    Latitude = createPin.Latitude,
                    Title = createPin.Title,
                    Description = createPin.Description
                };
                return CreatedAtRoute(nameof(GetPin), new { Id = pin.Id }, pin);
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
                return NotFound();
            }
        }

        //PUT api/pins
        [HttpPut]
        public ActionResult UpdatePin(PinUpdateDTO updatePin)
        {
            if (_dataAccess.GetPin(updatePin.Id) == null)
            {
                return NotFound();
            }

            try
            {
                _dataAccess.UpdatePin(updatePin);
                return Ok(_dataAccess.GetPin(updatePin.Id));
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
                return BadRequest();
            }
        }

        //DELETE api/pins/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePin(int id)
        {
            if (_dataAccess.GetPin(id) == null)
            {
                return NotFound();
            }

            try
            {
                _dataAccess.DeletePin(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
                return BadRequest();
            }
        }
    }
}
