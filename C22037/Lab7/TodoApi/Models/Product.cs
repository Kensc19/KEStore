namespace TodoApi;
public class Product : ICloneable
{
    public string Name { get; set; }
    public string ImageURL { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }

        // Implementation of the ICloneable interface
    public object Clone()
    {
        return new Product
        {
            Id = this.Id,
            Name = this.Name,
            ImageURL = this.ImageURL,
            Price = this.Price,
            Description = this.Description
        };
    }
}