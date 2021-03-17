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
using RBOService.Handlers.Groups;
using RBOService.Handlers.Groups.Commands;
using SimpleSoft.Mediator;


namespace RBOService.Controllers.Groups
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]  
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IMediator mediator, ILogger<GroupsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GroupModel>))]
        public async Task<IActionResult> GetAsync(CancellationToken ct)
        {
            IEnumerable<Group> result = await _mediator.FetchAsync(new GetGroupsQuery(), ct);
           return Ok(result.Select(t => new GroupModel
           {
                Id = t.Id,
                Name = t.Name,
           }));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken ct)
        {
            try
            {
                var result = await _mediator.FetchAsync(new GetGroupByIdQuery
                {
                    GroupId = id
                }, ct);

                return Ok(new GroupModel
                {
                    Id = result.Id,
                    Name = result.Name,

                });
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostAsync([FromBody] CreateGroupModel model, CancellationToken ct)
        {
            var userName = User.Identity?.Name;
            _logger.LogInformation($"User '{userName}' is creating group '{model.Name}'");
            var result = await _mediator.SendAsync(new CreateGroupCommand
            {
                Name = model.Name
            }, ct);
            var resultModel = new CreateGroupResultModel
            {
                Id = result.Id
            };
            if (result.IsNew)
            {
                return Created("", resultModel);
            }
            else
            {
                return Conflict(resultModel);
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateGroupModel model, CancellationToken ct)
        {
            try
            {
                await _mediator.SendAsync(new UpdateGroupCommand
                {
                    ExternalId = id,
                    Name = model.Name
                }, ct);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken ct)
        {
            try
            {
                await _mediator.SendAsync(new DeleteGroupCommand
                {
                    GroupId = id,
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
