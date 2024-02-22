using API.Validators;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SegmentController : ControllerBase
    {
        ISegmentService _segmentService;
        SegmentValidator _validator;
        ILogger<SegmentController> _logger;
        //logger is not use yet.

        public SegmentController(ISegmentService segmentService, SegmentValidator validator, ILogger<SegmentController> logger)
        {
            _segmentService = segmentService;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]

        public IActionResult Index()
        {
            try
            {
                return Ok(_segmentService.GetAll());
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_segmentService.GetById(id));
            }
            catch(Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]

        public IActionResult Create (Segment segment)
        {
            try
            {
                var validationResult = _validator.Validate(segment);
                if (validationResult.IsValid)
                {
                    var created = _segmentService.Create(segment);
                    return Ok(created);
                }
                else
                {
                    var error = validationResult.Errors.Select(e => e.ErrorMessage);
                    var errorMessage = string.Join(", ", error);
                    //_logger.LogError($"Validation failed: {errorMessage}");
                    return BadRequest(error);
                }
            }
            catch(Exception ex )
            {
                //_logger.LogError("Error occurred {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public IActionResult Update(int id, [FromBody]Segment segment)
        {
            segment.Id = id;
            try
            {
                var validationResult = _validator.Validate(segment);
                if (validationResult.IsValid)
                {
                    var updated = _segmentService.Update(segment);
                    return Ok(updated);
                }
                else
                {
                    var error = BadRequest(validationResult.Errors);
                    //_logger.LogError(error.ToString());
                    return BadRequest(error);
                }
            }
            catch(Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = _segmentService.Delete(id);
                return Ok(deleted);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}
