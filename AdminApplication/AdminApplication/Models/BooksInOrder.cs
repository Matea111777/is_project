namespace AdminApplication.Models
{
    public class BooksInOrder
    {
        public Guid BookId { get; set; }
        public Book? OrderedProduct { get; set; }

        public Guid OrderId { get; set; }
        public Orders? Order { get; set; }
        public int Quantity { get; set; }
    }
}
