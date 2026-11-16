namespace WellnessSpa.Api.DTO
{
    public record ReadDBWellnessDTO(
        int Id, 
        string Name, 
        int TreatmentId, 
        int PressureId, 
        int Duration, 
        decimal Price, 
        bool IsCoupled, 
        DateTime BookedDay
        );
}