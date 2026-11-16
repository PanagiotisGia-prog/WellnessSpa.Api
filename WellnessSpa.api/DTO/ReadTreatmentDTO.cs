namespace WellnessSpa.Api.DTO
{
    public record ReadTreatmentDTO(
        int Id, 
        string Name,
        int Duration, 
        decimal Price
        );
}