namespace ApiProduct.Models
{
    public class Product
    {
        public int Id { get; set;}
        public string? Name { get; set;}
        public string? Description { get; set;}
        public decimal Price { get; set;}
        public DateTime HighDate { get; set;}
        public bool Active { get; set;}	
    }
}