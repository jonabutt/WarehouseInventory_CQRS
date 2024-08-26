using MediatR;
using WarehouseInventory.Core.Constants;
using WarehouseInventory.DB.Entities;
using WarehouseInventory.DB.Readonly;

namespace WarehouseInventory.Application.Categories.Commands
{
    internal class CategoryCreatedNotificationHandler : INotificationHandler<CategoryCreatedNotification>
    {
        private readonly ICache _cache;
        public CategoryCreatedNotificationHandler(ICache cache)
        {
            _cache = cache;
        }

        public async Task Handle(CategoryCreatedNotification notification, CancellationToken cancellationToken)
        {
            var cachedCategories = await _cache.GetAsync<List<Category>>(Cache.Categories);
            if (cachedCategories != null)
            {
                cachedCategories.Add(notification.Category);
            }
            else
            {
                cachedCategories = new List<Category> { notification.Category };
            }
            await _cache.SetAsync(Cache.Categories, cachedCategories);
            await _cache.SetAsync(Cache.CategoryItem.Replace("{id}", notification.Category.Id.ToString()), notification.Category);
        }
    }
}
