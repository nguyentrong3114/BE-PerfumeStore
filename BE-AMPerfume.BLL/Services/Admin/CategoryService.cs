using BE_AMPerfume.DAL.Interfaces;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<CategoryDisplayDTO>> GetCategoriesAsync()
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategories();
        return categories.Select(c => new CategoryDisplayDTO
        {
            Id = c.Id,
            Name = c.Name,
            slug = c.Slug
        }).ToList();
    }
}