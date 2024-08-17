using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WarehouseInventory.Application.Categories.Commands;
using WarehouseInventory.Application.Categories.Queries;
using WarehouseInventory.Application.Categories.Responses;

namespace WarehouseInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;

        //protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        //private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCateogry([FromBody] CreateCategory command)
        {
            var result = await _mediator.Send(command);
            //return CreatedAtAction(nameof(GetCateogry), new { categoryId = result.Id }, result);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new ListCategories();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.OK)]
        public IActionResult GetCateogry([FromQuery] int id)
        {
            return NotFound();
        }
    }
}
