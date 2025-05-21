namespace CRUDtesting.Models
{
    public class AddBookDto
    {
        public required string Name { get; set; }
        public required decimal? Price { get; set; }
        public required int Quantity { get; set; }
    }
}
