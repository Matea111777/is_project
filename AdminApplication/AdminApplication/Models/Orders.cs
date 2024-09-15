namespace AdminApplication.Models
{
    public class Orders
    {
        public Guid? Id { get; set; }
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public ICollection<BooksInOrder>? BooksInOrders { get; set; }
    }
}
