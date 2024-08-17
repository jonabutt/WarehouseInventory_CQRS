using MediatR;
using WarehouseInventory.Application.Categories.Responses;

namespace WarehouseInventory.Application.Categories.Commands
{
    public class CreateCategory : IRequest<CategoryResponse>
    {
        public string Name { get; set; }

        public CreateCategory(string name)
        {
            Name = name;
        }
    }
}
