using System.ComponentModel.DataAnnotations;

namespace Idam.EFTimestamps.Sample.Models.Dto;

public class DtUtcUpdateDto
{
    public int Id { get; init; }

    [StringLength(191)] public required string Name { get; init; }

    [StringLength(191)] public string? Description { get; init; }
}