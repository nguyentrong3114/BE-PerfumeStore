using BE_AMPerfume.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository : ICategoryRepository
{
    private readonly AMPerfumeDbContext _context;

    public CategoryRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }

    public Task<List<Category>> GetALlCategoriesAvailable()
    {
        return _context.Categories.Select(c => new Category
        {
            Id = c.Id,
            Name = c.Name,
            IsActive = c.IsActive,
            Slug = c.Slug
        })
        .ToListAsync();
    }
    public async Task<int?> GetProductCountByCategoryIdAsync(int id)
    {
        return await _context.Categories
            .Where(c => c.Id == id)
            .Select(c => c.Products.Count)
            .FirstOrDefaultAsync();
    }

    public Task<List<Category>> GetCategories()
    {
        return _context.Categories.Select(c => new Category
        {
            Id = c.Id,
            Name = c.Name,
            IsActive = c.IsActive,
            Slug = c.Slug
        })
        .ToListAsync();
    }

}