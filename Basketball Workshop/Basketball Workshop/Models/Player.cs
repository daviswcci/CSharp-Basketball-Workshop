namespace Basketball_Workshop.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<PlayerPosition> Positions { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
