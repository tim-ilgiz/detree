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
    [Authorize]
    public class CardController : ApiController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardsListVm>> Get(long id)
        {
            var vm = await Mediator.Send(new GetCardsListQuery { ParentId = id });

            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Card>> Create([FromBody] CreateCardCommand command)
        {
            var card = await Mediator.Send(command);

            return Ok(card);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(UpdateCardCommand command)
        {
            var card = await Mediator.Send(command);

            return Ok(card);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(DeleteCardCommand command)
        {
            var card = await Mediator.Send(command);

            return Ok(card);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteCardCommand { Id = id });

            return NoContent();
        }
    }
}
