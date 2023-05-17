using CQRS.Domain.Commands.CreatePerson;
using CQRS.Domain.Core;
using CQRS.Domain.Domain;
using CQRS.Domain.Queries.GetPerson;
using CQRS.Domain.Queries.ListPerson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CQRS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly CreatePersonCommandHandler _createPersonCommandHandler;
        private readonly ListPersonQueryHandler _listPersonQueryHandler;
        private readonly GetPersonQueryHandler _getPersonQueryHandler;
        public PeopleController(CreatePersonCommandHandler createPersonCommandHandler,
            ListPersonQueryHandler listPersonQueryHandler,
            GetPersonQueryHandler getPersonQueryHandler)
        {
            _createPersonCommandHandler = createPersonCommandHandler;
            _listPersonQueryHandler = listPersonQueryHandler;
            _getPersonQueryHandler = getPersonQueryHandler;
        }


        [HttpPost(Name = "Insert Person")]
        public async Task<IActionResult> InsertPeopleAsync(
            CreatePersonCommand command,
            CancellationToken cancellationToken)
        {
            var response = await _createPersonCommandHandler.HandleAsync(command, cancellationToken);

            return GetResponse(_createPersonCommandHandler, response);
        }

        [HttpGet(Name = "List People")]

        public async Task<IActionResult> GetAsync(
            [FromQuery] string? name,
            [FromQuery] string? cpf,
            CancellationToken cancellationToken)
        {
            var response = await _listPersonQueryHandler.HandleAsync(new ListPersonQuery(name, cpf), cancellationToken);

            return GetResponse(_listPersonQueryHandler, response);
        }

        [HttpGet("{id:guid}", Name = "Get Person By Id")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _getPersonQueryHandler.HandleAsync(new GetPersonQuery(id), cancellationToken);
            return GetResponse(_getPersonQueryHandler, response);
        }


        private IActionResult GetResponse<THandler, TResponse>(THandler handler, TResponse response)
            where THandler : BaseHandler
        {
            return StatusCode((int)handler.GetStatusCode(), new { Data = response, Notifications = handler.GetNotifications() });
        }

        private IActionResult GetResponse<THandler>(THandler handler)
            where THandler : BaseHandler
        {
            return StatusCode((int)handler.GetStatusCode(), new { Notifications = handler.GetNotifications() });
        }
    }
}
