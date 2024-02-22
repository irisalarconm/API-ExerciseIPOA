using API.Validators;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class TypeController : ControllerBase
    {
        ITypeService _typeService;
        TypeValidator _validator;
        ILogger<TypeController> _logger;

        public TypeController(ITypeService service, TypeValidator validator, ILogger<TypeController> logger)
        {
            _typeService = service;
            _validator = validator;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return Ok(_typeService.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_typeService.GetById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public IActionResult Create(Models.Type type)
        {
            try
            {
                var validationResult = _validator.Validate(type);
                if(validationResult.IsValid)
                {
                    return Ok(_typeService.Create(type));
                }
                else
                {
                    var error = BadRequest(validationResult.Errors);
                    //_logger.LogError(error.ToString());
                    return BadRequest(error);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}")]

        public IActionResult Update(int id, [FromBody] Models.Type type)
        {
            try
            {
                type.Id = id;
                var validationResult = _validator.Validate(type);
                if(validationResult.IsValid)
                {
                    return Ok(_typeService.Update(type));
                }
                else
                {
                    var error = BadRequest(validationResult.Errors);
                    //_logger.LogError(error.ToString());
                    return BadRequest(error);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_typeService.Delete(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
