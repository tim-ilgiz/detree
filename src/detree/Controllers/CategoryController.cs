using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace detree.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriesListVm>> Get()
        {
            var vm = await Mediator.Send(new GetCategoriesListQuery());

            return Ok(vm);
        }

        [HttpPost]
        [Route(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Category>> Create([FromBody] CreateCategoryCommand command)
        {
            var card = await Mediator.Send(command);

            return Ok(card);
        }

        [HttpPost]
        [Route(nameof(Update))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            var card = await Mediator.Send(command);

            return Ok(card);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteCategoryCommand {Id = id});

            return NoContent();
        }
    }
}