using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SOSE_API.DTO;
using SOSE_API.Interface;
using SOSE_API.Utility;

namespace SOSE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {

        private readonly ITourInterface _tourService;

        public TourController(ITourInterface tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]

       
        public IActionResult Get()
        {
            var customers = _tourService.GetAllTours();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tour = _tourService.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }

        [HttpPost]
       [Authorize(Roles = SD.Role_Admin)]
        public IActionResult CreateTour([FromBody] TourDTO tour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InsertedTour = _tourService.AddTour(tour);
            return Ok(InsertedTour);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult UpdateTour(int id, [FromBody] TourDTO tour)
        {
            if (!ModelState.IsValid || id != tour.Id)
            {
                return BadRequest();
            }

            var updatedTour = _tourService.UpdateTour(id, tour);
            return Ok(updatedTour);
        }


        [HttpPatch]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult partialUpdateTour(int id, [FromBody] JsonPatchDocument<TourDTO> patchTour)


        {
            if (patchTour == null || id == 0)
            {
                return BadRequest();
            }


            var patchedTour = _tourService.PartialUpdateTour(id, patchTour);


            return Ok(patchedTour);

        }


        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult DeleteCustomer(int id)
        {
            _tourService.DeleteTour(id);
            return Ok("Deleted succssfully ");
        }
    }
}
