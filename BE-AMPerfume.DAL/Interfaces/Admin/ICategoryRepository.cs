using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategories();
}