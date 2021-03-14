using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBOService.Exceptions;
using RBOService.Handlers.Rules;
using RBOService.Handlers.Rules.Commands;
using RBOService.Handlers.Rules.Queries;
using SimpleSoft.Mediator;


namespace RBOService.Controllers.Rules
{
    [Route("[controller]")]
    public class RulesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RulesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]       
        public async Task<IActionResult> GetAllAsync( CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetRulesQuery {}, ct);
            return Ok(
                result.Select(r => new RuleModel
                {
                    Id = r.Id,
                    Index = r.Index,
                    Pattern = r.Pattern,
                    SourceType = r.SourceType,
                    DestinationType = r.DestinationType,
                    GroupId = r.GroupId
                }).ToList<RuleModel>());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken ct)
        {
            try
            {
                var result = await _mediator.FetchAsync(new GetRuleByIdQuery
                {
                    RuleId = id
                }, ct);

                var ruleModel = new RuleModel
                {
                    Id = result.Id,
                    Index = result.Index,
                    Pattern = result.Pattern,
                    SourceType = result.SourceType,
                    DestinationType = result.DestinationType,
                    GroupId = result.GroupId
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
        public async Task<IActionResult> PostAsync([FromBody] CreateRuleModel model, CancellationToken ct)
        {
            try
            {
                var result = await _mediator.SendAsync(new CreateRuleCommand
                {
                    Index = model.Index,
                    Pattern = model.Pattern,
                    SourceType = model.SourceType,
                    DestinationType = model.DestinationType,
                    GroupId = model.GroupId

                }, ct);

                var resultModel = new CreateRuleResultModel
                {
                    Id = result.Id
                };
                return Created("",resultModel);
            }
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(ConflictException e)
            {
                return Conflict(e.Message);                
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateRuleModel model, CancellationToken ct)
        {
            try
            {
                await _mediator.SendAsync(new UpdateRuleCommand
                {
                    ExternalId = id,
                    Index = model.Index,
                    Pattern = model.Pattern,
                    SourceType = model.SourceType,
                    DestinationType = model.DestinationType,
                    GroupId = model.GroupId
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
                await _mediator.SendAsync(new DeleteRuleCommand
                {
                    RuleId = id,
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
