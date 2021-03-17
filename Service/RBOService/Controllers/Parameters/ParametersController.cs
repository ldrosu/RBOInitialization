using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RBOService.Exceptions;
using RBOService.Handlers.Parameters;
using RBOService.Handlers.Parameters.Commands;
using RBOService.Handlers.Parameters.Queries;
using SimpleSoft.Mediator;


namespace RBOService.Controllers.Parameters
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ParametersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ParametersController> _logger;

        public ParametersController(IMediator mediator, ILogger<ParametersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetParametersQuery { }, ct);
            return Ok(
                result.Select(r => new ParameterModel
                {
                    Id = r.Id,
                    Index = r.Index,
                    Value = r.Value,
                    RuleId = r.RuleId
                }).ToList<ParameterModel>());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken ct)
        {
            try
            {
                var result = await _mediator.FetchAsync(new GetParameterByIdQuery
                {
                    ParameterId = id
                }, ct);

                var ruleModel = new ParameterModel
                {
                    Id = result.Id,
                    Index = result.Index,
                    Value = result.Value,
                    RuleId = result.RuleId
                };
                return Ok(ruleModel);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostAsync([FromBody] CreateParameterModel model, CancellationToken ct)
        {
            try
            {
                var userName = User.Identity?.Name;
                _logger.LogInformation($"User '{userName}' is creating parameter '{model.Value}'");

                var result = await _mediator.SendAsync(new CreateParameterCommand
                {
                    Index = model.Index,
                    Value = model.Value,
                    RuleId = model.RuleId

                }, ct);

                var resultModel = new CreateParameterResultModel
                {
                    Id = result.Id
                };
                return Created("", resultModel);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateParameterModel model, CancellationToken ct)
        {
            try
            {
                await _mediator.SendAsync(new UpdateParameterCommand
                {
                    ExternalId = id,
                    Index = model.Index,
                    Value = model.Value,
                    RuleId = model.RuleId
                }, ct);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken ct)
        {
            try
            {
                await _mediator.SendAsync(new DeleteParameterCommand
                {
                    ParameterId = id,
                }, ct);

                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}