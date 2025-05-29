public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public string? ImageUrl { get; set; }
    public int? Star { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public int BrandId { get; set; }
    public string BrandName { get; set; }
}

