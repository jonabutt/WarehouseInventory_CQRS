using WarehouseInventory.DB.Entities;

namespace WarehouseInventory.Application.Categories.Responses
{
    public class CategoryResponse(Category category)
    {
        public int Id { get; set; } = category.Id;

        public string Name { get; set; } = category.Name;
    }
}
