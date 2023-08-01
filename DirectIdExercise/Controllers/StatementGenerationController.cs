using DirectIdExercise.Quaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace DirectIdExercise.Controllers
{
    /// <summary>
    /// Controller to generate statement
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StatementGenerationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StatementGenerationController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public StatementGenerationController(ILogger<StatementGenerationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Method to generate statement
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetStatement")]
        public async Task<IActionResult> Get()
        {
            var reports = new List<Report>();
            try
            {
                reports = await _mediator.Send(new GetStatementQuery());
                _logger.LogInformation("Statement generation completed");
            }
            catch(Exception e)
            {
                _logger.LogError("Error Occured:",e);
                return BadRequest(e);
            }
            return Ok(reports);
        }
    }
}