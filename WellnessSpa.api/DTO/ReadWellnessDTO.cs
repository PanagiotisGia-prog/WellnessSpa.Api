namespace WellnessSpa.Api.DTO
{
    public record ReadWellnessDTO(
        int Id, 
        string Name, 
        string Treatment, 
        string Pressure, 
        int Duration, 
        decimal Price, 
        bool IsCoupled, 
        DateTime BookedDay);
}