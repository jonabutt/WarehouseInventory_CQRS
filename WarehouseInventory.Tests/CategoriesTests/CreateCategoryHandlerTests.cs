
using Testcontainers.MsSql;
using WarehouseInventory.Application.Categories.Commands;

namespace WarehouseInventory.Tests.CategoriesTests
{
    public class CreateCategoryHandlerTests : BaseIntegrationTest
    {
        public CreateCategoryHandlerTests(IntegrationTestWebAppFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Create_Category_ShouldAdd_NewCategoryToDatabase()
        {
            // arrange
            var command = new CreateCategory("Fish");
            // act
            var categoryResponse = await Sender.Send(command);
            // assert

            var category = DbContext.Categories.FirstOrDefault(c => c.Id == categoryResponse.Id);

            Assert.NotNull(category);
        }
    }
}
