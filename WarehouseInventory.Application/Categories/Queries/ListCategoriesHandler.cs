using MediatR;
using WarehouseInventory.Application.Categories.Responses;
using WarehouseInventory.Core.Constants;
using WarehouseInventory.DB.Entities;
using WarehouseInventory.DB.Readonly;

namespace WarehouseInventory.Application.Categories.Queries
{
    public class ListCategoriesHandler : IRequestHandler<ListCategories, IEnumerable<CategoryResponse>>
    {
        private readonly ICache _cache;

        public ListCategoriesHandler(ICache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(ListCategories request, CancellationToken cancellationToken)
        {
            var queryResult = await _cache.GetAsync<IEnumerable<Category>>(Cache.Categories);
            return queryResult.Select(c => new CategoryResponse(c));
        }
    }
}
