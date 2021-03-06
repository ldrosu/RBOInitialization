using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RBOService.Exceptions;
using RBOService.Handlers.RBObjects;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Controllers.RBObjects
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RBObjectsController : ControllerBase
    { 
        private readonly IMediator _mediator;
        private readonly ILogger<RBObjectsController> _logger;

        public RBObjectsController(IMediator mediator, ILogger<RBObjectsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
           
        [HttpPost]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromRoute] Guid id, [FromBody] JObject objectTemplate, CancellationToken ct)
        {
            try
            {
                var userName = User.Identity?.Name;
                _logger.LogInformation($"User '{userName}' is creating rule based object");

                var createRBObjectCommand = new CreateRBObjectCommand
                {
                    ObjectTemplate = objectTemplate,
                    GroupId = id
                };
                var result = await _mediator.SendAsync(createRBObjectCommand, ct);
                return Created("", result.InitializedObject);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("array/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromRoute] Guid id, [FromQuery] int length, [FromBody] JArray arrayTemplate, CancellationToken ct)
        {
            try 
            {
                var userName = User.Identity?.Name;
                _logger.LogInformation($"User '{userName}' is creating rule based array");

                var createRBArrayCommand = new CreateRBArrayCommand
                {
                    ArrayTemplate = arrayTemplate,
                    Length = length,
                    GroupId = id
                };
                var result = await _mediator.SendAsync(createRBArrayCommand, ct);
                return Created("", result.InitializedArray);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
