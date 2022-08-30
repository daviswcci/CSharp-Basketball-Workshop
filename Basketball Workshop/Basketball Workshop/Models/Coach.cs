namespace Basketball_Workshop.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
