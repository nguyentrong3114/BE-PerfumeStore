public interface ICategoryService
{
    Task<List<CategoryDisplayDTO>> GetCategoriesAsync();
    Task<List<CategoryDisplayDTO>> GetAllCategoriesAvailableAsync();
    Task<CategoryAdminDisplayDTO> CategoryDashBoardAdmin();
}