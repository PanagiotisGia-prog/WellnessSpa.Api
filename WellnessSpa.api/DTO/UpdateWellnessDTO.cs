using System.ComponentModel.DataAnnotations;

namespace WellnessSpa.Api.DTO
{
    public record UpdateWellnessDTO(
        [Required][StringLength(120)]string Name,
        [Range(1, int.MaxValue)]int TreatmentId,
        [Range(1, int.MaxValue)]int PressureId,
        bool IsCoupled,
        DateTime BookedDay);
}