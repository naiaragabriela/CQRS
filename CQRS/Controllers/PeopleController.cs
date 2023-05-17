using CQRS.Domain.Commands.CreatePerson;
using CQRS.Domain.Core;
using CQRS.Domain.Domain;
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
        public PeopleController(CreatePersonCommandHandler createPersonCommandHandler,
            ListPersonQueryHandler listPersonQueryHandler)
        {
            _createPersonCommandHandler = createPersonCommandHandler;
            _listPersonQueryHandler = listPersonQueryHandler;
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
