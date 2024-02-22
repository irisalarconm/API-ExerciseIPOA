using API.Validators;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        IExerciseService _exerciseService;
        ExerciseValidator _exerciseValidator;
        ILogger<ExerciseController> _logger;

        public ExerciseController(IExerciseService service,ExerciseValidator validator, ILogger<ExerciseController> logger)
        {
            _exerciseService = service;
            _logger = logger;
            _exerciseValidator = validator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_exerciseService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_exerciseService.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Exercise exercise)
        {
            try
            {
                var validationResult = _exerciseValidator.Validate(exercise);
                if (validationResult.IsValid)
                {
                    var exerciseCreated = _exerciseService.Create(exercise);
                    return Ok(exerciseCreated);
                }
                else
                {

                    var error = validationResult.Errors.Select(e => e.ErrorMessage);
                    var errorMessage = string.Join(", ", error);
                    _logger.LogError($"Validation failed: {errorMessage}");
                    return BadRequest(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Exercise exercise)
        {
            exercise.Id = id;
            var validationResult = _exerciseValidator.Validate(exercise);
            if (validationResult.IsValid)
            {
                return Ok(_exerciseService.Update(exercise));
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_exerciseService?.Delete(id));
        }



    }
}
