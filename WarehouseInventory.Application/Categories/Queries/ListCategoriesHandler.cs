using MediatR;
using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Application.Categories.Responses;
using WarehouseInventory.Core.Constants;
using WarehouseInventory.DB.Entities;
using WarehouseInventory.DB.Readonly;

namespace WarehouseInventory.Application.Categories.Queries
{
    public class ListCategoriesHandler : IRequestHandler<ListCategories, IEnumerable<CategoryResponse>>
    {
        private readonly ICache _cache;
        private readonly WarehouseInventoryContext _context;

        public ListCategoriesHandler(WarehouseInventoryContext context, ICache cache)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(ListCategories request, CancellationToken cancellationToken)
        {
            await _cache.DeleteAsync(Cache.Categories);
            Func<Task<IEnumerable<Category>>> getCategories = async () => await _context.Categories.ToListAsync();

            var queryResult = await _cache.GetAsync<IEnumerable<Category>>(Cache.Categories, getCategories);
            return queryResult.Select(x => new CategoryResponse(x));
        }
    }
}
