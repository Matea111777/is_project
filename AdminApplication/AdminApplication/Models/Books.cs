namespace AdminApplication.Models
{
    public class Book 
    {
        public Guid? BookId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int yearOfRelease { get; set; }

        public string? BookImage { get; set; }

        public Authors? Author { get; set; }
        public Guid? AuthorId { get; set; }

        public string genre { get; set; }

        public bool? IsAvailable { get; set; }
        public int Price { get; set; }

    }
}
