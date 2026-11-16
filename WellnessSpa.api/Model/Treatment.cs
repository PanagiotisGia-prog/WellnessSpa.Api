namespace WellnessSpa.Api.Model
{
    public class Treatment
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
    }
}