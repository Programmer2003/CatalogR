namespace CatalogR.Models
{
    public class Like
    {
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}
