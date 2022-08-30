namespace Basketball_Workshop.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Coach Coach { get; set; }
        public virtual List<Player> Players { get; set; }

    }
}
