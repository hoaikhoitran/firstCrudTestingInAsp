namespace CRUDtesting.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal? Price { get; set; }
        public required int Quantity { get; set; }
    }
}
