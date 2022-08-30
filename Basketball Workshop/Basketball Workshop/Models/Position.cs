namespace Basketball_Workshop.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<PlayerPosition> Players { get; set; }
    }
}
