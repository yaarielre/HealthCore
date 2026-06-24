namespace HealthCore.Application.Features.Specialties.DTOs;

public class SpecialtyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

public class CreateSpecialtyDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateSpecialtyDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}