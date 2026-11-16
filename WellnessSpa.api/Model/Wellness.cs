namespace WellnessSpa.Api.Model
{
    public class Wellness
    {
        public int Id { get; set; }
        
        public required string Name { get; set; }

        public Treatment? Treatment { get; set; }
        public int TreatmentId { get; set; }
        
        public Pressure? Pressure { get; set; }
        public int PressureId { get; set; }
        
        public int Duration { get; set; }
        
        public decimal Price { get; set; }
        
        public bool IsCoupled { get; set; }

        public DateTime BookedDay { get; set; }
    }
}