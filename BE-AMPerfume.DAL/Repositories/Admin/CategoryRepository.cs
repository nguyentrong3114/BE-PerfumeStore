using BE_AMPerfume.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository : ICategoryRepository
{
    private readonly AMPerfumeDbContext _context;

    public CategoryRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }


    public Task<List<Category>> GetCategories()
    {
        return _context.Categories.Select(c => new Category
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug
        })
        .ToListAsync();
    }
    
}