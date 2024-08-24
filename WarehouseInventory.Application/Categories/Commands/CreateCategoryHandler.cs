using MediatR;
using WarehouseInventory.Application.Categories.Responses;
using WarehouseInventory.DB.Entities;

namespace WarehouseInventory.Application.Categories.Commands
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryResponse>
    {
        private readonly WarehouseInventoryContext _context;
        private readonly IMediator _mediator;

        public CreateCategoryHandler(WarehouseInventoryContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CategoryResponse> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name
            };

            // Save the category to the database
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            //  store data in readonly store by publishing event
            await _mediator.Publish(new CategoryCreatedNotification(category));

            return new CategoryResponse(category);
        }
    }
}
