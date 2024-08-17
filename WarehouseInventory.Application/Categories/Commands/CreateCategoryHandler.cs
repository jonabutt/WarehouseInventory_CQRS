using MediatR;
using WarehouseInventory.Application.Categories.Responses;
using WarehouseInventory.DB.Entities;

namespace WarehouseInventory.Application.Categories.Commands
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryResponse>
    {
        private readonly WarehouseInventoryContext _context;

        public CreateCategoryHandler(WarehouseInventoryContext context)
        {
            _context = context;
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
            return new CategoryResponse(category);
        }
    }
}
