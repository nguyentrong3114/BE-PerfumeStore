using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryRepository
{
    /// <summary>
    /// Retrieves a list of all categories.
    Task<List<Category>> GetCategories();
    Task<List<Category>> GetALlCategoriesAvailable();
    Task<int?> GetProductCountByCategoryIdAsync(int id);
}