using MediatR;
using WarehouseInventory.DB.Entities;

namespace WarehouseInventory.Application.Categories.Commands
{
    public class CategoryCreatedNotification : INotification
    {
        public Category Category { get; set; }

        public CategoryCreatedNotification(Category category)
        {
            this.Category = category;
        }
    }
}
