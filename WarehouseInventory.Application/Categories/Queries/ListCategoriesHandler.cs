using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseInventory.Application.Categories.Models;
using WarehouseInventory.Application.Categories.Responses;
using WarehouseInventory.DB.Entities;

namespace WarehouseInventory.Application.Categories.Queries
{
    public class ListCategoriesHandler : IRequestHandler<ListCategories, IEnumerable<CategoryResponse>>
    {
        private readonly WarehouseInventoryContext _context;
        public ListCategoriesHandler(WarehouseInventoryContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryResponse>> Handle(ListCategories request, CancellationToken cancellationToken)
        {
            var queryResult = await _context.Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return queryResult.Select(x => new CategoryResponse(x));
        }
    }
}
