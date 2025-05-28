public interface ICategoryService
{
    Task<List<CategoryDisplayDTO>> GetCategoriesAsync();

}