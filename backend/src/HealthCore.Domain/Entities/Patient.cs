using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class Patient : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Cedula { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; }
}