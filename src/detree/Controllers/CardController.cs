using Application.Cards.Commands.CreateCard;
using Application.Cards.Commands.DeleteCard;
using Application.Cards.Commands.UpdateCard;
using Application.Cards.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace detree.Controllers
{
    [Authorize(Policy = "ApiReader")]
    public class CardController : ApiController
    {
        [Authorize(Policy = "Admin")]
        [HttpGet]
        [Route(nameof(Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardsListVm>> Get(long ParentId)
        {
            var vm = await Mediator.Send(new GetCardsListQuery { ParentId = ParentId });

            return Ok(vm);
        }

        //[Authorize(Policy = "Admin")]
        [HttpPost]
        [Route(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Card>> Create([FromBody] CreateCardCommand command)
        {
            var card = await Mediator.Send(command);

            return Ok(card);
        }

        //[Authorize(Policy = "Admin")]
        [HttpPost]
        [Route(nameof(Update))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateCardCommand command)
        {
            var card = await Mediator.Send(command);

            return Ok(card);
        }

        //[Authorize(Policy = "Admin")]
        [HttpDelete]
        [Route(nameof(Delete))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteCardCommand { Id = id });

            return NoContent();
        }
    }
}
