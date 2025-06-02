using BE_AMPerfume.DAL.Interfaces;
using BE_AMPerfume.DAL.Migrations;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryAdminDisplayDTO> CategoryDashBoardAdmin()
    {
        var allCategories = await _unitOfWork.CategoryRepository.GetALlCategoriesAvailable();
        int totalCategories = allCategories.Count;
        int totalActiveCategories = allCategories.Count(c => c.IsActive);
        var categoryDTOs = new List<CategoryAdminDTO>();
        int totalProducts = 0;
        foreach (var category in allCategories)
        {
            int productCount = await _unitOfWork.CategoryRepository.GetProductCountByCategoryIdAsync(category.Id) ?? 0;
            totalProducts += productCount;

            categoryDTOs.Add(new CategoryAdminDTO
            {
                Id = category.Id,
                Name = category.Name,
                ProductCount = productCount,
                IsActive = category.IsActive,
                slug = category.Slug
            });
        }
        return new CategoryAdminDisplayDTO
        {
            Categories = categoryDTOs,
            TotalProducts = totalProducts,
            TotalActiveCategories = totalActiveCategories,
            TotalInactiveCategories = totalCategories - totalActiveCategories,
            TotalCategories = totalCategories
        };
    }


    public async Task<List<CategoryDisplayDTO>> GetAllCategoriesAvailableAsync()
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategories();
        return categories.Select(c => new CategoryDisplayDTO
        {
            Id = c.Id,
            Name = c.Name,
            IsActive = true,
            slug = c.Slug
        }).ToList();
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