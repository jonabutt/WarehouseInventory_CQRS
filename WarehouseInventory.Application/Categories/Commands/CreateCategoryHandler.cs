using MediatR;
using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Application.Categories.Responses;
using WarehouseInventory.Core.Constants;
using WarehouseInventory.DB.Entities;
using WarehouseInventory.DB.Readonly;

namespace WarehouseInventory.Application.Categories.Commands
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryResponse>
    {
        private readonly WarehouseInventoryContext _context;
        private readonly ICache _cache;

        public CreateCategoryHandler(WarehouseInventoryContext context, ICache cache)
        {
            _context = context;
            _cache = cache;
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

            //  store data in readonly store
            var categories = await _context.Categories.ToListAsync();
            await _cache.SetAsync(Cache.Categories, categories);
            await _cache.SetAsync(Cache.CategoryItem.Replace("{id}", category.Id.ToString()), category);

            return new CategoryResponse(category);
        }
    }
}
