using MediatR;
using WarehouseInventory.Application.Categories.Responses;

namespace WarehouseInventory.Application.Categories.Queries
{
    public class ListCategories : IRequest<IEnumerable<CategoryResponse>>
    {
    }
}
